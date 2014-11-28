using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Xml.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OrdiniCatCe.Gui.Model;


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

        public string Name
        {
            get
            {
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
            using (OrdiniEntities db = new OrdiniEntities())
            {
                db.RemoveMarca(CurrentMarca.Id);
                if (CanMovePrevoious)
                {
                    MovePrevious();
                }
                else if (CanMoveNext)
                {
                    MoveNext();
                }
            }
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
                int total = (_view.SourceCollection as IEnumerable<Marche>).Count();
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
