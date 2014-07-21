using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AddFornitoreViewModel : AddAnagraficaItemViewModelBase
    {
        private const string _namePrpName = "Name";
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged(_namePrpName);
            }
        }

        private const string _emailPrpName = "Email";
        private string _email;

        public string Email
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


        public override void Setup()
        {
            this.Name = string.Empty;
            this.Email = string.Empty;
            this.Telefono = string.Empty;
        }

        public override void Annulla()
        {
            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.CancelAddFornitoreKey);
        }

        public override bool CanConferma
        {
            get
            {
                return !string.IsNullOrEmpty(Name);
            }
        }

        public override void Conferma()
        {
            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.ConfirmAddFornitoreKey);
        }


        internal Fornitori GetFornitore()
        {
            return new Fornitori
                   {
                       Email = Email,
                       Telefono = Telefono,
                       Name = Name
                   };
        }
    }
}
