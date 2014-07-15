using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class DetailViewModel : ViewModelBase
    {
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
    }
}
