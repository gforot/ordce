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
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        public bool MyDialogResult { get; private set; }

        public NewOrderWindow()
        {
            InitializeComponent();


            Messenger.Default.Register<MessageBase>(this, MsgKeys.NewOrderWindowCancelKey, OnCancelRequested);
            Messenger.Default.Register<AddRigaOrdineMessage>(this, MsgKeys.NewOrderWindowConfirmKey, OnConfirmRequested);

            MyDialogResult = false;
        }

        private void OnConfirmRequested(AddRigaOrdineMessage obj)
        {
            MyDialogResult = true;
            this.Close();
        }

        private void OnCancelRequested(MessageBase obj)
        {
            this.Close();
        }
    }
}
