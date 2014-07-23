using System;
using System.Net.Mail;
using System.Windows.Controls;


namespace OrdiniCatCe.Gui.Validation
{
    public class EMailValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return ValidationResult.ValidResult;
            }
            try
            {
                MailAddress address = new MailAddress((string)value);
                return ValidationResult.ValidResult;
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Inserire una email valida");
            }
        }
    }
}
