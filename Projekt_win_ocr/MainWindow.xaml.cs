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
using Projekt_win_ocr.MyViewModelNS;
using System.Windows.Forms;
using System.ComponentModel;
//using WindowsFormsIntegration;

namespace Projekt_win_ocr
{
    public partial class MainWindow : Window//, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //private string _viewerPathAux;
        //public string ViewerPathAux
        //{
        //    get { return _viewerPathAux; }
        //    set
        //    {
        //        _viewerPathAux = value;
        //        NotifyPropertyChanged("ViewerPath");
        //        pdfWebViewer.Navigate(new Uri(_viewerPathAux));
        //    }
        //}

        //protected void NotifyPropertyChanged(string info)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        //}

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MyViewModel();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    pdfWebViewer.Navigate(new Uri(ViewerLabel.Content.ToString()));
        //}
    }
}
