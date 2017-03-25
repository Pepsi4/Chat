using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    
   public interface IChatCallBack
    {
        /// <summary>
        /// Get the message and adding it to the chat history text box.
        /// </summary>
        /// <param name="msg"></param>
        [OperationContract(IsOneWay = true)]
        void GetMessage(string str);

        /// <summary>
        /// Sends message to all users what is current online.
        /// </summary>
        /// <param name="msg"></param>
        [OperationContract(IsOneWay = true)]
        void SendMessage(string str);

        /// <summary>
        /// Adds user to the box.
        /// </summary>
        /// <param name="name">User name witch added to the chat box.</param>
        [OperationContract(IsOneWay = true)]
        void AddUserToBox(string str);

        /// <summary>
        /// Equivalent to MessageBox().
        /// </summary>
        /// <param name="msg"></param>
        [OperationContract(IsOneWay = true)]
        void ErrorMessage(string msg);

        [OperationContract(IsOneWay = true)]
        void CallBackIsNotUnicException();

        [OperationContract(IsOneWay = true)]
        void NameIsNotUnicException();
    }
}
