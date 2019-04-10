using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;


namespace WpfApp1
{
    class TextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = 0;
            if (int.TryParse((string)value, out val))
            {
                if (val < 5000)
                    return Visibility.Visible;

                if (parameter is string && (string)parameter == "Collapsed")
                    return Visibility.Collapsed;
                else
                    return Visibility.Hidden;

            }
            else
            
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
