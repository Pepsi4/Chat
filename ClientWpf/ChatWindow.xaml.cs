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

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ClientLogic logic = new ClientLogic();
            logic.SendMessage(MessageBox.Text, UserName);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
