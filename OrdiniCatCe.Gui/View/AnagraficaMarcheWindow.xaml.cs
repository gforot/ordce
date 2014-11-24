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
using OrdiniCatCe.Gui.Model;
using OrdiniCatCe.Gui.ViewModel;


namespace OrdiniCatCe.Gui.View
{
    /// <summary>
    /// Interaction logic for AnagraficaMarcheWindow.xaml
    /// </summary>
    public partial class AnagraficaMarcheWindow : Window
    {
        public AnagraficaMarcheWindow(List<Marche> marche)
        {
            InitializeComponent();

            DataContext = new AnagraficaMarcheViewModel(marche);
        }
    }
}
