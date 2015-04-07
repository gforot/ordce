
namespace OrdiniCatCe.Gui.Utils
{
    public static class Utilities
    {
        public static void ParseModalitaAvviso(int modalitaAvviso, out bool avvisoTel, out bool avvisoCel, out bool avvisoMail)
        {
            ParseModalitaAvviso((OrdiniCatCe.Gui.Model.ModalitaAvviso)modalitaAvviso, out avvisoTel, out avvisoCel, out avvisoMail); 
        }

        public static void ParseModalitaAvviso(OrdiniCatCe.Gui.Model.ModalitaAvviso modalita, out bool avvisoTel, out bool avvisoCel, out bool avvisoMail)
        {
            avvisoCel = ((modalita & OrdiniCatCe.Gui.Model.ModalitaAvviso.Cellulare) == OrdiniCatCe.Gui.Model.ModalitaAvviso.Cellulare);
            avvisoTel = ((modalita & OrdiniCatCe.Gui.Model.ModalitaAvviso.Telefono) == OrdiniCatCe.Gui.Model.ModalitaAvviso.Telefono);
            avvisoMail = ((modalita & OrdiniCatCe.Gui.Model.ModalitaAvviso.EMail) == OrdiniCatCe.Gui.Model.ModalitaAvviso.EMail);
        }

    }
}
