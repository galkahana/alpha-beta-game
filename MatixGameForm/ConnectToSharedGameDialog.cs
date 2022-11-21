using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;

namespace MatixGameForm
{
    public partial class ConnectToSharedGameDialog : Form
    {
        private enum ConnectionSteps
        {
            eStepConnectionChoice,
            eStepPlayerChoice,
            eStepConnecting,
            eStepSearching,
            eStepGameChoice
        }
         
        public string mGameServerName;
        public EMatixPlayerType mSelectedPlayerType;
        public string mConnectedGameName;
        public string mSharedPlayerName;
        private bool mIsAborting;
        private Thread mRunner;
        private ConnectionSteps mCurrentStep;
        private ConnectionSteps mConnectingOriginatorStep;

         
        private MatixSharedGameServices.MatixSharedGameClient mGameClient;
        private RemoteGameCollectorService mCollectorService;

        public ConnectToSharedGameDialog()
        {
            InitializeComponent();
        }

        private void radioButtonLocal_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRemoteAddress.Enabled = false;
        }

        private void radioRemote_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRemoteAddress.Enabled = true;
        }

        private void ConnectToSharedGameDialog_Load(object sender, EventArgs e)
        {
            mIsAborting = false;
            mGameClient = null;
            mRunner = null;
            mCollectorService = null; 

            mGameServerName = string.Empty;
            mSelectedPlayerType = EMatixPlayerType.eNull;
            mConnectedGameName = "";
            mSharedPlayerName = "";
            mCurrentStep = ConnectionSteps.eStepConnectionChoice;
            mConnectingOriginatorStep = ConnectionSteps.eStepConnectionChoice;

            SetupStepConnectionChoice();
        }

        private void SetupButtonOKTextOfCurrentStep()
        {
            if (mCurrentStep == ConnectionSteps.eStepPlayerChoice)
                buttonOK.Text = "Finish";
            else
                buttonOK.Text = "Next";
        }

        private void SetupButtonPreviusEnabledOfCurrentStep()
        {
            if (mCurrentStep == ConnectionSteps.eStepConnectionChoice ||
                mCurrentStep == ConnectionSteps.eStepConnecting ||
                mCurrentStep == ConnectionSteps.eStepSearching)
                buttonPrevious.Enabled = false;
            else
                buttonPrevious.Enabled = true;
        }

        private void SetupButtonOKEnabledOfCurrentStep()
        {
            if (mCurrentStep == ConnectionSteps.eStepConnecting ||
                mCurrentStep == ConnectionSteps.eStepSearching)
                buttonOK.Enabled = false;
            else
                buttonOK.Enabled = true;
        }

        private void SetupCommonGUIForCurrentStep()
        {
            ShowOnlyPanelOfCurrentStep();
            SetupButtonOKEnabledOfCurrentStep();
            SetupButtonOKTextOfCurrentStep();
            SetupButtonPreviusEnabledOfCurrentStep();
        }

        private void SetupStepConnectionChoice()
        {
            mCurrentStep = ConnectionSteps.eStepConnectionChoice;
            SetupCommonGUIForCurrentStep();
        }

        private void ShowOnlyPanelOfCurrentStep()
        {
            ShowOnlyPanelOf(mCurrentStep);
        }

        private void ShowOnlyPanelOf(ConnectionSteps mCurrentStep)
        {
            panelConnecting.Visible = (mCurrentStep == ConnectionSteps.eStepConnecting);
            panelSearchingGames.Visible = (mCurrentStep == ConnectionSteps.eStepSearching);
            panelSelectGame.Visible = (mCurrentStep == ConnectionSteps.eStepGameChoice);
            panelStep1.Visible = (mCurrentStep == ConnectionSteps.eStepConnectionChoice);
            panelStep2.Visible = (mCurrentStep == ConnectionSteps.eStepPlayerChoice);
        }

        bool ValidateStepConnectionChoice(out string outServerName)
        {
            if (radioButtonLocal.Checked)
            {
                outServerName = "localhost";
                return true;
            }
            else
            {
                if (textBoxRemoteAddress.Text.Length == 0)
                {
                    outServerName = "";
                    MessageBox.Show("Please specify the remote server name for connection", "Connect To Shared Game");
                    return false;
                }
                else
                {
                    outServerName = textBoxRemoteAddress.Text;
                    return true;
                }
            }
        }

        private void SetupStepConnecting(
            ConnectionSteps inOriginorStep,
            string inServerNameToConnect)
        {
            mConnectingOriginatorStep = inOriginorStep;
            mCurrentStep = ConnectionSteps.eStepConnecting;
            SetupCommonGUIForCurrentStep();
            SetupFieldsFromServerSelections(inServerNameToConnect);
        }

        private void SetupFieldsFromServerSelections(string inServerNameToConnect)
        {
            mGameServerName = inServerNameToConnect;
            timerConnecting.Start();
            mRunner = new Thread(new ThreadStart(StartConnectingToGame));
            mRunner.Start();
        }

         private delegate void VoidCaller();
         private delegate void VoidCallerWithSharedOptions(MatixSharedGameServices.MatixSharedGameOptions inOptions);

         private void StartConnectingToGame()
         {
             string lastErrorMessage = string.Empty;
 
             EndpointAddress endpointAddress = 
                 new EndpointAddress(
                            URILocalToRemoteTranslator.TranslateHostToMatixGameServiceAddress(mGameServerName));

             do
             {

                 try
                 {
                     mGameClient = new MatixSharedGameServices.MatixSharedGameClient(
                         new InstanceContext(new DummyCallBack()),
                         "NetTcpBinding_IMatixSharedGame",
                         endpointAddress);
                     mGameClient.Open();
                 }
                 catch (System.ServiceModel.EndpointNotFoundException)
                 {
                     mGameClient = null;
                     lastErrorMessage = "Unable to connect to " + mGameServerName + ". The target computer might not be share games.";
                 }
                 catch (Exception ex)
                 {
                     mGameClient = null;
                     lastErrorMessage = ex.Message;
                 }
                 if (null == mGameClient && !mIsAborting)
                 {
                     Invoke(new VoidCaller(StopTimer));
                     MessageBox.Show(lastErrorMessage, "Connect to remote game");
                     Invoke(new VoidCaller(SetupOriginatorStepForConnection));
                 }
                 else
                 {
                     MatixSharedGameServices.MatixSharedGameOptions sharedGameOptions = null;
                     try
                     {
                         sharedGameOptions = mGameClient.GetGameFeatures();
                     }
                     catch (Exception ex)
                     {
                         lastErrorMessage = ex.Message;
                         Invoke(new VoidCaller(StopTimer));
                         if(!mIsAborting)
                            MessageBox.Show("Unable to get game options. Connection to shared game may have been terminated", "Connect to remote game");
                     }
                     if (!mIsAborting)
                         Invoke(new VoidCallerWithSharedOptions(FinalizeConnecting), sharedGameOptions);
                 }
                 mGameClient = null;
             }
             while (false);
         }

        private void SetupOriginatorStepForConnection()
        {
            switch (mConnectingOriginatorStep)
            {
                case ConnectionSteps.eStepConnectionChoice:
                    SetupStepConnectionChoice();
                    break;
                case ConnectionSteps.eStepGameChoice:
                    SetupStepRemoteGameChoice();
                    break;
            }
        }

         private void StopTimer()
         {
             if(timerConnecting.Enabled)
                timerConnecting.Stop();
            if (timerSearchingGames.Enabled)
                timerSearchingGames.Stop();
         }

         private void FinalizeConnecting(
             MatixSharedGameServices.MatixSharedGameOptions inSharedGameOptions)
         {
             timerConnecting.Stop();
             if (inSharedGameOptions != null)
             {
                 mCurrentStep = ConnectionSteps.eStepPlayerChoice;
                 SetupCommonGUIForCurrentStep();

                 labelGameName.Text = inSharedGameOptions.mSharedGameName;
                 mConnectedGameName = inSharedGameOptions.mSharedGameName;
                 if (MatixSharedGameServices.ESharedPlayersOptions.eNoneShared == inSharedGameOptions.mSharedPlayers)
                 {
                     radioButtonColumns.Enabled = false;
                     radioButtonRows.Enabled = false;
                 }
                 else
                 {
                     radioButtonColumns.Enabled = (inSharedGameOptions.mSharedPlayers != MatixSharedGameServices.ESharedPlayersOptions.eRowShared);
                     radioButtonRows.Enabled = (inSharedGameOptions.mSharedPlayers != MatixSharedGameServices.ESharedPlayersOptions.eColumnShared);
                     if (radioButtonRows.Enabled)
                         radioButtonRows.Checked = true;
                     else
                         radioButtonColumns.Checked = true;
                 }
             }
             else
                 SetupOriginatorStepForConnection();
         }

         private void buttonOK_Click(object sender, EventArgs e)
         {
             MoveToNextStep();
         }

        private void MoveToNextStep()
        {
            switch (mCurrentStep)
            {
                case ConnectionSteps.eStepConnectionChoice:
                    {
                        if (radioButtonSearchGames.Checked)
                        {
                            SetupStepSearchingGames();
                        }
                        else
                        {
                            string serverName;
                            if (ValidateStepConnectionChoice(out serverName))
                                SetupStepConnecting(ConnectionSteps.eStepConnectionChoice, serverName);
                        }
                        break;
                    }
                case ConnectionSteps.eStepPlayerChoice:
                    {
                        FinalizeConnectionWizard();
                        break;
                    }
                case ConnectionSteps.eStepSearching:
                    {
                        SetupStepRemoteGameChoice();
                        break;
                    }
                case ConnectionSteps.eStepGameChoice:
                    {
                        string serverName;
                        if (ValidateStepGameChoice(out serverName))
                            SetupStepConnecting(ConnectionSteps.eStepGameChoice, serverName);
                        break;
                    }
            }
        }

        private bool ValidateStepGameChoice(out string outServerName)
        {
            if (null == listBoxGames.SelectedItem)
            {
                outServerName = "";
                MessageBox.Show("Please specify the remote server for connection", "Connect To Shared Game");
                return false;
            }
            else
            {
                outServerName = ((RemoteAddressEntry)listBoxGames.SelectedItem).RemoteServerName;
                return true;
            }
        }

        private void SetupStepSearchingGames()
        {
            mCurrentStep = ConnectionSteps.eStepSearching;
            SetupCommonGUIForCurrentStep();

            timerSearchingGames.Start();
            mRunner = new Thread(new ThreadStart(SearchGames));
            mRunner.Start();
        }

        private void SearchGames()
        {
            mCollectorService = new RemoteGameCollectorService();

            bool collectionResult = mCollectorService.CollectAddresses();
            if (!mIsAborting)
            {
                Invoke(new VoidCaller(StopTimer));
                if (collectionResult)
                {
                    Invoke(new VoidCaller(MoveToNextStep));
                }
                else
                {
                    MessageBox.Show(mCollectorService.LastConnectionErrorMessage, "Searching Games Error");
                    Invoke(new VoidCaller(SetupStepConnectionChoice));
                }
            }
            mCollectorService = null;
        }

        private void SetupStepRemoteGameChoice()
        {
            mCurrentStep = ConnectionSteps.eStepGameChoice;
            SetupCommonGUIForCurrentStep();
            PopulateGamesList();
        }

        private void PopulateGamesList()
        {
            if (mCollectorService != null)
            {
                listBoxGames.Items.Clear();
                foreach (RemoteAddressEntry entry in mCollectorService.GameAddresses)
                {
                    listBoxGames.Items.Add(entry);
                }
            }
            // don't fail if collector service is unavailable, nor touch the list box.
            // this would be the case of pressing previous from player choice
            // when the scenario is of searching games.
        }

        private void FinalizeConnectionWizard()
        {
            do
            {
                if (textBoxRemotePlayerName.Text == string.Empty)
                {
                    MessageBox.Show("Please choose a name to identify yourself with the shared game", "Connect to shared game");
                    break;
                }

                if (!radioButtonRows.Checked && !radioButtonColumns.Checked)
                {
                    MessageBox.Show("Please choose a row or column player for shared game", "Connect to shared game");
                    break;
                }
                mSharedPlayerName = textBoxRemotePlayerName.Text;
                if (radioButtonRows.Checked)
                    mSelectedPlayerType = EMatixPlayerType.eMatixPlayerRows;
                else
                    mSelectedPlayerType = EMatixPlayerType.eMatixPlayerColumns;
                DialogResult = DialogResult.OK;
                Close();
            } while (false);
        }

         private void buttonPrevious_Click(object sender, EventArgs e)
         {
             switch (mCurrentStep)
             {
                 case ConnectionSteps.eStepPlayerChoice:
                     {
                         SetupOriginatorStepForConnection();
                         break;
                     }
                 case ConnectionSteps.eStepGameChoice:
                     {
                         SetupStepConnectionChoice();
                         break;
                     }
             } 
         }

         private void buttonCancel_Click(object sender, EventArgs e)
         {
             DialogResult = DialogResult.Cancel;
             Close();
         }

         private void ConnectToSharedGameDialog_FormClosing(object sender, FormClosingEventArgs e)
         {
             if(timerConnecting.Enabled)
                timerConnecting.Stop();
            if (timerSearchingGames.Enabled)
                timerSearchingGames.Stop();
             if (mRunner != null)
                 if (mRunner.IsAlive)
                 {
                     if (mGameClient != null)
                     {
                         mIsAborting = true;
                         mGameClient.Abort();
                         mGameClient = null;
                     }
                     if (mCollectorService != null)
                     {
                         mIsAborting = true;
                         mCollectorService.Abort();
                     }
                     mRunner.Join();
                 }		

         }

         private void timerConnecting_Tick(object sender, EventArgs e)
         {
             if (100 == progressBar1.Value)
                 progressBar1.Value = 0;
             else
                 progressBar1.Value += 10;
         }

         private void label2_Click(object sender, EventArgs e)
         {

         }

        private void timerSearchingGames_Tick(object sender, EventArgs e)
        {
            if (100 == progressBar2.Value)
                progressBar2.Value = 0;
            else
                progressBar2.Value += 10;
        }

        private void radioButtonSearchGames_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRemoteAddress.Enabled = false;
        }

        private void listBoxGames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxGames_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listBoxGames.SelectedItem != null)
                MoveToNextStep();
        }
    }



}