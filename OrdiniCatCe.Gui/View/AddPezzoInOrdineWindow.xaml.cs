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
using OrdiniCatCe.Gui.Constants;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;


namespace OrdiniCatCe.Gui.View
{
    /// <summary>
    /// Interaction logic for AddPezzoInOrdineWindow.xaml
    /// </summary>
    public partial class AddPezzoInOrdineWindow : Window
    {
        public bool MyDialogResult { get; private set; }

        public PezziInOrdine AddedPezzo{get; private set; }

        public AddPezzoInOrdineWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<MessageBase>(this, MsgKeys.CancelAddPezzoKey, OnCancelRequested);
            Messenger.Default.Register<AddPezzoMessage>(this, MsgKeys.ConfirmAddPezzoKey, OnConfirmRequested);
            Messenger.Default.Register<ErrorMessageMessage>(this, MsgKeys.ShowErrorMessageOnAddPezzoKey, OnShowErrorMessageRequested);
        }

        public void Setup()
        {
            MyDialogResult = false;
        }

        private void OnConfirmRequested(AddPezzoMessage msg)
        {
            MyDialogResult = true;
            AddedPezzo = msg.Pezzo;
            this.Close();
        }

        private void OnCancelRequested(MessageBase obj)
        {
            this.Close();
        }

        private void OnShowErrorMessageRequested(ErrorMessageMessage err)
        {
            Xceed.Wpf.Toolkit.MessageBox.Show(null, err.Message, AppConstants.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}
