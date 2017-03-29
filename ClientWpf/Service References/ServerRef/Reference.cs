﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientWpf.ServerRef {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServerRef.IChat", CallbackContract=typeof(ClientWpf.ServerRef.IChatCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IChat {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/LogginUser")]
        void LogginUser(string name);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/SendMessageToAll")]
        void SendMessageToAll(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChat/CheckUser", ReplyAction="http://tempuri.org/IChat/CheckUserResponse")]
        bool CheckUser(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChat/GetConnectionsCount", ReplyAction="http://tempuri.org/IChat/GetConnectionsCountResponse")]
        int GetConnectionsCount();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChat/GetMaxConnetionsNumber", ReplyAction="http://tempuri.org/IChat/GetMaxConnetionsNumberResponse")]
        int GetMaxConnetionsNumber();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/GetMessage")]
        void GetMessage(string str);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/SendMessage")]
        void SendMessage(string str);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/AddUserToBox")]
        void AddUserToBox(string str);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/ErrorMessage")]
        void ErrorMessage(string msg);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/CallBackIsNotUnicException")]
        void CallBackIsNotUnicException();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/NameIsNotUnicException")]
        void NameIsNotUnicException();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatChannel : ClientWpf.ServerRef.IChat, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChatClient : System.ServiceModel.DuplexClientBase<ClientWpf.ServerRef.IChat>, ClientWpf.ServerRef.IChat {
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void LogginUser(string name) {
            base.Channel.LogginUser(name);
        }
        
        public void SendMessageToAll(string message) {
            base.Channel.SendMessageToAll(message);
        }
        
        public bool CheckUser(string userName) {
            return base.Channel.CheckUser(userName);
        }
        
        public int GetConnectionsCount() {
            return base.Channel.GetConnectionsCount();
        }
        
        public int GetMaxConnetionsNumber() {
            return base.Channel.GetMaxConnetionsNumber();
        }
    }
}
