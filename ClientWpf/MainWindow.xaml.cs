using System;
using System.Windows;
using System.Windows.Controls;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string messageText;
        public static bool IsOpened { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();

            //creates connection and opens proxy to use
            try
            {
                ClientLogic logic = new ClientLogic();
                logic.CreateConnection();
                logic.OpenProxy();
                UpdateConnectionsCounter();
                UpdateConnectionState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }

            //The current login window is the main window now.
            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private void UpdateConnectionsCounter()
        {
            ClientLogic logic = new ClientLogic();
            ConnectionsCounterLabel.Content = $"{logic.GetConnectionsCounter()} / {logic.GetMaxConnetionsNumber()}";
        }

        private void UpdateConnectionState()
        {
            ClientLogic logic = new ClientLogic();
            ConnectionStateLabel.Content = logic.GetConnetionState();
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
                    // Shows to user his name.
                    chatWindow.UserNameBox.Text = nameBox.Text;
                    chatWindow.UserName = nameBox.Text;
                    //opens the chat window
                    chatWindow.Show();

                    MainWindow.IsOpened = false;
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
