using System;
using System.Collections.Generic;
using System.Text;

namespace MatixGameForm
{
    public class DummyCallBack : MatixSharedGameServices.IMatixSharedGameCallback
    {

        #region IMatixSharedGameCallack Members

        public void ConnectToGame(MatixSharedGameServices.MatixSharedGameInformation inSharedGameInformation)
        {
        }

        public void RefuseConnectionToGame()
        {

        }

        public void DisconnectGame()
        {

        }

        public void ExecuteMoveOnPlayer(MatixSharedGameServices.MatixMove inMatixMove,
                                        MatixSharedGameServices.MatixPlayer inMatixPlayer)
        {

        }

        public void PingPlayer()
        {

        }

        #endregion
    }
}
