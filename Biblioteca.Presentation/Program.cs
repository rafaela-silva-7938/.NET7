using Biblioteca.Presentation.Configuration;
using Biblioteca.Presentation.Configuration.Biblioteca.Presentation.Configurations;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
CorsConfiguration.AddCors(builder);

// Adicionando o serviço de sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tempo de expiração da sessão, ajuste conforme necessário
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Adicionando suporte ao Newtonsoft.Json
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Configuração da Injeção de Dependência
DependencyInjectionConfiguration.AddDependencyInjection(builder);

// Configuração do JWT - Este método já está implementado na classe JwtConfiguration
builder.Services.AddJwtConfiguration(builder.Configuration);

var app = builder.Build();

// Configure o pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Adicionando o middleware de CORS no pipeline
CorsConfiguration.UseCors(app);

// Adicionando o middleware de sessão no pipeline
app.UseSession();

// Adicionando autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

// Mapeando a rota padrão para controladores MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
public partial class Program { }