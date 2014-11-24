
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AnagraficaFornitoreViewModel : ViewModelBase
    {
        private ICollectionView _view;

        public FornitoriCollection Fornitori { get; set; }

        public RelayCommand PreviousCommand { get; private set; }
        public RelayCommand NextCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

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

        public AnagraficaFornitoreViewModel(List<Fornitori> fornitori)
        {
            _view = CollectionViewSource.GetDefaultView(fornitori);

            PreviousCommand = new RelayCommand(MovePrevious,() => CanMovePrevoious);
            NextCommand = new RelayCommand(MoveNext, () => CanMoveNext);
            DeleteCommand = new RelayCommand(Delete, () => CanDelete);
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

        private void Delete()
        {
            (_view.SourceCollection as List<Fornitori>).Remove(CurrentFornitore);
        }

        private bool CanDelete
        {
            get
            {
                return true;
            }
        }

        private bool CanMoveNext
        {
            get
            {
                int total = (_view.SourceCollection as List<Fornitori>).Count();
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
