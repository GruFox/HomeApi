using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HomeApi;
using HomeApi.Configuration;
using HomeApi.Contracts.Validation;
using HomeApi.Data;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Загружаем JSON конфиг
// -----------------------------
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile("HomeOptions.json", optional: false, reloadOnChange: true);

// -----------------------------
// Сервисы
// -----------------------------

// Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


// Репозитории
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

// База данных
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HomeApiContext>(options =>
    options.UseSqlServer(connection));

// FluentValidation
//builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AddDeviceRequestValidator>();

// Опции
builder.Services.Configure<HomeOptions>(builder.Configuration);
builder.Services.Configure<Address>(builder.Configuration.GetSection("Address"));

// Контроллеры
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HomeApi",
        Version = "v1"
    });
});

// -----------------------------
// Построение приложения
// -----------------------------

var app = builder.Build();

// -----------------------------
// Middlewares
// -----------------------------

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();