using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Features;
using Volo.Abp.Modularity;

namespace Tk.Abp.FeatureManagement.Blazor.BootstrapBlazorUI;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebBootstrapBlazorThemeModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpFeaturesModule)
)]
public class AbpFeatureManagementBlazorBootstrapBlazorModule : AbpModule
{

}
