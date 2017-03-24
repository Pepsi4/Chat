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

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        /// <summary>
        /// Shows if the chat window was already opened.
        /// </summary>
        public static bool IsOpened { get; set; } = false;

        public ChatWindow()
        {
            InitializeComponent();
            //refreshs the history box
            ClientLogic logic = new ClientLogic(historyBox);
            //logic.GetMessage();

            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ClientLogic logic = new ClientLogic();
            logic.SendMessage(MessageBox.Text);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
