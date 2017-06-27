using System.IO;
using System.Windows;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for HelpForm.xaml
    /// </summary>
    public partial class HelpForm : Window
    {
        public HelpForm()
        {
            InitializeComponent();
            string helpText = File.ReadAllText("HelpText.txt");
            HelpTextBox.Text = helpText;
        }
    }
}
