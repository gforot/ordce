using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.Messages
{
    public class AddPezzoMessage : MessageBase
    {
        public PezziInOrdine Pezzo { get; private set; }

        public AddPezzoMessage(PezziInOrdine pezzo)
        {
            Pezzo = pezzo;
        }

        public override string ToString()
        {
            return Pezzo.ToString();
        }
    }
}
