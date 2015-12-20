using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Windows.Data;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OrdiniCatCe.Gui.Db;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.Utils.Logging;
using OrdiniCatCe.Gui.View;



namespace OrdiniCatCe.Gui.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand AddOrdineCommand { get; private set; }
        public RelayCommand AddMarcaCommand { get; private set; }
        public RelayCommand AddFornitoreCommand { get; private set; }
        public RelayCommand ClearFiltersCommand { get; private set; }
        public RelayCommand ClearNomeFilterCommand { get; private set; }
        public RelayCommand ClearFornitoreFilterCommand { get; private set; }
        public RelayCommand AnagraficaFornitoriCommand { get; private set; }
        public RelayCommand AnagraficaMarcheCommand { get; private set; }
        public RelayCommand OrdinaCommand { get; private set; }
        public RelayCommand ViewStoricoCommand { get; private set; }
        public RelayCommand ViewPerFornitoreCommand { get; private set; }

        private const string _nameFilterPrpName="NameFilter";
        private string _nameFilter;
        public string NameFilter
        {
            get
            {
                return _nameFilter;
            }
            set
            {
                _nameFilter = value;
                RaisePropertyChanged(_nameFilterPrpName);
                RigheOrdine.Refresh();
            }
        }

        private const string _visualizzaStoricoPrpName = "VisualizzaStorico";
        private bool _visualizzaStorico;
        public bool VisualizzaStorico
        {
            get
            {
                return _visualizzaStorico;
            }
            set
            {
                _visualizzaStorico = value;
                RaisePropertyChanged(_visualizzaStoricoPrpName);
                UpdateRigheOrdineFromDb();
            }
        }

        private const string _fornitoreFilterPrpName = "FornitoreFilter";
        private string _fornitoreFilter;
        public string FornitoreFilter
        {
            get
            {
                return _fornitoreFilter;
            }
            set
            {
                _fornitoreFilter = value;
                RaisePropertyChanged(_fornitoreFilterPrpName);
                RigheOrdine.Refresh();
            }
        }

        private readonly ObservableCollection<RichiesteOrdine> _righeOrdine;

        public ICollectionView RigheOrdine { get; private set; }

        public MainViewModel()
        {
            _nameFilter = string.Empty;
            AddOrdineCommand = new RelayCommand(AddOrdine);
            AddMarcaCommand = new RelayCommand(AddMarca);
            AddFornitoreCommand = new RelayCommand(AddFornitore);
            ClearFiltersCommand = new RelayCommand(ClearFilters);
            ClearNomeFilterCommand = new RelayCommand(ClearNameFilter);
            ClearFornitoreFilterCommand = new RelayCommand(ClearFornitoreFilter);
            AnagraficaFornitoriCommand = new RelayCommand(AnagraficaFornitori);
            AnagraficaMarcheCommand = new RelayCommand(AnagraficaMarche);
            OrdinaCommand = new RelayCommand(Ordina);
            ViewPerFornitoreCommand = new RelayCommand(ViewPerFornitore);
            ViewStoricoCommand = new RelayCommand(ViewStorico);


            Messenger.Default.Register<UpdateRigaOrdineMessage>(this, MsgKeys.UpdateRigaOrdineKey, OnUpdateRigaOrdineToDbRequested);
            Messenger.Default.Register<MessageBase>(this, MsgKeys.UpdateRigheOrdineKey, OnUpdateRigheOrdineToDbRequested);
            Messenger.Default.Register<UpdateRigaOrdineMessage>(this, MsgKeys.DeleteKey, OnDeleteRequested);
            Messenger.Default.Register<AddRigaOrdineMessage>(this, MsgKeys.NewOrderWindowConfirmKey, OnRigaOrdineAddedToDb);

            _righeOrdine = new ObservableCollection<RichiesteOrdine>();

            
            foreach (RichiesteOrdine ro in DbManager.GetRichiesteOrdineAttive())
            {
                _righeOrdine.Add(ro);
            }

            VisualizzaStorico = false;
            

            RigheOrdine = CollectionViewSource.GetDefaultView(_righeOrdine);
            RigheOrdine.Filter = Filter;
        }

        private void ClearFilters()
        {
            ClearNameFilter();
            ClearFornitoreFilter();
        }

        private void ClearNameFilter()
        {
            NameFilter = string.Empty;
        }

        private void ClearFornitoreFilter()
        {
            FornitoreFilter = string.Empty;
        }

        private void Ordina()
        {
            
            IQueryable<IGrouping<int?, RichiesteOrdine>> daOrdinare = DbManager.GetProdottiDaOrdinare();

            foreach (IGrouping<int?, RichiesteOrdine> ordines in daOrdinare)
            {
                if (ordines.Key.HasValue)
                {
                    Fornitori f = DbManager.GetFornitore(ordines.Key.Value);
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    const string subject = "Ordine";

                    //scrivere il body in HTML
                    MailMessage message=new MailMessage();
                    message.IsBodyHtml = true;
                        


                    #region Creazione Body

                    StringWriter stringWriter = new StringWriter();
                    using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
                    {
                        //writer.AddAttribute(HtmlTextWriterAttribute.Class, classValue);
                        writer.RenderBeginTag(HtmlTextWriterTag.Html);
                        writer.RenderBeginTag(HtmlTextWriterTag.Body);
                        foreach (RichiesteOrdine richiesteOrdine in ordines)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);
                            writer.RenderEndTag();
                        }
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                    }
                    string body1 = stringWriter.ToString();
                    #endregion

                    string body = string.Empty;

                    foreach (RichiesteOrdine richiesteOrdine in ordines)
                    {
                        //body = string.IsNullOrEmpty(body) ? richiesteOrdine.Descrizione : 
                        //    string.Format("{0}{2}{1}", body, richiesteOrdine.Descrizione, Environment.NewLine);
                    }

                    string email = f.Email;
                    //per ora invio tutto al mio indirizzo
                    //TODO: togliere questo assegnamento.
                    email = "rotandrea@gmail.com";
                    message.From = new MailAddress(email);
                    message.To.Add(new MailAddress(email));
                    message.Body = body1;

                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new System.Net.NetworkCredential("rotandrea@gmail.com", "St3f@n01113");
                    smtpServer.EnableSsl = true;

                    try
                    {
                        smtpServer.Send(message);
                    }
                    catch (SmtpException smtpExc)
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("SMTP Exception: " + smtpExc.Message);
                    }
                    catch (Exception exc)
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("SMTP Exception: " + exc.Message);
                    }


                    proc.StartInfo.FileName = string.Format("mailto:{0}?subject={1}&body={2}", email, subject, body);
                    proc.Start();
                }
            }
            
        }

        private void ViewPerFornitore()
        {
            ServiceLocator.Current.GetInstance<FornitoriViewModel>().Setup();
            FornitoriWindow wnd = new FornitoriWindow();
            wnd.ShowDialog();
        }

        private void ViewStorico()
        {
            this.VisualizzaStorico = !VisualizzaStorico;
        }

        /// <summary>
        /// Recupera dal DB le righe ordine.
        /// </summary>
        private void UpdateRigheOrdineFromDb()
        {
            _righeOrdine.Clear();
            List<RichiesteOrdine> rOrdines = VisualizzaStorico ? DbManager.GetRichiesteOrdineStoricizzate() : DbManager.GetRichiesteOrdineAttive();
            foreach (RichiesteOrdine ro in rOrdines)
            {

                _righeOrdine.Add(ro);
            }
        }

        private void OnRigaOrdineAddedToDb(AddRigaOrdineMessage msg)
        {
            this._righeOrdine.Add(msg.RigaOrdine);

            //apertura dettaglio
            DetailWindow detailWindow = new DetailWindow();
            ServiceLocator.Current.GetInstance<DetailViewModel>().Setup(msg.RigaOrdine, true);
            detailWindow.ShowDialog();

            if (detailWindow.MyDialogResult)
            {
                //update
                Messenger.Default.Send<UpdateRigaOrdineMessage>(
                                                                new UpdateRigaOrdineMessage(detailWindow.CreateRigaOrdine()),
                                                                MsgKeys.UpdateRigaOrdineKey);
            }
        }



        private void OnDeleteRequested(UpdateRigaOrdineMessage message)
        {
            if (message.RigaOrdine != null)
            {
                DbManager.RemoveRigaOrdine(message.RigaOrdine.Id);
                UpdateRigheOrdineFromDb();
            }
        }

        private void AddOrdine()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.AddRigaOrdineKey);
        }

        private void AddMarca()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.AddMarcaKey);
        }

        private void AddFornitore()
        {
            Messenger.Default.Send(new MessageBase(), MsgKeys.AddFornitoreKey);
        }

        private void AnagraficaFornitori()
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                AnagraficaFornitoreWindow wnd = new AnagraficaFornitoreWindow(db.Fornitori.ToList());
                wnd.ShowDialog();

                //aggiorno DB?
                db.SaveChanges();
            }
        }

        private void AnagraficaMarche()
        {
            using (OrdiniEntities db = new OrdiniEntities())
            {
                AnagraficaMarcheWindow wnd = new AnagraficaMarcheWindow(db.Marche.ToList());
                wnd.ShowDialog();

                //aggiorno DB?
                db.SaveChanges();
            }
        }

        private void OnUpdateRigheOrdineToDbRequested(MessageBase m)
        {
            UpdateRigheOrdineFromDb();
        }

        private void OnUpdateRigaOrdineToDbRequested(UpdateRigaOrdineMessage message)
        {
            string errorMessage;
            DbManager.UpdateRigaOrdine(message.RigaOrdine, out errorMessage);
            UpdateRigheOrdineFromDb();
            //using (OrdiniEntities db = new OrdiniEntities())
            //{
            //    RichiesteOrdine ro = db.RichiesteOrdine.First(r => r.Id == message.RigaOrdine.Id);
            //    CopyAllProperties(message.RigaOrdine, ro);
            //    int updated = db.SaveChanges();
            //    UpdateRigheOrdineFromDb();
            //}
        }

        

        private bool Filter(object obj)
        {
            if (!(obj is RichiesteOrdine))
            {
                return true;
            }

            RichiesteOrdine rOrdine = obj as RichiesteOrdine;
            return FilterByFullName(rOrdine) &&
                   FilterByFornitore(rOrdine);
        }

        private bool FilterByFornitore(RichiesteOrdine rOrdine)
        {
            if (string.IsNullOrEmpty(FornitoreFilter))
            {
                return true;
            }

            return rOrdine.ContainsPezzoOfFornitore(FornitoreFilter);
        }


        private bool FilterByFullName(RichiesteOrdine rOrdine)
        {
            if (string.IsNullOrEmpty(NameFilter))
            {
                return true;
            }

            if (string.IsNullOrEmpty(rOrdine.FullName))
            {
                return false;
            }

            return rOrdine.FullName.ToLower().Contains(NameFilter.ToLower());
        }

    }
}