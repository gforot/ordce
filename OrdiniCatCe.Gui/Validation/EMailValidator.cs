using System;
using System.Windows.Controls;


namespace OrdiniCatCe.Gui.Validation
{
    public class EMailValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Non può essere vuoto");
            }

            return ValidationResult.ValidResult;
        }
    }
}
