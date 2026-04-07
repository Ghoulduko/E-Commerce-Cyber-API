using Cyber.Application.Cache;
using Cyber.Application.Mapper;
using Cyber.Application.Services;
using Cyber.Core.Database;
using Cyber.Core.Entities;
using Cyber.Core.Helper;
using E_Commerce_Cyber_API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<CyberDbContext>(i =>
    i.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<CacheService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<GenericService<Address>>();
builder.Services.AddScoped<GenericService<User>>();
builder.Services.AddScoped<GenericService<Role>>();
builder.Services.AddScoped<GenericService<Product>>();
builder.Services.AddScoped<GenericService<FavoriteProduct>>();
builder.Services.AddScoped<GenericService<MediaFile>>();
builder.Services.AddScoped<GenericService<Cart>>();
builder.Services.AddScoped<GenericService<CartItem>>();
builder.Services.AddScoped<CartRepository>();
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var jwtKey = builder.Configuration["JWTSecretKey"] ?? throw new ArgumentNullException("No jwt key found");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://ltdluka.ge/",
        ValidAudience = "https://ltdluka.ge/",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
   {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
       {
           Type = ReferenceType.SecurityScheme,
           Id = "Bearer"
       }
     },
       new string[] {}
     }
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

//builder.WebHost.UseKestrel(options =>
//{
//    options.ListenAnyIP(7054);
//});

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AngularPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();