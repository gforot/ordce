using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.View;



namespace OrdiniCatCe.Gui.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand AddCommand { get; private set; }


        public List<Marche> Marches { get; private set; }
        public List<RichiesteOrdine> RigheOrdine { get; private set; }
        public List<Fornitori> Fornitoris { get; private set; } 

        public MainViewModel()
        {
            AddCommand = new RelayCommand(Add);
            Messenger.Default.Register<AddRigaOrdineMessage>(this, MsgKeys.AddRigaOrdineToDbKey, OnAddRigaOrdineToDbRequested);
            Messenger.Default.Register<UpdateRigaOrdineMessage>(this, MsgKeys.UpdateRigaOrdineKey, OnUpdateRigaOrdineToDbRequested);
            // Code runs "for real"
            using (OrdiniEntities db = new OrdiniEntities())
            {
                Marches = db.Marche.ToList();
                RigheOrdine = db.RichiesteOrdine.ToList();
                Fornitoris = db.Fornitori.ToList();
            }
        }


        private void OnAddRigaOrdineToDbRequested(AddRigaOrdineMessage rigaOrdine)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                db.RichiesteOrdine.Add(rigaOrdine.RigaOrdine);
                db.SaveChanges();
            }
        }

        private void Add()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.AddRigaOrdineKey);
        }

        private void OnUpdateRigaOrdineToDbRequested(UpdateRigaOrdineMessage message)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                RichiesteOrdine ro= db.RichiesteOrdine.First(r => r.Id == message.RigaOrdine.Id);
                CopyAllProperties(message.RigaOrdine, ro);
                int updated = db.SaveChanges();
            }
        }

        private static void CopyAllProperties(RichiesteOrdine source, RichiesteOrdine target)
        {
            target.Avvisato = source.Avvisato;
            target.Cellulare = source.Cellulare;
            target.Codice = source.Codice;
            target.Cognome = source.Cognome;
            target.DataArrivato = source.DataArrivato;
            target.DataAvvisato = source.DataAvvisato;
            target.DataOrdinato = source.DataOrdinato;
            target.DataRitirato = source.DataRitirato;
            target.Descrizione = source.Descrizione;
            target.EMail = source.EMail;
            target.IdFornitore = source.IdFornitore;
            target.IdMarca = source.IdMarca;
            target.Indirizzo = source.Indirizzo;
            target.Localita = source.Localita;
            target.Modalit‡Avviso = source.Modalit‡Avviso;
            target.Nome = source.Nome;
            target.NumeroCivico = source.NumeroCivico;
            target.PrezzoAcquisto = source.PrezzoAcquisto;
            target.Telefono = source.Telefono;
            target.PrezzoVendita = source.PrezzoVendita;
        }
    }
}