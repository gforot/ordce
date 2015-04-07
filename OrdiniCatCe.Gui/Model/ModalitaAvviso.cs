using System;

namespace OrdiniCatCe.Gui.Model
{
    [Flags]
    public enum ModalitaAvviso
    {
        Telefono = 1,
        Cellulare = 2,
        EMail = 4,
    }
}
