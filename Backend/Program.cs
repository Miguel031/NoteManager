using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var key = Encoding.ASCII.GetBytes("Vt%jN94mWl&Z6qKrgT8pRvQ7yMxA12Cd");  
builder.Services.AddAuthentication(options => { 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; }) 
    .AddJwtBearer(options => { 
        options.TokenValidationParameters = new TokenValidationParameters { 
            ValidateIssuer = false, 
            ValidateAudience = false, 
            ValidateIssuerSigningKey = true, 
            IssuerSigningKey = new 
            SymmetricSecurityKey(key), 
            ValidateLifetime = true, 
        }; 
    });

// Agregar servicios de controladores y autorización
builder.Services.AddControllers();
builder.Services.AddAuthorization();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Note API", Version = "v1" });
});

var app = builder.Build();

// Configure el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Note API v1");
        c.RoutePrefix = string.Empty; // Para que Swagger UI esté en la raíz
    });
}

app.UseHttpsRedirection();

// Usar la política de CORS configurada
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
