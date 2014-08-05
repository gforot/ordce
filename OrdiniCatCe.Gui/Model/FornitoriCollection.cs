using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrdiniCatCe.Gui.Model
{
    public class FornitoriCollection : ObservableCollection<Fornitori>
    {
        public FornitoriCollection(IEnumerable<Fornitori> fornitori)
        {
            foreach (Fornitori f in fornitori)
            {
                this.Add(f);
            }
        }
    }
}
