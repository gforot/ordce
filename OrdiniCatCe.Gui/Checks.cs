using OrdiniCatCe.Gui.Validation;

namespace OrdiniCatCe.Gui
{
    public static class Checks
    {
        public static bool CheckEmail(string email)
        {
            EMailValidator validator = new EMailValidator();
            var res = validator.Validate(email, null);
            return res.IsValid;
        }
    }
}
