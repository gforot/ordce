using System;
using System.Windows.Data;


namespace OrdiniCatCe.Gui.Converters
{
    public class ValidEMailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool rval = false;
            if (value is string)
            {
                rval = !string.IsNullOrEmpty((string)value) && Checks.CheckEmail((string)value);
            }
            return rval;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
