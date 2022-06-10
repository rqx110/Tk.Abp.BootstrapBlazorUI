using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;

namespace Tk.Abp.BootstrapBlazorUI.Components;

public partial class AbpExtensibleDataGrid<TItem> : ComponentBase where TItem: class, new()
{
    protected const string DataFieldAttributeName = "Data";
    
    protected Regex ExtensionPropertiesRegex = new Regex(@"ExtraProperties\[(.*?)\]");
    
    protected IEnumerable<int> PageItemsSource => new [] { 10, 20, 40 };
    
    [Parameter]
    public IEnumerable<TableColumn> Columns { get; set; }
    
    [Parameter]
    public Func<QueryPageOptions, Task<QueryData<TItem>>> OnQueryAsync { get; set; }

    [Parameter]
    public bool Loading { get; set; }

    [Inject]
    public IStringLocalizerFactory StringLocalizerFactory { get; set; }
    
    private Table<TItem> _table;

    protected virtual RenderFragment RenderCustomTableColumnComponent(Type type, object data)
    {
        return (builder) =>
        {
            builder.OpenComponent(0, type);
            builder.AddAttribute(0, DataFieldAttributeName, data);
            builder.CloseComponent();
        };
    }
    
    protected virtual string GetConvertedFieldValue(TItem item, TableColumn columnDefinition)
    {
        var convertedValue = columnDefinition.ValueConverter.Invoke(item);
        if (!columnDefinition.DisplayFormat.IsNullOrEmpty())
        {
            return string.Format(columnDefinition.DisplayFormatProvider, columnDefinition.DisplayFormat,
                convertedValue);
        }

        return convertedValue;
    }
    
    public async Task Refresh()
    {
        await _table.QueryAsync();
    }
}
