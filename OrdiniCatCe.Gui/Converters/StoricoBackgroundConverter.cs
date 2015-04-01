using System;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Drawing.Color;


namespace OrdiniCatCe.Gui.Converters
{
    public class StoricoBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if((value is bool) && ((bool)value))
            {
                return new SolidColorBrush(Colors.Blue);
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
