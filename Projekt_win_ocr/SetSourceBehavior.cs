using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Projekt_win_ocr.SetSource
{
    //class SetSourceBehavior: Behavior<WebBrowser>
    class SetSourceBehavior
    {
        public static readonly DependencyProperty DocumentProperty =
      DependencyProperty.RegisterAttached("Document", typeof(string), typeof(SetSourceBehavior), new UIPropertyMetadata(null, DocumentPropertyChanged));

        public static string GetDocument(DependencyObject element)
        {
            return (string)element.GetValue(DocumentProperty);
        }

        public static void SetDocument(DependencyObject element, string value)
        {
            element.SetValue(DocumentProperty, value);
        }

        public static void DocumentPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = target as WebBrowser;
            if (browser != null)
            {
                string document = e.NewValue as string;
                //browser.Navigate(new Uri(document));
                browser.Source = new Uri(document);
            }
        }

        //public static readonly DependencyProperty BindableSourceProperty =
        //DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(SetSourceBehavior), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        //public static string GetBindableSource(DependencyObject obj)
        //{
        //    return (string)obj.GetValue(BindableSourceProperty);
        //}

        //public static void SetBindableSource(DependencyObject obj, string value)
        //{
        //    obj.SetValue(BindableSourceProperty, value);
        //}

        //public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        //{
        //    WebBrowser browser = o as WebBrowser;
        //    if (browser != null)
        //    {
        //        string uri = e.NewValue as string;
        //        browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
        //    }
        //}

        //-----------------------------------------------------------------------------

        //public static readonly DependencyProperty AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(SetSourceBehavior), new PropertyMetadata(default(string)));
        //public string Address
        //{
        //    get { return (string)GetValue(AddressProperty); }
        //    set 
        //    { 
        //        SetValue(AddressProperty, value);
        //        ChangeAddress();
        //    }
        //}

        //public void ChangeAddress()
        //{
        //    WebBrowser obj = AssociatedObject as WebBrowser;
        //    obj.Navigate(new Uri(Address));
        //    //obj.Source = new Uri(Address);
        //}
    }
}
