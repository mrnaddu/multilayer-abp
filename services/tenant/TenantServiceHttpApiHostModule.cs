using IVP.Shared.Hosting;
using IVP.TenantService.Application;
using IVP.TenantService.EntityFrameworkCore;
using IVP.TenantService.HttpApi;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace IVP.TenantService;

[DependsOn(
    typeof(IVPSharedHostingMicroserviceModule),
    typeof(TenantServiceApplicationModule),
    typeof(TenantServiceEntityFrameworkCoreModule),
    typeof(TenantServiceHttpApiModule)
)]
public class TenantServiceHttpApiHostModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, "TenantService");

        SwaggerConfigurationHelper.ConfigureWithAuth(
           context: context,
           authority: configuration["AuthServer:Authority"],
           scopes: new Dictionary<string, string>
           {
                {"TenantService", "TenantService API"}
                    
           },
           apiTitle: "TenantService API"
       );

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (!env.IsDevelopment())
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IdentityModelEventSource.ShowPII = true;
            app.UseDeveloperExceptionPage();
        }
        else if (env.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseMultiTenancy();
        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        app.UsePathBase("/tenant");
        app.UseSwagger(options =>
        {
            if (!env.IsDevelopment())
            {
                var basePath = "/tenant";
                options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new()
                        {
                            Url = $"https://{httpReq.Host.Value}{basePath}"
                        } 
                    };
                });
            }
        });
        app.UseAbpSwaggerUI(options =>
        {
            var swaggerURL = "/swagger/v1/swagger.json";
            if (!env.IsDevelopment())
                swaggerURL = "/tenant" + swaggerURL;
            options.SwaggerEndpoint(swaggerURL, "Support APP API");
            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("TenantService");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
