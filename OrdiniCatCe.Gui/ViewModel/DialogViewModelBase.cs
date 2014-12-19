using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace OrdiniCatCe.Gui.ViewModel
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        public abstract void Setup();
        public abstract void Conferma();
        public abstract void Annulla();
        public abstract bool CanConferma { get; }
        public RelayCommand AnnullaCommand { get; private set; }
        public RelayCommand ConfermaCommand { get; private set; }

        protected DialogViewModelBase()
        {
            AnnullaCommand = new RelayCommand(Annulla);
            ConfermaCommand = new RelayCommand(Conferma, () => CanConferma);
        }
    }
}
