using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Server
{
    #region exceptions

    public class CallBackIsNotUnic : Exception
    {
    }

    public class NameIsNotUnic : Exception
    {
    }

    #endregion

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Server : IChat
    {
        //TODO: max connetions sets in code
        static List<Client> _users = new List<Client>();

        public int MaxConnections { get; set; } = 10;
        public int CurrentConnections { get; set; } = 0;

        /// <summary>
        /// Bool var which shows if our server is pinging users or not.
        /// </summary>
        public bool IsPingingUsers { get; set; }

        private void StartPingUsers()
        {
            TimerCallback timerCallBack = new TimerCallback(PingUsers);
            Timer timer = new Timer(timerCallBack, null, 0, 1000);
            IsPingingUsers = true;
        }

        /// <summary>
        /// Checks the every callback if it's online. If not we should unloggin the user.
        /// </summary>
        public void PingUsers(object obj)
        {
            _users.ForEach(delegate (Client client)
            {
                ThreadPool.QueueUserWorkItem(s =>
                {
                    try
                    {
                        client.Callback.SendPing();
                    }
                    catch (Exception ex)
                    {
                        if (client.OthersNotified == false)
                        {
                            client.Online = false;
                            SendMessage($"{client.Name} has left!");
                            client.OthersNotified = true;
                        }
                    }
                });
            });
        }

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
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Name == userName)
                {
                    return false;
                }
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
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Callback == callback)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Deletes from the our dictionary the callback and username.
        /// </summary>
        public void UnLogginUser(IChatCallBack callback)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Callback == callback)
                {
                    // Makes user offline now. 
                    _users[i].Online = false;
                }
            }
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
        /// Pings everyone and sends to the online users message.
        /// </summary>
        /// <param name="msg"></param>
        public void SendMessageToAll(string msg)
        {
            SendMessage(msg);
        }

        /// <summary>
        /// Tries to call on all callbacks that was registred: method GetMessage(msg) on user`s side.
        /// </summary>
        /// <param name="msg"></param>
        private void SendMessage(string msg)
        {
            _users.ForEach(delegate (Client currentClient)
            {
                ThreadPool.QueueUserWorkItem(s =>
                {
                    try
                    {
                        currentClient.Callback.GetMessage(msg);
                    }
                    catch (Exception)
                    {
                        // If we got any error -- we should make sure that client is turned offline.
                        UnLogginUser(currentClient.Callback);
                    }
                });
            });
        }

        /// <summary>
        /// If the user joined. Adds to the dictionary username and callback. Server side.
        /// </summary>
        public void LogginUser(string userName)
        {
            // Creates new class.
            Client client = new Client();
            // Changes the properties.
            client.Name = userName;
            client.Online = true;
            client.RegTime = DateTime.Now;

            // Adds to the list.
            _users.Add(client);
            CurrentConnections++;

            //Starts the ping process.
            TryStartPingUsers();

            //for each registred callback
            _users.ForEach(delegate (Client currentClient)
            {
                ThreadPool.QueueUserWorkItem(s =>
                {
                    try
                    {
                        currentClient.Callback.ErrorMessage($"{userName} connected to the chat!");
                    }
                    catch (TimeoutException)
                    {
                    }
                    catch (CommunicationObjectAbortedException)
                    {
                    }
                    catch (Exception)
                    {
                    }
                });
            });
        }

        /// <summary>
        /// Trying to start to ping users if the server did not do this yet.
        /// </summary>
        private void TryStartPingUsers()
        {
            if (!IsPingingUsers)
            {
                StartPingUsers();
            }
        }

        /// <summary>
        /// Client info.
        /// </summary>
        public class Client
        {
            public DateTime RegTime;
            public string Name { get; set; }
            public bool Online { get; set; }
            public bool OthersNotified { get; set; }

            /// <summary>
            /// If the others knows thw { get; set; }
            /// </summary>
            public IChatCallBack Callback { get; set; }

            public Client()
            {
                Callback = OperationContext.Current.GetCallbackChannel<IChatCallBack>();
                RegTime = DateTime.Now;
            }

            public static int[] InvertValues(int[] input)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    input[i] = input[i] - 2 * input[i];
                }
                return input;
            }
        }
    }
}
