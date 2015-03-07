using OrdiniCatCe.Gui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OrdiniCatCe.Gui.Converters
{
    public class ContattoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is RichiesteOrdine)
            {
                RichiesteOrdine ro = value as RichiesteOrdine;

                if (!string.IsNullOrEmpty(ro.Telefono))
                {
                    return ro.Telefono;
                }


                if (!string.IsNullOrEmpty(ro.Cellulare))
                {
                    return ro.Cellulare;
                }

                if (!string.IsNullOrEmpty(ro.EMail))
                {
                    return ro.EMail;
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
