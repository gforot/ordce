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
                else if (ro.ContainsPezzoArrivatoButNotAvvisato)
                {
                    return defaultColor;
                }
                else if (ro.ContainsPezzoOrdinatoButNotArrivato)
                {
                    return new SolidColorBrush(Colors.White); 
                }
                else if (ro.ContainsPezzoSprovvisto)
                {
                    return defaultColor;
                }
            }
            else if (value is PezziInOrdine)
            {
                PezziInOrdine po = value as PezziInOrdine;

                if (!po.Ordinato)
                {
                    return defaultColor;
                }
                else if (po.IsArrivatoButNotAvvisato)
                {
                    return defaultColor;
                }
                else if (po.IsOrdinatoButNotArrivato)
                {
                    return new SolidColorBrush(Colors.White);
                }
                else if (po.Sprovvisto)
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
