using System;
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
           
        }





        private void OnAddRigaOrdineRequested(MessageBase obj)
        {
            ServiceLocator.Current.GetInstance<DetailViewModel>().Setup(null);
            DetailWindow wnd = new DetailWindow();
            bool? dialogResult = wnd.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                Messenger.Default.Send<AddRigaOrdineMessage>(new AddRigaOrdineMessage
                                                             {
                                                                 RigaOrdine = wnd.CreateRigaOrdine()
                                                             }, 
                    MsgKeys.AddRigaOrdineToDbKey);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;

                    if (row.DataContext is RichiesteOrdine)
                    {
                        RichiesteOrdine toUpdate = row.DataContext as RichiesteOrdine;
                        ServiceLocator.Current.GetInstance<DetailViewModel>().Setup(toUpdate);

                        DetailWindow wnd = new DetailWindow();
                        bool? dialogResult = wnd.ShowDialog();

                        if (dialogResult.HasValue && dialogResult.Value)
                        {
                            //update
                            Messenger.Default.Send<UpdateRigaOrdineMessage>(
                                new UpdateRigaOrdineMessage(wnd.CreateRigaOrdine()),
                                MsgKeys.UpdateRigaOrdineKey);
                        }
                        
                    }

                    break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DialogResult = null;
        }
    }
}
