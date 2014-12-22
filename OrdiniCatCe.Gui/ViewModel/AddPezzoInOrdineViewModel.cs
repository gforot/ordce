using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AddPezzoInOrdineViewModel : DialogViewModelBase
    {
        private int _idRigaOrdine;

        private PezziInOrdine _pezzo;

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
                RaisePropertyChanged(_fornitorePrpName);
            }
        }
        #endregion

        #region Ordinato
        private const string _ordinatoPrpName = "Ordinato";
        public bool? Ordinato
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

        public bool? Arrivato
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
        public bool? Ritirato
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
        public bool? Mancante
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
        }

        public void Setup(PezziInOrdine pezzoDaModificare)
        {
            _pezzo = pezzoDaModificare;
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


            Messenger.Default.Register<AddMarcaMessage>(this, MsgKeys.MarcaAddedKey, OnMarcaAdded);
            Messenger.Default.Register<AddFornitoreMessage>(this, MsgKeys.FornitoreAddedKey, OnFornitoreAdded);
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
