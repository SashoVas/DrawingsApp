using DrawingsApp.Data.Seeders;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DrawingsApp.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddAuthenticationWithJWT(this WebApplicationBuilder builder, JwtBearerEvents events = null)
        {
            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                if(!(events is null))
                {
                    o.Events = events;
                }
            });
        }
        public static void AddCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                 .WithOrigins("http://localhost:4200")
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());

            });
        }
        public static void SettupDb<T>(this WebApplication app,Type seederType=null) where T : DbContext
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<T>()!)
                {
                    context.Database.EnsureCreated();
                    
                    if (!(seederType is null))
                    {
                        var seeder=(ISeeder)Activator.CreateInstance(seederType,context)!;
                        seeder.Seed();
                    }
                }
            }
        }
        public static void AddMessages(this WebApplicationBuilder builder,params Type[] consumers)
        {
            builder.Services.AddMassTransit(x =>
            {
                foreach (var consumer in consumers)
                {
                    x.AddConsumer(consumer);
                }
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    foreach (var consumer in consumers)
                    {
                        cfg.ReceiveEndpoint(consumer.FullName,endpoint=>endpoint.ConfigureConsumer(context,consumer));
                    }
                });
            });
        }
    }
}
