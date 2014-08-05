
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AnagraficaFornitoreWiewModel : ViewModelBase
    {
        private ICollectionView _view;

        public FornitoriCollection Fornitori { get; set; }

        public RelayCommand PreviousCommand { get; private set; }
        public RelayCommand NextCommand { get; private set; }

        public Fornitori CurrentFornitore
        {
            get
            {
                return _view.CurrentItem as Fornitori;
            }
        }

        public string Name
        {
            get
            {
                return CurrentFornitore.Name;
            }
            set
            {
                CurrentFornitore.Name = value;
            }
        }

        public string Telefono
        {
            get
            {
                return CurrentFornitore.Telefono;
            }
            set
            {
                CurrentFornitore.Telefono = value;
            }
        }

        public string EMail
        {
            get
            {
                return CurrentFornitore.Email;
            }
            set
            {
                CurrentFornitore.Email = value;
            }
        }

        public AnagraficaFornitoreWiewModel(IEnumerable<Fornitori> fornitori)
        {
            _view = CollectionViewSource.GetDefaultView(fornitori);

            PreviousCommand = new RelayCommand(MovePrevious,() => CanMovePrevoious);
            NextCommand = new RelayCommand(MoveNext, () => CanMoveNext);
        }

        private void MoveNext()
        {
            if (CanMoveNext)
            {
                _view.MoveCurrentToNext();
                UpdateModel();
            }
        }

        private void MovePrevious()
        {
            if (CanMovePrevoious)
            {
                _view.MoveCurrentToPrevious();
                UpdateModel();
            }
        }

        private bool CanMoveNext
        {
            get
            {
                int total = (_view.SourceCollection as IEnumerable<Fornitori>).Count();
                return _view.CurrentPosition < total - 1;
            }
        }

        private bool CanMovePrevoious
        {
            get
            {
                return _view.CurrentPosition > 0;
            }
        }

        private void UpdateModel()
        {
            RaisePropertyChanged(null);
        }
    }
}
