using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.Messages
{
    public class UpdateRigaOrdineMessage
    {
        public  RichiesteOrdine RigaOrdine { get; private set; }

        public UpdateRigaOrdineMessage(RichiesteOrdine richiestaOrdine)
        {
            this.RigaOrdine = richiestaOrdine;
        }
    }
}
