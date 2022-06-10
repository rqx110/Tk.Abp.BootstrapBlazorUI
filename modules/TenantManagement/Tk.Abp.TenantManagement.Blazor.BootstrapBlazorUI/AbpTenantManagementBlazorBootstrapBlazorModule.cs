using Microsoft.Extensions.DependencyInjection;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Routing;
using Tk.Abp.FeatureManagement.Blazor.BootstrapBlazorUI;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;

namespace Tk.Abp.TenantManagement.Blazor.BootstrapBlazorUI;


[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpFeatureManagementBlazorBootstrapBlazorModule)
)]
public class AbpTenantManagementBlazorBootstrapBlazorModule : AbpModule
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpTenantManagementBlazorBootstrapBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<AbpTenantManagementBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new TenantManagementBlazorMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpTenantManagementBlazorBootstrapBlazorModule).Assembly);
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    TenantManagementModuleExtensionConsts.ModuleName,
                    TenantManagementModuleExtensionConsts.EntityNames.Tenant,
                    createFormTypes: new[] { typeof(TenantCreateDto) },
                    editFormTypes: new[] { typeof(TenantUpdateDto) }
                );
        });
    }
}
