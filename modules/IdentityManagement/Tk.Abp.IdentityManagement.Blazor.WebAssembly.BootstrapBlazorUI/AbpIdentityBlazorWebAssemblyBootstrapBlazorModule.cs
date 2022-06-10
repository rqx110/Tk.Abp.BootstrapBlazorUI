using Tk.Abp.IdentityManagement.Blazor.BootstrapBlazorUI;
using Tk.Abp.PermissionManagement.Blazor.WebAssembly.BootstrapBlazorUI;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Tk.Abp.IdentityManagement.Blazor.WebAssembly.BootstrapBlazorUI;

[DependsOn(
    typeof(AbpIdentityBlazorBootstrapBlazorModule),
    typeof(AbpPermissionManagementBlazorWebAssemblyBootstrapBlazorModule),
    typeof(AbpIdentityHttpApiClientModule)
)]
public class AbpIdentityBlazorWebAssemblyBootstrapBlazorModule: AbpModule
{
}
