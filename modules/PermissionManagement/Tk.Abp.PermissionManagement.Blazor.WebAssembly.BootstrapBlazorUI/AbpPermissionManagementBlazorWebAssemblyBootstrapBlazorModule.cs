using Tk.Abp.AspnetCore.Components.WebAssembly.BootstrapBlazorTheme;
using Tk.Abp.PermissionManagement.Blazor.BootstrapBlazorUI;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Tk.Abp.PermissionManagement.Blazor.WebAssembly.BootstrapBlazorUI;

[DependsOn(
    typeof(AbpPermissionManagementBlazorBootstrapBlazorModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyBootstrapBlazorThemeModule),
    typeof(AbpPermissionManagementHttpApiClientModule)
)]
public class AbpPermissionManagementBlazorWebAssemblyBootstrapBlazorModule : AbpModule
{
}
