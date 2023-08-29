//using BusinessLogicLayer;
//using DatabaseAccessLayer;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile("appsettings.json");
//builder.Services.AddDbContext<HRMSDbcontext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Add services to the container.

//builder.Services.AddControllers();
//builder.Services.AddMvc().AddSessionStateTempDataProvider(); // Add this line
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<EmployeeLogic>();

//var app = builder.Build();


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();
//app.UseRouting();
//app.Run();
//using BusinessLogicLayer;
//using DatabaseAccessLayer;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile("appsettings.json");

//builder.Services.AddDbContext<HRMSDbcontext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Configure JWT authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//    };
//});

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddMvc().AddSessionStateTempDataProvider();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<EmployeeLogic>();
//builder.Services.AddScoped<LoginLogic>();
//builder.Services.AddScoped<SalaryLogic>();
//builder.Services.AddScoped<RoleLogic>();
//builder.Services.AddScoped<AttendanceLogic>();
//builder.Services.AddScoped<LeaveLogic>();

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthentication();

//app.MapControllers();
//app.UseRouting();
//app.UseAuthorization();
//app.Run();
using BusinessLogicLayer;
using DatabaseAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<HRMSDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<EmployeeLogic>();
builder.Services.AddScoped<LoginLogic>();
builder.Services.AddScoped<SalaryLogic>();
builder.Services.AddScoped<RoleLogic>();
builder.Services.AddScoped<AttendanceLogic>();
builder.Services.AddScoped<LeaveLogic>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Configure JWT bearer authorization
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
        c.OAuthClientId("swagger");
        c.OAuthClientSecret("swagger");
        c.OAuthAppName("Your API - Swagger");
        c.OAuthUsePkce();
    });
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HRMSDbcontext>();
    db.Database.Migrate();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();
app.UseRouting();
app.UseAuthorization();
app.Run();

