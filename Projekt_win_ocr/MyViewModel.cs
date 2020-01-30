using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Projekt_win_ocr.MyViewModelNS
{
    using MyModelNS;
    using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

    public class MyViewModel: INotifyPropertyChanged
    {
        private MyModel model = new MyModel();
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand FileButtonClick{ get; set; }
        public ICommand DirButtonClick{ get; set; }
        public ICommand ConvertButtonClick{ get; set; }

        private bool _enabledWindow = true;
        private bool _viewerEnabled = true;
        private string _path;
        private string _outPath;
        private string _outFile;
        private string _readText;
        private string _viewerPath;
        private bool _basicMode = true;
        private bool _advancedMode;
        
        private bool _cleanBackgroundNoise;
        private bool _enhanceContrast;
        private bool _enhanceResolution;
        private bool _detectWhiteTextOnDarkBackgrounds;
        private bool _rotateAndStraighten;
        private bool _readBarCodes;
        private string _language = "English";
        private string _strategy = "Advanced";
        public string ViewerPath
        {
            get { return _viewerPath; }
            set
            {
                _viewerPath = value;
                NotifyPropertyChanged("ViewerPath");
                //SetSource.SetSourceBehavior.ChangeAddress();
            }
        }
        public bool ViewerEnabled
        {
            get { return _viewerEnabled; }
            set
            {
                _viewerEnabled = value;
                NotifyPropertyChanged("ViewerEnabled");
            }
        }
        public bool EnabledWindow
        {
            get { return _enabledWindow; }
            set
            {
                _enabledWindow = value;
                NotifyPropertyChanged("EnabledWindow");
            }
        }
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                NotifyPropertyChanged("Path");
            }
        }
        public string OutPath
        {
            get { return _outPath; }
            set
            {
                _outPath = value;
                NotifyPropertyChanged("OutPath");
            }
        }
        public string OutFile
        {
            get { return _outFile; }
            set
            {
                _outFile = value;
                NotifyPropertyChanged("OutFile");
            }
        }
        public string ReadText
        {
            get { return _readText; }
            set
            {
                _readText = value;
                NotifyPropertyChanged("ReadText");
            }
        }
        public bool BasicMode
        {
            get { return _basicMode; }
            set
            {
                _basicMode = value;
                NotifyPropertyChanged("BasicMode");
            }
        }
        public bool AdvancedMode
        {
            get { return _advancedMode; }
            set
            {
                _advancedMode = value;
                NotifyPropertyChanged("AdvancedMode");
            }
        }

        public bool CleanBackgroundNoise
        {
            get { return _cleanBackgroundNoise; }
            set
            {
                _cleanBackgroundNoise = value;
                NotifyPropertyChanged("CleanBackgroundNoise");
            }
        }
        public bool EnhanceContrast
        {
            get { return _enhanceContrast; }
            set
            {
                _enhanceContrast = value;
                NotifyPropertyChanged("EnhanceContrast");
            }
        }
        public bool EnhanceResolution
        {
            get { return _enhanceResolution; }
            set
            {
                _enhanceResolution = value;
                NotifyPropertyChanged("EnhanceResolution");
            }
        }
        public bool DetectWhiteTextOnDarkBackgrounds
        {
            get { return _detectWhiteTextOnDarkBackgrounds; }
            set
            {
                _detectWhiteTextOnDarkBackgrounds = value;
                NotifyPropertyChanged("DetectWhiteTextOnDarkBackgrounds");
            }
        }
        public bool RotateAndStraighten
        {
            get { return _rotateAndStraighten; }
            set
            {
                _rotateAndStraighten = value;
                NotifyPropertyChanged("RotateAndStraighten");
            }
        }
        public bool ReadBarCodes
        {
            get { return _readBarCodes; }
            set
            {
                _readBarCodes = value;
                NotifyPropertyChanged("ReadBarCodes");
            }
        }
        public string Language
        {
            get { return _language; }
            set
            {
                var tmp = value.Split(' ');
                if (tmp.Length > 1) 
                    _language = tmp[1];
                else 
                    _language = value;
                NotifyPropertyChanged("Language");
            }
        }
        public string Strategy
        {
            get { return _strategy; }
            set
            {
                var tmp = value.Split(' ');
                if (tmp.Length > 1)
                    _strategy = tmp[1];
                else
                    _strategy = value;
                NotifyPropertyChanged("Strategy");
            }
        }
        public MyViewModel()
        {
            FileButtonClick= new RelayCommand(o => FileButtonClk("FileButton"));
            DirButtonClick= new RelayCommand(o => DirButtonClk("DirButton"));
            ConvertButtonClick= new RelayCommand(o => ConvertButtonClk("ConvertButton"));
        }
        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        
        private void FileButtonClk(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Path = openFileDialog.FileName;
                OutPath = openFileDialog.FileName.Substring(0, openFileDialog.FileName.LastIndexOf("\\")+1);
                OutFile = "OCR_" + openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf("\\") + 1, openFileDialog.FileName.LastIndexOf(".pdf")-3);
                //ViewerPath = Path;
                //PdfViewerWindow pwin = new PdfViewerWindow();
                //pwin.SetSource(ViewerPath);
                //pwin.Show();
            }
        }
        private void DirButtonClk(object sender)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    OutPath = fbd.SelectedPath;
                }
            }
        }
        private async void ConvertButtonClk(object sender)
        {
            //Encoding Utf8 = Encoding.UTF8;
            //ViewerPath = "about:blank";
            ViewerEnabled = false;
            EnabledWindow = false;
            if (Path == null || OutPath == null || OutFile == null)
            {
                MessageBox.Show("Please specify all paths and names.");
                return;
            }
            WaitingWindow win = new WaitingWindow();
            win.Show();
            win.Topmost = true;
            ReadText = await Task<string>.Run(() => model.ConvertPDF(Path, AdvancedMode, BasicMode, CleanBackgroundNoise, EnhanceContrast, EnhanceResolution, Language, Strategy, DetectWhiteTextOnDarkBackgrounds, RotateAndStraighten, ReadBarCodes));
            //byte[] utf8Bytes = Utf8.GetBytes(ReadText);
            //ReadText = Utf8.GetString(utf8Bytes);
            win.ConversionDone();
            model.SavePDF(OutPath, OutFile, ReadText);
            EnabledWindow = true;
            ViewerEnabled = true;
            ViewerPath = OutPath + OutFile + ".pdf";
            //PdfViewerWindow pwin = new PdfViewerWindow();
            //pwin.SetSource(ViewerPath);
            //pwin.Show();
        }


    }
}
