using Tk.Abp.AspnetCore.Components.Server.BootstrapBlazorTheme;
using Tk.Abp.FeatureManagement.Blazor.BootstrapBlazorUI;
using Volo.Abp.Modularity;

namespace Tk.Abp.FeatureManagement.Blazor.Server.BootstrapBlazorUI;

[DependsOn(
    typeof(AbpFeatureManagementBlazorBootstrapBlazorModule),
    typeof(AbpAspNetCoreComponentsServerBootstrapBlazorThemeModule)
)]
public class AbpFeatureManagementBlazorWebServerBootstrapBlazorModule : AbpModule
{
}
