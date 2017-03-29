using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Threading;

namespace Server
{


    #region exceptions
    public class CallBackIsNotUnic : Exception { }
    public class NameIsNotUnic : Exception { }
    #endregion

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Server : IChat
    {
        //TODO: max connetions sets in code
        
        public int MaxConnections { get; set; } = 5;
        
        public int CurrentConnections { get; set; } = 0;

        static Dictionary<IChatCallBack, string> _userNames =
        new Dictionary<IChatCallBack, string>();

        /// <summary>
        /// Checks if the element with the key is already exists. Or any others problems with the name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>true if we added callback and userName to the our dictionary</returns>
        public bool CheckUser(string userName)
        {
            var callback =
            OperationContext.Current.GetCallbackChannel<IChatCallBack>();

            if (userName == null)
            {
                //todo: callback.username exc
                return false;
            }

            if (!IsCallBackUnic(callback))
            {
                callback.CallBackIsNotUnicException();
                return false;
            }

            if (!IsNameUnic(userName))
            {
                callback.NameIsNotUnicException();
                return false;
            }

            LogginUser(userName);
            return true;
        }

        /// <summary>
        /// Returns true if we haven't this name in main the dictionary.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool IsNameUnic(string userName)
        {
            if (_userNames.ContainsValue(userName))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if we haven't this callback(context) in the main dictionary.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        private bool IsCallBackUnic(IChatCallBack callback)
        {
            if (_userNames.ContainsKey(callback))
            {
                return false;
            }

            return true;
        }
        
        //todo: method that checks is user online or not
        /// <summary>
        /// Deletes from the our dictionary the callback and username.
        /// </summary>
        public void UnLogginUser(IChatCallBack callback)
        {
            _userNames.Remove(callback);
            CurrentConnections--;
        }

        /// <summary>
        /// Gets count of current connetions.
        /// </summary>
        /// <returns></returns>
        public int GetConnectionsCount()
        {
            return CurrentConnections;
        }

        /// <summary>
        /// Retruns the value of max connections to the server what is possible.
        /// </summary>
        /// <returns></returns>
        public int GetMaxConnetionsNumber()
        {
            return MaxConnections;
        }

        /// <summary>
        /// Tries to call on all callbacks that was registred: method GetMessage(msg) on user`s side.
        /// </summary>
        /// <param name="msg"></param>
        public void SendMessageToAll(string msg)
        {
            IChatCallBack callback = OperationContext.Current.GetCallbackChannel<IChatCallBack>();

            foreach (IChatCallBack keyValue in _userNames.Keys)
            {
                ThreadPool.QueueUserWorkItem(s =>
                {
                    try
                    {
                        keyValue.GetMessage(msg);
                    }
                    //If we cann't to connect to the current client.
                    catch (TimeoutException)
                    {
                        callback.ErrorMessage("Time out. Try again later.");
                    }
                });
            }
        }

        /// <summary>
        /// If the user joined. Adds to the dictionary username and callback. Server side.
        /// </summary>
        public void LogginUser(string userName)
        {
            try
            {
                IChatCallBack callback = OperationContext.Current.GetCallbackChannel<IChatCallBack>();

                //adding to the dictionary 2 elements: callback and user name.
                try
                {
                    _userNames.Add(callback, userName);
                    CurrentConnections++;
                }
                catch (TimeoutException) { }
                catch (CommunicationObjectAbortedException) { }


                //for each registred callback
                foreach (IChatCallBack callbacks in _userNames.Keys)
                {
                    ThreadPool.QueueUserWorkItem(s =>
                    {
                        try
                        {
                            //Says that the user connects to the chat
                            callbacks.ErrorMessage($"{userName} connected to the chat!");
                        }
                        //if we cannot to connect to the callback
                        catch (TimeoutException) { }
                        catch (CommunicationObjectAbortedException) { }
                    });
                }
            }
            catch (Exception) { }
        }
    }
}

