using Biblioteca.Domain.Interfaces.Repositories;
using Biblioteca.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Biblioteca.Infra.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext? _dataContext;
        private IDbContextTransaction? _transaction;

        
        private IUsuarioRepository? _usuarioRepository;
        private ILivroRepository? _livroRepository;

        public UnitOfWork(DataContext? dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext)); // Garantir que o DataContext não seja nulo
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new InvalidOperationException("A transação já foi iniciada.");

            _transaction = _dataContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new InvalidOperationException("A transação não foi iniciada.");

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Rollback()
        {
            if (_transaction == null)
                throw new InvalidOperationException("A transação não foi iniciada.");

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public IUsuarioRepository UsuarioRepository
        {
            get
            {
                if (_usuarioRepository == null)
                    _usuarioRepository = new UsuarioRepository(_dataContext);

                return _usuarioRepository;
            }
        }

        public ILivroRepository LivroRepository
        {
            get
            {
                if (_livroRepository == null)
                    _livroRepository = new LivroRepository(_dataContext);

                return _livroRepository;
            }
        }

        public void Dispose()
        {
            _dataContext?.Dispose();
            _transaction?.Dispose();
        }
    }
}
