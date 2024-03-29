﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MatixGameForm.MatixSharedGameServices
{
    using System.Runtime.Serialization;
    using System;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.xblip.com", ConfigurationName = "MatixGameForm.MatixSharedGameServices.IRemoteAddressesCollector", SessionMode = System.ServiceModel.SessionMode.Required)]
    public interface IRemoteAddressesCollector
    {

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://www.xblip.com/IRemoteAddressesCollector/CollectRemoteAddress")]
        void CollectRemoteAddress(string inRemoteServerName, string inRemoteApproachabeAddressURI);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IRemoteAddressesCollectorChannel : IRemoteAddressesCollector, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class RemoteAddressesCollectorClient : System.ServiceModel.ClientBase<IRemoteAddressesCollector>, IRemoteAddressesCollector
    {

        public RemoteAddressesCollectorClient()
        {
        }

        public RemoteAddressesCollectorClient(string endpointConfigurationName)
            :
                base(endpointConfigurationName)
        {
        }

        public RemoteAddressesCollectorClient(string endpointConfigurationName, string remoteAddress)
            :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public RemoteAddressesCollectorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress)
            :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public RemoteAddressesCollectorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress)
            :
                base(binding, remoteAddress)
        {
        }

        public void CollectRemoteAddress(string inRemoteServerName, string inRemoteApproachabeAddressURI)
        {
            base.Channel.CollectRemoteAddress(inRemoteServerName, inRemoteApproachabeAddressURI);
        }
    }

}
