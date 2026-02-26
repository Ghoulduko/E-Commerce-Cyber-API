using Cyber.Application.Cache;
using Cyber.Application.Mapper;
using Cyber.Application.Services;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Helper;
using E_Commerce_Cyber_API.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<CyberDbContext>(i => i.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<CacheService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<GenericService<Address>>();
builder.Services.AddScoped<GenericService<User>>();
builder.Services.AddScoped<GenericService<Role>>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
