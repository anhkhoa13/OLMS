
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OLMS.API.Middleware;
using OLMS.Application;
using OLMS.Application.Services;
using OLMS.Infrastructure;
using System.Text;
using System.Text.Json.Serialization;

namespace OLMS.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IncludeFields = true;
            });

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        //Register Discussion Service
        builder.Services.AddScoped<IDiscussionService, DiscussionService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("OLMSOrigin", policy =>
            {
                string OLMSOrigin = builder.Configuration.GetSection("CorsSettings:OLMSOrigin").ToString() ??
                    throw new ArgumentNullException("CorsSettings:AllowedOrigins", "Missing AllowedOrigins configuration");

                policy.WithOrigins(OLMSOrigin)
                    .AllowAnyMethod()
                    .WithExposedHeaders("Authorization")
                    .AllowAnyHeader();
            });
            options.AddPolicy("AllowReactApp", builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = builder.Configuration.GetSection("JwtSettings").Get<JwtOptions>()
                                ?? throw new ArgumentNullException("JwtSettings", "Missing Jwt configuration");
                var key = Encoding.UTF8.GetBytes(jwtOptions.Secret);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        builder.Services.AddExceptionHandler<GlobalExecptionHandler>();
        builder.Services.AddProblemDetails();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors();
        // Add CORS middleware
        app.UseCors("AllowReactApp");


        app.Use(async (context, next) =>
        {
            var token = context.Request.Headers["Authorization"];
            Console.WriteLine($"Incoming Token: {token}");
            await next();
        });

        app.UseCors("OLMSOrigin");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }


}
