using GalaSoft.MvvmLight;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OrdiniCatCe.Gui.ViewModel
{
    public class FornitoriViewModel : ViewModelBase
    {
        private Fornitori _fornitore;
        public Fornitori Fornitore 
        {
            get
            {
                return _fornitore;
            }
            set
            {
                if((_fornitore == null)||(value==null) ||
                    (_fornitore.Id != value.Id))
                {
                    _fornitore = value;
                    FornitoreChanged();
                }
            }
        }

        public string FName
        {
            get
            {
                return Fornitore == null ? string.Empty : Fornitore.Name;
            }
        }

        public string FTelefono
        {
            get
            {
                return Fornitore == null ? string.Empty : Fornitore.Telefono;
            }
        }

        public string FEmail
        {
            get
            {
                return Fornitore == null ? string.Empty : Fornitore.Email;
            }
        }

        private void FornitoreChanged()
        {
            PezziInOrdine.Clear();

            if (_fornitore == null)
            {
                return;
            }
            using (OrdiniEntities db = new OrdiniEntities())
            {
                foreach (PezziInOrdine po in DbManager.GetPezziByIdFornitore(_fornitore.Id))
                {
                    PezziInOrdine.Add(po);
                }

                RaisePropertyChanged("FName");
                RaisePropertyChanged("FTelefono");
                RaisePropertyChanged("FEmail");
            }
        }

        public ObservableCollection<Fornitori> Fornitori { get; private set; }
        public ObservableCollection<PezziInOrdine> PezziInOrdine { get; set; }

        public FornitoriViewModel()
        {
            PezziInOrdine = new ObservableCollection<PezziInOrdine>();
            Init();
        }

        private void Init()
        {
            
            using (OrdiniEntities db = new OrdiniEntities())
            {
                Fornitori = new ObservableCollection<Fornitori>(db.Fornitori.ToList());
            }
        }

        internal void Setup()
        {
            Fornitore = null;
            Init();

        }
    }
}
