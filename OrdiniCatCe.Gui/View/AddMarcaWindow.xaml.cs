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
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using OrdiniCatCe.Gui.Constants;
using OrdiniCatCe.Gui.Messages;
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.ViewModel;


namespace OrdiniCatCe.Gui.View
{
    /// <summary>
    /// Interaction logic for AddMarcaWindow.xaml
    /// </summary>
    public partial class AddMarcaWindow : Window
    {
        public bool MyDialogResult { get; private set; }

        public AddMarcaWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<MessageBase>(this, MsgKeys.CancelAddMarcaKey, OnCancelRequested);
            Messenger.Default.Register<MessageBase>(this, MsgKeys.ConfirmAddMarcaKey, OnConfirmRequested);
            Messenger.Default.Register<ErrorMessageMessage>(this, MsgKeys.ShowErrorMessageOnAddMarcaKey, OnShowErrorMessageRequested);
        }



        public void Setup()
        {
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

        private void OnShowErrorMessageRequested(ErrorMessageMessage err)
        {
            Xceed.Wpf.Toolkit.MessageBox.Show(null, err.Message, AppConstants.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Stop);
        }

    }
}
