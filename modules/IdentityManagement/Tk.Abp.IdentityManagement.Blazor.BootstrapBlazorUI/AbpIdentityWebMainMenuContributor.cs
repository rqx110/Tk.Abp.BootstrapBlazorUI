using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.UI.Navigation;

namespace Tk.Abp.IdentityManagement.Blazor.BootstrapBlazorUI;

public class AbpIdentityWebMainMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var administrationMenu = context.Menu.GetAdministration();
        administrationMenu.Icon = "fa fa-fw fa-desktop";

        var l = context.GetLocalizer<IdentityResource>();

        var identityMenuItem = new ApplicationMenuItem(IdentityMenuNames.GroupName, l["Menu:IdentityManagement"],
            icon: "fa fa-id-card");
        administrationMenu.AddItem(identityMenuItem);

        identityMenuItem.AddItem(new ApplicationMenuItem(
            IdentityMenuNames.Roles,
            l["Roles"],
            url: "~/identity/roles").RequirePermissions(IdentityPermissions.Roles.Default));

        identityMenuItem.AddItem(new ApplicationMenuItem(
            IdentityMenuNames.Users,
            l["Users"],
            url: "~/identity/users").RequirePermissions(IdentityPermissions.Users.Default));

        return Task.CompletedTask;
    }
}
