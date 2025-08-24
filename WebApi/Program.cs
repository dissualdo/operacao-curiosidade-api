using WebApi.NativeInjector;
using WebApi.ApiConfigurations.Extensions; 
var builder = WebApplication.CreateBuilder(args);

// Cria e injeta JwtConfiguration ANTES do builder.Build()
//var jwtConfig = new JwtConfiguration(builder.Configuration);
//builder.Services.AddSingleton(jwtConfig);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();


// Extensões customizadas
builder.Services.ConfigurePipeline(); // Se você tiver middlewares próprios
builder.Services.ConfigureAuth(builder.Configuration); // JWT Auth
builder.Services.ConfigureSwagger(); // Swagger com token
builder.Services.DependencyInjection(builder.Configuration); // DI nativa

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", policy =>
        policy
            .WithOrigins(
                "http://localhost:4200",          // Angular dev
                "https://localhost:4200"          // se usar HTTPS no dev
                                                  // adicione aqui seus domínios de produção
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()                  // use só se realmente precisa de cookies/credenciais
    );
});

var app = builder.Build();

// Swagger (mesmo fora de dev, já que está comentado o bloco de dev)
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("Cors"); 


 
// 🔒 Segurança
app.UseHttpsRedirection();

// ✅ Ordem correta
app.UseAuthentication();   // Autentica o usuário (JWT)
app.UseAuthorization();    // Aplica as regras de autorização

app.MapControllers();

app.Run();
