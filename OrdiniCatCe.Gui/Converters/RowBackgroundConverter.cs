﻿using System;
using System.Windows.Data;
using System.Windows.Media;
using OrdiniCatCe.Gui.Constants;
using OrdiniCatCe.Gui.Model;


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
                    return new SolidColorBrush(AppColors.NotOrdinatoColor);
                }

                if (ro.ContainsPezzoArrivatoButNotAvvisato)
                {
                    return new SolidColorBrush(AppColors.ArrivatoButNotAvvisatoColor);
                }

                if (ro.ContainsPezzoOrdinatoButNotArrivato)
                {
                    return new SolidColorBrush(AppColors.OrdinatoButNotArrivatoColor);
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
