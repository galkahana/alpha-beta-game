using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace MatixGameForm
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant,
                    InstanceContextMode = InstanceContextMode.Single,
                    UseSynchronizationContext = true)]
    class JoinedGameClient : MatixSharedGameServices.IMatixSharedGameCallback
    {
        private MatixBoardView mMatixBoardView;
        private MatixSharedGameServices.MatixSharedGameClient mSharedGameClient;
        public string mLastConnectionErrorMessage;
        private MatixPlayer mJoinedPlayer;
        private Thread mPingingThread;
        private bool mContinuePinging;

        public JoinedGameClient(MatixBoardView inBoardView)
        {
            mMatixBoardView = inBoardView;
            mSharedGameClient = null;
            mLastConnectionErrorMessage = string.Empty;
            mPingingThread = null;
            mContinuePinging = false;
        }

        public bool StartCalling(string inServerName)
        {
            EndpointAddress endpointAddress = 
                new EndpointAddress(
                       URILocalToRemoteTranslator.TranslateHostToMatixGameServiceAddress(inServerName));
            try
            {
                mSharedGameClient = new MatixSharedGameServices.MatixSharedGameClient(
                    new InstanceContext(this),
                    "NetTcpBinding_IMatixSharedGame",
                    endpointAddress);

                mSharedGameClient.Open();
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                mSharedGameClient = null;
                mLastConnectionErrorMessage = "Unable to connect to " + inServerName + ". The target computer might not be share games."; 
            }
            catch (Exception ex)
            {
                mSharedGameClient = null;
                mLastConnectionErrorMessage = ex.Message;
            }
            return mSharedGameClient != null;
        }

        public void StopCalling()
        {
            StopSharedGamePinging();
            if (mSharedGameClient != null && mSharedGameClient.State != CommunicationState.Closed)
            {
                try
                {
                    mSharedGameClient.Close();
                }
                catch (Exception ex)
                {
                    mLastConnectionErrorMessage = ex.Message;
                    // no need to report. if unavailable it doesn't matter at this point
                }
                mSharedGameClient = null;
            }
        }

        private void StartSharedGamePinging()
        {
            mContinuePinging = true;
            mPingingThread = new Thread(new ThreadStart(PingSharedGame));
            mPingingThread.Start();
        }

        private void PingSharedGame()
        {
            while (mContinuePinging && mSharedGameClient != null)
            {
                try
                {
                    mSharedGameClient.Ping();
                }
                catch (Exception ex)
                {
                    OnPingUnanswered(); 
                    mLastConnectionErrorMessage = ex.Message;
                    break;
                }
                Thread.Sleep(1000);
            }
            mContinuePinging = false;
         }

        private void OnPingUnanswered()
        {
            DisconnectGame();
        }

        private void StopSharedGamePinging()
        {
                mContinuePinging = false;
        }

        public bool JoinGame(MatixPlayer inPlayer,out bool outJoiningSucceeded)
        {
            outJoiningSucceeded = false;
            if (mSharedGameClient != null)
            {
                bool statusOK = true;
                mJoinedPlayer = inPlayer;
                try
                {
                    mSharedGameClient.InitiatePlayerConnection(LocalMatixPlayerToRemoteMatixPlayer(inPlayer));
                    outJoiningSucceeded = true;
                }
                catch (Exception ex)
                {
                    mLastConnectionErrorMessage = ex.Message;
                    statusOK = false;
                }
                return statusOK;
            }
            else
                return false;
        }

        private MatixSharedGameServices.MatixPlayer LocalMatixPlayerToRemoteMatixPlayer(MatixPlayer inPlayer)
        {
            MatixSharedGameServices.MatixPlayer matixPlayer = new MatixSharedGameServices.MatixPlayer();
            matixPlayer.IsRemote = inPlayer.IsRemote;
            matixPlayer.IsRemoteAvailable = inPlayer.IsAvailable;
            matixPlayer.MoveType = (inPlayer.MoveType == EMatixMoveType.eMatixMoveAutomatic ?
                                    MatixSharedGameServices.EMatixMoveType.eMatixMoveAutomatic :
                                    MatixSharedGameServices.EMatixMoveType.eMatixMoveManual);
            matixPlayer.PlayerType = (inPlayer.PlayerType == EMatixPlayerType.eMatixPlayerColumns ?
                                    MatixSharedGameServices.EMatixPlayerType.eMatixPlayerColumns :
                                    MatixSharedGameServices.EMatixPlayerType.eMatixPlayerRows);
            matixPlayer.RemotePlayerName = inPlayer.RemotePlayerName;
            return matixPlayer;
        }

        #region IMatixSharedGameCallback Members

        public void ConnectToGame(MatixSharedGameServices.MatixSharedGameInformation inSharedGameInformation)
        {
            mMatixBoardView.SetupGameWithDetails(inSharedGameInformation);
            StartSharedGamePinging();
        }

        public void RefuseConnectionToGame()
        {
            mMatixBoardView.DisplayRefusedJoining();
        }

        private delegate void VoidCaller();

        public void DisconnectGame()
        {
            mMatixBoardView.RowPlayer.IsRemoteAvailable = !mMatixBoardView.RowPlayer.IsRemote;
            mMatixBoardView.ColumnPlayer.IsRemoteAvailable = !mMatixBoardView.ColumnPlayer.IsRemote;
            StopCalling(); // just stop calling. no need to notify disconnection...cause already disconnecting.
            mMatixBoardView.Invoke(new VoidCaller(mMatixBoardView.DisconnectFromJoinedGame));
        }

        public void ExecuteMoveOnPlayer(MatixSharedGameServices.MatixMove inMatixMove,
                                        MatixSharedGameServices.MatixPlayer inMatixPlayer)
        {
            if (IsWaitingForRemotePlayerMove(inMatixPlayer))
            {
                MatixMove newMatixMove = new MatixMove(inMatixMove.SelectionIndex);
                mMatixBoardView.ExecuteRemoteMove(newMatixMove);
            }
        }

        private bool IsWaitingForRemotePlayerMove(MatixSharedGameServices.MatixPlayer inMatixPlayer)
        {
            MatixPlayer localPlayer = (inMatixPlayer.PlayerType == MatixSharedGameServices.EMatixPlayerType.eMatixPlayerRows) ?
                                       mMatixBoardView.RowPlayer :
                                       mMatixBoardView.ColumnPlayer;
            return !mMatixBoardView.IsGameEnded() &&
                   localPlayer.IsRemote &&
                   mMatixBoardView.GetCurrentPlayerType() == 
                        (inMatixPlayer.PlayerType == MatixSharedGameServices.EMatixPlayerType.eMatixPlayerColumns ? 
                        EMatixPlayerType.eMatixPlayerColumns:
                        EMatixPlayerType.eMatixPlayerRows);
        }


        public void PingPlayer()
        {
            // do nothing. just an excuse for making a call
        }

        #endregion

        internal bool BroadcastMove(MatixMove inMatixMove, MatixPlayer inCurrentPlayer)
        {
            bool result = false;
            if (mSharedGameClient != null)
            {
                MatixSharedGameServices.MatixMove newMatixMove = new MatixSharedGameServices.MatixMove();
                newMatixMove.SelectionIndex = inMatixMove.SelectionIndex;
                try
                {
                    mSharedGameClient.ExecuteMove(newMatixMove, LocalMatixPlayerToRemoteMatixPlayer(inCurrentPlayer));
                    result = true;
                }
                catch (Exception ex)
                {
                    mLastConnectionErrorMessage = ex.Message;
                }
            }
            return result;
        }

        internal bool Disconnect()
        {
            bool statusOK = true;
            if (mSharedGameClient != null)
            {
                try
                {
                    mSharedGameClient.DisconnectPlayer(LocalMatixPlayerToRemoteMatixPlayer(mJoinedPlayer));
                }
                catch (Exception ex)
                {
                    statusOK = false;
                    mLastConnectionErrorMessage = ex.Message;
                }
            }
            return statusOK;
        }

        internal void Terminate()
        {
            if (mPingingThread != null)
            {
                mContinuePinging = false;
                mPingingThread.Join();
            }
        }
    }
}
