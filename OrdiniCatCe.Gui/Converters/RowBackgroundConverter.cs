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

                if (ro.ContainsPezzoNotOrdinato)
                {
                    //imageHeight = (int)Application.Current.Resources["ImageHeight"];
                    SolidColorBrush notOrdinatoColor = (SolidColorBrush)Application.Current.Resources["notOrdinatoColor"];
                    return notOrdinatoColor;
                }

                if (ro.ContainsPezzoArrivatoButNotAvvisato)
                {
                    SolidColorBrush arrivatoButNotAvvisatoColor = (SolidColorBrush)Application.Current.Resources["arrivatoButNotAvvisatoColor"];
                    return arrivatoButNotAvvisatoColor;
                }

                if (ro.ContainsPezzoOrdinatoButNotArrivato)
                {
                    SolidColorBrush ordinatoButNotArrivatoColor = (SolidColorBrush)Application.Current.Resources["ordinatoButNotArrivatoColor"];
                    return ordinatoButNotArrivatoColor;
                }

                if (ro.ContainsPezzoSprovvisto)
                {
                    return new SolidColorBrush(AppColors.HighlightProblemBackColor);
                }
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
