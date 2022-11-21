using System;
using System.ServiceModel;

namespace MatixGameForm
{
    [ServiceContract(Namespace = "http://www.xblip.com", 
                     CallbackContract = typeof(IMatixSharingPlayer), 
                     SessionMode = SessionMode.Required)]
    public interface IMatixSharedGame
    {
        // InitiatePlayerConnection is to be called when a player wishes to connect to a game.
         [OperationContract(IsOneWay=true)]
        void InitiatePlayerConnection(MatixPlayer inConnectingPlayer);

        // ExecuteMove is called when the player wishes to make a move. The move will be executed if the move is valid
        // and the acting player is valid for the current state of the game.
        [OperationContract(IsOneWay=true)]
        void ExecuteMove(MatixMove inMatixMove,
                         MatixPlayer inMatixPlayer);
        
        // DisconnectPlayer disconnects an existing player for the game. it will cause the game to pause till another
        // player is connected to take this player place
        [OperationContract(IsOneWay=true)]
        void DisconnectPlayer(MatixPlayer inMatixPlayer);

        // Ping. use for determining whether the shared game is still shared.
        // not one way, should return! 
        [OperationContract()]
        void Ping();

        // Get the game features for display. This is to be used prior to connecting the game for allowing
        // a user to choose which player to play (if there is such a choice).
        [OperationContract()]
        MatixSharedGameOptions GetGameFeatures();        

    }

}

