using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Windows.Documents;
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

        #region Nome
        private const string _nomePrpName = "Nome";
        private string _nome;
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
                RaisePropertyChanged(_nomePrpName);
            }
        }

        #endregion

        #region Cognome
        private const string _cognomePrpName = "Cognome";
        private string _cognome;
        public string Cognome
        {
            get
            {
                return _cognome;
            }
            set
            {
                _cognome = value;
                RaisePropertyChanged(_cognomePrpName);
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

        #region Localita
        private const string _localitaPrpName = "Localita";
        private string _localita;
        public string Localita
        {
            get
            {
                return _localita;
            }
            set
            {
                _localita = value;
                RaisePropertyChanged(_localitaPrpName);
            }
        }

        #endregion

        #region NumeroCivico
        private const string _numeroCivicoPrpName = "NumeroCivico";
        private string _numeroCivico;
        public string NumeroCivico
        {
            get
            {
                return _numeroCivico;
            }
            set
            {
                _numeroCivico = value;
                RaisePropertyChanged(_numeroCivicoPrpName);
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

        #region PrezzoAcquisto
        private const string _prezzoAcquistoPrpName = "PrezzoAcquisto";
        private decimal? _prezzoAcquisto;
        public decimal? PrezzoAcquisto
        {
            get
            {
                return _prezzoAcquisto;
            }
            set
            {
                _prezzoAcquisto = value;
                RaisePropertyChanged(_prezzoAcquistoPrpName);
            }
        }

        #endregion

        #region PrezzoVendita
        private const string _prezzoVenditaPrpName = "PrezzoVendita";
        private decimal? _prezzoVendita;
        public decimal? PrezzoVendita
        {
            get
            {
                return _prezzoVendita;
            }
            set
            {
                _prezzoVendita = value;
                RaisePropertyChanged(_prezzoVenditaPrpName);
            }
        }

        #endregion

        #region ModalitaAvviso
        private const string _modalitaAvvisoPrpName = "ModalitaAvviso";
        private ModalitaAvviso _modalitaAvviso;
        public ModalitaAvviso ModalitaAvviso
        {
            get
            {
                return _modalitaAvviso;
            }
            set
            {
                _modalitaAvviso = value;
                RaisePropertyChanged(_modalitaAvvisoPrpName);
            }
        }

        #endregion

        #region IdMarca
        private const string _idMarcaPrpName = "IdMarca";
        private int _idMarca;
        public int IdMarca
        {
            get
            {
                return _idMarca;
            }
            set
            {
                _idMarca = value;
                RaisePropertyChanged(_idMarcaPrpName);
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



        public List<Marche> Marche { get; private set; }

        /*
        public Nullable<int> IdMarca { get; set; }
        public Nullable<int> IdFornitore { get; set; }
         */

        


        public RelayCommand AnnullaCommand { get; private set; }
        public RelayCommand ConfermaCommand { get; private set; }

        public DetailViewModel()
        {
            AnnullaCommand = new RelayCommand(Annulla);
            ConfermaCommand = new RelayCommand(Conferma, () => CanConferma);

            //todo: trasformarlo in message per inizializzare Marche
            OnInitRequested(null);

            Messenger.Default.Register<MessageBase>(this, MsgKeys.InitKey, OnInitRequested);
        }

        private void OnInitRequested(MessageBase message)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                Marche = db.Marche.ToList();
            }
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
                       Cognome = Cognome,
                       Nome = Nome,
                       Cellulare = Cellulare,
                       Telefono = Telefono,
                       EMail = EMail,
                       Indirizzo = Indirizzo,
                       Avvisato = Avvisato,
                       DataArrivato = DataArrivato,
                       DataAvvisato = DataAvvisato,
                       DataOrdinato = DataOrdinato,
                       DataRitirato = DataRitirato,
                       Localita = Localita,
                       NumeroCivico = NumeroCivico,
                       ModalitàAvviso = ModalitaAvviso,
                       PrezzoAcquisto = PrezzoAcquisto,
                       PrezzoVendita = PrezzoVendita,
                       Codice = Codice,
                       Descrizione = Descrizione,
                       Ritirato = Ritirato,
                       IdMarca = Marca.Id
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
            this.Nome = string.Empty;
            this.Cognome = string.Empty;
            this.Telefono = string.Empty;
            this.EMail = string.Empty;
            this.Indirizzo = string.Empty;
            this.Localita = string.Empty;
            this.NumeroCivico = string.Empty;
            this.Cellulare = string.Empty;
            this.Codice = string.Empty;
            this.Avvisato = false;
            this.DataArrivato = null;
            this.DataOrdinato = null;
            this.DataAvvisato= null;
            this.DataRitirato = null;
            this.ModalitaAvviso = ModalitaAvviso.Undefined;
            this.Ritirato = false;
            this.Marca = null;
        }

        private void SetupValuesFromRichiestaOrdine(RichiesteOrdine richiestaOrdine)
        {
            _id = richiestaOrdine.Id;
            this.Cognome = richiestaOrdine.Cognome;
            this.Nome = richiestaOrdine.Nome;
            this.Telefono = richiestaOrdine.Telefono;
            this.EMail = richiestaOrdine.EMail;
            this.Indirizzo = richiestaOrdine.Indirizzo;
            this.Localita = richiestaOrdine.Localita;
            this.NumeroCivico = richiestaOrdine.NumeroCivico;
            this.Cellulare  = richiestaOrdine.Cellulare;
            this.Avvisato = richiestaOrdine.Avvisato;
            this.Codice = richiestaOrdine.Codice;
            this.DataArrivato = richiestaOrdine.DataArrivato;
            this.DataOrdinato = richiestaOrdine.DataOrdinato;
            this.DataAvvisato = richiestaOrdine.DataAvvisato;
            this.DataRitirato = richiestaOrdine.DataRitirato;
            this.ModalitaAvviso = richiestaOrdine.ModalitàAvviso;
            this.PrezzoAcquisto = richiestaOrdine.PrezzoAcquisto;
            this.PrezzoVendita = richiestaOrdine.PrezzoVendita;
            this.Ritirato = richiestaOrdine.Ritirato;



            this.Marca = this.Marche.FirstOrDefault(m => richiestaOrdine.Marche.Id == m.Id);
        }
    
    }
}
