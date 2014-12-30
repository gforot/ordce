using System;
using System.Windows.Data;
using System.Windows.Media;
using OrdiniCatCe.Gui.Constants;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.Converters
{
    public class OrdinatoBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new SolidColorBrush(AppColors.EnabledButtonColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
