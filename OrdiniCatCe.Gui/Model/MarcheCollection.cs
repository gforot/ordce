using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrdiniCatCe.Gui.Model
{
    public class MarcheCollection : ObservableCollection<Marche>
    {
        public MarcheCollection(IEnumerable<Marche> marche)
        {
            foreach (Marche f in marche)
            {
                this.Add(f);
            }
        }
    }
}