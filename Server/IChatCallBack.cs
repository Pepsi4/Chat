﻿using System.Collections.Generic;
using System.ServiceModel;

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
        void SendMessage(string str, string name);

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

        [OperationContract(IsOneWay = false)]
        bool SendPing();
    }
}
