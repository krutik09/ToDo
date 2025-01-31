using AngularAuthAPI.Data;
using AngularAuthAPI.Data.Repo;
using AngularAuthAPI.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AngularAuthAPI.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//add this for using UOW
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Add this to use auto-mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

//add 3 lines for using sql server
var provider=builder.Services.BuildServiceProvider();
var config=provider.GetService<IConfiguration>();
builder.Services.AddDbContext<DBC>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add this to use swagger authenentication UI
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//Add this to resolve CORS error 
builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder => {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
}); 

//Add this to use JWT
var secretKey = config.GetSection("JWT:Key").Value;
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = key,
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//add this to use cors
app.UseCors("MyPolicy");

//Add this enable authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

