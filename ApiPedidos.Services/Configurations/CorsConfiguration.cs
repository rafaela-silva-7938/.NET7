namespace ApiPedidos.Services.Configurations
{
    public class CorsConfiguration
    {
        public static void AddCors(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
            s => s.AddPolicy("DefaultPolicy", builder =>
            {
                builder.AllowAnyOrigin()
    //qualquer servidor pode acessar a API
    .AllowAnyMethod()
    //qualquer método (POST, PUT, DELETE, GET)
    .AllowAnyHeader();
                //qualquer informação de cabeçalho
            })
            );
        }

        public static void UseCors(WebApplication app)
        {
            app.UseCors("DefaultPolicy");
        }
    }
}