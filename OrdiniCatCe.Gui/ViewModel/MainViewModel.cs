using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.View;



namespace OrdiniCatCe.Gui.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand AddOrdineCommand { get; private set; }
        public RelayCommand AddMarcaCommand { get; private set; }
        public RelayCommand AddFornitoreCommand { get; private set; }
        public RelayCommand ClearFiltersCommand { get; private set; }
        public RelayCommand ClearNomeFilterCommand { get; private set; }
        public RelayCommand ClearCognomeFilterCommand { get; private set; }
        public RelayCommand ClearFornitoreFilterCommand { get; private set; }

        private const string _nameFilterPrpName="NameFilter";
        private string _nameFilter;
        public string NameFilter
        {
            get
            {
                return _nameFilter;
            }
            set
            {
                _nameFilter = value;
                RaisePropertyChanged(_nameFilterPrpName);
                RigheOrdine.Refresh();
            }
        }

        private const string _cognomeFilterPrpName = "CognomeFilter";
        private string _cognomeFilter;
        public string CognomeFilter
        {
            get
            {
                return _cognomeFilter;
            }
            set
            {
                _cognomeFilter = value;
                RaisePropertyChanged(_cognomeFilterPrpName);
                RigheOrdine.Refresh();
            }
        }

        private const string _fornitoreFilterPrpName = "FornitoreFilter";
        private string _fornitoreFilter;
        public string FornitoreFilter
        {
            get
            {
                return _fornitoreFilter;
            }
            set
            {
                _fornitoreFilter = value;
                RaisePropertyChanged(_fornitoreFilterPrpName);
                RigheOrdine.Refresh();
            }
        }

        private const string _viewRitiratiPrpName = "ViewRitirati";
        private bool _viewRitirati;

        public bool ViewRitirati
        {
            get
            {
                return _viewRitirati;
            }
            set
            {
                _viewRitirati = value;
                RaisePropertyChanged(_viewRitiratiPrpName);
                RigheOrdine.Refresh();
            }
        }

        private readonly ObservableCollection<RichiesteOrdine> _righeOrdine;

        public ICollectionView RigheOrdine { get; private set; }

        public MainViewModel()
        {
            _viewRitirati = false;
            _nameFilter = string.Empty;
            AddOrdineCommand = new RelayCommand(AddOrdine);
            AddMarcaCommand = new RelayCommand(AddMarca);
            AddFornitoreCommand = new RelayCommand(AddFornitore);
            ClearFiltersCommand = new RelayCommand(ClearFilters);
            ClearNomeFilterCommand = new RelayCommand(ClearNameFilter);
            ClearCognomeFilterCommand = new RelayCommand(ClearCognomeFilter);
            ClearFornitoreFilterCommand = new RelayCommand(ClearFornitoreFilter);
            Messenger.Default.Register<AddRigaOrdineMessage>(this, MsgKeys.AddRigaOrdineToDbKey, OnAddRigaOrdineToDbRequested);
            Messenger.Default.Register<AddMarcaMessage>(this, MsgKeys.AddMarcaToDbKey, OnAddMarcaToDbRequested);
            Messenger.Default.Register<AddFornitoreMessage>(this, MsgKeys.AddFornitoreToDbKey, OnAddFornitoreToDbRequested);


            Messenger.Default.Register<UpdateRigaOrdineMessage>(this, MsgKeys.UpdateRigaOrdineKey, OnUpdateRigaOrdineToDbRequested);
            Messenger.Default.Register<UpdateRigaOrdineMessage>(this, MsgKeys.SetAvvisatoKey, OnUpdateAvvisatoRequested);
            Messenger.Default.Register<UpdateRigaOrdineMessage>(this, MsgKeys.SetRitiratoKey, OnUpdateRitiratoRequested);
            Messenger.Default.Register<UpdateRigaOrdineMessage>(this, MsgKeys.SetArrivatoKey, OnUpdateArrivatoRequested);
            Messenger.Default.Register<UpdateRigaOrdineMessage>(this, MsgKeys.SetOrdinatoKey, OnUpdateOrdinatoRequested);
            Messenger.Default.Register<UpdateRigaOrdineMessage>(this, MsgKeys.DeleteKey, OnDeleteRequested);

            _righeOrdine = new ObservableCollection<RichiesteOrdine>();

            // Code runs "for real"
            using (OrdiniEntities db = new OrdiniEntities())
            {
                foreach (RichiesteOrdine ro in db.RichiesteOrdine.Include("Fornitori").Include("Marche").ToList())
                {
                    _righeOrdine.Add(ro);  
                } 
            }

            RigheOrdine = CollectionViewSource.GetDefaultView(_righeOrdine);
            RigheOrdine.Filter = Filter;
        }

        private void ClearFilters()
        {
            ClearNameFilter();
            ClearCognomeFilter();
            ClearFornitoreFilter();
        }

        private void ClearCognomeFilter()
        {
            CognomeFilter = string.Empty;
        }

        private void ClearNameFilter()
        {
            NameFilter = string.Empty;
        }

        private void ClearFornitoreFilter()
        {
            FornitoreFilter = string.Empty;
        }

        private void UpdateRigheOrdineFromDb()
        {
            _righeOrdine.Clear();

            // Code runs "for real"
            using (OrdiniEntities db = new OrdiniEntities())
            {
                foreach (RichiesteOrdine ro in db.RichiesteOrdine.Include("Fornitori").Include("Marche").ToList())
                {
                    _righeOrdine.Add(ro);
                }
            }
        }

        private void OnAddRigaOrdineToDbRequested(AddRigaOrdineMessage rigaOrdine)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                db.RichiesteOrdine.Add(rigaOrdine.RigaOrdine);
                db.SaveChanges();
                UpdateRigheOrdineFromDb();
            }
        }

        private void OnAddMarcaToDbRequested(AddMarcaMessage message)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                db.Marche.Add(message.Marca);
                db.SaveChanges();
            }
        }

        private void OnAddFornitoreToDbRequested(AddFornitoreMessage message)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                db.Fornitori.Add(message.Fornitore);
                db.SaveChanges();
            }
        }

        private void OnUpdateArrivatoRequested(UpdateRigaOrdineMessage message)
        {
            if (message.RigaOrdine != null)
            {
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    RichiesteOrdine toUpdate = db.RichiesteOrdine.First(ordine => ordine.Id == message.RigaOrdine.Id);
                    toUpdate.Arrivato = true;
                    toUpdate.DataArrivato = DateTime.Now;
                    db.SaveChanges();
                    UpdateRigheOrdineFromDb();
                }
            }
        }

        private void OnUpdateOrdinatoRequested(UpdateRigaOrdineMessage message)
        {
            if (message.RigaOrdine != null)
            {
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    RichiesteOrdine toUpdate = db.RichiesteOrdine.First(ordine => ordine.Id == message.RigaOrdine.Id);
                    toUpdate.Ordinato = true;
                    toUpdate.DataOrdinato = DateTime.Now;
                    db.SaveChanges();
                    UpdateRigheOrdineFromDb();
                }
            }
        }

        private void OnDeleteRequested(UpdateRigaOrdineMessage message)
        {
            if (message.RigaOrdine != null)
            {
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    RichiesteOrdine toUpdate = db.RichiesteOrdine.First(ordine => ordine.Id == message.RigaOrdine.Id);
                    db.RichiesteOrdine.Remove(toUpdate);
                    db.SaveChanges();
                    UpdateRigheOrdineFromDb();
                }
            }
        }

        private void OnUpdateAvvisatoRequested(UpdateRigaOrdineMessage message)
        {
            if (message.RigaOrdine != null)
            {
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    RichiesteOrdine toUpdate = db.RichiesteOrdine.First(ordine => ordine.Id == message.RigaOrdine.Id);
                    toUpdate.Avvisato = true;
                    toUpdate.DataAvvisato = DateTime.Now;
                    db.SaveChanges();
                    UpdateRigheOrdineFromDb();
                }
            }
        }

        private void OnUpdateRitiratoRequested(UpdateRigaOrdineMessage message)
        {
            if (message.RigaOrdine != null)
            {
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    RichiesteOrdine toUpdate = db.RichiesteOrdine.First(ordine => ordine.Id == message.RigaOrdine.Id);
                    toUpdate.Ritirato = true;
                    toUpdate.DataRitirato = DateTime.Now;
                    db.SaveChanges();
                    UpdateRigheOrdineFromDb();
                }
            }
        }

        private void AddOrdine()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.AddRigaOrdineKey);
        }

        private void AddMarca()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.AddMarcaKey);
        }

        private void AddFornitore()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.AddFornitoreKey);
        }

        private void OnUpdateRigaOrdineToDbRequested(UpdateRigaOrdineMessage message)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                RichiesteOrdine ro = db.RichiesteOrdine.First(r => r.Id == message.RigaOrdine.Id);
                CopyAllProperties(message.RigaOrdine, ro);
                int updated = db.SaveChanges();
                UpdateRigheOrdineFromDb();
            }
        }

        private static void CopyAllProperties(RichiesteOrdine source, RichiesteOrdine target)
        {
            target.Avvisato = source.Avvisato;
            target.Cellulare = source.Cellulare;
            target.Codice = source.Codice;
            target.Cognome = source.Cognome;
            target.DataArrivato = source.DataArrivato;
            target.DataAvvisato = source.DataAvvisato;
            target.DataOrdinato = source.DataOrdinato;
            target.DataRitirato = source.DataRitirato;
            target.Descrizione = source.Descrizione;
            target.EMail = source.EMail;
            target.IdFornitore = source.IdFornitore;
            target.IdMarca = source.IdMarca;
            target.Indirizzo = source.Indirizzo;
            target.Localita = source.Localita;
            target.Modalit‡Avviso = source.Modalit‡Avviso;
            target.Nome = source.Nome;
            target.NumeroCivico = source.NumeroCivico;
            target.PrezzoAcquisto = source.PrezzoAcquisto;
            target.Ritirato = source.Ritirato;
            target.Telefono = source.Telefono;
            target.PrezzoVendita = source.PrezzoVendita;
        }

        private bool Filter(object obj)
        {
            if (!(obj is RichiesteOrdine))
            {
                return true;
            }

            RichiesteOrdine rOrdine = obj as RichiesteOrdine;
            return FilterByName(rOrdine) && 
                FilterByCognome(rOrdine) && 
                FilterByRitirati(rOrdine) &&
                FilterByFornitore(rOrdine);
        }

        private bool FilterByFornitore(RichiesteOrdine rOrdine)
        {
            if (string.IsNullOrEmpty(FornitoreFilter))
            {
                return true;
            }

            if (string.IsNullOrEmpty(rOrdine.Fornitori.Name))
            {
                return false;
            }

            return rOrdine.Fornitori.Name.ToLower().Contains(FornitoreFilter.ToLower());
        }

        private bool FilterByRitirati(RichiesteOrdine rOrdine)
        {
            return ViewRitirati || !rOrdine.Ritirato;
        }

        private bool FilterByName(RichiesteOrdine rOrdine)
        {
            if (string.IsNullOrEmpty(NameFilter))
            {
                return true;
            }

            if (string.IsNullOrEmpty(rOrdine.Nome))
            {
                return false;
            }

            return rOrdine.Nome.ToLower().Contains(NameFilter.ToLower());
        }

        private bool FilterByCognome(RichiesteOrdine rOrdine)
        {
            if (string.IsNullOrEmpty(CognomeFilter))
            {
                return true;
            }

            if (string.IsNullOrEmpty(rOrdine.Cognome))
            {
                return false;
            }

            return rOrdine.Cognome.ToLower().Contains(CognomeFilter.ToLower());
        }
    }
}