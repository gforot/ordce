using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.ViewModel;
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
using System.Windows.Shapes;

namespace OrdiniCatCe.Gui.View
{
    /// <summary>
    /// Interaction logic for AddAnagraficaElementWindow.xaml
    /// </summary>
    public partial class AddAnagraficaElementWindow : Window
    {
        public bool MyDialogResult { get; private set; }

        public AddAnagraficaElementWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<MessageBase>(this, MsgKeys.CancelAnagraficaKey, OnCancelRequested);
            Messenger.Default.Register<MessageBase>(this, MsgKeys.ConfirmAnagraficaKey, OnConfirmRequested);
            MyDialogResult = false;
        }

        private void OnConfirmRequested(MessageBase obj)
        {
            MyDialogResult = true;
            this.Close();
        }

        private void OnCancelRequested(MessageBase obj)
        {
            this.Close();
        }

        internal string GetName()
        {
            return (DataContext as AddAnagraficaViewModel).Name;
        }
    }
}
