using ModsDude.WindowsClient.Model.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ModsDude.WindowsClient.Wpf.DynamicForms;
public class DirectoryPathToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is DirectoryPath path ? path.ToString() : string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is string str ? new DirectoryPath(str) : null;
    }
}
