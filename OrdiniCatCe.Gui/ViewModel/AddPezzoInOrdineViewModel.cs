using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using GalaSoft.MvvmLight.Command;
using OrdiniCatCe.Gui.View;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AddPezzoInOrdineViewModel : DialogViewModelBase
    {
        private int _idRigaOrdine;

        private PezziInOrdine _pezzo;

        public RelayCommand AddMarcaCommand { get; private set; }
        public RelayCommand AddFornitoreCommand { get; private set; }

        public ObservableCollection<Marche> Marche { get; private set; }
        public ObservableCollection<Fornitori> Fornitori { get; private set; }

        #region Codice
        private const string _codicePrpName = "Codice";
        public string Codice
        {
            get
            {
                return _pezzo.Codice;
            }
            set
            {
                _pezzo.Codice = value;
                RaisePropertyChanged(_codicePrpName);
            }
        }
        #endregion

        #region Descrizione
        private const string _descrizionePrpName = "Descrizione";
        public string Descrizione
        {
            get
            {
                return _pezzo.Description;
            }
            set
            {
                _pezzo.Description = value;
                RaisePropertyChanged(_descrizionePrpName);
            }
        }
        #endregion

        #region PrezzoAcquisto
        private const string _prezzoAcqPrpName = "PrezzoAcquisto";

        public decimal? PrezzoAcquisto
        {
            get
            {
                return _pezzo.PrezzoAcquisto;
            }
            set
            {
                _pezzo.PrezzoAcquisto = value;
                RaisePropertyChanged(_prezzoAcqPrpName);
            }
        }
        #endregion

        #region PrezzoVendita
        private const string _prezzoVenPrpName = "PrezzoVendita";

        public decimal? PrezzoVendita
        {
            get
            {
                return _pezzo.PrezzoVendita;
            }
            set
            {
                _pezzo.PrezzoVendita = value;
                RaisePropertyChanged(_prezzoVenPrpName);
            }
        }
        #endregion

        #region Marca
        private const string _marcaPrpName = "Marca";
        private Marche _marca;
        public Marche Marca
        {
            get
            {
                return _marca;
            }
            set
            {
                _marca = value;
                _pezzo.IdMarca = _marca.Id;
                RaisePropertyChanged(_marcaPrpName);
            }
        }
        #endregion

        #region Fornitore
        private const string _fornitorePrpName = "Fornitore";
        private Fornitori _fornitore;
        public Fornitori Fornitore
        {
            get
            {
                return _fornitore;
            }
            set
            {
                _fornitore = value;
                _pezzo.IdFornitore = _fornitore.Id;
                RaisePropertyChanged(_fornitorePrpName);
            }
        }
        #endregion

        #region Ordinato
        private const string _ordinatoPrpName = "Ordinato";
        public bool Ordinato
        {
            get
            {
                return _pezzo.Ordinato;
            }
            set
            {
                _pezzo.Ordinato = value;
                RaisePropertyChanged(_ordinatoPrpName);
            }
        }
        #endregion

        #region Arrivato
        private const string _arrivatoPrpName = "Arrivato";

        public bool Arrivato
        {
            get
            {
                return _pezzo.Arrivato;
            }
            set
            {
                _pezzo.Arrivato = value;
                RaisePropertyChanged(_arrivatoPrpName);
            }
        }
        #endregion

        #region Ritirato
        private const string _ritiratoPrpName = "Ritirato";
        public bool Ritirato
        {
            get
            {
                return _pezzo.Ritirato;
            }
            set
            {
                _pezzo.Ritirato = value;
                RaisePropertyChanged(_ritiratoPrpName);
            }
        }
        #endregion

        #region Mancante
        private const string _mancantePrpName = "Mancante";
        public bool Mancante
        {
            get
            {
                return _pezzo.Mancante;
            }
            set
            {
                _pezzo.Mancante = value;
                RaisePropertyChanged(_mancantePrpName);
            }
        }
        #endregion

        #region Avvisato
        private const string _avvisatoPrpName = "Avvisato";

        public bool Avvisato
        {
            get
            {
                return _pezzo.Avvisato;
            }
            set
            {
                _pezzo.Avvisato = value;
                RaisePropertyChanged(_avvisatoPrpName);
            }
        }
        #endregion

        #region DataOrdinato
        private const string _dataOrdinatoPrpName = "DataOrdinato";
        public DateTime? DataOrdinato
        {
            get
            {
                return _pezzo.DataOrdinato;
            }
            set
            {
                _pezzo.DataOrdinato = value;
                RaisePropertyChanged(_dataOrdinatoPrpName);
            }
        }
        #endregion

        #region DataArrivato
        private const string _dataArrivatoPrpName = "DataArrivato";
        public DateTime? DataArrivato
        {
            get
            {
                return _pezzo.DataArrivato;
            }
            set
            {
                _pezzo.DataArrivato = value;
                RaisePropertyChanged(_dataArrivatoPrpName);
            }
        }
        #endregion

        #region DataRitirato
        private const string _dataRitiratoPrpName = "DataRitirato";
        public DateTime? DataRitirato
        {
            get
            {
                return _pezzo.DataRitirato;
            }
            set
            {
                _pezzo.DataRitirato = value;
                RaisePropertyChanged(_dataRitiratoPrpName);
            }
        }
        #endregion

        #region DataAvvisato
        private const string _dataAvvisatoPrpName = "DataAvvisato";
        public DateTime? DataAvvisato
        {
            get
            {
                return _pezzo.DataAvvisato;
            }
            set
            {
                _pezzo.DataAvvisato = value;
                RaisePropertyChanged(_dataAvvisatoPrpName);
            }
        }
        #endregion

        public void Setup(int idRigaOrdine)
        {
            _pezzo = new PezziInOrdine();
            _pezzo.IdRichiestaOrdine = idRigaOrdine;
            _pezzo.Codice = string.Empty;
            _pezzo.Description = string.Empty;
            _pezzo.Mancante = false;
            _pezzo.Arrivato = false;
            _pezzo.Ordinato = false;
            _pezzo.Ritirato = false;
            _pezzo.PrezzoAcquisto = 0;
            _pezzo.PrezzoVendita = 0;
            _pezzo.DataArrivato = null;
            _pezzo.DataAvvisato = null;
            _pezzo.DataRitirato = null;
            _pezzo.DataOrdinato = null;
            _pezzo.IdFornitore = null;
            _pezzo.IdMarca = null;
        }

        public void Setup(PezziInOrdine pezzoDaModificare)
        {
            _pezzo = pezzoDaModificare;
            if (_pezzo.IdFornitore.HasValue)
            {
                Fornitore = Fornitori.FirstOrDefault(f => f.Id == _pezzo.IdFornitore.Value);
            }
            if (_pezzo.IdMarca.HasValue)
            {
                Marca = Marche.FirstOrDefault(m => m.Id == _pezzo.IdMarca.Value);
            }
        }


        private void Init()
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                Marche = new ObservableCollection<Marche>(db.Marche.ToList());
                Fornitori = new ObservableCollection<Fornitori>(db.Fornitori.ToList());
            }
        }

        public AddPezzoInOrdineViewModel()
            : base()
        {
            Init();
            
            //Commands
            this.AddFornitoreCommand = new RelayCommand(AddFornitore);
            this.AddMarcaCommand = new RelayCommand(AddMarca);
            Messenger.Default.Register<AddMarcaMessage>(this, MsgKeys.MarcaAddedKey, OnMarcaAdded);
            Messenger.Default.Register<AddFornitoreMessage>(this, MsgKeys.FornitoreAddedKey, OnFornitoreAdded);
        }

        private void AddMarca()
        {
            AddMarcaWindow wnd = new AddMarcaWindow();
            wnd.ShowDialog();
        }

        private void AddFornitore()
        {
            AddFornitoreWindow wnd = new AddFornitoreWindow();
            wnd.ShowDialog();
        }

        public override void Conferma()
        {
            string errorMessage;
            if (!DbManager.AddPezzo(_pezzo, out errorMessage))
            {
                Messenger.Default.Send(new ErrorMessageMessage(errorMessage), MsgKeys.ShowErrorMessageOnAddPezzoKey);
            }
            else//se tutto va bene mando messaggio di Conferma
            {
                Messenger.Default.Send(new AddPezzoMessage(_pezzo), MsgKeys.ConfirmAddPezzoKey);
            }
        }

        public override void Annulla()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.CancelAddPezzoKey);
        }

        public override bool CanConferma
        {
            get
            {
                return !string.IsNullOrEmpty(Codice) && !string.IsNullOrEmpty(Descrizione) && (Marca != null) && (Fornitore != null);
            }
        }

        private void OnMarcaAdded(AddMarcaMessage message)
        {
            Marche.Add(message.Marca);
            Marca = message.Marca;
        }

        private void OnFornitoreAdded(AddFornitoreMessage message)
        {
            Fornitori.Add(message.Fornitore);
            Fornitore = message.Fornitore;
        }

        public override void Setup()
        {
            throw new System.NotImplementedException();
        }
    }
}
