using System;
using System.Net.Http;
using IdentityModel;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Blazor.Menus;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Routing;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Themes.BootstrapBlazorTheme;
using Tk.Abp.AspnetCore.Components.WebAssembly.BootstrapBlazorTheme;
using Tk.Abp.IdentityManagement.Blazor.WebAssembly.BootstrapBlazorUI;
using Tk.Abp.SettingManagement.Blazor.WebAssembly.BootstrapBlazorUI;
using Tk.Abp.TenantManagement.Blazor.WebAssembly.BootstrapBlazorUI;
using Volo.Abp.UI.Navigation;

namespace BookStore.Blazor;

[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
    typeof(BookStoreHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyBootstrapBlazorThemeModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpIdentityBlazorWebAssemblyBootstrapBlazorModule),
    typeof(AbpTenantManagementBlazorWebAssemblyBootstrapBlazorModule),
    typeof(AbpSettingManagementBlazorWebAssemblyBootstrapBlazorModule)
)]
public class BookStoreBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

        ConfigureAuthentication(builder);
        ConfigureHttpClient(context, environment);
        ConfigureRouter(context);
        ConfigureUI(builder);
        ConfigureMenu(context);
        ConfigureAutoMapper(context);
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options => { options.AppAssembly = typeof(BookStoreBlazorModule).Assembly; });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BookStoreMenuContributor(context.Services.GetConfiguration()));
        });
    }

    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AuthServer", options.ProviderOptions);
            options.UserOptions.RoleClaim = JwtClaimTypes.Role;
            options.ProviderOptions.DefaultScopes.Add("BookStore");
            options.ProviderOptions.DefaultScopes.Add("role");
            options.ProviderOptions.DefaultScopes.Add("email");
            options.ProviderOptions.DefaultScopes.Add("phone");
        });
    }

    private static void ConfigureUI(WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#ApplicationContainer");
    }

    private static void ConfigureHttpClient(ServiceConfigurationContext context,
        IWebAssemblyHostEnvironment environment)
    {
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<BookStoreBlazorModule>(); });
    }
}
