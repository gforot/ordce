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
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Messages;


namespace OrdiniCatCe.Gui.View
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        public DetailWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<MessageBase>(this, MsgKeys.CancelKey, OnCancelRequested);
            Messenger.Default.Register<MessageBase>(this, MsgKeys.ConfirmKey, OnConfirmRequested);
        }

        private void OnConfirmRequested(MessageBase obj)
        {
            this.Close();
        }

        private void OnCancelRequested(MessageBase obj)
        {
            this.Close();
        }
    }
}
