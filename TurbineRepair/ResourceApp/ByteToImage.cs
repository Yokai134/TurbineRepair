using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TurbineRepair.ResourceApp
{
    class ByteToImage : IValueConverter
    {
        public object Convert(object value, Type targetType,object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return null;

            var imgBytes = System.Convert.FromBase64String(value.ToString() ?? throw new InvalidOperationException());

            using var stream = new MemoryStream(imgBytes);
            return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
