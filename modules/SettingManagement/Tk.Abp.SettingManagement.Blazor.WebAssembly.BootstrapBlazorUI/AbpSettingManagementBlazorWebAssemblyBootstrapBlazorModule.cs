using Tk.Abp.AspnetCore.Components.WebAssembly.BootstrapBlazorTheme;
using Tk.Abp.SettingManagement.Blazor.BootstrapBlazorUI;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Tk.Abp.SettingManagement.Blazor.WebAssembly.BootstrapBlazorUI;

[DependsOn(
    typeof(AbpSettingManagementBlazorBootstrapBlazorModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyBootstrapBlazorThemeModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
public class AbpSettingManagementBlazorWebAssemblyBootstrapBlazorModule : AbpModule
{
    
}
