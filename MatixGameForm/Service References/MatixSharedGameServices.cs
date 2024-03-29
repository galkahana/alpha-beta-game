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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    [System.SerializableAttribute()]
    public partial class MatixPlayer : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsRemoteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsRemoteAvailableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatixGameForm.MatixSharedGameServices.EMatixMoveType MoveTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatixGameForm.MatixSharedGameServices.EMatixPlayerType PlayerTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemotePlayerNameField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsRemote
        {
            get
            {
                return this.IsRemoteField;
            }
            set
            {
                this.IsRemoteField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsRemoteAvailable
        {
            get
            {
                return this.IsRemoteAvailableField;
            }
            set
            {
                this.IsRemoteAvailableField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MatixGameForm.MatixSharedGameServices.EMatixMoveType MoveType
        {
            get
            {
                return this.MoveTypeField;
            }
            set
            {
                this.MoveTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MatixGameForm.MatixSharedGameServices.EMatixPlayerType PlayerType
        {
            get
            {
                return this.PlayerTypeField;
            }
            set
            {
                this.PlayerTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RemotePlayerName
        {
            get
            {
                return this.RemotePlayerNameField;
            }
            set
            {
                this.RemotePlayerNameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    public enum EMatixMoveType : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        eMatixMoveManual = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        eMatixMoveAutomatic = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    public enum EMatixPlayerType : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        eNull = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        eMatixPlayerRows = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        eMatixPlayerColumns = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    [System.SerializableAttribute()]
    public partial class MatixMove : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SelectionIndexField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SelectionIndex
        {
            get
            {
                return this.SelectionIndexField;
            }
            set
            {
                this.SelectionIndexField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    [System.SerializableAttribute()]
    public partial class MatixSharedGameOptions : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string mSharedGameNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatixGameForm.MatixSharedGameServices.ESharedPlayersOptions mSharedPlayersField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string mSharedGameName
        {
            get
            {
                return this.mSharedGameNameField;
            }
            set
            {
                this.mSharedGameNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MatixGameForm.MatixSharedGameServices.ESharedPlayersOptions mSharedPlayers
        {
            get
            {
                return this.mSharedPlayersField;
            }
            set
            {
                this.mSharedPlayersField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    public enum ESharedPlayersOptions : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        eRowShared = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        eColumnShared = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        eBothShared = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        eNoneShared = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    [System.SerializableAttribute()]
    public partial class MatixSharedGameInformation : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatixGameForm.MatixSharedGameServices.MatixStage GameStageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatixGameForm.MatixSharedGameServices.MatixBoard SharedBoardField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatixGameForm.MatixSharedGameServices.EMatixPlayerType StartingPlayerField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MatixGameForm.MatixSharedGameServices.MatixStage GameStage
        {
            get
            {
                return this.GameStageField;
            }
            set
            {
                this.GameStageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MatixGameForm.MatixSharedGameServices.MatixBoard SharedBoard
        {
            get
            {
                return this.SharedBoardField;
            }
            set
            {
                this.SharedBoardField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MatixGameForm.MatixSharedGameServices.EMatixPlayerType StartingPlayer
        {
            get
            {
                return this.StartingPlayerField;
            }
            set
            {
                this.StartingPlayerField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    [System.SerializableAttribute()]
    public partial class MatixStage : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatixGameForm.MatixSharedGameServices.EMatixPlayerType CurrentTurnPlayerTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LatestSelectionIndexField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatixGameForm.MatixSharedGameServices.EMatixPlayerType LatestSelectionTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<MatixGameForm.MatixSharedGameServices.EMatixPlayerType, MatixGameForm.MatixSharedGameServices.PlayerFeatures> PlayerFeaturesField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MatixGameForm.MatixSharedGameServices.EMatixPlayerType CurrentTurnPlayerType
        {
            get
            {
                return this.CurrentTurnPlayerTypeField;
            }
            set
            {
                this.CurrentTurnPlayerTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int LatestSelectionIndex
        {
            get
            {
                return this.LatestSelectionIndexField;
            }
            set
            {
                this.LatestSelectionIndexField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MatixGameForm.MatixSharedGameServices.EMatixPlayerType LatestSelectionType
        {
            get
            {
                return this.LatestSelectionTypeField;
            }
            set
            {
                this.LatestSelectionTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<MatixGameForm.MatixSharedGameServices.EMatixPlayerType, MatixGameForm.MatixSharedGameServices.PlayerFeatures> PlayerFeatures
        {
            get
            {
                return this.PlayerFeaturesField;
            }
            set
            {
                this.PlayerFeaturesField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    [System.SerializableAttribute()]
    public partial class MatixBoard : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.ComponentModel.BindingList<long> BoardCellsField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.ComponentModel.BindingList<long> BoardCells
        {
            get
            {
                return this.BoardCellsField;
            }
            set
            {
                this.BoardCellsField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.datacontract.org/2004/07/MatixGameForm")]
    [System.SerializableAttribute()]
    public partial class PlayerFeatures : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<int, int> AvailableSelectionsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long PlayerScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatixGameForm.MatixSharedGameServices.EMatixPlayerType PlayerTypeField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<int, int> AvailableSelections
        {
            get
            {
                return this.AvailableSelectionsField;
            }
            set
            {
                this.AvailableSelectionsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long PlayerScore
        {
            get
            {
                return this.PlayerScoreField;
            }
            set
            {
                this.PlayerScoreField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MatixGameForm.MatixSharedGameServices.EMatixPlayerType PlayerType
        {
            get
            {
                return this.PlayerTypeField;
            }
            set
            {
                this.PlayerTypeField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.xblip.com", ConfigurationName="MatixGameForm.MatixSharedGameServices.IMatixSharedGame", CallbackContract=typeof(MatixGameForm.MatixSharedGameServices.IMatixSharedGameCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IMatixSharedGame
    {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://www.xblip.com/IMatixSharedGame/InitiatePlayerConnection")]
        void InitiatePlayerConnection(MatixGameForm.MatixSharedGameServices.MatixPlayer inConnectingPlayer);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://www.xblip.com/IMatixSharedGame/ExecuteMove")]
        void ExecuteMove(MatixGameForm.MatixSharedGameServices.MatixMove inMatixMove, MatixGameForm.MatixSharedGameServices.MatixPlayer inMatixPlayer);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://www.xblip.com/IMatixSharedGame/DisconnectPlayer")]
        void DisconnectPlayer(MatixGameForm.MatixSharedGameServices.MatixPlayer inMatixPlayer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.xblip.com/IMatixSharedGame/Ping", ReplyAction="http://www.xblip.com/IMatixSharedGame/PingResponse")]
        void Ping();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.xblip.com/IMatixSharedGame/GetGameFeatures", ReplyAction="http://www.xblip.com/IMatixSharedGame/GetGameFeaturesResponse")]
        MatixGameForm.MatixSharedGameServices.MatixSharedGameOptions GetGameFeatures();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IMatixSharedGameCallback
    {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://www.xblip.com/IMatixSharedGame/ConnectToGame")]
        void ConnectToGame(MatixGameForm.MatixSharedGameServices.MatixSharedGameInformation inGameInformation);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://www.xblip.com/IMatixSharedGame/RefuseConnectionToGame")]
        void RefuseConnectionToGame();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://www.xblip.com/IMatixSharedGame/ExecuteMoveOnPlayer")]
        void ExecuteMoveOnPlayer(MatixGameForm.MatixSharedGameServices.MatixMove inMatixMove, MatixGameForm.MatixSharedGameServices.MatixPlayer inMatixPlayer);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://www.xblip.com/IMatixSharedGame/PingPlayer")]
        void PingPlayer();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://www.xblip.com/IMatixSharedGame/DisconnectGame")]
        void DisconnectGame();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IMatixSharedGameChannel : MatixGameForm.MatixSharedGameServices.IMatixSharedGame, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class MatixSharedGameClient : System.ServiceModel.DuplexClientBase<MatixGameForm.MatixSharedGameServices.IMatixSharedGame>, MatixGameForm.MatixSharedGameServices.IMatixSharedGame
    {
        
        public MatixSharedGameClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance)
        {
        }
        
        public MatixSharedGameClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName)
        {
        }
        
        public MatixSharedGameClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }
        
        public MatixSharedGameClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }
        
        public MatixSharedGameClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress)
        {
        }
        
        public void InitiatePlayerConnection(MatixGameForm.MatixSharedGameServices.MatixPlayer inConnectingPlayer)
        {
            base.Channel.InitiatePlayerConnection(inConnectingPlayer);
        }
        
        public void ExecuteMove(MatixGameForm.MatixSharedGameServices.MatixMove inMatixMove, MatixGameForm.MatixSharedGameServices.MatixPlayer inMatixPlayer)
        {
            base.Channel.ExecuteMove(inMatixMove, inMatixPlayer);
        }
        
        public void DisconnectPlayer(MatixGameForm.MatixSharedGameServices.MatixPlayer inMatixPlayer)
        {
            base.Channel.DisconnectPlayer(inMatixPlayer);
        }
        
        public void Ping()
        {
            base.Channel.Ping();
        }
        
        public MatixGameForm.MatixSharedGameServices.MatixSharedGameOptions GetGameFeatures()
        {
            return base.Channel.GetGameFeatures();
        }
    }
}
