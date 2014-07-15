using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public List<Marche> Marches { get; private set; }
        public List<RichiesteOrdine> RigheOrdine { get; private set; } 

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    Marches = db.GetMarche();
                    RigheOrdine = db.RichiesteOrdine.ToList();
                }
            }
            else
            {
                // Code runs "for real"
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    Marches = db.GetMarche();
                    RigheOrdine = db.RichiesteOrdine.ToList();
                }
            }

        }
    }
}