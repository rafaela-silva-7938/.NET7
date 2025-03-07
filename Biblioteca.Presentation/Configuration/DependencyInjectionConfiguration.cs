using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Interfaces.Repositories;
using Biblioteca.Domain.Interfaces.Services;
using Biblioteca.Domain.Services;
using Biblioteca.Infra.Data.Contexts;
using Biblioteca.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Biblioteca.Application.Mapping;
namespace Biblioteca.Presentation.Configuration
{
    public class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection
       (WebApplicationBuilder builder)
        {            
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

            builder.Services.AddTransient<IUsuarioApplicationService, UsuarioApplicationService>();       
            builder.Services.AddTransient<ILivroApplicationService, LivroApplicationService>();

            builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
            builder.Services.AddTransient<ILivroDomainService, LivroDomainService>();

            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddTransient<ILivroRepository, LivroRepository>();

            builder.Services.AddTransient<IEmailService, EmailService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            

            // Adicionando o DataContext com SQL Server
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Conexao")));

        }

    }
}
