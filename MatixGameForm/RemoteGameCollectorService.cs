using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Configuration;
using System.Net;

namespace MatixGameForm
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
                    InstanceContextMode = InstanceContextMode.Single,
                    UseSynchronizationContext = true)]
    class RemoteGameCollectorService : IRemoteAddressesCollector
    {
        internal RemoteGameCollectorService()
        {
            mGameAddresses = new List<RemoteAddressEntry>();
            string waitTimeString = ConfigurationManager.AppSettings.Get("GameSearchTimout");
            if (waitTimeString.Length == 0)
                mWaitTime = 2000;
            else
                mWaitTime = Int32.Parse(waitTimeString);

        }

        internal List<RemoteAddressEntry> GameAddresses
        {
            get { return mGameAddresses; }
        }

        internal string LastConnectionErrorMessage
        {
            get { return mLastConnectionErrorMessage; }
        }

        #region IRemoteAddressesCollector Members

        public void CollectRemoteAddress(string inRemoteServerName, string inRemoteApproachabeAddressURI)
        {
            lock (new Object())
            {
                if(!mLocalhostCollected && (inRemoteServerName == mLocalhostName))
                    mLocalhostCollected = true;
                mGameAddresses.Add(new RemoteAddressEntry(inRemoteServerName, inRemoteApproachabeAddressURI));
            }
        }

        #endregion
       
        private List<RemoteAddressEntry> mGameAddresses;
        private string mLastConnectionErrorMessage;
        private ServiceHost mServiceHost;
        private bool mIsAborting;
        private int mWaitTime;
        private bool mLocalhostCollected;
        private string mLocalhostName;

        public bool CollectAddresses()
        {
            bool statusOK = false;
            mIsAborting = false;
            mLocalhostCollected = false;
            mLocalhostName = Dns.GetHostName();
            mGameAddresses.Clear();
            do
            {

                if (!StartRecievingService())
                    break;
                if (mIsAborting)
                    break;
                if (!SendMulticastRequest())
                    break;
                if (mIsAborting)
                    break; 
                WaitForResponses();
                if (mIsAborting)
                    break; 
                statusOK = true;
            } while (false);
            EndRecievingService();
            if(!mIsAborting)
                CheckLocalhostGame();
            return statusOK;
        }

        private void CheckLocalhostGame()
        {
            // on some computers i saw that localhost does not get UDP multicast messages. that is
            // if localhost is sending a multicast message it's not recieving it. To check localhost 
            // see if the local computer is in the list or not. 
            if (!mLocalhostCollected)
            {
                bool connectionOK = false;
                EndpointAddress endpointAddress =
                    new EndpointAddress(
                               URILocalToRemoteTranslator.TranslateHostToMatixGameServiceAddress(mLocalhostName));
                try
                {
                    MatixSharedGameServices.MatixSharedGameClient gameClient = new MatixSharedGameServices.MatixSharedGameClient(
                        new InstanceContext(new DummyCallBack()),
                        "NetTcpBinding_IMatixSharedGame",
                        endpointAddress);
                    gameClient.Open();
                    connectionOK = true;
                }
                catch (Exception ex)
                {
                    mLastConnectionErrorMessage = ex.Message;
                }
                if (connectionOK)
                {
                    CollectRemoteAddress(mLocalhostName, endpointAddress.Uri.ToString());
                }

            }
        }

        private void EndRecievingService()
        {
            if (mServiceHost != null && mServiceHost.State != CommunicationState.Closed)
            {
                mServiceHost.Close();
                mServiceHost = null;
            }
        }

        private void WaitForResponses()
        {
            // The waiting waits a maximum of mWaitTime X 10 seconds. 
            // This is done by waiting mWaitTime seconds 10 times
            // after each time, if there was no response in this interval
            // it assumes that this means that there will be no more
            // and stops.
            int currentListSize = mGameAddresses.Count;
            for (int i = 0; i < 10 && !mIsAborting; ++i)
            {
                System.Threading.Thread.Sleep(mWaitTime);
                if (currentListSize == mGameAddresses.Count)
                    break; 
                else
                    currentListSize = mGameAddresses.Count;
            }
        }

        private bool SendMulticastRequest()
        {
            MatixSharedGameServices.PingableClient multicastClient = new MatixSharedGameServices.PingableClient();

            ServiceEndpoint returnEndPoint = 
                mServiceHost.Description.Endpoints.Find(typeof(IRemoteAddressesCollector));
            if (returnEndPoint == null)
                return false;

            using (new OperationContextScope((IContextChannel)multicastClient.InnerChannel))
            {
                 OperationContext.Current.OutgoingMessageHeaders.ReplyTo =
                    new EndpointAddress(URILocalToRemoteTranslator.Translate(returnEndPoint.ListenUri));
                multicastClient.Ping();
            }
            return true;
        }

        private bool StartRecievingService()
        {
            mServiceHost = new ServiceHost(this);
            try
            {
                mServiceHost.Open();
            }
            catch (System.ServiceModel.AddressAlreadyInUseException)
            {
                mServiceHost = null;
                mLastConnectionErrorMessage = "There's a already a shared game in session on this computer. Only one game may be shared on a single computer.";
            }
            catch (Exception ex)
            {
                mServiceHost = null;
                mLastConnectionErrorMessage = ex.Message;
            }
            return mServiceHost != null;
        }

        internal void Abort()
        {
            mIsAborting = true;
        }
    }


    class RemoteAddressEntry
    {
        internal RemoteAddressEntry(string inRemoteServerName,string inRemoteApproachabeAddressURI)
        {
            RemoteServerName = inRemoteServerName;
            RemoteApproachableAddressURI = inRemoteApproachabeAddressURI;
        }

        public string RemoteServerName;
        public string RemoteApproachableAddressURI;

        public override string ToString()
        {
            return RemoteServerName;
        }
    }
}
