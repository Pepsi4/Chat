﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows.Controls;
using System.Text;
using System.Windows.Markup;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ClientWpf.ServerRef;
using System.Threading;

namespace ClientWpf
{
    //[CallbackBehavior(
    //    UseSynchronizationContext = false)]
    public class ClientLogic : Window, IChatCallback
    {
        #region fields
        //static ChatClient proxy;
        // public static string chatHistoryString;
        static TextBox _chatHistoryBox;
        static TextBox _userNameBox;
        #endregion

        #region the calling exceptions methods from the server
        public void NameIsNotUnicException()
        {
            ErrorMessage("User name is not unic!");
        }

        public void CallBackIsNotUnicException()
        {
            ErrorMessage("CALLBACK IS NOT UNIC - ex");
        }
        #endregion

        public ClientLogic()
        {

        }
        public ClientLogic(TextBox historyBox)
        {
            _chatHistoryBox = historyBox;
        }

        static ChatClient proxy = null;
        //static DuplexChannelFactory<IChat> myChannelFactory;
        //EndpointAddress myEndpoint;
        //WSDualHttpBinding myBinding;

        //static ChatClient client;

        #region public methods for client side
        /// <summary>
        /// Connects to the server.
        /// </summary>
        public void CreateConnection()
        {
            try
            {
                InstanceContext context = new InstanceContext(new ClientLogic());
                proxy = new ChatClient(context);

                    proxy.Open();
                    MessageBox.Show(proxy.State.ToString());
                    proxy.Test("tetas");
               
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Data.ToString());
                MessageBox.Show(ex.GetType().ToString());
            }
        }

        /// <summary>
        /// Makes the proxy state opened.
        /// </summary>
        public void OpenProxy()
        {
            //try
            //{
            //    if (proxy.State != CommunicationState.Opened)
            //    {
                    

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.StackTrace);
            //    MessageBox.Show(ex.Message);
            //    MessageBox.Show(ex.Data.ToString());
            //    MessageBox.Show(ex.GetType().ToString());
            //}

        }

        /// <summary>
        /// Closes the connection with the server.
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                //10 tries to close connection.
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    if (proxy.State == CommunicationState.Opened)
                    {
                        proxy.Close();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Tries to add user to the box. If the user was successful added than server will call method AddUserToBox().
        /// </summary>
        /// <param name="name">User name wich is trying to add.</param>
        public void TryAddUser(string name)
        {
            //todo: button enabled = false when the thread is not ready yet
            //todo: button enabled = true when the thread is done
            //todo: if the state is closed - open it

            if (proxy.State != CommunicationState.Opened)
            {
                try
                {
                    CreateConnection();
                    OpenProxy();
                }
                //If we cannot to connect to the server
                catch (Exception ex)
                {

                    //exit from the function
                    return;
                }
            }

            ThreadPool.QueueUserWorkItem(s =>
            {
                //max time to connect
                proxy.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 30);

                try
                {
                    if (proxy.CheckUser(name))
                    {
                        //_userNameBox.Text = name;
                    }
                }
                catch (Exception ex)
                {

                }

            });
        }
        #endregion


        #region callback methods

        /// <summary>
        /// Adds user to the box.
        /// </summary>
        /// <param name="name">User name witch added to the chat box.</param>
        public void AddUserToBox(string name)
        {
            //chatHistoryString += $"{name} connected to the chat! \n";
            GetMessage($"{name} connected to the chat! \n");
        }

        /// <summary>
        /// Sends message to all users what is current online.
        /// </summary>
        /// <param name="msg"></param>
        public void SendMessage(string msg)
        {
            try
            {
                proxy.SendMessageToAll(msg);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// Get the message and adding it to the chat history text box.
        /// </summary>
        /// <param name="msg"></param>
        public void GetMessage(string msg)
        {
            _chatHistoryBox.Text += msg + "\n";
        }

        /// <summary>
        /// Equivalent to MessageBox().
        /// </summary>
        /// <param name="msg"></param>
        public void ErrorMessage(string msg)
        {
            MessageBox.Show(msg);
        }
        #endregion
    }
}