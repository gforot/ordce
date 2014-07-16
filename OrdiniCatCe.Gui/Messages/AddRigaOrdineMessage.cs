using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.Messages
{
    public class AddRigaOrdineMessage : MessageBase
    {
        public RichiesteOrdine RigaOrdine { get; set; }

        public AddRigaOrdineMessage()
        {
            
        }
    }
}
