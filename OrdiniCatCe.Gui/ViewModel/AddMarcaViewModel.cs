using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Constants;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AddMarcaViewModel : DialogViewModelBase
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

        public override void Setup()
        {
            this.Name = string.Empty;
        }

        public override void Annulla()
        {
            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.CancelAddMarcaKey);
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
            string errorMessage;
            if (!DbManager.AddMarca(new Marche {Nome = Name}, out errorMessage))
            {
                Messenger.Default.Send(new ErrorMessageMessage(errorMessage), MsgKeys.ShowErrorMessageOnAddMarcaKey);
            }
            else//se tutto va bene mando messaggio di Conferma
            {
                Messenger.Default.Send(new MessageBase(), MsgKeys.ConfirmAddMarcaKey);
            }
        }

    }
}
