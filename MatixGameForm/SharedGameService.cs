using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace MatixGameForm
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant,
                     InstanceContextMode= InstanceContextMode.Single,
                     UseSynchronizationContext=true)]
    class SharedGameService : IMatixSharedGame
    {
        private MatixBoardView mMatixBoardView;
        private ServiceHost mServiceHost;
        private ServiceHost mPublisherServiceHost;
        public string mLastConnectionErrorMessage;
        private IMatixSharingPlayer mRowPlayerCallback;
        private IMatixSharingPlayer mColumnPlayerCallback;
        private MatixSharedGameInformation mSharedGameInformation;
        private Thread mPingingThread;
        private bool mContinuePinging;

        public SharedGameService()
        {
            // dummy loader, just for mex discovery
        }
        
        public SharedGameService(MatixBoardView inBoardView)
        {
            mMatixBoardView = inBoardView;
            mServiceHost = null;
            mRowPlayerCallback = null;
            mColumnPlayerCallback = null;
            mLastConnectionErrorMessage = string.Empty;
            mPingingThread = null;
            mContinuePinging = false;
        }

        public bool StartListening(MatixSharedGameInformation inSharedGameInformation)
        {
            mSharedGameInformation = inSharedGameInformation;
            mServiceHost = new ServiceHost(this);
            mPublisherServiceHost = new ServiceHost(typeof(PingableService));

            try
            {
                mServiceHost.Open();
                mPublisherServiceHost.Open();
            }
            catch (System.ServiceModel.AddressAlreadyInUseException)
            {
                mServiceHost = null;
                mPublisherServiceHost = null;
                mLastConnectionErrorMessage = "There's a already a shared game in session on this computer. Only one game may be shared on a single computer.";
            }
            catch (Exception ex)
            {
                mServiceHost = null;
                mPublisherServiceHost = null;
                mLastConnectionErrorMessage = ex.Message;
            }
            if (mServiceHost != null)
                StartSharedGamePinging();
            return mServiceHost != null;
        }

        private void StartSharedGamePinging()
        {
            mContinuePinging = true;
            mPingingThread = new Thread(new ThreadStart(PingShareingPlayers));
            mPingingThread.Start();
        }

        private void PingShareingPlayers()
        {
            while (mContinuePinging && mServiceHost != null)
            {
                if(mRowPlayerCallback != null)
                    PingPlayer(mRowPlayerCallback,mMatixBoardView.RowPlayer);
                if(mColumnPlayerCallback != null)
                    PingPlayer(mColumnPlayerCallback,mMatixBoardView.ColumnPlayer);
                Thread.Sleep(1000);
            }
            mContinuePinging = false;
        }

        void PingPlayer(IMatixSharingPlayer inRemotePlayerCaller, MatixPlayer inPlayer)
        {
            try
            {
                inRemotePlayerCaller.PingPlayer();
            }
            catch (Exception ex)
            {
                mContinuePinging = false;
                OnPingUnanswered(inPlayer);
                mLastConnectionErrorMessage = ex.Message;
            }
        }

        void OnPingUnanswered(MatixPlayer inPlayer)
        {
            DisconnectPlayer(inPlayer);
        }

        public void StopListening()
        {
            StopSharedGamePinging();
            if (mPublisherServiceHost != null && mPublisherServiceHost.State != CommunicationState.Closed)
            {
                mPublisherServiceHost.Close();
                mPublisherServiceHost = null;
            } 
            if (mServiceHost != null && mServiceHost.State != CommunicationState.Closed)
            {
                mServiceHost.Close();
                mServiceHost = null;
            }
        }

        private void StopSharedGamePinging()
        {
            mContinuePinging = false;
        }

        #region IMatixSharedGame Members

        public void InitiatePlayerConnection(MatixPlayer inConnectingPlayer)
        {
            IMatixSharingPlayer callback = OperationContext.Current.GetCallbackChannel<IMatixSharingPlayer>();
            if (callback != null)
            {
                if (IsPlayerOKForConnectingSharedGame(inConnectingPlayer))
                {
                    SaveConnectingPlayerInformation(inConnectingPlayer, callback);
                    try
                    {
                        callback.ConnectToGame(mSharedGameInformation);
                    }
                    catch (Exception ex)
                    {
                        SaveConnectingPlayerInformation(inConnectingPlayer, null);
                        mLastConnectionErrorMessage = ex.Message;
                    }
                    mMatixBoardView.ContinueGameIfPossible();
                }
                else
                {
                    try
                    {
                        callback.RefuseConnectionToGame();
                    }
                    catch (Exception ex)
                    {
                        mLastConnectionErrorMessage = ex.Message;
                    }
                }
            }
        }

        private bool IsPlayerOKForConnectingSharedGame(MatixPlayer inConnectingPlayer)
        {
            MatixPlayer localPlayer = (EMatixPlayerType.eMatixPlayerColumns == inConnectingPlayer.PlayerType) ?
                                        mMatixBoardView.ColumnPlayer :
                                        mMatixBoardView.RowPlayer;
            bool result = false;
            // must be an atomic operation!
            lock(new Object())
            {
                if (!localPlayer.IsAvailable)
                {
                    localPlayer.IsRemoteAvailable = true;
                    result = true;
                }
            }
            return result;
        }
    
        private void SaveConnectingPlayerInformation(MatixPlayer inConnetingPlayer,IMatixSharingPlayer inCallback)
        {
            if (EMatixPlayerType.eMatixPlayerColumns == inConnetingPlayer.PlayerType)
            {
                mColumnPlayerCallback = inCallback;
                mMatixBoardView.ColumnPlayer.IsRemoteAvailable = (inCallback != null);
                mMatixBoardView.ColumnPlayer.RemotePlayerName = inConnetingPlayer.RemotePlayerName;
            }
            else
            {
                mRowPlayerCallback = inCallback;
                mMatixBoardView.RowPlayer.IsRemoteAvailable = (inCallback != null);
                mMatixBoardView.RowPlayer.RemotePlayerName = inConnetingPlayer.RemotePlayerName;
            }
        }

        public void ExecuteMove(MatixMove inMatixMove, MatixPlayer inMatixPlayer)
        {
            if (IsWaitingForRemotePlayerMove(inMatixPlayer))
            {
                SendMoveToOtherRemotePlayer(inMatixPlayer,inMatixMove);
                mMatixBoardView.ExecuteRemoteMove(inMatixMove);
            }
        }

        private void SendMoveToOtherRemotePlayer(MatixPlayer inMatixPlayer, MatixMove inMatixMove)
        {
            MatixPlayer otherLocalPlayer = (inMatixPlayer.PlayerType == EMatixPlayerType.eMatixPlayerRows) ?
                                      mMatixBoardView.ColumnPlayer :
                                      mMatixBoardView.RowPlayer;

            if (otherLocalPlayer.IsRemote && otherLocalPlayer.IsRemoteAvailable)
            {
                IMatixSharingPlayer otherCallback = (inMatixPlayer.PlayerType == EMatixPlayerType.eMatixPlayerRows) ?
                                      mColumnPlayerCallback :
                                      mRowPlayerCallback;
                if (otherCallback != null)
                {
                    try
                    {
                        otherCallback.ExecuteMoveOnPlayer(inMatixMove, inMatixPlayer);
                    }
                    catch (Exception ex)
                    {
                        mLastConnectionErrorMessage = ex.Message;
                        DisconnectPlayer(inMatixPlayer);
                    }
                }
            }
        }

        private bool IsWaitingForRemotePlayerMove(MatixPlayer inMatixPlayer)
        {
            MatixPlayer localPlayer = (inMatixPlayer.PlayerType == EMatixPlayerType.eMatixPlayerRows) ?
                                       mMatixBoardView.RowPlayer :
                                       mMatixBoardView.ColumnPlayer;
            return !mMatixBoardView.IsGameEnded() &&
                   localPlayer.IsRemote &&
                   mMatixBoardView.GetCurrentPlayerType() == inMatixPlayer.PlayerType;
        }

        private delegate void VoidCaller();

        public void DisconnectPlayer(MatixPlayer inMatixPlayer)
        {
            SaveConnectingPlayerInformation(inMatixPlayer, null);
            mMatixBoardView.Invoke(new VoidCaller(mMatixBoardView.ContinueGameIfPossible));
        }

        public void Ping()
        {
            // do nothing. just a stub method for someone else to check if we're still here.
            // in the future i might add ID or something to make sure this is the right game, if we're not sure.
        }

        public MatixSharedGameOptions GetGameFeatures()
        {
            MatixSharedGameOptions sharedGameOptions = mMatixBoardView.SharedGameOptions.Clone() ;

            bool rowPlayerAvailable = mMatixBoardView.RowPlayer.IsAvailable;
            bool columnPlayerAvailable = mMatixBoardView.ColumnPlayer.IsAvailable;

            if (rowPlayerAvailable || columnPlayerAvailable)
            {
                switch (sharedGameOptions.mSharedPlayers)
                {
                    case ESharedPlayersOptions.eBothShared:
                        {
                            if (rowPlayerAvailable && columnPlayerAvailable)
                                sharedGameOptions.mSharedPlayers = ESharedPlayersOptions.eNoneShared;
                            else if (rowPlayerAvailable)
                                sharedGameOptions.mSharedPlayers = ESharedPlayersOptions.eColumnShared;
                            else if (columnPlayerAvailable)
                                sharedGameOptions.mSharedPlayers = ESharedPlayersOptions.eRowShared;
                            break;
                        }
                    case ESharedPlayersOptions.eRowShared:
                        {
                            if (rowPlayerAvailable)
                                sharedGameOptions.mSharedPlayers = ESharedPlayersOptions.eNoneShared;
                            break;
                        }
                    case ESharedPlayersOptions.eColumnShared:
                        {
                            if (columnPlayerAvailable)
                                sharedGameOptions.mSharedPlayers = ESharedPlayersOptions.eNoneShared;
                            break;
                        }
                }
            }
            return sharedGameOptions;
        }

        #endregion

        internal void BroadcastMove(MatixMove inMove,MatixPlayer currentPlayer)
        {
            IMatixSharingPlayer callbackToCall = (currentPlayer.PlayerType == EMatixPlayerType.eMatixPlayerColumns ?
                                                                            mRowPlayerCallback :
                                                                            mColumnPlayerCallback);
            if (callbackToCall != null)
            {
                try
                {
                    callbackToCall.ExecuteMoveOnPlayer(inMove, currentPlayer);
                }
                catch (Exception ex)
                {
                    mLastConnectionErrorMessage = ex.Message;
                    DisconnectPlayer(currentPlayer.PlayerType == EMatixPlayerType.eMatixPlayerColumns ?
                                                                            mMatixBoardView.RowPlayer :
                                                                            mMatixBoardView.ColumnPlayer);
                }
            }   
        }

        internal void Disconnect()
        {
            if (mRowPlayerCallback != null)
            {
                try
                {
                    mRowPlayerCallback.DisconnectGame();
                }
                catch (Exception ex)
                {
                    mLastConnectionErrorMessage = ex.Message;
                }
                mRowPlayerCallback = null;
            }
            if (mColumnPlayerCallback != null)
            {
                try
                {
                    mColumnPlayerCallback.DisconnectGame();
                }
                catch (Exception ex)
                {
                    mLastConnectionErrorMessage = ex.Message;
                }
                mColumnPlayerCallback = null;
            }
        }

        internal void Terminate()
        {
            if (mPingingThread != null)
                mPingingThread.Join();
        }
    }
}
