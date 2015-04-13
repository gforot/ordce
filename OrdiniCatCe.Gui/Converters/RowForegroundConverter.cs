using OrdiniCatCe.Gui.Model;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace OrdiniCatCe.Gui.Converters
{
    public class RowForegroundConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush defaultColor = new SolidColorBrush(Colors.Black);
            if (value is RichiesteOrdine)
            {
                RichiesteOrdine ro = value as RichiesteOrdine;

                if (ro.ContainsPezzoNotOrdinato)
                {
                    return defaultColor;
                }

                if (ro.ContainsPezzoArrivatoButNotAvvisato)
                {
                    return defaultColor;
                }

                if (ro.ContainsPezzoOrdinatoButNotArrivato)
                {
                    return new SolidColorBrush(Colors.White); 
                }

                if (ro.ContainsPezzoSprovvisto)
                {
                    return defaultColor;
                }
            }

            return defaultColor;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
