using Biblioteca.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Domain.Entities;
using Biblioteca.Application.Commands;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Biblioteca.Presentation.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Domain.Validations;
using Biblioteca.Application.Responses;
using Biblioteca.Domain.Entities.Enums;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Protocol;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using iText.Kernel.Pdf;
using iText.Layout.Element;

namespace Biblioteca.Presentation.Controllers
{

    public class UsuarioController : Controller
    {
        private readonly ILivroApplicationService _livroAppService;
        private readonly IUsuarioApplicationService _usuarioAppService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UsuarioController(IUsuarioApplicationService usuarioAppService,
                                 IConfiguration configuration,
                                 IEmailService emailService,
                                 ILivroApplicationService livroAppService)
        {
            _usuarioAppService = usuarioAppService;
            _configuration = configuration;
            _emailService = emailService;
            _livroAppService = livroAppService;
        }



        [HttpGet]
        public IActionResult CriarConta()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CriarConta(UsuarioCreateViewModel model)
        {
            var erro = new ErrorModel();

            try
            {
                var usuario = new UsuarioCreateCommand
                {
                    Nome = model.Nome,
                    DataNascimento = model.DataNascimento,
                    Email = model.Email,
                    Senha = model.Senha
                };

                _usuarioAppService.CriarConta(usuario);

                ModelState.Clear();
                return Json("Usuário cadastrado com sucesso.");

            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }

        [HttpGet]
        public IActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EsqueciSenha(EsqueciSenhaViewModel model)
        {
            var erro = new ErrorModel();
            try
            {
                var usuarioResponse = await _usuarioAppService.ObterUsuarioPorEmail(model.Email);

                if (usuarioResponse == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                usuarioResponse.ResetSenhaToken = Guid.NewGuid().ToString();
                usuarioResponse.ResetSenhaExpiraEm = DateTime.UtcNow.AddHours(1);

                var usuarioPreUpdateSenhaCommand = new UsuarioPreUpdateSenhaCommand
                {
                    Id = usuarioResponse.Id,
                    ResetSenhaToken = usuarioResponse.ResetSenhaToken,
                    ResetSenhaExpiraEm = usuarioResponse.ResetSenhaExpiraEm.Value,
                };

                _usuarioAppService.UpdatePreSenha(usuarioPreUpdateSenhaCommand); // Chama UpdatePreSenha

                string link = $"https://localhost:7211/Usuario/RedefinirSenha?token={usuarioResponse.ResetSenhaToken}";

                string corpoEmail = $@"
                    <p>Olá {usuarioResponse.Nome},</p>
                    <p>Você solicitou a redefinição de senha na Nossa Biblioteca.</p>
                    <p>Para registrar sua nova senha, clique no link abaixo:</p>
                    <p><a href='{link}'>Clique aqui para redefinir sua senha</a></p>
                    <p>Se você não solicitou a redefinição de senha, por favor ignore este e-mail.</p>
                    <p>Atenciosamente,<br />Equipe Nossa Biblioteca</p>";

                _emailService.Enviar(usuarioResponse.Email, "Redefinição de Senha", corpoEmail);

                return Json("E-mail de redefinição enviado.");
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RedefinirSenha(string token)
        {
            var erro = new ErrorModel();
            try
            {
                if (string.IsNullOrEmpty(token))
                    throw new Exception("Token inválido.");

                var usuario = await _usuarioAppService.ObterUsuarioPorToken(token);

                if (usuario == null || usuario.ResetSenhaExpiraEm < DateTime.UtcNow)
                    throw new Exception("Token expirado ou inválido.");

                ViewData["Token"] = token;

                return View();
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(RedefinirSenhaViewModel model)
        {
            var erro = new ErrorModel();
            try
            {
                if (string.IsNullOrEmpty(model.Token))
                    throw new Exception("Token inválido.");

                var usuario = await _usuarioAppService.ObterUsuarioPorToken(model.Token);

                if (usuario == null || usuario.ResetSenhaExpiraEm < DateTime.UtcNow)
                    throw new Exception("Token expirado ou inválido.");

                var validationResult = new RedefinirSenhaValidation().Validate((model.NovaSenha, model.ConfirmarSenha));

                if (!validationResult.IsValid)
                {
                    throw new Exception(string.Join("<br>", validationResult.Errors.Select(e => e.ErrorMessage)));
                }

                var comando = new UsuarioUpdateSenhaCommand
                {
                    Id = usuario.Id,
                    NovaSenha = model.NovaSenha
                };

                _usuarioAppService.UpdateSenha(comando); // Chama UpdateSenha

                return Json("Senha redefinida com sucesso.");
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var erro = new ErrorModel();

            try
            {
                var validationResult = new LoginValidation().Validate((model.Email, model.Senha));

                if (!validationResult.IsValid)
                {
                    throw new Exception(string.Join("<br>", validationResult.Errors.Select(e => e.ErrorMessage)));

                }

                var usuarioResponse = await _usuarioAppService.ValidarCredenciais(model.Email, model.Senha);

                if (usuarioResponse == null)
                {
                    throw new Exception("Usuário ou senha inválidos.");

                }

                // Adiciona as claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuarioResponse.Nome),
                    new Claim(ClaimTypes.Email, usuarioResponse.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenString = tokenHandler.WriteToken(token);

                if (tokenString == "")
                    throw new Exception("Acesso negado.");

                var m = new BemVindoViewModel();
                m.Nome = usuarioResponse.Nome;
                m.Perfil = usuarioResponse.Perfil;
                m.Email = usuarioResponse.Email;
                m.Token = tokenString;
                m.Id = usuarioResponse.Id;

                HttpContext.Session.SetString("BemVindo", JsonConvert.SerializeObject(m));

                return Json("Bem vindo, " + m.Nome + "!");
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }

        [HttpGet]
        public ActionResult LivrosCadastrados()
        {
            var erro = new ErrorModel();
            try
            {
                var bemVindoJson = HttpContext.Session.GetString("BemVindo");

                if (string.IsNullOrEmpty(bemVindoJson))
                {
                    return RedirectToAction("Login"); // Redireciona caso o modelo não esteja presente
                }

                // Desserializar o objeto da session
                var model = JsonConvert.DeserializeObject<BemVindoViewModel>(bemVindoJson);


                return View(model);
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }

        [HttpGet("/Usuario/GetAllUsuariosComLivrosAsync")]
        public async Task<IActionResult> GetAllUsuariosComLivrosAsync()
        {
            var erro = new ErrorModel();
            try
            {
                var bemVindoJson = HttpContext.Session.GetString("BemVindo");

                if (string.IsNullOrEmpty(bemVindoJson))
                {
                    return RedirectToAction("Login"); // Redireciona caso o modelo não esteja presente
                }

                var bemVindoModel = JsonConvert.DeserializeObject<BemVindoViewModel>(bemVindoJson);

                var lista = await _usuarioAppService.GetAllUsuariosComLivrosAsync();

                var listaModel = new List<UsuarioLivroBuscarTodosViewModel>();

                foreach (var item in lista)
                {
                    foreach (var item2 in item.LivrosResponse)
                    {
                        var m = new UsuarioLivroBuscarTodosViewModel
                        {
                            UsuarioNome = item.UsuarioNome,
                            Autor = item2.Autor,
                            ISBN = item2.ISBN,
                            Titulo = item2.Titulo,
                            Editora = item2.Editora,
                            Genero = item2.Genero,
                            UsuarioId = item2.UsuarioId,
                            LivroId = item2.Id,
                            CaminhoDaFoto = "/Uploads/" + item2.Foto.ToString()
                        };

                        listaModel.Add(m);
                    }
                }
                if (bemVindoModel.Perfil == "0")
                    listaModel = listaModel.Where(l => l.UsuarioId.ToString() == bemVindoModel.Id.ToString()).ToList();

                return Json(listaModel);
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }
    }
}
