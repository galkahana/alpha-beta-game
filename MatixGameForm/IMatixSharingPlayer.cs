using System;
using System.ServiceModel;

namespace MatixGameForm
{
    public interface IMatixSharingPlayer
    {
        [OperationContract(IsOneWay = true)]
        void ConnectToGame(MatixSharedGameInformation inGameInformation);

        [OperationContract(IsOneWay = true)]
        void RefuseConnectionToGame();

        [OperationContract(IsOneWay = true)]
        void ExecuteMoveOnPlayer(MatixMove inMatixMove,
                         MatixPlayer inMatixPlayer);
        // not one way!
        [OperationContract(IsOneWay = true)]
        void PingPlayer();

        [OperationContract(IsOneWay = true)]
        void DisconnectGame();
    }

} 