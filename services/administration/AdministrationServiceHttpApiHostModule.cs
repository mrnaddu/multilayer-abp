using IVP.AdministrationService.Application;
using IVP.AdministrationService.EntityFrameworkCore;
using IVP.AdministrationService.HttpApi;
using IVP.Shared.Hosting;
using Microsoft.AspNetCore.Cors;
using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

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
           scopes: new Dictionary<string, string> {
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

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
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
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthScopes("AdministrationService");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
