using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces.Repositories;
using Biblioteca.Domain.Interfaces.Services;
using Biblioteca.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUnitOfWork? _unitOfWork;
        public UsuarioDomainService(IUnitOfWork? unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Usuario entity)
        {
            try
            {
                var validationResult = entity.Validar();
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join("<br>", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new Exception($"Erro(s) de validação: {errorMessages}");
                }
                _unitOfWork.UsuarioRepository.Add(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Usuario> ValidarCredenciaisAsync(string email, string senha)
        {
            try
            {
                var usuario = await _unitOfWork.UsuarioRepository.GetAsync(u => u.Email == email && u.Senha == senha);
                return usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Usuario> ObterUsuarioPorEmail(string email)
        {
            try
            {
                var usuario = await _unitOfWork.UsuarioRepository.GetAsync(u => u.Email == email);
                return usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Usuario> ObterUsuarioPorToken(string token)
        {
            try
            {
                var usuario = await _unitOfWork.UsuarioRepository.GetAsync(u => u.ResetSenhaToken == token);
                return usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Usuario>> GetAllUsuariosComLivrosAsync()
        {
            try
            {
                var lista = await _unitOfWork.UsuarioRepository.GetAllUsuariosComLivrosAsync();

                if (lista == null)
                    throw new Exception("Não há usuários com livros cadastrados.");
                
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Delete(Usuario entity)
        {
            try
            {
                _unitOfWork.UsuarioRepository.Delete(entity);
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
               var lista = _unitOfWork.UsuarioRepository.GetAll();
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
               var u= _unitOfWork.UsuarioRepository.GetById(id);
                return u;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(Usuario entity)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var validationResult = entity.Validar();

                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join("<br>", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new Exception($"Erro(s) de validação: {errorMessages}");
                }

                var u = _unitOfWork.UsuarioRepository.GetById(entity.Id);

                if (u == null)
                    throw new Exception("Usuário não encontrado.");

                entity.Senha = u.Senha;

                _unitOfWork.UsuarioRepository.Update(entity);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
        public void CriarConta(Usuario usuario)
        {
            var validationResult = new UsuarioValidation().Validate(usuario);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("<br>", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            #region Verificar se o email informado já está cadastrado.
            if (_unitOfWork.UsuarioRepository.Get(u => u.Email.Equals(usuario.Email)) != null)
                throw new ArgumentException("O email informado já está cadastrado no sistema, tente outro.");
            #endregion

            _unitOfWork.UsuarioRepository.Add(usuario);
        }
        public void Dispose()
        {
            try
            {
                _unitOfWork.UsuarioRepository.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
