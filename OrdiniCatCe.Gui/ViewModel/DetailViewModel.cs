using System.Data;
using System.Data.Entity.Migrations.Model;
using System.Windows.Media.TextFormatting;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class DetailViewModel : ViewModelBase
    {
        private int _id;

        #region Cliente
        private const string _clientePrpName = "Cliente";
        private string _cliente;
        public string Cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                _cliente = value;
                RaisePropertyChanged(_clientePrpName);
            }
        }

        #endregion

        #region Telefono
        private const string _telefonoPrpName = "Telefono";
        private string _telefono;
        public string Telefono
        {
            get
            {
                return _telefono;
            }
            set
            {
                _telefono = value;
                RaisePropertyChanged(_telefonoPrpName);
            }
        }

        #endregion

        #region Cellulare
        private const string _cellularePrpName = "Cellulare";
        private string _cellulare;
        public string Cellulare
        {
            get
            {
                return _cellulare;
            }
            set
            {
                _cellulare = value;
                RaisePropertyChanged(_cellularePrpName);
            }
        }

        #endregion

        #region Indirizzo
        private const string _indirizzoPrpName = "Indirizzo";
        private string _indirizzo;
        public string Indirizzo
        {
            get
            {
                return _indirizzo;
            }
            set
            {
                _indirizzo = value;
                RaisePropertyChanged(_indirizzoPrpName);
            }
        }

        #endregion

        #region EMail
        private const string _emailPrpName = "EMail";
        private string _email;
        public string EMail
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged(_emailPrpName);
            }
        }

        #endregion

        #region Avvisato


        private const string _avvisatoPrpName = "Avvisato";
        private bool _avvisato;
        public bool Avvisato
        {
            get
            {
                return _avvisato;
            }
            set
            {
                _avvisato = value;
                RaisePropertyChanged(_avvisatoPrpName);
            }
        }
        #endregion

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

        #region DataAvvisato
        private const string _dataAvvisatoPrpName = "DataAvvisato";
        private System.DateTime? _dataAvvisato;
        public System.DateTime? DataAvvisato
        {
            get
            {
                return _dataAvvisato;
            }
            set
            {
                _dataAvvisato = value;
                RaisePropertyChanged(_dataAvvisatoPrpName);
            }
        }

        #endregion

        #region DataOrdinato
        private const string _dataOrdinatoPrpName = "DataOrdinato";
        private System.DateTime? _dataOrdinato;
        public System.DateTime? DataOrdinato
        {
            get
            {
                return _dataOrdinato;
            }
            set
            {
                _dataOrdinato = value;
                RaisePropertyChanged(_dataOrdinatoPrpName);
            }
        }

        #endregion

        #region DataArrivato
        private const string _dataArrivatoPrpName = "DataArrivato";
        private System.DateTime? _dataArrivato;
        public System.DateTime? DataArrivato
        {
            get
            {
                return _dataArrivato;
            }
            set
            {
                _dataArrivato = value;
                RaisePropertyChanged(_dataArrivatoPrpName);
            }
        }

        #endregion

        #region DataRitirato
        private const string _dataRitiratoPrpName = "DataRitirato";
        private System.DateTime? _dataRitirato;
        public System.DateTime? DataRitirato
        {
            get
            {
                return _dataRitirato;
            }
            set
            {
                _dataRitirato = value;
                RaisePropertyChanged(_dataRitiratoPrpName);
            }
        }

        #endregion

        /*
        public Nullable<decimal> PrezzoAcquisto { get; set; }
        public string PrezzoVendita { get; set; }
        public Nullable<ModalitaAvviso> ModalitàAvviso { get; set; }
        public Nullable<int> IdMarca { get; set; }
        public Nullable<int> IdFornitore { get; set; }
         */

        


        public RelayCommand AnnullaCommand { get; private set; }
        public RelayCommand ConfermaCommand { get; private set; }

        public DetailViewModel()
        {
            AnnullaCommand = new RelayCommand(Annulla);
            ConfermaCommand = new RelayCommand(Conferma, () => CanConferma);
        }

        private void Annulla()
        {
            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.CancelKey);
        }

        private bool CanConferma
        {
            get
            {
                return true;
            }
        }

        private void Conferma()
        {
            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.ConfirmKey);
        }

        internal Model.RichiesteOrdine CreateRigaOrdine()
        {
            return new RichiesteOrdine()
                   {
                       Id = _id,
                       Cliente = Cliente,
                       Cellulare = Cellulare,
                       Telefono = Telefono,
                       EMail = EMail,
                       Indirizzo = Indirizzo,
                       Avvisato = Avvisato,
                       DataArrivato = DataArrivato,
                       DataAvvisato = DataAvvisato,
                       DataOrdinato = DataOrdinato,
                       DataRitirato = DataRitirato
                   };
        }

        internal void Setup(RichiesteOrdine richiesteOrdine)
        {
            if (richiesteOrdine == null)
            {
                SetupDefaultValues();
            }
            else
            {
                SetupValuesFromRichiestaOrdine(richiesteOrdine);
            }
        }

        private void SetupDefaultValues()
        {
            _id = -1;
            this.Cliente = string.Empty;
            this.Telefono = string.Empty;
            this.EMail = string.Empty;
            this.Indirizzo = string.Empty;
            this.Cellulare = string.Empty;
            this.Codice = string.Empty;
            this.Avvisato = false;
            this.DataArrivato = null;
            this.DataOrdinato = null;
            this.DataAvvisato= null;
            this.DataRitirato = null;
        }

        private void SetupValuesFromRichiestaOrdine(RichiesteOrdine richiestaOrdine)
        {
            _id = richiestaOrdine.Id;
            this.Cliente = richiestaOrdine.Cliente;
            this.Telefono = richiestaOrdine.Telefono;
            this.EMail = richiestaOrdine.EMail;
            this.Indirizzo = richiestaOrdine.Indirizzo;
            this.Cellulare  = richiestaOrdine.Cellulare;
            this.Avvisato = richiestaOrdine.Avvisato;
            this.Codice = richiestaOrdine.Codice;
            this.DataArrivato = richiestaOrdine.DataArrivato;
            this.DataOrdinato = richiestaOrdine.DataOrdinato;
            this.DataAvvisato = richiestaOrdine.DataAvvisato;
            this.DataRitirato = richiestaOrdine.DataRitirato;
        }
    
    }
}
