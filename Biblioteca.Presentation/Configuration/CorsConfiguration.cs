﻿namespace Biblioteca.Presentation.Configuration
{
    public class CorsConfiguration
    {
        public static void AddCors(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }

        public static void UseCors(WebApplication app)
        {
            app.UseCors("DefaultPolicy");
        }
    }
}
