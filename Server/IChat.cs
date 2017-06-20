using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace Server
{
    [ServiceContract(CallbackContract = typeof(IChatCallBack), SessionMode = SessionMode.Required)]
    
    interface IChat
    {
        /// <summary>
        /// If the user joined. Adds to the dictionary username and callback. Server side.
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void LogginUser(string name);

        /// <summary>
        /// Tries to call on all callbacks that was registred: method GetMessage(msg) on user`s side.
        /// </summary>
        /// <param name="msg"></param>
        [OperationContract(IsOneWay = true)]
        void SendMessageToAll(string message);

        /// <summary>
        /// Checks if the element with the key is already exists. Or any others problems with the name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>true if we added callback and userName to the our dictionary</returns>
        [OperationContract(IsOneWay = false)]
        bool CheckUser(string userName);

        /// <summary>
        /// Gets count of current connetions.
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        int GetConnectionsCount();

        /// <summary>
        /// Retruns the value of max connections to the server what is possible.
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        int GetMaxConnetionsNumber();

        /// <summary>
        /// Use this method if you can't send IChatCallBack
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void UnLogginUser();

        /// <summary>
        /// Const which is showing how much connections could be.
        /// </summary>
        [DataMember]
        int MaxConnections { get; set; }

        [DataMember]
        int CurrentConnections { get; set; }
    }
}
