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

namespace Projekt_win_ocr
{
    /// <summary>
    /// Logika interakcji dla klasy PdfViewerWindow.xaml
    /// </summary>
    public partial class PdfViewerWindow : Window
    {
        public PdfViewerWindow()
        {
            InitializeComponent();
        }

        public void SetSource(string address)
        {
            pdfWebViewer.Navigate(new Uri(address));
        }
    }
}
