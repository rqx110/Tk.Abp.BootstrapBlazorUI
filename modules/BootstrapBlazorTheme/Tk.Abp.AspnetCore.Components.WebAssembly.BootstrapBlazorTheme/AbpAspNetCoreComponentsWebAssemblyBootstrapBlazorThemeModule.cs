using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Routing;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Toolbars;
using Volo.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Http.Client.IdentityModel.WebAssembly;
using Volo.Abp.Modularity;

namespace Tk.Abp.AspnetCore.Components.WebAssembly.BootstrapBlazorTheme;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebBootstrapBlazorThemeModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyModule),
    typeof(AbpHttpClientIdentityModelWebAssemblyModule)
)]
public class AbpAspNetCoreComponentsWebAssemblyBootstrapBlazorThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpAspNetCoreComponentsWebAssemblyBootstrapBlazorThemeModule).Assembly);
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new BootstrapBlazorThemeToolbarContributor());
        });
    }
}
