using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.Messages
{
    public class AddFornitoreMessage
    {
        public AddFornitoreMessage(Fornitori f)
        {
            Fornitore = f;
        }

        public Fornitori Fornitore { get; set; }
    }
}
