using Biblioteca.Application.Commands;
using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Validations;
using Biblioteca.Presentation.Models;
using FluentValidation;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Biblioteca.Presentation.Controllers
{
    public class LivroController : Controller
    {
        // Required to obtain physical path
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILivroApplicationService _livroAppService;
        private readonly IUsuarioApplicationService _usuarioApplicationService;

        public LivroController(ILivroApplicationService livroAppService,
            IUsuarioApplicationService usuarioApplicationService,
            IWebHostEnvironment hostingEnvironment)
        {
            _livroAppService = livroAppService;
            _usuarioApplicationService = usuarioApplicationService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult CadastrarLivro()
        {
            var bemVindoJson = HttpContext.Session.GetString("BemVindo");

            if (string.IsNullOrEmpty(bemVindoJson))
            {
                return RedirectToAction("Login");
            }

            var model = JsonConvert.DeserializeObject<BemVindoViewModel>(bemVindoJson);

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> CadastrarLivro(CadastrarLivroViewModel model)
        {
            var erro = new ErrorModel();
            try
            {
                if (model.FotoJPGJPEG == null)
                    throw new Exception("Escolha uma foto.");

                var validationResult = new LivroModelValidation().Validate((model.Titulo, model.ISBN, model.Genero,
                                                                          model.Autor, model.Editora, model.Sinopse,
                                                                          model.FotoJPGJPEG.FileName));
                if (!validationResult.IsValid)
                {
                    throw new Exception(string.Join("<br>", validationResult.Errors.Select(e => e.ErrorMessage)));
                }

                var bemVindoJson = HttpContext.Session.GetString("BemVindo");

                if (string.IsNullOrEmpty(bemVindoJson))
                {
                    return RedirectToAction("Login"); // Redireciona caso o modelo não esteja presente
                }

                var bemVindoModel = JsonConvert.DeserializeObject<BemVindoViewModel>(bemVindoJson);

                var command = new LivroCreateCommand
                {
                    Autor = model.Autor,
                    Editora = model.Editora,
                    Foto = Guid.NewGuid(),
                    Genero = model.Genero,
                    ISBN = model.ISBN,
                    Titulo = model.Titulo,
                    Sinopse = model.Sinopse,
                    UsuarioId = bemVindoModel.Id
                };

                string contentRootPath = _hostingEnvironment.ContentRootPath;
                string tempPath = contentRootPath + "\\" + "Uploads\\" + command.Foto;

                // Salvando a foto
                using (var stream = new FileStream(tempPath, FileMode.Create))
                {
                    await model.FotoJPGJPEG.CopyToAsync(stream);
                }

                _livroAppService.Add(command);

                return Json("Livro " + model.Titulo + " cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }

        [HttpPost]
        public ActionResult Excluir(int livroId)
        {
            var erro = new ErrorModel();
            try
            {
                var livro = _livroAppService.GetById(livroId);
                if (livro == null)
                {
                    throw new Exception("Livro não encontrado.");
                }

                // Deletar a foto, se existir
                string fotoNome = livro.Foto.ToString();
                string contentRootPath = _hostingEnvironment.ContentRootPath;
                string fotoPath = Path.Combine(contentRootPath, "Uploads", fotoNome);
                if (System.IO.File.Exists(fotoPath))
                {
                    System.IO.File.Delete(fotoPath);
                }

                _livroAppService.Delete(livroId);

                return Json(new { success = true, redirectUrl = Url.Action("LivrosCadastrados", "Usuario") });
            }
            catch (Exception e)
            {
                erro.ErrorStr = e.Message;
                return Json(new { success = false, errorStr = erro.ErrorStr });
            }
        }


        [HttpGet("/Livro/GerarPdf")]
        public async Task<IActionResult> GerarPdf()
        {
            var erro = new ErrorModel();
            try
            {
                var bemVindoJson = HttpContext.Session.GetString("BemVindo");
                if (string.IsNullOrEmpty(bemVindoJson))
                {
                    return RedirectToAction("Login");
                }

                var bemVindoModel = JsonConvert.DeserializeObject<BemVindoViewModel>(bemVindoJson);
                          
                var lista = await _usuarioApplicationService.GetAllUsuariosComLivrosAsync();
                var listaModel = new List<UsuarioLivroBuscarTodosViewModel>();

                foreach (var usuario in lista)
                {
                    foreach (var livro in usuario.LivrosResponse)
                    {
                        listaModel.Add(new UsuarioLivroBuscarTodosViewModel
                        {
                            UsuarioNome = usuario.UsuarioNome,
                            Autor = livro.Autor,
                            ISBN = livro.ISBN,
                            Titulo = livro.Titulo,
                            Editora = livro.Editora,
                            Genero = livro.Genero,
                            UsuarioId = livro.UsuarioId,
                            LivroId = livro.Id,
                            Sinopse = livro.Sinopse,
                            Foto = livro.Foto
                        });
                    }
                }

                // Se o perfil for "0", filtra os livros apenas do usuário logado
                if (bemVindoModel.Perfil == "0")
                    listaModel = listaModel.Where(l => l.UsuarioId == bemVindoModel.Id).ToList();

                if (!listaModel.Any())
                    return BadRequest("Nenhum livro encontrado.");

                using (var stream = new MemoryStream())
                {
                    using (var writer = new PdfWriter(stream))
                    {
                        using (var pdf = new PdfDocument(writer))
                        {
                            var document = new Document(pdf);
                            document.Add(new Paragraph("Nossa Biblioteca").SetFontSize(22).SetBold().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                            // Caminho físico da pasta de uploads
                            var caminhoUploads = @"C:\Biblioteca\Biblioteca.Presentation\Uploads";

                            // Criar uma tabela única para todos os livros
                            var table = new Table(new float[] { 15, 20, 20, 20, 20, 20, 20 }).SetWidth(100);
                            table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);

                            // Cabeçalho da tabela
                            table.AddCell(new Paragraph("Foto").SetBold());
                            table.AddCell(new Paragraph("Título").SetBold());
                            table.AddCell(new Paragraph("Autor").SetBold());
                            table.AddCell(new Paragraph("Gênero").SetBold());
                            table.AddCell(new Paragraph("ISBN").SetBold());
                            table.AddCell(new Paragraph("Editora").SetBold());
                            table.AddCell(new Paragraph("Sinopse").SetBold());

                            foreach (var livro in listaModel)
                            {
                                // Linha para cada livro
                                // Coluna 1: Foto
                                string caminhoFoto = Path.Combine(caminhoUploads, livro.Foto.ToString());
                                var fotoCell = new Cell();
                                if (System.IO.File.Exists(caminhoFoto))
                                {
                                    try
                                    {
                                        var imageData = ImageDataFactory.Create(caminhoFoto);
                                        var image = new Image(imageData).ScaleToFit(60, 90); // Ajusta o tamanho da imagem para 2x3 cm
                                        fotoCell.Add(image);
                                    }
                                    catch (Exception)
                                    {
                                        fotoCell.Add(new Paragraph("Erro ao carregar a imagem."));
                                    }
                                }
                                else
                                {
                                    fotoCell.Add(new Paragraph("Foto não encontrada."));
                                }
                                table.AddCell(fotoCell);

                                // Coluna 2: Título
                                table.AddCell(new Paragraph(livro.Titulo));

                                // Coluna 3: Autor
                                table.AddCell(new Paragraph(livro.Autor));

                                // Coluna 4: Gênero
                                table.AddCell(new Paragraph(livro.Genero));

                                // Coluna 5: ISBN
                                table.AddCell(new Paragraph(livro.ISBN));

                                // Coluna 6: Editora
                                table.AddCell(new Paragraph(livro.Editora));

                                // Coluna 7: Sinopse
                                table.AddCell(new Paragraph(livro.Sinopse));

                                // Pula uma linha para separar o próximo livro
                                document.Add(new Paragraph("\n"));
                            }

                            // Adiciona a tabela ao documento
                            document.Add(table);

                            document.Close();
                        }
                    }

                    return File(stream.ToArray(), "application/pdf", "Livros.pdf");
                }
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }

        public ActionResult Editar(int id)
        {
            var erro = new ErrorModel();
            try
            {
                var command = _livroAppService.GetById(id);

                var model = new EditarLivroViewModel();
                model.Id = command.Id;
                model.ISBN = command.ISBN;
                model.Autor = command.Autor;
                model.Titulo = command.Titulo;
                model.Editora = command.Editora;
                model.Foto = command.Foto;
                model.Genero = command.Genero;
                model.Sinopse = command.Sinopse;
                model.UsuarioId = command.UsuarioId;

                return View(model);
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Editar(EditarLivroViewModel model)
        {
            var erro = new ErrorModel();
            try
            {
                var livroAntigo = _livroAppService.GetById(model.Id);
                if (livroAntigo == null)
                {
                    erro.ErrorStr = "Livro não encontrado.";
                    return Json(erro);
                }

                // Inserir valores na model antes da validação
                model.Autor = string.IsNullOrEmpty(model.Autor) ? livroAntigo.Autor : model.Autor;
                model.Editora = string.IsNullOrEmpty(model.Editora) ? livroAntigo.Editora : model.Editora;
                model.ISBN = string.IsNullOrEmpty(model.ISBN) ? livroAntigo.ISBN : model.ISBN;
                model.Titulo = string.IsNullOrEmpty(model.Titulo) ? livroAntigo.Titulo : model.Titulo;
                model.Genero = string.IsNullOrEmpty(model.Genero) ? livroAntigo.Genero : model.Genero;
                model.Foto = model.Foto == Guid.Empty ? livroAntigo.Foto : Guid.NewGuid();

                // Caminho da pasta onde as fotos são armazenadas
                string caminhoFotos = @"C:\Biblioteca\Biblioteca.Presentation\Uploads";

                // Excluir foto antiga se o GUID da foto foi alterado
                if (!livroAntigo.Foto.ToString().ToUpper().Equals(model.Foto.ToString().ToUpper()) && livroAntigo.Foto != Guid.Empty)
                {
                    string caminhoFotoAntiga = Path.Combine(caminhoFotos, livroAntigo.Foto.ToString().ToUpper()); // Sem extensão

                    if (System.IO.File.Exists(caminhoFotoAntiga))
                    {
                        System.IO.File.Delete(caminhoFotoAntiga); // Excluir foto antiga
                    }
                }

                // Validação do modelo (incluindo a foto)
                var validationResult = new LivroModelValidation().Validate((model.Titulo, model.ISBN, model.Genero,
                                                                            model.Autor, model.Editora, model.Sinopse,
                                                                            model.FotoJPGJPEG?.Name));

                if (!validationResult.IsValid)
                {
                    throw new Exception(string.Join("<br>", validationResult.Errors.Select(e => e.ErrorMessage)));
                }

                // Validar a extensão da foto antes de salvar
                if (model.FotoJPGJPEG != null && model.FotoJPGJPEG.Length > 0)
                {
                    var nomeOriginal = model.FotoJPGJPEG.FileName;
                    if (!TemExtensaoValida(nomeOriginal))  // Função que verifica a extensão
                    {
                        erro.ErrorStr = "A foto deve ser um arquivo JPG ou JPEG.";
                        return Json(erro);
                    }
                }

                // Criar o comando de atualização com o novo GUID da foto (sem extensão)
                var command = new LivroEditCommand
                {
                    Id = model.Id,
                    ISBN = model.ISBN,
                    Autor = model.Autor,
                    Titulo = model.Titulo,
                    Editora = model.Editora,
                    Foto = model.Foto, // Atualizar com o novo GUID da foto (sem extensão)
                    Genero = model.Genero,
                    Sinopse = model.Sinopse,
                    UsuarioId = model.UsuarioId,
                };
                string contentRootPath = _hostingEnvironment.ContentRootPath;
                string tempPath = contentRootPath + "\\" + "Uploads\\" + command.Foto;

                // Se o usuário enviar uma nova foto, salvar com o nome baseado no novo GUID
                if (model.FotoJPGJPEG != null && model.FotoJPGJPEG.Length > 0)
                {
                    // Salvando a foto
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        await model.FotoJPGJPEG.CopyToAsync(stream);
                    }
                }

                // Atualizar o livro no banco de dados
                _livroAppService.Update(command);

                return Json("Livro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                erro.ErrorStr = ex.Message;
                return Json(erro);
            }
        }


        private static bool TemExtensaoValida(string caminhoArquivo)
        {
            var extensao = Path.GetExtension(caminhoArquivo)?.ToLower();
            return extensao == ".jpg" || extensao == ".jpeg";
        }

    }
}
