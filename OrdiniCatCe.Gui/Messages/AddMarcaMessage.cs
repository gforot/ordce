using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.Messages
{
    public class AddMarcaMessage
    {
        public AddMarcaMessage(Marche m)
        {
            Marca = m;
        }

        public Marche Marca { get; set; }
    }
}
