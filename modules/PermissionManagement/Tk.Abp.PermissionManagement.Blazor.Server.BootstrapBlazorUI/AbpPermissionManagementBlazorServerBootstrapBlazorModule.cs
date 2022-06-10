using Tk.Abp.AspnetCore.Components.Server.BootstrapBlazorTheme;
using Tk.Abp.PermissionManagement.Blazor.BootstrapBlazorUI;
using Volo.Abp.Modularity;

namespace Tk.Abp.PermissionManagement.Blazor.Server.BootstrapBlazorUI;

[DependsOn(
    typeof(AbpPermissionManagementBlazorBootstrapBlazorModule),
    typeof(AbpAspNetCoreComponentsServerBootstrapBlazorThemeModule)
)]
public class AbpPermissionManagementBlazorServerBootstrapBlazorModule : AbpModule
{
}
