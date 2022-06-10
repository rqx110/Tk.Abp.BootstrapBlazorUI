using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using BootstrapBlazor.Components;

namespace Tk.Abp.BootstrapBlazorUI.Components.ObjectExtending;

public partial class SelectExtensionProperty<TEntity, TResourceType>
    where TEntity : IHasExtraProperties
{
    protected List<SelectedItem> SelectItems = new();

    public int SelectedValue {
        get => Entity.GetProperty<int>(PropertyInfo.Name);
        set => Entity.SetProperty(PropertyInfo.Name, value, false);
    }

    protected virtual List<SelectedItem> GetSelectItemsFromEnum()
    {
        var selectItems = new List<SelectedItem>();

        foreach (var enumValue in PropertyInfo.Type.GetEnumValues())
        {
            selectItems.Add(new SelectedItem
            {
                Value = ((int)enumValue).ToString(),
                Text = EnumHelper.GetLocalizedMemberName(PropertyInfo.Type, enumValue, StringLocalizerFactory)
            });
        }

        return selectItems;
    }

    protected override void OnParametersSet()
    {
        SelectItems = GetSelectItemsFromEnum();

        if (!Entity.HasProperty(PropertyInfo.Name))
        {
            SelectedValue = (int)PropertyInfo.Type.GetEnumValues().GetValue(0);
        }
    }
}
