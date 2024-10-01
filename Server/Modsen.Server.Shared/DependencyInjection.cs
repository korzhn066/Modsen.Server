using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Modsen.Server.Shared.Models;
using Serilog;
using Serilog.Sinks.Http.BatchFormatters;
using System.Text;

namespace Modsen.Server.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShared(
            this IServiceCollection services,
            JwtOptions jwtOptions)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jwtOptions.ValidAudience,
                        ValidIssuer = jwtOptions.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey)),
                    };
                });

            return services;
        }

        public static IHostBuilder ConfigureLogger(this IHostBuilder host, string logstashUri)
        {
            host.UseSerilog((_, configuration) => configuration
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
                    requestUri: logstashUri,
                    textFormatter: new Serilog.Formatting.Elasticsearch.ElasticsearchJsonFormatter(),
                    batchFormatter: new ArrayBatchFormatter())
            );

            return host;
        }
    }
}
