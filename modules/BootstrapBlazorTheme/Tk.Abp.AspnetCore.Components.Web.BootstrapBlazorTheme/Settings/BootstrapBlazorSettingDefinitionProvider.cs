using Volo.Abp.Settings;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Settings;

public class BootstrapBlazorSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(BootstrapBlazorSettingNames.MenuPlacement, MenuPlacement.Left.ToString()),
            new SettingDefinition(BootstrapBlazorSettingNames.Theme, BootstrapBlazorThemes.Default.ToString())
        );
    }
}
