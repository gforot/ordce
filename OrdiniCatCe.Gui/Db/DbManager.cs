using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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


        public static List<RichiesteOrdine> GetRichiesteOrdineAttiveByFornitore(int idFornitore)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                List<RichiesteOrdine> attive = GetRichiesteOrdineAttive();

                return attive.Where(r=>r.ContainsPezzoOfFornitoreById(idFornitore)).ToList();
            }
        }

        public static List<RichiesteOrdine> GetRichiesteOrdineAttive()
        {
            return GetAllRichiesteOrdines(false);
        }

        public static List<RichiesteOrdine> GetRichiesteOrdineStoricizzate()
        {
            return GetAllRichiesteOrdines(true);
        }

    

        private static List<RichiesteOrdine> GetAllRichiesteOrdines(bool storicizzate)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                //IQueryable<RichiesteOrdine> richieste = db.RichiesteOrdine.Include("PezziInOrdine").Where(ro => ro.Storicizzata == storicizzate);

                IQueryable<RichiesteOrdine> richieste = db.RichiesteOrdine.Include(ordine => ordine.PezziInOrdine).Include(ordine => ordine.PezziInOrdine.Select(p => p.Marche)).Include(ordine => ordine.PezziInOrdine.Select(p => p.Fornitori)).Where(ro => ro.Storicizzata == storicizzate);
                return richieste.ToList();
            }
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
                return db.RichiesteOrdine.Include(ro => ro.PezziInOrdine).Include(ordine => ordine.PezziInOrdine.Select(p => p.Marche)).Include(ordine => ordine.PezziInOrdine.Select(p => p.Fornitori)).FirstOrDefault(ro => ro.Id == id);

                //db.RichiesteOrdine.Include(ordine => ordine.PezziInOrdine).Include(ordine => ordine.PezziInOrdine.Select(p => p.Marche)).Include(ordine => ordine.PezziInOrdine.Select(p => p.Fornitori)).Where(ro => ro.Storicizzata == storicizzate);
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
                bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;

                try
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    var fornitore = new Fornitori() { Id = id };

                    db.Fornitori.Attach(fornitore);
                    db.Entry(fornitore).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                finally
                {
                    db.Configuration.ValidateOnSaveEnabled = oldValidateOnSaveEnabled;
                }
            }
        }

        //public static void RemoveFornitore(int id)
        //{
        //    using (OrdiniEntities db = new OrdiniEntities())
        //    {
        //        Fornitori f = GetFornitore(id);
        //        if (f == null)
        //        {
        //            return;
        //        }
        //        db.Fornitori.Remove(f);
        //        db.SaveChanges();
        //    }
        //}

        //public static void RemoveMarca(int id)
        //{
        //    using (OrdiniEntities db = new OrdiniEntities())
        //    {
        //        Marche m = GetMarca(id);
        //        if (m == null)
        //        {
        //            return;
        //        }
        //        db.Marche.Remove(m);
        //        db.SaveChanges();
        //    }
        //}

        public static void RemoveMarca(int id)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;

                try
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    var marca = new Marche() { Id = id };

                    db.Marche.Attach(marca);
                    db.Entry(marca).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                finally
                {
                    db.Configuration.ValidateOnSaveEnabled = oldValidateOnSaveEnabled;
                }
            }

        }


        /*bool oldValidateOnSaveEnabled = localDb.Configuration.ValidateOnSaveEnabled;

try
{
  localDb.Configuration.ValidateOnSaveEnabled = false;

  var customer = new Customer { CustomerId = id };

  localDb.Customers.Attach(customer);
  localDb.Entry(customer).State = EntityState.Deleted;
  localDb.SaveChanges();
}
finally
{
  localDb.Configuration.ValidateOnSaveEnabled = oldValidateOnSaveEnabled;
}*/

        public static List<PezziInOrdine> GetPezziByIdRichiesta(int id)
        {
            RichiesteOrdine ro = GetRichiestaOrdine(id);
            return ro == null ? new List<PezziInOrdine>() : ro.PezziInOrdine.ToList();
        }


        public static List<PezziInOrdine> GetPezziByIdFornitore(int idFornitore)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                return db.PezziInOrdine.Include(p => p.Marche).Include(p => p.RichiesteOrdine).Where(p => (p.IdFornitore.HasValue) && (p.IdFornitore.Value == idFornitore)).ToList();
            }
        }


        public static Fornitori GetFornitore(int idFornitore)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                return db.Fornitori.First(f => f.Id == idFornitore);
            }
        }

        public static List<Fornitori> GetFornitori()
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                return db.Fornitori.ToList();
            }
        }

        public static List<Marche> GetMarche()
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                return db.Marche.ToList();
            }
        }

        public static Marche GetMarca(int idMarca)
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                return db.Marche.First(f => f.Id == idMarca);
            }
        }

        public static bool ExistsPezzo(PezziInOrdine pezzo)
        {
            try
            {
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    return db.PezziInOrdine.Contains(pezzo);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool AddPezzo(PezziInOrdine pezzo, out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    if (pezzo.Id == 0)
                    {
                        db.Entry(pezzo).State = EntityState.Added;
                    }
                    else
                    {
                        PezziInOrdine oldInstance = db.PezziInOrdine.FirstOrDefault(p => p.Id == pezzo.Id);
                        db.Entry(oldInstance).CurrentValues.SetValues(pezzo);
                    }

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

        public static bool RemoveRigaOrdine(int idOrdine)
        {
            try
            {
                using (OrdiniEntities db = new OrdiniEntities())
                {
                    List<PezziInOrdine> pezzi = GetPezziByIdRichiesta(idOrdine);
                    foreach (PezziInOrdine p in pezzi)
                    {
                        db.PezziInOrdine.Attach(p);
                        db.PezziInOrdine.Remove(p);
                        
                    }
                    RichiesteOrdine toDelete = GetRichiestaOrdine(idOrdine);
                    db.RichiesteOrdine.Remove(toDelete);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
