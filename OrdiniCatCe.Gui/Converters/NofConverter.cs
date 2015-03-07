using OrdiniCatCe.Gui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OrdiniCatCe.Gui.Converters
{
    public class NofConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((parameter is string) && (value is RichiesteOrdine))
            {
                string par = parameter as string;
                RichiesteOrdine ro = value as RichiesteOrdine;
                int n = 0;
                switch (par.ToLower())
                {
                    case "ordinati":
                        n = ro.PezziInOrdine.Where(p => p.Ordinato).Count();
                        break;
                    case "arrivati":
                        n = ro.PezziInOrdine.Where(p => p.Arrivato).Count();
                        break;
                    case "avvisati":
                        n = ro.PezziInOrdine.Where(p => p.Avvisato).Count();
                        break;
                    case "ritirati":
                        n = ro.PezziInOrdine.Where(p => p.Ritirato).Count();
                        break;
                    case "mancanti":
                        n = ro.PezziInOrdine.Where(p => p.Mancante).Count();
                        break;
                }

                return string.Format("{0}/{1}", n, ro.NumberOfPezzi);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
