using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdiniCatCe.Gui.ViewModel
{
    public class AddAnagraficaViewModel : ViewModelBase
    {
        private AnagraficaElementType _type;
        public RelayCommand AnnullaCommand { get; private set; }
        public RelayCommand ConfermaCommand { get; private set; }

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


        public AddAnagraficaViewModel()
        {
            AnnullaCommand = new RelayCommand(Annulla);
            ConfermaCommand = new RelayCommand(Conferma, () => CanConferma);
        }

        public void Setup(AnagraficaElementType element)
        {
            _type = element;
            _name = string.Empty;
        }

        private void Annulla()
        {
            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.CancelAnagraficaKey);
        }

        private bool CanConferma
        {
            get
            {
                return !string.IsNullOrEmpty(Name);
            }
        }

        private void Conferma()
        {
            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.ConfirmAnagraficaKey);
        }
    }
}
