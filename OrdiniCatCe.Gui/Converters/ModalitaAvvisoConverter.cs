using System.Windows.Data;

namespace OrdiniCatCe.Gui.Converters
{
    public class ModalitaAvvisoConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is int))
            {
                return string.Empty;
            }

            //
            int modAvvisoInt = (int)value;
            string rVal = string.Empty;

            bool avvEmail, avvTel, avvCell;

            OrdiniCatCe.Gui.Utils.Utilities.ParseModalitaAvviso(modAvvisoInt, out avvTel, out avvCell, out avvEmail);

            if (avvCell)
            {
                rVal = string.Concat(rVal, "Cellulare ");
            }
            if (avvTel)
            {
                rVal = string.Concat(rVal, "Telefono ");
            }
            if (avvEmail)
            {
                rVal = string.Concat(rVal, "Email ");
            }

            if (rVal.Length > 0)
            {
                rVal = rVal.Substring(0, rVal.Length - 1);
            }

            return rVal;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
