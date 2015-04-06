using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Xml.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.Constants;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AnagraficaMarcheViewModel : ViewModelBase
    {
        private ICollectionView _view;

        public MarcheCollection Marche   { get; set; }

        public RelayCommand PreviousCommand { get; private set; }
        public RelayCommand NextCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        public Marche CurrentMarca
        {
            get
            {
                return _view.CurrentItem as Marche;
            }
        }

        public string Message
        {
            get
            {
                if (_view.IsEmpty)
                {
                    return Texts.NoMarche;
                }
                return string.Empty;
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
                return CurrentMarca.Nome;
            }
            set
            {
                CurrentMarca.Nome = value;
            }
        }

        public AnagraficaMarcheViewModel(List<Marche> marche)
        {
            _view = CollectionViewSource.GetDefaultView(marche);

            PreviousCommand = new RelayCommand(MovePrevious,() => CanMovePrevoious);
            NextCommand = new RelayCommand(MoveNext, () => CanMoveNext);
            DeleteCommand = new RelayCommand(Delete, () => CanDelete);
        }

        private void UpdateListOfMarche()
        {
            _view = CollectionViewSource.GetDefaultView(DbManager.GetMarche());
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
            DbManager.RemoveMarca(CurrentMarca.Id);
            UpdateListOfMarche();
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
                int total = (_view.SourceCollection as IEnumerable<Marche>).Count();
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
