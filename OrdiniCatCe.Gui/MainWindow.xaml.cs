﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.View;
using OrdiniCatCe.Gui.ViewModel;


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
            ServiceLocator.Current.GetInstance<AddAnagraficaViewModel>().Setup(AnagraficaElementType.Marchio);
            AddAnagraficaElementWindow wnd = new AddAnagraficaElementWindow();
            wnd.ShowDialog();

            if (wnd.MyDialogResult)
            {
                Messenger.Default.Send<AddMarcaMessage>(new AddMarcaMessage
                {
                    Name = wnd.GetName()
                },
                                             MsgKeys.AddMarcaToDbKey);
            }
        }


        private void OnAddFornitoreRequested(MessageBase obj)
        {
            ServiceLocator.Current.GetInstance<AddAnagraficaViewModel>().Setup(AnagraficaElementType.Fornitore);
            AddAnagraficaElementWindow wnd = new AddAnagraficaElementWindow();
            wnd.ShowDialog();

            if (wnd.MyDialogResult)
            {
                Messenger.Default.Send<AddFornitoreMessage>(new AddFornitoreMessage

                {
                    Name = wnd.GetName()
                },
                                             MsgKeys.AddFornitoreToDbKey);
            }
        }


        private void OnAddRigaOrdineRequested(MessageBase obj)
        {
            ServiceLocator.Current.GetInstance<DetailViewModel>().Setup(null);
            DetailWindow wnd = new DetailWindow();
            wnd.ShowDialog();

            if (wnd.MyDialogResult)
            {
                Messenger.Default.Send<AddRigaOrdineMessage>(new AddRigaOrdineMessage
                                                             {
                                                                 RigaOrdine = wnd.CreateRigaOrdine()
                                                             },
                                                             MsgKeys.AddRigaOrdineToDbKey);
            }
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
            RichiesteOrdine toUpdate = GetRichiestaOrdineFromSenderOfButtonClick(sender);
            if (toUpdate == null)
            {
                return;
            }

            ServiceLocator.Current.GetInstance<DetailViewModel>().Setup(toUpdate);

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
    }
}
