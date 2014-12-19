using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Converters;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AddPezzoInOrdineViewModel : DialogViewModelBase
    {

        public ObservableCollection<Marche> Marche { get; private set; }
        public ObservableCollection<Fornitori> Fornitori { get; private set; }

        #region Codice
        private const string _codicePrpName = "Codice";
        private string _codice;

        public string Codice
        {
            get
            {
                return _codice;
            }
            set
            {
                _codice = value;
                RaisePropertyChanged(_codicePrpName);
            }
        }
        #endregion

        #region Descrizione
        private const string _descrizionePrpName = "Descrizione";
        private string _descrizione;

        public string Descrizione
        {
            get
            {
                return _descrizione;
            }
            set
            {
                _descrizione = value;
                RaisePropertyChanged(_descrizionePrpName);
            }
        }
        #endregion

        #region PrezzoAcquisto
        private const string _prezzoAcqPrpName = "PrezzoAcquisto";
        private decimal _prezzoAcq;

        public decimal PrezzoAcquisto
        {
            get
            {
                return _prezzoAcq;
            }
            set
            {
                _prezzoAcq = value;
                RaisePropertyChanged(_prezzoAcqPrpName);
            }
        }
        #endregion

        #region PrezzoVendita
        private const string _prezzoVenPrpName = "PrezzoVendita";
        private decimal _prezzoVen;

        public decimal PrezzoVendita
        {
            get
            {
                return _prezzoVen;
            }
            set
            {
                _prezzoVen = value;
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
        private bool _ordinato;

        public bool Ordinato
        {
            get
            {
                return _ordinato;
            }
            set
            {
                _ordinato = value;
                RaisePropertyChanged(_ordinatoPrpName);
            }
        }
        #endregion

        #region Arrivato
        private const string _arrivatoPrpName = "Arrivato";
        private bool _arrivato;

        public bool Arrivato
        {
            get
            {
                return _arrivato;
            }
            set
            {
                _arrivato = value;
                RaisePropertyChanged(_arrivatoPrpName);
            }
        }
        #endregion

        #region Ritirato
        private const string _ritiratoPrpName = "Ritirato";
        private bool _ritirato;

        public bool Ritirato
        {
            get
            {
                return _ritirato;
            }
            set
            {
                _ritirato = value;
                RaisePropertyChanged(_ritiratoPrpName);
            }
        }
        #endregion

        #region Mancante
        private const string _mancantePrpName = "Mancante";
        private bool _mancante;

        public bool Mancante
        {
            get
            {
                return _mancante;
            }
            set
            {
                _mancante = value;
                RaisePropertyChanged(_mancantePrpName);
            }
        }
        #endregion


        public override void Setup()
        {
            //sbiancare tutto
            Codice = string.Empty;
            Descrizione = string.Empty;
            Marca = null;
            Fornitore = null;
            Mancante = false;
            Arrivato = false;
            Ritirato = false;
            Ordinato = false;
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
            PezziInOrdine pezzo = new PezziInOrdine();
            pezzo.Codice = Codice;
            pezzo.Description = Descrizione;
            pezzo.PrezzoAcquisto = PrezzoAcquisto;
            pezzo.PrezzoVendita = PrezzoVendita;
            pezzo.IdFornitore = Fornitore.Id;
            pezzo.IdMarca = Marca.Id;
            pezzo.Ordinato = Ordinato;
            pezzo.Arrivato = Arrivato;
            pezzo.Ritirato = Ritirato;
            pezzo.Mancante = Mancante;

            if (!DbManager.AddPezzo(pezzo, out errorMessage))
            {
                Messenger.Default.Send(new ErrorMessageMessage(errorMessage), MsgKeys.ShowErrorMessageOnAddPezzoKey);
            }
            else//se tutto va bene mando messaggio di Conferma
            {
                Messenger.Default.Send(new AddPezzoMessage(pezzo), MsgKeys.ConfirmAddPezzoKey);
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
    }
}
