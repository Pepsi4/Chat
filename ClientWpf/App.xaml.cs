using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using ClientWpf.ServerRef;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application//, IChatCallback
    {
        //public void CallBackFunction(string msg)
        //{
        //    MessageBox.Show(msg);
        //}

        //public void CallMethod()
        //{
        //    InstanceContext context = new InstanceContext(this);
        //    ChatClient proxy = new ChatClient(context);
        //    proxy.UserJoined();
        //}
    }
}
