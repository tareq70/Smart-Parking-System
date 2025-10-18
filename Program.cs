using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Smart_Parking_System.Application.Interfaces;
using Smart_Parking_System.DomainLayer.Repositories;
using Smart_Parking_System.Infrastructure.Data;
using Smart_Parking_System.Infrastructure_Layer.Services;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IParkingAreaRepository, ParkingAreaRepository>();
builder.Services.AddScoped<IParkingSpotRepository, ParkingSpotRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddHostedService<ReservationBackgroundService>();
builder.Services.AddSwaggerGen(Options =>
{
    Options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Enter 'Bearer' [Space] and then Your Token in the text input below.\r\n\r\nExample: \"Bearer 1234SabCdshb\" ",
        Scheme = "Bearer"
    });
    Options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id ="Bearer"
            }

        },
        new string [] {}

    }
    });

});



#region Keycloak Authentication

//builder.Services.AddScoped<KeycloakService>();

//// Add Keycloak authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    var keycloak = builder.Configuration.GetSection("Keycloak");
//    options.Authority = keycloak["Authority"];
//    options.MetadataAddress = keycloak["MetadataAddress"];
//    options.RequireHttpsMetadata = false;
//    options.Audience = keycloak["ClientId"];


//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateAudience = true,
//        ValidateIssuer = true,

//        NameClaimType = "sub",
//        RoleClaimType = "roles"

//    };
//}); 
#endregion 

builder.Services.AddAuthorization();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.ConfigObject = new()
        {
            DefaultModelsExpandDepth = -1
        };
        options.DocumentTitle = "Hall Managment System";
        options.DocExpansion(DocExpansion.None);
        options.EnableFilter();
        options.EnablePersistAuthorization();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
