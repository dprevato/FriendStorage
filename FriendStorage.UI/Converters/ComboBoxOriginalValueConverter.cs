using FriendStorage.UI.DataProvider.Lookups;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace FriendStorage.UI.Converters
{
    public class ComboBoxOriginalValueConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = (int)value;
            if (parameter is ComboBox comboBox && comboBox.ItemsSource != null)
            {
                var lookupItem = comboBox.ItemsSource.OfType<LookupItem>().SingleOrDefault(x => x.Id == id);
                if (lookupItem is not null)
                {
                    return lookupItem.DisplayValue;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
