using System;
using System.ServiceModel;

namespace MatixGameForm
{
    [ServiceContract(Namespace = "http://www.xblip.com")]
    interface IPingable 
    {
        [OperationContract(IsOneWay=true)]
        void Ping();
    }
}
