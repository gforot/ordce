using System;
using System.Windows.Data;
using System.Windows.Media;
using OrdiniCatCe.Gui.Constants;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.Converters
{
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is RichiesteOrdine)
            {
                RichiesteOrdine ro = value as RichiesteOrdine;

                if (!ro.Ordinato)
                {
                    return new SolidColorBrush(AppColors.OrdinatoRowBackground);                  
                }
                if (ro.Avvisato)
                {
                    return new SolidColorBrush(AppColors.AvvisatoRowBackground);
                }
                if (ro.Ritirato)
                {
                    return new SolidColorBrush(AppColors.RitiratoRowBackground);
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
