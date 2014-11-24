using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.ViewModel
{
    public class AddMarcaViewModel : AddAnagraficaItemViewModelBase
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
            //controllare l'esistenza della marca
            //Se esiste già, evitarne l'inserimento.
            using (OrdiniEntities db = new OrdiniEntities())
            {
                if (db.Marche.Any(m => m.Nome.ToLower().Equals(Name.ToLower())))
                {
                    MessageBox.Show(string.Format("La marca {0} è già presente in anagrafica.", Name));
                    return;
                }
            }

            Messenger.Default.Send<MessageBase>(new MessageBase(), MsgKeys.ConfirmAddMarcaKey);
        }

        internal Marche GetMarca()
        {
            return new Marche {Nome = Name};
        }
    }
}
