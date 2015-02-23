﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace OrdiniCatCe.Gui.Converters
{
    public class VisualizzaStoricoTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                //se non sto visualizzando lo storico(VisualizzaStorico=false) il testo deve essere "Visualizza Storico".
                if (!(bool)value)
                {
                    return "Visualizza Storico";
                }
            }

            return "Visualizza Ordini Attivi";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
