using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SkiService_Backend.Models;
using MongoDB.Driver;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace SkiService_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://127.0.0.1:5500")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Configure MongoDB
            var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings");
            builder.Services.AddSingleton<IMongoClient>(ServiceProvider =>
            {
                return new MongoClient(mongoDbSettings["mongodb://localhost:27017/M165Azin"]);
            });

            builder.Services.AddScoped<MongoDbContext>(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var databaseName = mongoDbSettings["M165Azin"];
                return new MongoDbContext(mongoClient, databaseName);
            });

            // Configure Authentication
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
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
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
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
