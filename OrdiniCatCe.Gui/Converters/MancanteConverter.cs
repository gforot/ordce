using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using OrdiniCatCe.Gui.Constants;
using System.Windows;


namespace OrdiniCatCe.Gui.Converters
{
    public class MancanteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool?)
            {
                bool? mancante = (bool?)value;
                if (mancante.HasValue && mancante.Value)
                {
                    SolidColorBrush arrivatoButNotAvvisatoColor = (SolidColorBrush)Application.Current.Resources["sprovvistoColor"];
                    return arrivatoButNotAvvisatoColor;
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