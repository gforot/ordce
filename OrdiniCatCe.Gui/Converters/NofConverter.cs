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
                DateTime? date = null;
                switch (par.ToLower())
                {
                    case "ordinati":
                        n = ro.PezziInOrdine.Where(p => p.Ordinato).Count();
                        if (n > 0)
                        {
                            date = ro.PezziInOrdine.First(p => p.Ordinato).DataOrdinato;
                        }
                        break;
                    case "arrivati":
                        n = ro.PezziInOrdine.Where(p => p.Arrivato).Count();
                        if (n > 0)
                        {
                            date = ro.PezziInOrdine.First(p => p.Arrivato).DataArrivato;
                        }
                        break;
                    case "avvisati":
                        n = ro.PezziInOrdine.Where(p => p.Avvisato).Count();
                        if (n > 0)
                        {
                            date = ro.PezziInOrdine.First(p => p.Avvisato).DataAvvisato;
                        }
                        break;
                    case "ritirati":
                        n = ro.PezziInOrdine.Where(p => p.Ritirato).Count();
                        if (n > 0)
                        {
                            date = ro.PezziInOrdine.First(p => p.Ritirato).DataRitirato;
                        }
                        break;
                    case "sprovvisti":
                        n = ro.PezziInOrdine.Where(p => p.Sprovvisto).Count();
                        break;
                    case "fuoristock":
                        n = ro.PezziInOrdine.Where(p => p.FuoriStock).Count();
                        break;

                }
                if (date == null)
                {
                    return string.Format("{0}/{1}", n, ro.NumberOfPezzi);
                }
                else
                {
                    return string.Format("{0}/{1}{3}{2}", n, ro.NumberOfPezzi, date.Value.ToString("dd-MM-yyyy"), Environment.NewLine);
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
