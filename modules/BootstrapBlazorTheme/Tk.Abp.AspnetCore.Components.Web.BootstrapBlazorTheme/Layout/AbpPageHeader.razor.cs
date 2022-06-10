using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tk.Abp.BootstrapBlazorUI;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.PageToolbars;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Layout;

public partial class AbpPageHeader : ComponentBase
{
    protected List<RenderFragment> ToolbarItemRenders { get; set; }

    public IPageToolbarManager PageToolbarManager { get; set; }

    [Inject]
    public PageLayout PageLayout { get; private set; }

    [Parameter] // TODO: Consider removing this property in future and use only PageLayout.
    public string Title { get => PageLayout.Title; set => PageLayout.Title = value; }

    [Parameter]
    public bool BreadcrumbShowHome { get; set; } = true;

    [Parameter]
    public bool BreadcrumbShowCurrent { get; set; } = true;

    [Parameter]
    public RenderFragment ChildContent { get; set; }
    
    [Parameter] // TODO: Consider removing this property in future and use only PageLayout.
    public List<AbpBreadcrumbItem> BreadcrumbItems {
        get => PageLayout.BreadcrumbItems.ToList();
        set => PageLayout.BreadcrumbItems = new ObservableCollection<AbpBreadcrumbItem>(value);
    }
    
    [Parameter]
    public PageToolbar Toolbar { get; set; }

    public AbpPageHeader()
    {
        ToolbarItemRenders = new List<RenderFragment>();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (Toolbar != null)
        {
            if (!Options.Value.RenderToolbar)
            {
                return;
            }
            
            var toolbarItems = await PageToolbarManager.GetItemsAsync(Toolbar);
            ToolbarItemRenders.Clear();

            foreach (var item in toolbarItems)
            {
                var sequence = 0;
                ToolbarItemRenders.Add(builder =>
                {
                    builder.OpenComponent(sequence, item.ComponentType);
                    if (item.Arguments != null)
                    {
                        foreach (var argument in item.Arguments)
                        {
                            sequence++;
                            builder.AddAttribute(sequence, argument.Key, argument.Value);
                        }
                    }
                    builder.CloseComponent();
                });
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}
