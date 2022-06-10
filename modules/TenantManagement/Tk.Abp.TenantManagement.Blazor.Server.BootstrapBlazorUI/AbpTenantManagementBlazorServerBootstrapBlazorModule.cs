using Tk.Abp.FeatureManagement.Blazor.Server.BootstrapBlazorUI;
using Tk.Abp.TenantManagement.Blazor.BootstrapBlazorUI;
using Volo.Abp.Modularity;

namespace Tk.Abp.TenantManagement.Blazor.Server.BootstrapBlazorUI;

[DependsOn(
    typeof(AbpTenantManagementBlazorBootstrapBlazorModule),
    typeof(AbpFeatureManagementBlazorWebServerBootstrapBlazorModule)
)]
public class AbpTenantManagementBlazorServerBootstrapBlazorModule : AbpModule
{
    
}
