using System;
using System.Windows.Data;
using System.Windows.Media;
using OrdiniCatCe.Gui.Constants;
using OrdiniCatCe.Gui.Model;
using System.Windows;


namespace OrdiniCatCe.Gui.Converters
{
    public class RowBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is RichiesteOrdine)
            {
                RichiesteOrdine ro = value as RichiesteOrdine;
                if (ro.ContainsPezzoSprovvisto)
                {
                    return GetSprovvistoColor();
                }
                else if (ro.ContainsPezzoNotOrdinato)
                {
                    return GetNotOrdinatoColor();
                }
                else if (ro.ContainsPezzoArrivatoButNotAvvisato)
                {
                    return GetArrivatoButNotAvvisatoColor();
                }
                else if (ro.ContainsPezzoOrdinatoButNotArrivato)
                {
                    return GetOrdinatoButNotArrivatoColor();
                }

            }
            else if (value is PezziInOrdine)
            {
                PezziInOrdine po = value as PezziInOrdine;

                if (po.Sprovvisto)
                {
                    return GetSprovvistoColor();
                }
                else if (!po.Ordinato)
                {
                    return GetNotOrdinatoColor();
                }
                else if (po.IsArrivatoButNotAvvisato)
                {
                    return GetArrivatoButNotAvvisatoColor();
                }
                else if (po.IsOrdinatoButNotArrivato)
                {
                    return GetOrdinatoButNotArrivatoColor();
                }

            }

            return new SolidColorBrush(Colors.Transparent);
        }

        private static SolidColorBrush GetNotOrdinatoColor()
        {
            return GetWarningColorByKey("notOrdinatoColor");
        }

        private static SolidColorBrush GetArrivatoButNotAvvisatoColor()
        {
            return GetWarningColorByKey("arrivatoButNotAvvisatoColor");
        }

        private static SolidColorBrush GetSprovvistoColor()
        {
            return GetWarningColorByKey("sprovvistoColor");
        }

        private static SolidColorBrush GetOrdinatoButNotArrivatoColor()
        {
            return GetWarningColorByKey("ordinatoButNotArrivatoColor");
        }

        private static SolidColorBrush GetWarningColorByKey(string colorKey)
        {
            return (SolidColorBrush)Application.Current.Resources[colorKey];
        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
