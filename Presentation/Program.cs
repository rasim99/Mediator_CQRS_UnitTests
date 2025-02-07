using Business.MappingProfiles;
using Data.Contexts;
using Data.Repositories.Abstract.Product;
using Data.Repositories.Concrete.Product;
using Data.UnitOfWork;
using Business.Extensions;
using Microsoft.EntityFrameworkCore;

using Presentation.Middlewares;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;
using Business.Services.Producer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.Configure<RouteOptions>(opt => opt.LowercaseUrls = true);

// AddSwaggerGen
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyProductAPI",
        Version = "v1"
    });

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        In = ParameterLocation.Header
        //Description="JWT token"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id = "Bearer"

                }
            },
            new string[] { }

        }
    });

    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Presentation.xml"));

});

//Adding Authentication
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// adding JWT Bearer
    .AddJwtBearer(options => {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };
    });

// Auto Mapper
builder.Services.AddAutoMapper(x =>
{
    x.AddProfile<ProductMappingProfile>();
    x.AddProfile<AuthMappingProfile>();
    x.AddProfile<RoleMappingProfile>();
    x.AddProfile<UserMappingProfile>();
});

#region Repositories
builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
#endregion

#region Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddApplicationExtensions();
#endregion
builder.Services.AddCors(builder =>
{
    builder.AddPolicy("AllowAll", options =>
    {
        options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

Log.Logger=new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Services.AddSerilog();

builder.Services.AddScoped<IProducerService, ProducerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbcontext.Database.MigrateAsync();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedDataAsync(roleManager, userManager);
}
app.MapControllers();

app.UseMiddleware<CustomExceptionMiddleware>();
app.Run();
