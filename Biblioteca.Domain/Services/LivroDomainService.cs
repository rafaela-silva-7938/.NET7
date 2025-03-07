using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces.Repositories;
using Biblioteca.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Services
{
    public class LivroDomainService : ILivroDomainService
    {
        private readonly IUnitOfWork? _unitOfWork;
        public LivroDomainService(IUnitOfWork? unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Livro entity)
        {
            try
            {
                var validationResult = entity.Validar();

                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join("<br>", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new Exception($"Erro(s) de validação: {errorMessages}");
                }

                _unitOfWork.LivroRepository.Add(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(Livro entity)
        {
            try
            {
                _unitOfWork.LivroRepository.Delete(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Livro> GetAll()
        {
            try
            {
                var lista = _unitOfWork.LivroRepository.GetAll();
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Livro GetById(Int32 id)
        {
            try
            {
                var l = _unitOfWork.LivroRepository.GetById(id);
                return l;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(Livro entity)
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
                                
                var l = _unitOfWork.LivroRepository.GetById(entity.Id);

                if (l == null)
                    throw new Exception("Livro não encontrado.");
               
                _unitOfWork.LivroRepository.Update(entity);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
