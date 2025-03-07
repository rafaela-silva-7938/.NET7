using AutoMapper;
using Biblioteca.Application.Commands;
using Biblioteca.Application.Responses;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Usuario, UsuarioValidarCredenciaisResponse>()
                .AfterMap((entity, command) =>
                {
                    command.Id = entity.Id;
                    command.Perfil = entity.Perfil.ToString(); // Caso o Perfil seja uma enumeração, convertendo para string
                    command.Nome = entity.Nome;
                });

            CreateMap<LivroEditCommand, Livro>();

            CreateMap<Usuario, UsuarioValidarCredenciaisResponse>()
    .AfterMap((entity, command) =>
    {
        command.Nome = entity.Nome;
        command.Email = entity.Email;
        command.Perfil = entity.Perfil.ToString();
    });

            CreateMap<UsuarioCreateCommand, Usuario>()
                .AfterMap((command, entity) =>
                {
                    entity.Perfil = (int)Perfil.UsuarioComum;
                });

            CreateMap<LivroCreateCommand, Livro>()
                .AfterMap((command, entity) =>
                {
                    entity.Foto = command.Foto;
                });

            CreateMap<Usuario, UsuarioObterUsuarioPorEmailResponse>()
                .AfterMap((entity, command) =>
                {
                    command.Email = entity.Email;
                    command.ResetSenhaToken = entity.ResetSenhaToken;
                    command.ResetSenhaExpiraEm = entity.ResetSenhaExpiraEm;
                });

            CreateMap<Usuario, UsuarioObterUsuarioPorTokenResponse>()
                .AfterMap((entity, command) =>
                {
                    command.Id = entity.Id;
                    command.Email = entity.Email;
                    command.ResetSenhaToken = entity.ResetSenhaToken;
                    command.ResetSenhaExpiraEm = entity.ResetSenhaExpiraEm;
                    command.Nome = entity.Nome;
                    command.Perfil = entity.Perfil.ToString();
                });

            CreateMap<Usuario, GetAllUsuariosComLivrosAsyncResponse>()
               .AfterMap((e, r) =>
               {
                   r.UsuarioNome = e.Nome; // Aqui você mapeia o nome do usuário para o campo correto

                   r.LivrosResponse = new List<LivroResponse>();

                   foreach (var item in e.Livros)
                   {
                       r.LivrosResponse.Add(new LivroResponse
                       {
                           Id = item.Id,
                           Autor = item.Autor,
                           ISBN = item.ISBN,
                           Genero = item.Genero,
                           Editora = item.Editora,
                           Titulo = item.Titulo,
                           Sinopse = item.Sinopse,
                           Foto = item.Foto,
                           UsuarioId = item.UsuarioId
                       });
                   }
               });

        }
    }
}
