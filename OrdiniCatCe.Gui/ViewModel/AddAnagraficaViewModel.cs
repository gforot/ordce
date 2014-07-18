using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;

namespace OrdiniCatCe.Gui.ViewModel
{
    public class AddAnagraficaViewModel : ViewModelBase
    {
        private const string _windowTitleFormat = "Aggiungi {0}";
        private const string _marcheInTitle = "Marche";
        private const string _fornitoreInTitle = "Fornitore";

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

        private const string _windowTitlePrpName = "WindowTitle";
        private string _windowTitle;
        public string WindowTitle
        {
            get
            {
                return _windowTitle;
            }
            set
            {
                _windowTitle = value;
                RaisePropertyChanged(_windowTitlePrpName);
            }
        }

        private const string _elementNamePrpName = "ElementName";
        private string _elementName;
        public string ElementName
        {
            get
            {
                return _elementName;
            }
            set
            {
                _elementName = value;
                RaisePropertyChanged(_elementNamePrpName);
            }
        }


        public AddAnagraficaViewModel()
        {
            AnnullaCommand = new RelayCommand(Annulla);
            ConfermaCommand = new RelayCommand(Conferma, () => CanConferma);
            _windowTitle = "Add Anagrafica";
            _elementName = "Nome";
        }

        public void Setup(AnagraficaElementType element)
        {
            _type = element;
            _name = string.Empty;
            SetWindowTitle();
            SetElementName();
        }

        private void SetWindowTitle()
        {
            WindowTitle = string.Format(_windowTitleFormat, 
                _type == AnagraficaElementType.Fornitore ? _fornitoreInTitle : _marcheInTitle);
        }

        private void SetElementName()
        {
            ElementName = _type == AnagraficaElementType.Fornitore ? _fornitoreInTitle
                              : _marcheInTitle;
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
