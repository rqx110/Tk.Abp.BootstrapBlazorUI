using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Settings;

public class BootstrapBlazorSettingsProvider : IBootstrapBlazorSettingsProvider, IScopedDependency
{
    //TODO use SettingProvider instead of AbpBootstrapBlazorThemeOptions
    // [Inject]
    // protected ISettingProvider SettingProvider { get; set; }
    
    [Inject]
    public IOptions<AbpBootstrapBlazorThemeOptions> Options { get; set; }
    
    public delegate Task BootstrapBlazorSettingChangedHandler();
    
    public event BootstrapBlazorSettingChangedHandler SettingChanged;

    public Task<MenuPlacement> GetMenuPlacementAsync()
    {
        return Task.FromResult(Options.Value.MenuPlacement);
    }
    
    public Task<BootstrapBlazorThemes> GetThemeAsync()
    {
        return Task.FromResult(Options.Value.Theme);
    }

    public Task TriggerSettingChanged()
    {
        return SettingChanged?.Invoke();
    }
}
