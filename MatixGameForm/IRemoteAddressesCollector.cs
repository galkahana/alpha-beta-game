using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace MatixGameForm
{
    [ServiceContract(Namespace = "http://www.xblip.com",
                     SessionMode = SessionMode.Required)]
    interface IRemoteAddressesCollector
    {
        [OperationContract(IsOneWay=true)]
        void CollectRemoteAddress(string inRemoteServerName,string inRemoteApproachabeAddressURI);
    }
}
