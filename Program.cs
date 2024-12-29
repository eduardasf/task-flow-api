using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using TaskFlow_API.Data;
using TaskFlow_API.Handles;
using TaskFlow_API.Repositories.IRepositories;
using TaskFlow_API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Configuração Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionando a configuração do DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de dependências
builder.Services.AddScoped<TarefaHandle>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<UsuarioHandle>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Configuração de localidade para o Brasil
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

// Configuração de autenticação JWT
var secretKey = "b3c3655d9deb26d2fdefd177ca618015";
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero // Opcional: Elimina a tolerância de tempo para o token
    };
});

// Adicionar configuração de CORS para permitir localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Permitir o front-end no localhost:4200
               .AllowAnyMethod() // Permitir qualquer método HTTP
               .AllowAnyHeader() // Permitir qualquer cabeçalho
               .AllowCredentials(); // Permitir cookies e autenticação
    });
});

var app = builder.Build();

// Configurar o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicar a política de CORS
app.UseCors("AllowLocalhost");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
