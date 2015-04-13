namespace OrdiniCatCe.Gui.Model
{
    public partial class PezziInOrdine
    {
        public override bool Equals(object obj)
        {
            if (!(obj is PezziInOrdine))
            {
                return false;
            }
            return (obj as PezziInOrdine).Id == Id;
        }

        public bool IsArrivatoButNotAvvisato
        {
            get { return Arrivato && (!Avvisato); }
        }

        public bool IsOrdinatoButNotArrivato
        {
            get { return Ordinato && (!Arrivato); }
        }




    }
}
