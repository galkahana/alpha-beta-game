using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Net;

namespace MatixGameForm
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
                     InstanceContextMode = InstanceContextMode.PerCall,
                     UseSynchronizationContext = false)]
    class PingableService : IPingable
    {
 
        #region IPingable Members

        public void Ping()
        {
            string thisHostName = Dns.GetHostName();

            EndpointAddress endpointAddress = OperationContext.Current.IncomingMessageHeaders.ReplyTo;
             try
             {
                 MatixSharedGameServices.RemoteAddressesCollectorClient pingOriginator =
                     new MatixSharedGameServices.RemoteAddressesCollectorClient(
                            "NetTcpBinding_IRemoteAddressesCollector", endpointAddress);
                 pingOriginator.Open();
                 pingOriginator.CollectRemoteAddress(thisHostName, 
                     URILocalToRemoteTranslator.TranslateHostToMatixGameServiceAddress(thisHostName).ToString());
             }
             catch (Exception)
             {
                 // if something fails..bygones.
             }
        }

        #endregion
    }
}
