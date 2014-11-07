using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Sandbox.Converters
{
    public class SelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isMultipleSelection = (bool)value;
            return isMultipleSelection ? ListViewSelectionMode.Multiple : ListViewSelectionMode.Single;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var selection = (ListViewSelectionMode)value;
            return selection == ListViewSelectionMode.Multiple;
        }
    }
}
