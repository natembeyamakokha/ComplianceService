using Autofac;
using Compliance.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace Compliance.Api
{
    public static class Extensions
    {
        public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>(optional: true)
                .AddEnvironmentVariables("OMNI_");

            return builder;
        }

        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging
                 .AddApplicationInsights()
                 .AddConsole()
                  .AddFilter<ApplicationInsightsLoggerProvider>("Microsoft.*", LogLevel.Error)
                 .AddFilter<ApplicationInsightsLoggerProvider>("OpenSleigh.*", LogLevel.Error);

            return builder;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["JWT:Authority"];
                options.Audience = configuration["JWT:Audience"];
                
                if (configuration.GetValue<bool>("JWT:EnableTokenEncryption"))
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        TokenDecryptionKey = new SymmetricSecurityKey(Convert.FromBase64String(configuration["JWT:TokenEncryptionKey"]))
                    };
                }
                else
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                    };
                }
            });

            return services;
        }

        public static IServiceCollection AddCaching(this IServiceCollection services, WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment() || builder.Environment.IsLocal())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                var cacheConnection = builder.Configuration["CacheConnection"];
                cacheConnection = cacheConnection.Replace("{CacheName}", builder.Configuration["CACHE_HOST_NAME"]);
                cacheConnection = cacheConnection.Replace("{AccessKey}", builder.Configuration["ACCESS_KEY"]);
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = cacheConnection;
                });
            }

            return services;
        }

        public static IServiceCollection AddCors(this IServiceCollection services, string policy)
        {
            services.AddCors(options =>
                {
                    options.AddPolicy(policy,
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
                });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Compliance Service API", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
                       Array.Empty<string>()
                   }
                    });
                });

            return services;
        }

        public static void AddAutofacServices(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            ServiceCompositionRoot.Set(builder.Build());
        }

        public static string GetDefaultConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

#if !DEBUG
                connectionString = connectionString.Replace("{SQLServerName}", configuration["SQLSERVER_NAME"]);
                connectionString = connectionString.Replace("{SQLServerUser}", configuration["SQLSERVER_USER"]);
                connectionString = connectionString.Replace("{SQLServerPassword}", configuration["SQLSERVER_PASSWORD"]);                
#else

#endif

            return connectionString;
        }
    }
}