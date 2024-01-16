using IVP.AdministrationService.Application;
using IVP.AdministrationService.EntityFrameworkCore;
using IVP.AdministrationService.HttpApi;
using IVP.Shared.Hosting;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace IVP.AdministrationService;

[DependsOn(
    typeof(IVPSharedHostingMicroserviceModule),
    typeof(AdministrationServiceApplicationModule),
    typeof(AdministrationServiceEntityFrameworkCoreModule),
    typeof(AdministrationServiceHttpApiModule),
    typeof(AbpIdentityHttpApiClientModule)
)]
public class AdministrationServiceHttpApiHostModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, "AdministrationService");

        SwaggerConfigurationHelper.ConfigureWithAuth(
           context: context,
           authority: configuration["AuthServer:Authority"],
           scopes: new Dictionary<string, string> 
           {
                {"AdministrationService", "AdministrationService API"}
                    
           },
           apiTitle: "AdministrationService API"
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

        context.ServiceProvider.GetRequiredService<SettingManager>();

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
        app.UsePathBase("/admin");
        app.UseSwagger(options =>
        {
            if (!env.IsDevelopment())
            {
                var basePath = "/admin";
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
                swaggerURL = "/admin" + swaggerURL;
            options.SwaggerEndpoint(swaggerURL, "Support APP API");
            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("AdministrationService");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
