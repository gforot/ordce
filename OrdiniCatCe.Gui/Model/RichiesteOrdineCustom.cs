using System.Linq;


namespace OrdiniCatCe.Gui.Model
{
    public partial class RichiesteOrdine
    {
        /// <summary>
        /// Torna true se esiste almeno un pezzo mancante.
        /// </summary>
        public bool ContainsPezzoSprovvisto
        {
            get
            {
                return PezziInOrdine != null && PezziInOrdine.Any(p => p.Sprovvisto);
            }
        }

        public int NumberOfPezzi
        {
            get
            {
                return PezziInOrdine == null ? 0 : PezziInOrdine.Count;
            }
        }
    }
}
