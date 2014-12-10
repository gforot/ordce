﻿using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;

namespace OrdiniCatCe.Gui.ViewModel
{
    public class NewOrderViewModel : ViewModelBase
    {
        public RelayCommand AnnullaCommand { get; private set; }
        public RelayCommand ConfermaCommand { get; private set; }

        public NewOrderViewModel()
        {
            AnnullaCommand = new RelayCommand(Annulla);
            ConfermaCommand = new RelayCommand(Conferma, () => CanConferma);
        }

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

        private void Annulla()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.NewOrderWindowCancelKey);
        }

        private bool CanConferma
        {
            get
            {
                return !string.IsNullOrEmpty(this.Nome) &&
                       !string.IsNullOrEmpty(this.Cognome);
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

            RichiesteOrdine ro = new RichiesteOrdine();
            ro.Nome = Nome;
            ro.Cognome = Cognome;
            ro.Cellulare = Cellulare;
            ro.EMail = EMail;
            ro.Indirizzo = Indirizzo;
            ro.Telefono = Telefono;
            ro.DataRichiesta = DateTime.Now;
            DbManager.AddRigaOrdine(ro);
            //riga aggiunta nel DB
            //ora bisogna aprire il dettaglio ordine per l'inserimento dei pezzi.

            Messenger.Default.Send(new AddRigaOrdineMessage(ro), MsgKeys.NewOrderWindowConfirmKey);
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


            //if (Arrivato && DataArrivato == null)
            //{
            //    errorMessage = "Pezzo arrivato ma data di arrivo non impostata";
            //    return false;
            //}

            //if (!Arrivato && DataArrivato != null)
            //{
            //    errorMessage = "Pezzo non arrivato ma data di arrivo impostata";
            //    return false;
            //}

            //if (Ritirato && DataRitirato == null)
            //{
            //    errorMessage = "Pezzo ritirato ma data di ritiro non impostata";
            //    return false;
            //}

            //if (!Ritirato && DataRitirato != null)
            //{
            //    errorMessage = "Pezzo non ritirato ma data di ritiro impostata";
            //    return false;
            //}

            //if (Ordinato && DataOrdinato == null)
            //{
            //    errorMessage = "Pezzo ordinato ma data di ordine non impostata";
            //    return false;
            //}

            //if (!Ordinato && DataOrdinato != null)
            //{
            //    errorMessage = "Pezzo non ordinato ma data di ordine impostata";
            //    return false;
            //}

            return true;
        }

        public void Setup()
        {
            Nome = string.Empty;
            Cognome = string.Empty;
            EMail = string.Empty;
            Telefono = string.Empty;
            Cellulare = string.Empty;
        }
    }
}