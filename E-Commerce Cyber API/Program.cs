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
using Cyber.Application.Interfaces;
using Cyber.Core.Interfaces;

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

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFavoriteProductService, FavoriteProductService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartQuantityCalculator, CartQuantityCalculator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IShippingService, ShippingService>();
builder.Services.AddScoped<IGenericService<Address>, GenericService<Address>>();
builder.Services.AddScoped<IGenericService<User>, GenericService<User>>(); 
builder.Services.AddScoped<IGenericService<Role>, GenericService<Role>>();
builder.Services.AddScoped<IGenericService<Product>, GenericService<Product>>();
builder.Services.AddScoped<IGenericService<FavoriteProduct>, GenericService<FavoriteProduct>>();
builder.Services.AddScoped<IGenericService<MediaFile>, GenericService<MediaFile>>();
builder.Services.AddScoped<IGenericService<Cart>, GenericService<Cart>>();
builder.Services.AddScoped<IGenericService<CartItem>, GenericService<CartItem>>();
builder.Services.AddScoped<IGenericService<Shipping>, GenericService<Shipping>>();
builder.Services.AddScoped<IGenericService<ShippingHistory>, GenericService<ShippingHistory>>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();

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