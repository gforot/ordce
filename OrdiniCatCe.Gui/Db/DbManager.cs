using System;
using System.Linq;
using System.Web.UI.WebControls;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;

//http://stackoverflow.com/questions/15336248/entity-framework-5-updating-a-record
namespace OrdiniCatCe.Gui.Db
{
    public class DbManager
    {
        /// <summary>
        /// Aggiunge una marca nel Database
        /// Controlla l'esistenza della marca. Se esiste, non permette l'aggiunta e torna false.
        /// Per qualsiasi problema deve tornare un messaggio contenente l'errore da visualizzare all'utente.
        /// </summary>
        /// <param name="marca">La marca da aggiungere.</param>
        /// <param name="errorMessage">Il messaggio di errore(ha un valore significativo solamente se il metodo torna false).</param>
        /// <returns>True se l'inserimento è andato a buon fine, altrimenti torna false.</returns>
        public static bool AddMarca(Marche marca, out string errorMessage)
        {
            errorMessage = null;
            bool addOk = false;
            using (OrdiniEntities db = new OrdiniEntities())
            {
                if (db.Marche.Any(m => m.Nome.ToLower().Equals(marca.Nome.ToLower())))
                {
                    errorMessage = string.Format("La marca {0} è già presente in anagrafica.", marca.Nome);
                }
                else
                {
                    db.Marche.Add(marca);
                    db.SaveChanges();
                    addOk = true;
                    //se viene aggiunta la marca correttamente, mando messaggio di Marca Added
                    Messenger.Default.Send(new AddMarcaMessage(marca), MsgKeys.MarcaAddedKey);
                }
            }

            return addOk;
        }


        public static void AddRigaOrdine(RichiesteOrdine richiestaOrdine)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                db.RichiesteOrdine.Add(richiestaOrdine);
                db.SaveChanges();
            }
        }

        public static RichiesteOrdine GetRichiestaOrdine(int id)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                return db.RichiesteOrdine.Include("PezziInOrdine").FirstOrDefault(ro => ro.Id == id);
            }
        }

        /// <summary>
        /// Aggiunge un fornitore nel Database
        /// Controlla l'esistenza del fornitore. Se esiste, non permette l'aggiunta e torna false.
        /// Per qualsiasi problema deve tornare un messaggio contenente l'errore da visualizzare all'utente.
        /// </summary>
        /// <param name="fornitore">Il fornitore da aggiungere.</param>
        /// <param name="errorMessage">Il messaggio di errore(ha un valore significativo solamente se il metodo torna false).</param>
        /// <returns>True se l'inserimento è andato a buon fine, altrimenti torna false.</returns>
        public static bool AddFornitore(Fornitori fornitore, out string errorMessage)
        {
            errorMessage = null;
            bool addOk = false;
            using (OrdiniEntities db = new OrdiniEntities())
            {
                if (db.Fornitori.Any(f => f.Name.ToLower().Equals(fornitore.Name.ToLower())))
                {
                    errorMessage = string.Format("Il fornitore {0} è già presente in anagrafica.", fornitore.Name);
                }
                else
                {
                    db.Fornitori.Add(fornitore);
                    db.SaveChanges();
                    addOk = true;
                    //se viene aggiunta la marca correttamente, mando messaggio di Marca Added
                    Messenger.Default.Send(new AddFornitoreMessage(fornitore), MsgKeys.FornitoreAddedKey);
                }
            }

            return addOk;
        }


        public static IQueryable<IGrouping<int?, RichiesteOrdine>> GetProdottiDaOrdinare()
        {
            //[TODO] Usare la nuova tabella PezziInOrdine
            throw new NotImplementedException();
            //using (OrdiniEntities db = new OrdiniEntities())
            //{
            //    return db.RichiesteOrdine.Where(ro => (!ro.Ordinato)).GroupBy(ro => ro.IdFornitore);
            //}
        }

        public static void RemoveFornitore(int id)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                Fornitori f = GetFornitore(id);
                if (f == null)
                {
                    return;
                }
                db.Fornitori.Remove(f);
                db.SaveChanges();
            }
        }

        public static void RemoveMarca(int id)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                Marche m = GetMarca(id);
                if (m == null)
                {
                    return;
                }
                db.Marche.Remove(m);
                db.SaveChanges();
            }
        }

        public static Fornitori GetFornitore(int idFornitore)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                return db.Fornitori.First(f => f.Id == idFornitore);
            }
        }

        public static Marche GetMarca(int idMarca)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                return db.Marche.First(f => f.Id == idMarca);
            }
        }

        public static bool AddPezzo(PezziInOrdine pezzo, out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    //TODO: Controllare esistenza dell'ordine.
                    db.PezziInOrdine.Add(pezzo);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

    }
}
