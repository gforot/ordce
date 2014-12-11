using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media.TextFormatting;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.Validation;
using OrdiniCatCe.Gui.View;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class DetailViewModel : ViewModelBase
    {
        private int _id;

        public ObservableCollection<PezziInOrdine> Pezzi { get; set; }

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

        #region DataRichiesta
        private const string _dataRichiestaPrpName = "DataRichiesta";
        private System.DateTime? _dataRichiesta;
        public System.DateTime? DataRichiesta
        {
            get
            {
                return _dataRichiesta;
            }
            set
            {
                _dataRichiesta = value;
                RaisePropertyChanged(_dataRichiestaPrpName);
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

        #region RicevutoAcconto
        private const string _ricevutoCaparraPrpName = "RicevutoCaparra";
        private bool _ricevutoCaparra;
        public bool RicevutoCaparra
        {
            get
            {
                return _ricevutoCaparra;
            }
            set
            {
                _ricevutoCaparra = value;

                //se flaggo NON RICEVUTO ACCONTO annullo i campi annessi.
                if (!_ricevutoCaparra)
                {
                    Caparra = null;
                    DataCaparra = null;
                }

                RaisePropertyChanged(_ricevutoCaparraPrpName);
            }
        }
        #endregion

        #region Caparra
        private const string _caparraPrpName = "Caparra";
        private decimal? _caparra;
        public decimal? Caparra
        {
            get
            {
                return _caparra;
            }
            set
            {
                _caparra = value;
                RaisePropertyChanged(_caparraPrpName);
            }
        }
        #endregion

        #region DataCaparra
        private const string _dataCaparraPrpName = "DataCaparra";
        private System.DateTime? _dataCaparra;
        public System.DateTime? DataCaparra
        {
            get
            {
                return _dataCaparra;
            }
            set
            {
                _dataCaparra = value;
                RaisePropertyChanged(_dataCaparraPrpName);
            }
        }

        #endregion


        private const string _errorMessagePrpName = "ErrorMessage";
        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(_errorMessagePrpName);
            }
        }

        public ObservableCollection<Marche> Marche { get; private set; }
        public ObservableCollection<Fornitori> Fornitori { get; private set; }

        public RelayCommand AnnullaCommand { get; private set; }
        public RelayCommand ConfermaCommand { get; private set; }

        //nuovi comandi per creazione Marca e Fornitore
        public RelayCommand CreaMarcaCommand { get; private set; }
        public RelayCommand CreaFornitoreCommand { get; private set; }

        public RelayCommand AddPezzoCommand { get; private set; }

        public DetailViewModel()
        {
            AnnullaCommand = new RelayCommand(Annulla);
            ConfermaCommand = new RelayCommand(Conferma, () => CanConferma);
            CreaFornitoreCommand = new RelayCommand(CreaFornitore);
            CreaMarcaCommand = new RelayCommand(CreaMarca);
            AddPezzoCommand = new RelayCommand(AddPezzo);

            //todo: trasformarlo in message per inizializzare Marche
            Init();

            Messenger.Default.Register<AddMarcaMessage>(this, MsgKeys.MarcaAddedKey, OnMarcaAdded);
            Messenger.Default.Register<AddFornitoreMessage>(this, MsgKeys.FornitoreAddedKey, OnFornitoreAdded);
        }

        private void Init()
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                Marche = new ObservableCollection<Marche>(db.Marche.ToList());
                Fornitori = new ObservableCollection<Fornitori>(db.Fornitori.ToList());
            }
        }

        private void OnMarcaAdded(AddMarcaMessage message)
        {
            Marche.Add(message.Marca);
        }

        private void OnFornitoreAdded(AddFornitoreMessage message)
        {
            Fornitori.Add(message.Fornitore);
        }

        private void Annulla()
        {
            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.CancelKey);
        }

        private bool CanConferma
        {
            get
            {
                return !string.IsNullOrEmpty(this.Nome) &&
                    !string.IsNullOrEmpty(this.Cognome) &&
                    Pezzi.Count>0;
            }
        }

        private void AddPezzo()
        {
            PezziInOrdine daAggiungere = new PezziInOrdine();
            daAggiungere.Arrivato = false;
            daAggiungere.Mancante = false;
            daAggiungere.IdRichiestaOrdine = _id;
            daAggiungere.Description = "Nuovo pezzo";
            DbManager.AddPezzo(daAggiungere);
        }

        private void Conferma()
        {
            string errorMessage;
            if (!Check(out errorMessage))
            {
                ErrorMessage = errorMessage;
                return;
            }


            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.ConfirmKey);
        }

        private void CreaFornitore()
        {
            ServiceLocator.Current.GetInstance<AddFornitoreViewModel>().Setup();
            AddFornitoreWindow wnd = new AddFornitoreWindow();
            wnd.ShowDialog();
        }

        private void CreaMarca()
        {
            ServiceLocator.Current.GetInstance<AddMarcaViewModel>().Setup();
            AddMarcaWindow wnd = new AddMarcaWindow();
            wnd.ShowDialog();
        }

        private bool Check(out string errorMessage)
        {
            errorMessage = null;

            if (!Checks.CheckEmail(EMail))
            {
                errorMessage = "Il campo EMail non è corretto";
                return false;
            }

            if ((string.IsNullOrEmpty(Telefono)) &&
                (string.IsNullOrEmpty(Cellulare)) &&
                (string.IsNullOrEmpty(EMail)))
            {
                errorMessage = "Inserire almeno uno fra telefono, email e cellulare";
                return false;
            }


            if (Arrivato && DataArrivato == null)
            {
                errorMessage = "Pezzo arrivato ma data di arrivo non impostata";
                return false;
            }

            if (!Arrivato && DataArrivato != null)
            {
                errorMessage = "Pezzo non arrivato ma data di arrivo impostata";
                return false;
            }

            if (Ritirato && DataRitirato == null)
            {
                errorMessage = "Pezzo ritirato ma data di ritiro non impostata";
                return false;
            }

            if (!Ritirato && DataRitirato != null)
            {
                errorMessage = "Pezzo non ritirato ma data di ritiro impostata";
                return false;
            }

            if (Ordinato && DataOrdinato == null)
            {
                errorMessage = "Pezzo ordinato ma data di ordine non impostata";
                return false;
            }

            if (!Ordinato && DataOrdinato != null)
            {
                errorMessage = "Pezzo non ordinato ma data di ordine impostata";
                return false;
            }

            if (this.Pezzi.Count <= 0)
            {
                errorMessage = "Inserire almeno un pezzo da ordinare";
                return false;
            }

            return true;
        }



        internal Model.RichiesteOrdine CreateRigaOrdine()
        {
            //la riga è già stata creata
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
                       DataRichiesta = DataRichiesta,
                       Localita = Localita,
                       NumeroCivico = NumeroCivico,
                       ModalitàAvviso = ModalitaAvviso,
                       Ritirato = Ritirato,
                       Arrivato = Arrivato,
                       Ordinato = Ordinato,
                       DataCaparra = DataCaparra,
                       Caparra = Caparra,
                       RicevutaCaparra = RicevutoCaparra
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

            ErrorMessage = string.Empty;
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
            this.Avvisato = false;
            this.DataArrivato = null;
            this.DataOrdinato = null;
            this.DataRichiesta = DateTime.Now;
            this.DataAvvisato= null;
            this.DataRitirato = null;
            this.ModalitaAvviso = ModalitaAvviso.Undefined;
            this.Ritirato = false;
            this.Ordinato = false;
            this.Arrivato = false;
            this.DataCaparra = null;
            this.Caparra = null;
            this.RicevutoCaparra = false;
            this.Pezzi = new ObservableCollection<PezziInOrdine>();
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
            this.DataArrivato = richiestaOrdine.DataArrivato;
            this.DataOrdinato = richiestaOrdine.DataOrdinato;
            this.DataAvvisato = richiestaOrdine.DataAvvisato;
            this.DataRichiesta = richiestaOrdine.DataRichiesta;
            this.DataRitirato = richiestaOrdine.DataRitirato;
            this.ModalitaAvviso = richiestaOrdine.ModalitàAvviso;
            this.Ritirato = richiestaOrdine.Ritirato;
            this.Ordinato = richiestaOrdine.Ordinato;
            this.Arrivato = richiestaOrdine.Arrivato;
            this.DataCaparra = richiestaOrdine.DataCaparra;
            this.Caparra = richiestaOrdine.Caparra;
            this.RicevutoCaparra = richiestaOrdine.RicevutaCaparra;

            this.Pezzi = new ObservableCollection<PezziInOrdine>(richiestaOrdine.PezziInOrdine);
        }
    
    }
}
