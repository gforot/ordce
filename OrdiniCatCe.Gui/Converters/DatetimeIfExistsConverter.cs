using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;


namespace OrdiniCatCe.Gui.Converters
{
    public class DatetimeIfExistsConverter : IMultiValueConverter
    {

        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if((values == null) || (values.Count()!=2))
            {
                return string.Empty;
            }

            if (!(values[0] is bool))
            {
                return string.Empty;
            }

            if (!(values[1] is DateTime))
            {
                return string.Empty;
            }

            bool exists = (bool)values[0];
            if (!exists)
            {
                return string.Empty;
            }
            DateTime dt = (DateTime)values[1];
            string dtStr = dt.ToShortDateString();
            return dtStr;
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
