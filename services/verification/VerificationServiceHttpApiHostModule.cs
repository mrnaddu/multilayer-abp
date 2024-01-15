using IVP.Shared.Hosting;
using IVP.VerificationService.Application;
using IVP.VerificationService.EntityFrameworkCore;
using IVP.VerificationService.HttpApi;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace IVP.VerificationService;

[DependsOn(
    typeof(IVPSharedHostingMicroserviceModule),
    typeof(VerificationServiceApplicationModule),
    typeof(VerificationServiceEntityFrameworkCoreModule),
    typeof(VerificationServiceHttpApiModule)
)]
public class VerificationServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, "VerificationService");

        SwaggerConfigurationHelper.ConfigureWithAuth(
           context: context,
           authority: configuration["AuthServer:Authority"],
           scopes: new Dictionary<string, string>
           {
                    {"VerificationService", "VerificationService API"}
                   
           },
           apiTitle: "VerificationService API"
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
        app.UsePathBase("/verification");
        app.UseSwagger(options =>
        {
            if (!env.IsDevelopment())
            {
                var basePath = "/verification";
                options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new()
                        {
                            Url = $"https://{httpReq.Host.Value
                        }{basePath}"
                        } };
                });
            }
        });
        app.UseAbpSwaggerUI(options =>
        {
            var swaggerURL = "/swagger/v1/swagger.json";
            if (!env.IsDevelopment())
                swaggerURL = "/verification" + swaggerURL;
            options.SwaggerEndpoint(swaggerURL, "Support APP API");
            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("VerificationService");
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
