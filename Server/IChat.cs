using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace Server
{
    [ServiceContract(CallbackContract = typeof(IChatCallBack), SessionMode = SessionMode.Required)]
    interface IChat
    {
        [OperationContract(IsOneWay = true)]
        void Test(string message);

        [OperationContract(IsOneWay = true)]
        void AddUser(string name);

        [OperationContract(IsOneWay = true)]
        void SendMessageToAll(string message);

        [OperationContract(IsOneWay = false)]
        bool CheckUser(string userName);
    }
}
