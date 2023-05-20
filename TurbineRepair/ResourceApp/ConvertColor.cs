using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TurbineRepair.ResourceApp
{
    internal class ConvertColor : IValueConverter
    {
        SolidColorBrush colorBrush = new SolidColorBrush();     
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is decimal))
                return new SolidColorBrush(Colors.Black);

            var dValue = System.Convert.ToDecimal(value);
            if (dValue < 0)
                return new SolidColorBrush(Colors.Red);
            else if(dValue == 0)
            {
                colorBrush.Color = System.Windows.Media.Color.FromArgb(255, 62, 65, 71);
                return colorBrush;
            }
            else
                return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
