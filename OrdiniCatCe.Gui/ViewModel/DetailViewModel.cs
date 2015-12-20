using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OrdiniCatCe.Gui.Constants;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.View;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;
using OrdiniCatCe.Gui.Utils;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class DetailViewModel : ViewModelBase
    {
        private const string _errorMessagePrpName = "ErrorMessage";
        private string _errorMessage;
        private int _id;

        public DetailViewModel()
        {
            AnnullaCommand = new RelayCommand(Annulla);
            ConfermaCommand = new RelayCommand(Conferma, () => CanConferma);
            CreaFornitoreCommand = new RelayCommand(CreaFornitore);
            CreaMarcaCommand = new RelayCommand(CreaMarca);
            AddPezzoCommand = new RelayCommand(AddPezzo);
            UpdatePezzoCommand = new RelayCommand<PezziInOrdine>(UpdatePezzo);
            RemovePezzoCommand = new RelayCommand<PezziInOrdine>(RemovePezzo);
            StoricizzaCommand = new RelayCommand(Storicizza);

            //todo: trasformarlo in message per inizializzare Marche
            Init();

            Messenger.Default.Register<AddMarcaMessage>(this, MsgKeys.MarcaAddedKey, OnMarcaAdded);
            Messenger.Default.Register<AddFornitoreMessage>(this, MsgKeys.FornitoreAddedKey, OnFornitoreAdded);

            Pezzi = new ObservableCollection<PezziInOrdine>();
        }

        public ObservableCollection<PezziInOrdine> Pezzi { get; set; }

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
        public RelayCommand StoricizzaCommand { get; private set; }

        //nuovi comandi per creazione Marca e Fornitore
        public RelayCommand CreaMarcaCommand { get; private set; }
        public RelayCommand CreaFornitoreCommand { get; private set; }

        public RelayCommand AddPezzoCommand { get; private set; }

        public RelayCommand<PezziInOrdine> UpdatePezzoCommand { get; private set; }

        public RelayCommand<PezziInOrdine> RemovePezzoCommand { get; private set; }

        private bool CanConferma
        {
            get
            {
                return !string.IsNullOrEmpty(Nome) &&
                       Pezzi.Count > 0;
            }
        }

        #region Conferma Button Content
        private const string _confermaButtonContentOnNewRichiesta = "Inserisci";
        private const string _confermaButtonContentOnUpdateRichiesta = "Modifica";
        private string _confermaButtonContent;
        private const string _confermaButtonKey = "ConfermaButtonContent";
        public string ConfermaButtonContent
        {
            get
            {
                return _confermaButtonContent;
            }
            set
            {
                _confermaButtonContent = value;
                RaisePropertyChanged(_confermaButtonKey);
            }
        }
        #endregion

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

        #region Note1
        private const string _note1PrpName = "Note1";
        private string _note1;

        public string Note1
        {
            get
            {
                return _note1;
            }
            set
            {
                _note1 = value;
                RaisePropertyChanged(_note1PrpName);
            }
        }
        #endregion

        #region Note2
        private const string _note2PrpName = "Note2";
        private string _note2;

        public string Note2
        {
            get
            {
                return _note2;
            }
            set
            {
                _note2 = value;
                RaisePropertyChanged(_note2PrpName);
            }
        }
        #endregion

        #region Note3
        private const string _note3PrpName = "Note3";
        private string _note3;

        public string Note3
        {
            get
            {
                return _note3;
            }
            set
            {
                _note3 = value;
                RaisePropertyChanged(_note3PrpName);
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

        #region DataRichiesta
        private const string _dataRichiestaPrpName = "DataRichiesta";
        private DateTime? _dataRichiesta;

        public DateTime? DataRichiesta
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
        private bool _updateModalitaAvvisoComponents = true;
        private const string _modalitaAvvisoPrpName = "ModalitaAvviso";
        private Int32 _modalitaAvviso;

        public Int32 ModalitaAvviso
        {
            get
            {
                return _modalitaAvviso;
            }
            set
            {
                _modalitaAvviso = value;

                if (_updateModalitaAvvisoComponents)
                {
                    UpdateComponentsFromModalitaAvviso();
                }

                RaisePropertyChanged(_modalitaAvvisoPrpName);
            }
        }
        #endregion

        #region AvvisatoTelefono
        private const string _avvisatoTelefonoPrpName = "AvvisatoTelefono";
        private bool _avvisatoTelefono;

        public bool AvvisatoTelefono
        {
            get
            {
                return _avvisatoTelefono;
            }
            set
            {
                _avvisatoTelefono = value;
                UpdateModalitaAvviso();
                RaisePropertyChanged(_avvisatoTelefonoPrpName);
            }
        } 
        #endregion

        #region AvvisatoCellulare
        private const string _avvisatoCellularePrpName = "AvvisatoCellulare";
        private bool _avvisatoCellulare;

        public bool AvvisatoCellulare
        {
            get
            {
                return _avvisatoCellulare;
            }
            set
            {
                _avvisatoCellulare = value;
                UpdateModalitaAvviso();
                RaisePropertyChanged(_avvisatoCellularePrpName);
            }
        } 
        #endregion

        #region AvvisatoEMail
        private const string _avvisatoEMailPrpName = "AvvisatoEMail";
        private bool _avvisatoEMail;

        public bool AvvisatoEMail
        {
            get
            {
                return _avvisatoEMail;
            }
            set
            {
                _avvisatoEMail = value;
                UpdateModalitaAvviso();
                RaisePropertyChanged(_avvisatoEMailPrpName);
            }
        } 
        #endregion

        #region AvvisatoSegreteria
        private const string _avvisatoSegreteriaPrpName = "AvvisatoSegreteria";
        private bool _avvisatoSegreteria;

        public bool AvvisatoSegreteria
        {
            get
            {
                return _avvisatoSegreteria;
            }
            set
            {
                _avvisatoSegreteria = value;
                UpdateModalitaAvviso();
                RaisePropertyChanged(_avvisatoSegreteriaPrpName);
            }
        }
        #endregion

        private void UpdateComponentsFromModalitaAvviso()
        {
            Utilities.ParseModalitaAvviso(ModalitaAvviso, out _avvisatoTelefono, out _avvisatoCellulare, out _avvisatoEMail, out _avvisatoSegreteria);
            RaisePropertyChanged(_avvisatoCellularePrpName);
            RaisePropertyChanged(_avvisatoEMailPrpName);
            RaisePropertyChanged(_avvisatoTelefonoPrpName);
            RaisePropertyChanged(_avvisatoSegreteriaPrpName);
        }

        private void UpdateModalitaAvviso()
        {
            int modalitaAvviso = 0;

            if (_avvisatoTelefono)
            {
                modalitaAvviso += (int)OrdiniCatCe.Gui.Model.ModalitaAvviso.Telefono;
            }
            if (_avvisatoCellulare)
            {
                modalitaAvviso += (int)OrdiniCatCe.Gui.Model.ModalitaAvviso.Cellulare;
            }
            if (_avvisatoEMail)
            {
                modalitaAvviso += (int)OrdiniCatCe.Gui.Model.ModalitaAvviso.EMail;
            }
            if (_avvisatoSegreteria)
            {
                modalitaAvviso += (int)OrdiniCatCe.Gui.Model.ModalitaAvviso.Segreteria;
            }

            try
            {
                _updateModalitaAvvisoComponents = false;
                ModalitaAvviso = modalitaAvviso;
            }
            finally
            {
                _updateModalitaAvvisoComponents = true;
            }
        }




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
                else
                {
                    DataCaparra = DateTime.Now;
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
        private DateTime? _dataCaparra;

        public DateTime? DataCaparra
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

        #region Storicizzato
        private const string _storicizzatoPrpName = "Storicizzato";
        private bool _storicizzato;

        public bool Storicizzato
        {
            get
            {
                return _storicizzato;
            }
            set
            {
                _storicizzato = value;
                RaisePropertyChanged(_storicizzatoPrpName);
            }
        }
        #endregion

        #region IsNew
        private const string _isNewPrpName = "IsNew";
        private bool _isNew;

        public bool IsNew
        {
            get
            {
                return _isNew;
            }
            set
            {
                _isNew = value;
                RaisePropertyChanged(_isNewPrpName);
            }
        }
        #endregion

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

        private void Storicizza()
        {
            //richiedo se l'utente è sicuro.
            MessageBoxResult result = MessageBox.Show(null, Texts.ConfermaStoricizzazione, AppConstants.ApplicationName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Storicizzato = true;
                    Messenger.Default.Send(new MessageBase(), MsgKeys.StoricizzaKey);
                    break;
            }
        }

        private void UpdatePezzo(PezziInOrdine pezzo)
        {
            //
            ServiceLocator.Current.GetInstance<AddPezzoInOrdineViewModel>().Setup(pezzo);
            AddPezzoInOrdineWindow wnd = new AddPezzoInOrdineWindow();

            wnd.ShowDialog();

            if (wnd.MyDialogResult)
            {
                RefreshPezzi();
            }
        }

        private void RemovePezzo(PezziInOrdine pezzo)
        {
            //richiedo se l'utente è sicuro.
            MessageBoxResult result = MessageBox.Show(null, Texts.ConfermaCancellazionePezzo, AppConstants.ApplicationName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    break;
                default:
                    return;
            }

            //se l'utente ha confermato la rimozione, cancello il pezzo.
            DbManager.RemovePezzo(pezzo);
            RefreshPezzi();
        }


        private void RefreshPezzi()
        {
            Pezzi.Clear();

            foreach (PezziInOrdine p in DbManager.GetPezziByIdRichiesta(_id))
            {
                Pezzi.Add(p);
            }
        }

        private void AddPezzo()
        {
            //apro maschera di inserimento pezzo.
            if (_id < 0)
            {
                return;
            }

            ServiceLocator.Current.GetInstance<AddPezzoInOrdineViewModel>().Setup(_id);
            AddPezzoInOrdineWindow wnd = new AddPezzoInOrdineWindow();

            wnd.ShowDialog();

            if (wnd.MyDialogResult)
            {
                //devo aggiornare la lista di pezzi.
                Pezzi.Add(wnd.AddedPezzo);
            }
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

            if (Pezzi.Count <= 0)
            {
                errorMessage = "Inserire almeno un pezzo da ordinare";
                return false;
            }

            return true;
        }

        internal RichiesteOrdine CreateRigaOrdine()
        {
            //la riga è già stata creata
            return new RichiesteOrdine
                   {
                       Id = _id,
                       Nome = Nome,
                       Cellulare = Cellulare,
                       Telefono = Telefono,
                       EMail = EMail,
                       Indirizzo = Indirizzo,
                       Note1 = Note1,
                       Note2 = Note2,
                       Note3 = Note3,
                       DataRichiesta = DataRichiesta,
                       Localita = Localita,
                       NumeroCivico = NumeroCivico,
                       ModalitàAvviso = ModalitaAvviso,
                       DataCaparra = DataCaparra,
                       Caparra = Caparra,
                       RicevutaCaparra = RicevutoCaparra,
                       Storicizzata = Storicizzato
                   };
        }

        internal void Setup(RichiesteOrdine richiesteOrdine, bool isnew)
        {
            IsNew = isnew;
            if (richiesteOrdine == null)
            {
                SetupDefaultValues();
            }
            else
            {
                SetupValuesFromRichiestaOrdine(richiesteOrdine);
            }

            if (IsNew)
            {
                ConfermaButtonContent = _confermaButtonContentOnNewRichiesta;
            }
            else
            {
                ConfermaButtonContent = _confermaButtonContentOnUpdateRichiesta;
            }

            ErrorMessage = string.Empty;
        }

        private void SetupDefaultValues()
        {
            _id = -1;
            Nome = string.Empty;
            Telefono = string.Empty;
            EMail = string.Empty;
            Indirizzo = string.Empty;
            Localita = string.Empty;
            NumeroCivico = string.Empty;
            Cellulare = string.Empty;
            DataRichiesta = DateTime.Today;
            ModalitaAvviso = 0;
            DataCaparra = DateTime.Today;
            Caparra = null;
            RicevutoCaparra = false;
            Storicizzato = false;

            Pezzi.Clear();
        }

        private void SetupValuesFromRichiestaOrdine(RichiesteOrdine richiestaOrdine)
        {
            _id = richiestaOrdine.Id;
            Nome = richiestaOrdine.Nome;
            Telefono = richiestaOrdine.Telefono;
            EMail = richiestaOrdine.EMail;
            Indirizzo = richiestaOrdine.Indirizzo;
            Note1 = richiestaOrdine.Note1;
            Note2 = richiestaOrdine.Note2;
            Note3 = richiestaOrdine.Note3;
            Localita = richiestaOrdine.Localita;
            NumeroCivico = richiestaOrdine.NumeroCivico;
            Cellulare = richiestaOrdine.Cellulare;
            DataRichiesta = richiestaOrdine.DataRichiesta;
            ModalitaAvviso = richiestaOrdine.ModalitàAvviso;
            DataCaparra = richiestaOrdine.DataCaparra;
            Caparra = richiestaOrdine.Caparra;
            RicevutoCaparra = richiestaOrdine.RicevutaCaparra;
            Storicizzato = richiestaOrdine.Storicizzata;
            Pezzi.Clear();
            foreach (var p in richiestaOrdine.PezziInOrdine)
            {
                Pezzi.Add(p);
            }
        }
    }
}