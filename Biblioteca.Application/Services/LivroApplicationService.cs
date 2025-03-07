using AutoMapper;
using Biblioteca.Application.Commands;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces.Services;
using Biblioteca.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Services
{
    public class LivroApplicationService : ILivroApplicationService
    {
        private readonly ILivroDomainService _livroDomainService;
        private readonly IMapper _mapper;
        public LivroApplicationService(ILivroDomainService livroDomainService,
                                        IMapper mapper)
        {
            _livroDomainService = livroDomainService;
            _mapper = mapper;
        }

        public void Add(LivroCreateCommand command)
        {
            try
            {
                var l = _mapper.Map<Livro>(command);
                _livroDomainService.Add(l);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var l = _livroDomainService.GetById(id);
                if (l == null)
                    throw new Exception("Livro inexistente.");

                _livroDomainService.Delete(l);  
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
                var lista = _livroDomainService.GetAll();
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
                var l = _livroDomainService.GetById(id);
                return l;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(LivroEditCommand command)
        {
            try
            {
                var livroBanco = _livroDomainService.GetById(command.Id);
                
                _mapper.Map(command, livroBanco);

                _livroDomainService.Update(livroBanco);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            _livroDomainService.Dispose();
        }


    }
}
