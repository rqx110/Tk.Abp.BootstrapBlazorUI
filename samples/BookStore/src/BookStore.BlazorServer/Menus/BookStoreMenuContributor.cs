using System.Threading.Tasks;
using BookStore.Localization;
using BookStore.MultiTenancy;
using Tk.Abp.IdentityManagement.Blazor.BootstrapBlazorUI;
using Tk.Abp.SettingManagement.Blazor.BootstrapBlazorUI;
using Tk.Abp.TenantManagement.Blazor.BootstrapBlazorUI;
using Volo.Abp.UI.Navigation;

namespace BookStore.Blazor.Menus;

public class BookStoreMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<BookStoreResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BookStoreMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fa fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }
}
