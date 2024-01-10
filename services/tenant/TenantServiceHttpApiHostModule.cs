using IVP.TenantService.Application;
using IVP.TenantService.EntityFrameworkCore;
using IVP.TenantService.HttpApi;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace IVP.TenantService;

[DependsOn(
    typeof(OnebillSharedHostingMicroserviceModule),
    typeof(TenantServiceApplicationModule),
    typeof(TenantServiceEntityFrameworkCoreModule),
    typeof(TenantServiceHttpApiModule),
    typeof(AdministrationServiceEntityFrameworkCoreModule)
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
           scopes: new Dictionary<string, string> {
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
            options.OAuthScopes("TenantService");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
