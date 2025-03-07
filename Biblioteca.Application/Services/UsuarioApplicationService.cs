using AutoMapper;
using Biblioteca.Application.Commands;
using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Responses;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces.Services;
using Biblioteca.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Services
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IUsuarioDomainService? _usuarioDomainService;
        private readonly IMapper _mapper;
       
        public UsuarioApplicationService(IUsuarioDomainService usuarioDomainService,
                                        IMapper mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _mapper = mapper;
        }
       
        public void CriarConta(UsuarioCreateCommand command)
        {
            try
            {
                var u = _mapper.Map<Usuario>(command);
                _usuarioDomainService?.Add(u);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UsuarioValidarCredenciaisResponse> ValidarCredenciais(string email, string senha)
        {
            try
            {
                var usuario = await _usuarioDomainService.ValidarCredenciaisAsync(email, senha);

                var usuarioResponse = _mapper.Map<UsuarioValidarCredenciaisResponse>(usuario);

                return usuarioResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UsuarioObterUsuarioPorEmailResponse> ObterUsuarioPorEmail(string email)
        {
            try
            {
                var u = await _usuarioDomainService.ObterUsuarioPorEmail(email);

                if (u == null)
                    throw new Exception("Usuário não encontrado.");

                var usuarioResponse = _mapper.Map<UsuarioObterUsuarioPorEmailResponse>(u);

                return usuarioResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UsuarioObterUsuarioPorTokenResponse> ObterUsuarioPorToken(string token)
        {
            try
            {
                var u = await _usuarioDomainService.ObterUsuarioPorToken(token);

                if (u == null)
                    throw new Exception("Usuário não encontrado.");

                var usuarioResponse = _mapper.Map<UsuarioObterUsuarioPorTokenResponse>(u);

                return usuarioResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdatePreSenha(UsuarioPreUpdateSenhaCommand command)
        {
            try
            {
                var usuario = _usuarioDomainService.GetById(command.Id);

                if (usuario != null)
                {
                    usuario.ResetSenhaToken = command.ResetSenhaToken;
                    usuario.ResetSenhaExpiraEm = command.ResetSenhaExpiraEm;

                    _usuarioDomainService.Update(usuario);
                }
                else
                {
                    throw new Exception("Usuário não encontrado.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateSenha(UsuarioUpdateSenhaCommand command)
        {
            try
            {
                var usuario = _usuarioDomainService.GetById(command.Id);

                if (usuario != null)
                {
                    if (string.IsNullOrEmpty(command.NovaSenha))
                    {
                        throw new Exception("A nova senha não pode ser vazia.");
                    }

                    usuario.Senha = command.NovaSenha; // Atualiza a senha
                    usuario.ResetSenhaToken = null; // Invalida o token
                    usuario.ResetSenhaExpiraEm = DateTime.MinValue;

                    _usuarioDomainService.Update(usuario);
                }
                else
                {
                    throw new Exception("Usuário não encontrado.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

 
        public async Task<List<GetAllUsuariosComLivrosAsyncResponse>> GetAllUsuariosComLivrosAsync()
        {
            try
            {
                var lista = await _usuarioDomainService.GetAllUsuariosComLivrosAsync();

                var usuariosComLivros = _mapper.Map<List<GetAllUsuariosComLivrosAsyncResponse>>(lista);
                return usuariosComLivros;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Usuario> GetAll()
        {
            try
            {
                var lista = _usuarioDomainService.GetAll();
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario GetById(Int32 id)
        {
            try
            {
                var u=_usuarioDomainService.GetById(id);
                return u;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            _usuarioDomainService.Dispose();
        }

        public void Delete(int id)
        {
            try
            {
                var u = _usuarioDomainService.GetById(id);
                _usuarioDomainService.Delete(u);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
