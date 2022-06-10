using Tk.Abp.AspnetCore.Components.Server.BootstrapBlazorTheme;
using Tk.Abp.SettingManagement.Blazor.BootstrapBlazorUI;
using Volo.Abp.Modularity;

namespace Tk.Abp.SettingManagement.Blazor.Server.BootstrapBlazorUI;

[DependsOn(
    typeof(AbpSettingManagementBlazorBootstrapBlazorModule),
    typeof(AbpAspNetCoreComponentsServerBootstrapBlazorThemeModule)
)]
public class AbpSettingManagementBlazorServerBootstrapBlazorModule : AbpModule
{
    
}
