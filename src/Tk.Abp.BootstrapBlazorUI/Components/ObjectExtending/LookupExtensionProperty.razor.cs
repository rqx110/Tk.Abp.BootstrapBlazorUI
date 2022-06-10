using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Extensibility;
using Volo.Abp.Data;

namespace Tk.Abp.BootstrapBlazorUI.Components.ObjectExtending;

public partial class LookupExtensionProperty<TEntity, TResourceType>
    where TEntity : IHasExtraProperties
{
    [Inject] public ILookupApiRequestService LookupApiService { get; set; }

    private string TextPropertyName => PropertyInfo.Name + "_Text";
    
    protected List<SelectItem<object>> lookupItems;

    // public SelectItem<object> SelectedValue { get; set; }
    public SelectItem<object> SelectedValue {
        get
        {
            return new SelectItem<object>
            {
                Text = Entity.GetProperty(TextPropertyName)?.ToString(),
                Value = Entity.GetProperty(PropertyInfo.Name)
            };
        }
        set {
            Entity.SetProperty(PropertyInfo.Name, value.Value, false);
            UpdateLookupTextProperty(value.Value);
        }
    }
    
    public LookupExtensionProperty()
    {
        lookupItems = new List<SelectItem<object>>();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await SearchFilterChangedAsync(string.Empty);
    }
    
    protected virtual void UpdateLookupTextProperty(object value)
    {
        var selectedItemText = lookupItems.SingleOrDefault(t => t.Value.Equals(value))?.Text;
        Entity.SetProperty(TextPropertyName, selectedItemText);
    }
    
    protected virtual async Task<List<SelectItem<object>>> GetLookupItemsAsync(string filter)
    {
        var selectItems = new List<SelectItem<object>>();

        var url = PropertyInfo.Lookup.Url;
        if (!filter.IsNullOrEmpty())
        {
            url += $"?{PropertyInfo.Lookup.FilterParamName}={filter.Trim()}";
        }

        var response = await LookupApiService.SendAsync(url);

        var document = JsonDocument.Parse(response);
        var itemsArrayProp = document.RootElement.GetProperty(PropertyInfo.Lookup.ResultListPropertyName);
        foreach (var item in itemsArrayProp.EnumerateArray())
        {
            selectItems.Add(new SelectItem<object>
            {
                Text = item.GetProperty(PropertyInfo.Lookup.DisplayPropertyName).GetString(),
                Value = JsonSerializer.Deserialize(item.GetProperty(PropertyInfo.Lookup.ValuePropertyName).GetRawText(), PropertyInfo.Type)
            });
        }

        return selectItems;
    }

    protected virtual Task SelectedValueChanged(SelectItem<object> selectedItem)
    {
        SelectedValue = selectedItem;
        return Task.CompletedTask;
    }

    protected virtual async Task<IEnumerable<SelectItem<object>>> SearchFilterChangedAsync(string filter)
    {
        lookupItems = await GetLookupItemsAsync(filter);
        return lookupItems;
    }
}

public class SelectItem<TValue>
{
    public string Text { get; set; }
    public TValue Value { get; set; }
}