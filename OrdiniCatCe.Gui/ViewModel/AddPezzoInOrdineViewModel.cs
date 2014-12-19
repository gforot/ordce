using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AddPezzoInOrdineViewModel : ViewModelBase
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

        public RelayCommand AnnullaCommand { get; private set; }
        public RelayCommand ConfermaCommand { get; private set; }

        internal void Setup()
        {
            //sbiancare tutto
            Codice = string.Empty;
            Descrizione = string.Empty;
            Marca = null;
            Fornitore = null;
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
        {
            AnnullaCommand = new RelayCommand(Annulla);
            ConfermaCommand = new RelayCommand(Conferma, () => CanConferma);

            Init();


            Messenger.Default.Register<AddMarcaMessage>(this, MsgKeys.MarcaAddedKey, OnMarcaAdded);
            Messenger.Default.Register<AddFornitoreMessage>(this, MsgKeys.FornitoreAddedKey, OnFornitoreAdded);
        }

        private void Conferma()
        {
            string errorMessage;
            PezziInOrdine pezzo = new PezziInOrdine();
            pezzo.Codice = Codice;
            pezzo.Description = Descrizione;
            pezzo.PrezzoAcquisto = PrezzoAcquisto;
            pezzo.PrezzoVendita = PrezzoVendita;
            pezzo.IdFornitore = Fornitore.Id;
            pezzo.IdMarca = Marca.Id;

            if (!DbManager.AddPezzo(pezzo, out errorMessage))
            {
                Messenger.Default.Send(new ErrorMessageMessage(errorMessage), MsgKeys.ShowErrorMessageOnAddPezzoKey);
            }
            else//se tutto va bene mando messaggio di Conferma
            {
                Messenger.Default.Send(new AddPezzoMessage(pezzo), MsgKeys.ConfirmAddPezzoKey);
            }
        }

        public void Annulla()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.CancelAddPezzoKey);
        }

        public bool CanConferma
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
