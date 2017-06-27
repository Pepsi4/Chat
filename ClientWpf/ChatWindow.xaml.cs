using System.Windows;

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

        /// <summary>
        /// The name of user.
        /// </summary>
        public string UserName { get; set; }

        public ChatWindow()
        {
            InitializeComponent();
            //refreshs the history box
            ClientLogic logic = new ClientLogic(historyBox);

            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            historyBox.IsReadOnly = true;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            ClientLogic logic = new ClientLogic();
            logic.SendMessage(MessageBox.Text, UserName);
            // Clears the message box.
            MessageBox.Text = "";
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SendMessage();
            }
        }

        private void UnLoggin_click(object sender, RoutedEventArgs e)
        {
            ClientLogic logic = new ClientLogic();
            if (!MainWindow.IsOpened)
            {
                logic.UnLoggin();
                logic.CloseConnection();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                ChatWindow.IsOpened = false;
                MainWindow.IsOpened = true;

                this.Close();
            }
        }

        private void getHelpButton_Click(object sender, RoutedEventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }
    }
}
