using System;
using System.Windows.Data;
using System.Windows.Media;


namespace OrdiniCatCe.Gui.Converters
{
    public class TextboxBackgroundConverter : IValueConverter
    {
        private readonly SolidColorBrush _errorColor = new SolidColorBrush(Colors.AntiqueWhite);

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((value is string) && (string.IsNullOrEmpty((string)value)))
            {
                return _errorColor;
            }
            if (value == null)
            {
                return _errorColor;
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
