
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AnagraficaFornitoreViewModel : ViewModelBase
    {
        private readonly ICollectionView _view;

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
                if (_view.IsEmpty)
                {
                    return string.Empty;
                }
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
                if (_view.IsEmpty)
                {
                    return string.Empty;
                }
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
                if (_view.IsEmpty)
                {
                    return string.Empty;
                }
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
            DbManager.RemoveFornitore(CurrentFornitore.Id);
            if (CanMovePrevoious)
            {
                MovePrevious();
            }
            else if (CanMoveNext)
            {
                MoveNext();
            }
        }

        private bool CanDelete
        {
            get
            {
                if (_view.IsEmpty)
                {
                    return false;
                }
                return true;
            }
        }

        private bool CanMoveNext
        {
            get
            {
                if (_view.IsEmpty)
                {
                    return false;
                }
                int total = (_view.SourceCollection as List<Fornitori>).Count();
                return _view.CurrentPosition < total - 1;
            }
        }

        private bool CanMovePrevoious
        {
            get
            {
                if (_view.IsEmpty)
                {
                    return false;
                }
                return _view.CurrentPosition > 0;
            }
        }

        private void UpdateModel()
        {
            RaisePropertyChanged(null);
        }
    }
}
