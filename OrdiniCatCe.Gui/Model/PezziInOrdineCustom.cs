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
    }
}
