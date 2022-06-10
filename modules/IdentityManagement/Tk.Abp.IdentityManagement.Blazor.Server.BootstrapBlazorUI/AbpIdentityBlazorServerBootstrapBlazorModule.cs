using Tk.Abp.IdentityManagement.Blazor.BootstrapBlazorUI;
using Tk.Abp.PermissionManagement.Blazor.BootstrapBlazorUI;
using Volo.Abp.Modularity;

namespace Tk.Abp.IdentityManagement.Blazor.Server.BootstrapBlazorUI;

[DependsOn(
    typeof(AbpIdentityBlazorBootstrapBlazorModule),
    typeof(AbpPermissionManagementBlazorBootstrapBlazorModule)
)]
public class AbpIdentityBlazorServerBootstrapBlazorModule : AbpModule
{
    
}
