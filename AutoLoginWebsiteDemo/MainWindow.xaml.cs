using System;
using System.Collections.Generic;
using System.Configuration;
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

        System.Timers.Timer t = new System.Timers.Timer(5000);//实例化Timer类，设置间隔时间为1000毫秒 就是1秒；


        
        string absolutePath =string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            this.Title ="版本号："+ ConfigurationManager.AppSettings["Version"];
            txtUserId.Text = "admin";
            txtPwd.Text = "lzga@2156";
            tbHtml.Text = "http://10.50.171.146/indexAction!gotoIndexPage.action";


            System.Drawing.Image img = System.Drawing.Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"\资源加载动效2.gif");

            pictureBox1.Image = img;


            //string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.html");
            string url = tbHtml.Text;
            InitWebHandler(url);

            double interval = Convert.ToInt32(ConfigurationManager.AppSettings["time"]) * 1000;

            System.Timers.Timer t = new System.Timers.Timer(interval);//实例化Timer类，设置间隔时间为interval毫秒；

            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
            t.AutoReset = false;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

        }
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new TextOption(f));//invok 委托实现跨线程的调用
        }

        delegate void TextOption();//定义一个委托

        void f()
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

            int interval = Convert.ToInt32(ConfigurationManager.AppSettings["sleep"]) * 1000;

            System.Threading.Thread.Sleep(interval);
            this.wfh1.Visibility = Visibility.Collapsed;
            this.wfh2.Visibility = Visibility.Visible;
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
            //System.Threading.Thread.Sleep(20000);
            //try
            //{
            //    HtmlElement tbUserid = Browser.Document.GetElementById("name");
            //    HtmlElement tbPasswd = Browser.Document.GetElementById("pwd");

            //    HtmlElementCollection elements = Browser.Document.GetElementsByTagName("SPAN");
            //    HtmlElement btnSubmit = elements[elements.Count - 1];
            //    //HtmlElement btnSubmit = Browser.Document.GetElementsByTagName("login-button")[0];

            //    ////用相应方法为元素赋值
            //    tbUserid.SetAttribute("value", txtUserId.Text);
            //    tbPasswd.SetAttribute("value", txtPwd.Text);
            //    btnSubmit.InvokeMember("click");
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.ToString());
            //}
        }
    }
}
