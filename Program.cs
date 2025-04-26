using ApisConvenciones9.Data;
using ApisConvenciones9.Models;
using ApisConvenciones9.Repositories;
using ApisConvenciones9.Servicios;
using ApisConvenciones9.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Inicio area de servicios
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCors =>
    {
        opcionesCors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
opciones.UseSqlServer("name=DefaultConnection"));

//Codigo para usuarios con identity
builder.Services.AddIdentityCore<Usuario>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<Usuario>>();
builder.Services.AddScoped<SignInManager<Usuario>>();
builder.Services.AddTransient<IServiciosUsuarios, ServiciosUsuarios>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication().AddJwtBearer(opciones =>
{
    opciones.MapInboundClaims = false;

    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["LlaveJWT"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(opciones =>
{
    opciones.AddPolicy("Admin", politica => politica.RequireClaim("Admin"));
});

builder.Services.AddScoped<IEventosRepository, EventosRepository>();

builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Apis Convenciones",
        Description = "Apis para sitio convenciones Click, manejador web y app"
    });

    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    opciones.OperationFilter<FiltroAutorizacion>();

});

var app = builder.Build();

//Inicio area de middlewares

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.MapControllers();

app.Run();
