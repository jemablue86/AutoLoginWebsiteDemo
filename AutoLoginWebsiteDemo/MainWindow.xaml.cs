using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoLoginWebsiteDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtUserId.Text = "admin";
            txtPwd.Text = "lzga@2156";
            tbHtml.Text = "http://10.50.171.146/indexAction!gotoIndexPage.action";

            //string url = AppDomain.CurrentDomain.BaseDirectory + "test.html";
            string url = tbHtml.Text;
            InitWebHandler(url);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string url = AppDomain.CurrentDomain.BaseDirectory + "test.html";
            //string url = tbHtml.Text;
            //InitWebHandler(url);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
         
            try
            {
                HtmlElement tbUserid = Browser.Document.GetElementById("name");
                HtmlElement tbPasswd = Browser.Document.GetElementById("pwd");

                HtmlElementCollection elements = Browser.Document.GetElementsByTagName("SPAN");
                HtmlElement btnSubmit=elements[elements.Count - 1];
                //HtmlElement btnSubmit = Browser.Document.GetElementsByTagName("login-button")[0];

                ////用相应方法为元素赋值
                tbUserid.SetAttribute("value", txtUserId.Text);
                tbPasswd.SetAttribute("value", txtPwd.Text);
                btnSubmit.InvokeMember("click");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        public HtmlElement GetElement_Type(string type)
        {
            HtmlElement e = null;
            HtmlElementCollection elements = Browser.Document.GetElementsByTagName("span");
            var o = elements[elements.Count - 1];
            HtmlElement btnSubmit = o;
            btnSubmit.InvokeMember("click");
            foreach (HtmlElement element in elements)
            {
                
                if (element.GetAttribute("type") == type)
                {
                    e = element;
                }
            }

            return e;
        }
        public void InitWebHandler(string webUrl)
        {
            try
            {
                Uri i = new Uri(webUrl, UriKind.Absolute);
                this.Browser.Navigate(i);
                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        private void webPage_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            try
            {
                string url = this.Browser.Document.ActiveElement.GetAttribute("href");
                this.Browser.Url = new Uri(url);
               
            }
            catch
            {
                try
                {
                    this.Browser.Url = this.Browser.Document.ActiveElement.Document.Url;
                }
                catch
                {

                }
            }
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                HtmlElement tbUserid = Browser.Document.GetElementById("name");
                HtmlElement tbPasswd = Browser.Document.GetElementById("pwd");

                HtmlElementCollection elements = Browser.Document.GetElementsByTagName("SPAN");
                HtmlElement btnSubmit = elements[elements.Count - 1];
                //HtmlElement btnSubmit = Browser.Document.GetElementsByTagName("login-button")[0];

                ////用相应方法为元素赋值
                tbUserid.SetAttribute("value", txtUserId.Text);
                tbPasswd.SetAttribute("value", txtPwd.Text);
                btnSubmit.InvokeMember("click");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
    }
}
