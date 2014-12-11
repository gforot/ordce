using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.View;
using OrdiniCatCe.Gui.ViewModel;
using OrdiniCatCe.Gui.Db;

namespace OrdiniCatCe.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<MessageBase>(this, MsgKeys.AddRigaOrdineKey, OnAddRigaOrdineRequested);
            Messenger.Default.Register<MessageBase>(this, MsgKeys.AddMarcaKey, OnAddMarcaRequested);
            Messenger.Default.Register<MessageBase>(this, MsgKeys.AddFornitoreKey, OnAddFornitoreRequested);
        }


        private void OnAddMarcaRequested(MessageBase obj)
        {
            ServiceLocator.Current.GetInstance<AddMarcaViewModel>().Setup();
            AddMarcaWindow wnd = new AddMarcaWindow();
            wnd.ShowDialog();
        }


        private void OnAddFornitoreRequested(MessageBase obj)
        {
            ServiceLocator.Current.GetInstance<AddFornitoreViewModel>().Setup();
            AddFornitoreWindow wnd = new AddFornitoreWindow();
            wnd.ShowDialog();
        }


        private void OnAddRigaOrdineRequested(MessageBase obj)
        {
            //apertura finestra di inserimento riga ordine
            ServiceLocator.Current.GetInstance<NewOrderViewModel>().Setup();
            NewOrderWindow wnd = new NewOrderWindow();
            wnd.ShowDialog();
        }

        //private void Aggiorna_OnClick(object sender, RoutedEventArgs e)
        //{
        //    for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
        //    {
        //        if (vis is DataGridRow)
        //        {
        //            var row = (DataGridRow)vis;

        //            if (row.DataContext is RichiesteOrdine)
        //            {
        //                RichiesteOrdine toUpdate = row.DataContext as RichiesteOrdine;
        //                ServiceLocator.Current.GetInstance<DetailViewModel>().Setup(toUpdate);

        //                DetailWindow wnd = new DetailWindow();
        //                bool? dialogResult = wnd.ShowDialog();

        //                if (dialogResult.HasValue && dialogResult.Value)
        //                {
        //                    //update
        //                    Messenger.Default.Send<UpdateRigaOrdineMessage>(
        //                                                                    new UpdateRigaOrdineMessage(wnd.CreateRigaOrdine()),
        //                                                                    MsgKeys.UpdateRigaOrdineKey);
        //                }

        //            }

        //            break;
        //        }
        //    }
        //}

        private void Aggiorna_OnClick(object sender, RoutedEventArgs e)
        {

            //TODO: Recuperare da DB con informazioni relative a Pezzi.
            RichiesteOrdine toUpdate = GetRichiestaOrdineFromSenderOfButtonClick(sender);
            if (toUpdate == null)
            {
                return;
            }
            RichiesteOrdine ro = DbManager.GetRichiestaOrdine(toUpdate.Id);
            if (ro == null)
            {
                return;
            }
            ServiceLocator.Current.GetInstance<DetailViewModel>().Setup(ro);

            DetailWindow wnd = new DetailWindow();
            wnd.ShowDialog();

            if (wnd.MyDialogResult)
            {
                //update
                Messenger.Default.Send<UpdateRigaOrdineMessage>(
                                                                new UpdateRigaOrdineMessage(wnd.CreateRigaOrdine()),
                                                                MsgKeys.UpdateRigaOrdineKey);
            }

        }

        private void SetAvvisato_OnClick(object sender, RoutedEventArgs e)
        {
            RichiesteOrdine richiestaOrdine = GetRichiestaOrdineFromSenderOfButtonClick(sender);
            if (richiestaOrdine == null)
            {
                return;
            }
            Messenger.Default.Send<UpdateRigaOrdineMessage>(
                                                new UpdateRigaOrdineMessage(richiestaOrdine),
                                                MsgKeys.SetAvvisatoKey);

        }

        private void SetRitirato_OnClick(object sender, RoutedEventArgs e)
        {
            RichiesteOrdine richiestaOrdine = GetRichiestaOrdineFromSenderOfButtonClick(sender);
            if (richiestaOrdine == null)
            {
                return;
            }
            Messenger.Default.Send<UpdateRigaOrdineMessage>(
                                                new UpdateRigaOrdineMessage(richiestaOrdine),
                                                MsgKeys.SetRitiratoKey);
        }

        //SetOrdinato_OnClick
        private void SetOrdinato_OnClick(object sender, RoutedEventArgs e)
        {
            RichiesteOrdine richiestaOrdine = GetRichiestaOrdineFromSenderOfButtonClick(sender);
            if (richiestaOrdine == null)
            {
                return;
            }
            Messenger.Default.Send<UpdateRigaOrdineMessage>(
                                                new UpdateRigaOrdineMessage(richiestaOrdine),
                                                MsgKeys.SetOrdinatoKey);
        }

        private void SetArrivato_OnClick(object sender, RoutedEventArgs e)
        {
            RichiesteOrdine richiestaOrdine = GetRichiestaOrdineFromSenderOfButtonClick(sender);
            if (richiestaOrdine == null)
            {
                return;
            }
            Messenger.Default.Send<UpdateRigaOrdineMessage>(
                                                new UpdateRigaOrdineMessage(richiestaOrdine),
                                                MsgKeys.SetArrivatoKey);
        }

        private RichiesteOrdine GetRichiestaOrdineFromSenderOfButtonClick(object sender)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;

                    if (row.DataContext is RichiesteOrdine)
                    {
                        return row.DataContext as RichiesteOrdine;
                    }
                    break;
                }
            }
            return null;
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            RichiesteOrdine richiestaOrdine = GetRichiestaOrdineFromSenderOfButtonClick(sender);
            if (richiestaOrdine == null)
            {
                return;
            }
            Messenger.Default.Send<UpdateRigaOrdineMessage>(
                                                new UpdateRigaOrdineMessage(richiestaOrdine),
                                                MsgKeys.DeleteKey);
        }

        private const string _avvisoText = "Avviso di arrivo pezzo ordinato";

        private void AvvisaViaEMail_OnClick(object sender, RoutedEventArgs e)
        {
            RichiesteOrdine richiestaOrdine = GetRichiestaOrdineFromSenderOfButtonClick(sender);
            if (richiestaOrdine == null)
            {
                return;
            }

            string email = richiestaOrdine.EMail;
            string subject = _avvisoText;
            string body = string.Format("Buongiorno, il ricambio '{0}' da Lei richiesto è ora presente nel nostro magazzino. Rota Angelo di Molteni Simona(0341369364)", "<DESCRIZIONE>");
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = string.Format("mailto:{0}?subject={1}&body={2}", email, subject, body);
            proc.Start();
        }
    }
}
