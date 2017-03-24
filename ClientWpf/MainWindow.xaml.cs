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
using System.ServiceModel;
using ClientWpf.ServerRef;
using System.Threading;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string messageText;
        public MainWindow()
        {
            InitializeComponent();

            //create connection and open proxy to use
            try
            {
                ClientLogic logic = new ClientLogic();
                logic.CreateConnection();
                logic.OpenProxy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }

            //Current login window is main window now.
            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientLogic logic = new ClientLogic();
                logic.TryAddUser(nameBox.Text);

                //if we already have one window with the chat
                if (!ChatWindow.IsOpened)
                {
                    ChatWindow chatWindow = new ChatWindow();
                    //opens the chat window
                    chatWindow.Show();
                    ChatWindow.IsOpened = true;
                    //
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
