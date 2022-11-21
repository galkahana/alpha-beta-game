using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;


namespace MatixGameForm
{
    public partial class MatixBoardView : Form
    {
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
        

        private Label[,] mBoardLabelsGrid;
        private MatixGame mMatixGame;
        private MatixPlayer mRowPlayer;
        private MatixPlayer mColumnPlayer;
        private EMatixPlayerType mStartingPlayer;
        private bool mRecievingHumanInteraction;
        private Dictionary<Label, Pair<int, int>> mLabelsIndexes;
        private Font mBoldTextFont;
        private Font mRegularTextFont;
        private MatixOptions mMatixOptions;
        private MatixSharedGameOptions mSharedGameOptions;
        private EGameType mSharedGameType;
        private SharedGameService mSharedGameService;
        private JoinedGameClient mJoinedGameClient;
        private string mConnectedGameName;
        private Thread mConnectionStartThread;

        private int mPreviousSelectionIndex;
        private EMatixPlayerType mPreviousSelectionType;

        public MatixBoardView()
        {
            InitializeComponent();

            labelRowScore.Text = "";
            labelColumnScore.Text = "";
            SetupLocalGameSpecificGui();
            mMatixOptions = new MatixOptions();
            mSharedGameOptions = new MatixSharedGameOptions();
            mMatixGame = new MatixGame();
            mRowPlayer = new MatixPlayer();
            mRowPlayer.PlayerType = EMatixPlayerType.eMatixPlayerRows;
            mColumnPlayer = new MatixPlayer();
            mColumnPlayer.PlayerType = EMatixPlayerType.eMatixPlayerColumns;
            mSharedGameService = new SharedGameService(this);
            mJoinedGameClient = new JoinedGameClient(this);
            mRecievingHumanInteraction = false;
            mBoldTextFont = new Font(labelRowScore.Font, FontStyle.Bold);
            mRegularTextFont = (Font)labelRowScore.Font.Clone();
            recieveHintToolStripMenuItem.Enabled = false;
            mSharedGameType = EGameType.eLocal;
            mConnectionStartThread = null;
            

            SetupBoardLabelsGrid();
            SetupBoardLabelsEvents();
        }

        internal MatixPlayer RowPlayer
        {
            get { return mRowPlayer; }
        }

        internal MatixPlayer ColumnPlayer
        {
            get { return mColumnPlayer; }
        }

        private void SetupBoardLabelsGrid()
        {
            mBoardLabelsGrid = new Label[8, 8];
            mLabelsIndexes = new Dictionary<Label, Pair<int, int>>();
            mBoardLabelsGrid[7,7] = label64;
			mLabelsIndexes.Add(label64,new Pair<int,int>(7,7));
            mBoardLabelsGrid[6,7] = label63;
			mLabelsIndexes.Add(label63,new Pair<int,int>(6,7));
            mBoardLabelsGrid[5,7] = label62;
			mLabelsIndexes.Add(label62,new Pair<int,int>(5,7));
            mBoardLabelsGrid[4,7] = label61;
			mLabelsIndexes.Add(label61,new Pair<int,int>(4,7));
            mBoardLabelsGrid[3,7] = label60;
			mLabelsIndexes.Add(label60,new Pair<int,int>(3,7));
            mBoardLabelsGrid[2,7] = label59;
			mLabelsIndexes.Add(label59,new Pair<int,int>(2,7));
            mBoardLabelsGrid[1,7] = label58;
			mLabelsIndexes.Add(label58,new Pair<int,int>(1,7));
            mBoardLabelsGrid[0,7] = label57;
			mLabelsIndexes.Add(label57,new Pair<int,int>(0,7));
            mBoardLabelsGrid[7,6] = label56;
			mLabelsIndexes.Add(label56,new Pair<int,int>(7,6));
            mBoardLabelsGrid[6,6] = label55;
			mLabelsIndexes.Add(label55,new Pair<int,int>(6,6));
            mBoardLabelsGrid[5,6] = label54;
			mLabelsIndexes.Add(label54,new Pair<int,int>(5,6));
            mBoardLabelsGrid[4,6] = label53;
			mLabelsIndexes.Add(label53,new Pair<int,int>(4,6));
            mBoardLabelsGrid[3,6] = label52;
			mLabelsIndexes.Add(label52,new Pair<int,int>(3,6));
            mBoardLabelsGrid[2,6] = label51;
			mLabelsIndexes.Add(label51,new Pair<int,int>(2,6));
            mBoardLabelsGrid[1,6] = label50;
			mLabelsIndexes.Add(label50,new Pair<int,int>(1,6));
            mBoardLabelsGrid[0,6] = label49;
			mLabelsIndexes.Add(label49,new Pair<int,int>(0,6));
            mBoardLabelsGrid[7,5] = label48;
			mLabelsIndexes.Add(label48,new Pair<int,int>(7,5));
            mBoardLabelsGrid[6,5] = label47;
			mLabelsIndexes.Add(label47,new Pair<int,int>(6,5));
            mBoardLabelsGrid[5,5] = label46;
			mLabelsIndexes.Add(label46,new Pair<int,int>(5,5));
            mBoardLabelsGrid[4,5] = label45;
			mLabelsIndexes.Add(label45,new Pair<int,int>(4,5));
            mBoardLabelsGrid[3,5] = label44;
			mLabelsIndexes.Add(label44,new Pair<int,int>(3,5));
            mBoardLabelsGrid[2,5] = label43;
			mLabelsIndexes.Add(label43,new Pair<int,int>(2,5));
            mBoardLabelsGrid[1,5] = label42;
			mLabelsIndexes.Add(label42,new Pair<int,int>(1,5));
            mBoardLabelsGrid[0,5] = label41;
			mLabelsIndexes.Add(label41,new Pair<int,int>(0,5));
            mBoardLabelsGrid[7,4] = label40;
			mLabelsIndexes.Add(label40,new Pair<int,int>(7,4));
            mBoardLabelsGrid[6,4] = label39;
			mLabelsIndexes.Add(label39,new Pair<int,int>(6,4));
            mBoardLabelsGrid[5,4] = label38;
			mLabelsIndexes.Add(label38,new Pair<int,int>(5,4));
            mBoardLabelsGrid[4,4] = label37;
			mLabelsIndexes.Add(label37,new Pair<int,int>(4,4));
            mBoardLabelsGrid[3,4] = label36;
			mLabelsIndexes.Add(label36,new Pair<int,int>(3,4));
            mBoardLabelsGrid[2,4] = label35;
			mLabelsIndexes.Add(label35,new Pair<int,int>(2,4));
            mBoardLabelsGrid[1,4] = label34;
			mLabelsIndexes.Add(label34,new Pair<int,int>(1,4));
            mBoardLabelsGrid[0,4] = label33;
			mLabelsIndexes.Add(label33,new Pair<int,int>(0,4));
            mBoardLabelsGrid[7,3] = label32;
			mLabelsIndexes.Add(label32,new Pair<int,int>(7,3));
            mBoardLabelsGrid[6,3] = label31;
			mLabelsIndexes.Add(label31,new Pair<int,int>(6,3));
            mBoardLabelsGrid[5,3] = label30;
			mLabelsIndexes.Add(label30,new Pair<int,int>(5,3));
            mBoardLabelsGrid[4,3] = label29;
			mLabelsIndexes.Add(label29,new Pair<int,int>(4,3));
            mBoardLabelsGrid[3,3] = label28;
			mLabelsIndexes.Add(label28,new Pair<int,int>(3,3));
            mBoardLabelsGrid[2,3] = label27;
			mLabelsIndexes.Add(label27,new Pair<int,int>(2,3));
            mBoardLabelsGrid[1,3] = label26;
			mLabelsIndexes.Add(label26,new Pair<int,int>(1,3));
            mBoardLabelsGrid[0,3] = label25;
			mLabelsIndexes.Add(label25,new Pair<int,int>(0,3));
            mBoardLabelsGrid[7,2] = label24;
			mLabelsIndexes.Add(label24,new Pair<int,int>(7,2));
            mBoardLabelsGrid[6,2] = label23;
			mLabelsIndexes.Add(label23,new Pair<int,int>(6,2));
            mBoardLabelsGrid[5,2] = label22;
			mLabelsIndexes.Add(label22,new Pair<int,int>(5,2));
            mBoardLabelsGrid[4,2] = label21;
			mLabelsIndexes.Add(label21,new Pair<int,int>(4,2));
            mBoardLabelsGrid[3,2] = label20;
			mLabelsIndexes.Add(label20,new Pair<int,int>(3,2));
            mBoardLabelsGrid[2,2] = label19;
			mLabelsIndexes.Add(label19,new Pair<int,int>(2,2));
            mBoardLabelsGrid[1,2] = label18;
			mLabelsIndexes.Add(label18,new Pair<int,int>(1,2));
            mBoardLabelsGrid[0,2] = label17;
			mLabelsIndexes.Add(label17,new Pair<int,int>(0,2));
            mBoardLabelsGrid[7,1] = label16;
			mLabelsIndexes.Add(label16,new Pair<int,int>(7,1));
            mBoardLabelsGrid[6,1] = label15;
			mLabelsIndexes.Add(label15,new Pair<int,int>(6,1));
            mBoardLabelsGrid[5,1] = label14;
			mLabelsIndexes.Add(label14,new Pair<int,int>(5,1));
            mBoardLabelsGrid[4,1] = label13;
			mLabelsIndexes.Add(label13,new Pair<int,int>(4,1));
            mBoardLabelsGrid[3,1] = label12;
			mLabelsIndexes.Add(label12,new Pair<int,int>(3,1));
            mBoardLabelsGrid[2,1] = label11;
			mLabelsIndexes.Add(label11,new Pair<int,int>(2,1));
            mBoardLabelsGrid[1,1] = label10;
			mLabelsIndexes.Add(label10,new Pair<int,int>(1,1));
            mBoardLabelsGrid[0,1] = label9;
			mLabelsIndexes.Add(label9,new Pair<int,int>(0,1));
            mBoardLabelsGrid[7,0] = label8;
			mLabelsIndexes.Add(label8,new Pair<int,int>(7,0));
            mBoardLabelsGrid[6,0] = label7;
			mLabelsIndexes.Add(label7,new Pair<int,int>(6,0));
            mBoardLabelsGrid[5,0] = label6;
			mLabelsIndexes.Add(label6,new Pair<int,int>(5,0));
            mBoardLabelsGrid[4,0] = label5;
			mLabelsIndexes.Add(label5,new Pair<int,int>(4,0));
            mBoardLabelsGrid[3,0] = label4;
			mLabelsIndexes.Add(label4,new Pair<int,int>(3,0));
            mBoardLabelsGrid[2,0] = label3;
			mLabelsIndexes.Add(label3,new Pair<int,int>(2,0));
            mBoardLabelsGrid[1,0] = label2;
			mLabelsIndexes.Add(label2,new Pair<int,int>(1,0));
            mBoardLabelsGrid[0,0] = label1;
			mLabelsIndexes.Add(label1,new Pair<int,int>(0,0));
        }

        private void SetupBoardLabelsEvents()
        {
            foreach(Label label in mBoardLabelsGrid)
            {
                label.Click += new EventHandler(OnSelectableLabelClick);
                label.MouseEnter += new EventHandler(onSelectableLabelMouseEnter);
                label.MouseLeave += new EventHandler(onSelectableLabelMouseLeave);
            }

        }

        private void onSelectableLabelMouseEnter(object sender, EventArgs e)
        {
            if (mRecievingHumanInteraction && !mMatixGame.IsGameEnded())
            {
                Label aLabel = (Label)sender;
                Pair<int, int> coordinates = mLabelsIndexes[aLabel];

                ColorLabels(
                    GetLabelsOfCurrentPlayerInCoordinates(coordinates.first, coordinates.second),
                    Color.FromKnownColor(KnownColor.Highlight));
            }
        }

        private void OnSelectableLabelClick(object sender, EventArgs e)
        {
            if (mRecievingHumanInteraction && !mMatixGame.IsGameEnded())
            {
                Label aLabel = (Label)sender;
                Pair<int, int> coordinates = mLabelsIndexes[aLabel];

                MatixMove newMove = new MatixMove();

                if (mMatixGame.CurrentGameStage.CurrentTurnPlayerType == EMatixPlayerType.eMatixPlayerColumns)
                    newMove.SelectionIndex = coordinates.first;
                else
                    newMove.SelectionIndex = coordinates.second;
                ExecuteHumanPlayerMove(newMove);
            }
        }

        private void onSelectableLabelMouseLeave(object sender, EventArgs e)
        {
            if (mRecievingHumanInteraction && !mMatixGame.IsGameEnded())
            {
                Label aLabel = (Label)sender;
                Pair<int, int> coordinates = mLabelsIndexes[aLabel];

                ColorLabels(
                    GetLabelsOfCurrentPlayerInCoordinates(coordinates.first, coordinates.second),
                    Color.FromKnownColor(KnownColor.Control));

                ShowLatestSelection(); // show latest selection is carried out in case unhighlighting was done
                                       // also on "previously selected" cells
            }
        }

        private IEnumerable<Label> GetLabelsOfRow(int inRowIndex)
        {
            for (int i = 0; i < 8; ++i)
            {
                yield return mBoardLabelsGrid[i, inRowIndex];
            }
        }

        private IEnumerable<Label> GetLabelsOfColumn(int inColumnIndex)
        {
            for (int i = 0; i < 8; ++i)
            {
                yield return mBoardLabelsGrid[inColumnIndex,i];
            }
        }


        private void StartNewGame()
        {
            SetupParametersFromOptions();

            mMatixGame.StartNewGame(mRowPlayer, 
                                    mColumnPlayer, 
                                    mStartingPlayer,
                                    mMatixOptions.mMaximumMoveSearchLevel,
                                    mMatixOptions.mMaximumProfilerSearchLevel); 

            ShowInitialBoard();
            ShowPlayersScore();

            SetupGUIForCurrentPlayer();

            recieveHintToolStripMenuItem.Enabled = true;
        }

        private void SetupParametersFromOptions()
        {
            mColumnPlayer.MoveType = mMatixOptions.mColumnMoveType;
            mRowPlayer.MoveType = mMatixOptions.mRowMoveType;
            mStartingPlayer = mMatixOptions.mWhoStartsTheGame;
        }

        private void ShowInitialBoard()
        {
            SetBoardLabelsToBoardNumbers();
            SetupEnabledDisabledCellsDisplay();
            ShowLatestSelection();
        }

        private void SetBoardLabelsToBoardNumbers()
        {
            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                    mBoardLabelsGrid[i, j].Text = mMatixGame.BoardGame.GetScoreAt(i, j).ToString();
        }

        private void SetupEnabledDisabledCellsDisplay()
        {
            EnableAllCells();
            ColorAllCellsInControl();
            DisableUnavailableCells();
        }

        private void EnableAllCells()
        {
            foreach (Label label in mBoardLabelsGrid) // disabling should create a "grayed out" effect. if not i'll got for it
                label.Enabled = true;
        }

        private void ColorAllCellsInControl()
        {
            foreach (Label label in mBoardLabelsGrid)
                label.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void DisableUnavailableCells()
        {
            foreach(int columnIndex in mMatixGame.GetUnAvailablePlayerIndexes(EMatixPlayerType.eMatixPlayerColumns))
            {
                foreach (Label label in GetLabelsOfColumn(columnIndex))
                    label.Enabled = false;
            }

            foreach (int rowIndex in mMatixGame.GetUnAvailablePlayerIndexes(EMatixPlayerType.eMatixPlayerRows))
            {
                foreach (Label label in GetLabelsOfRow(rowIndex))
                    label.Enabled = false;
            }

            // re-enable available indexes on the current selected row/column
            EMatixPlayerType currentPlayerType = mMatixGame.CurrentGameStage.CurrentTurnPlayerType;
            foreach (Label label in GetLabelsOfLatestSelection())
            {
                int labelCurrentIndex = (EMatixPlayerType.eMatixPlayerColumns == currentPlayerType ? 
                                            mLabelsIndexes[label].first  : mLabelsIndexes[label].first);

                if (mMatixGame.CurrentGameStage.PlayerFeatures[currentPlayerType].AvailableSelections.ContainsKey(labelCurrentIndex))
                    label.Enabled = true;
            }
        }

        private IEnumerable<Label> GetLabelsOfLatestSelection()
        {
            return GetLabelsOfType(mMatixGame.CurrentGameStage.LatestSelectionType,
                                    mMatixGame.CurrentGameStage.LatestSelectionIndex);
        }

        private IEnumerable<Label> GetLabelsOfType(EMatixPlayerType inType, int inLabelsIndex)
        {
            if (EMatixPlayerType.eMatixPlayerRows == inType)
                return GetLabelsOfRow(inLabelsIndex);
            else
                return GetLabelsOfColumn(inLabelsIndex);
        }

        private IEnumerable<Label> GetLabelsOfCurrentPlayerInCoordinates(int inColumnIndex,int inRowIndex)
        {
            EMatixPlayerType currentPlayerType = mMatixGame.CurrentGameStage.CurrentTurnPlayerType;
            int selectedIndex = (EMatixPlayerType.eMatixPlayerColumns == currentPlayerType ? inColumnIndex : inRowIndex);
            if (mMatixGame.CurrentGameStage.PlayerFeatures[currentPlayerType].AvailableSelections.ContainsKey(selectedIndex))
                return GetLabelsOfType(currentPlayerType, selectedIndex);
            else
                return EmptyLabelIterator();
        }

        private IEnumerable<Label> EmptyLabelIterator()
        {
            yield break;
        }

        private void ShowLatestSelection()
        {
            ColorLabels(GetLabelsOfLatestSelection(), Color.PaleTurquoise);

            mPreviousSelectionIndex = mMatixGame.CurrentGameStage.LatestSelectionIndex;
            mPreviousSelectionType = mMatixGame.CurrentGameStage.LatestSelectionType;
        }

        private void ColorLabels(IEnumerable<Label> inLabelEnumerator, Color inColor)
        {
            foreach (Label label in inLabelEnumerator)
                label.BackColor = inColor;
        }

        private void ShowPlayersScore()
        {
            long rowPlayerScore = mMatixGame.GetPlayerScore(mRowPlayer);
            long columnPlayerScore = mMatixGame.GetPlayerScore(mColumnPlayer);

            labelRowScore.Text = rowPlayerScore.ToString();
            labelColumnScore.Text = columnPlayerScore.ToString();

            labelRowScore.Font = (rowPlayerScore > columnPlayerScore) ? mBoldTextFont : mRegularTextFont;
            labelColumnScore.Font = (columnPlayerScore > rowPlayerScore) ? mBoldTextFont : mRegularTextFont;
        }

        private void ShowPlayersProfiledLevel()
        {
            labelColumnProfiledLevel.Text = mMatixGame.GetProfiledColumnPlayerLevel().ToString();
            labelRowProfiledLevel.Text = mMatixGame.GetProfiledRowPlayerLevel().ToString();
        }

        private void ExecuteHumanPlayerMove(MatixMove inMove)
        {
            MatixPlayer currentPlayer = mMatixGame.GetCurrentPlayer();
            ExecuteExternalMove(inMove);
            if (EGameType.eSharing == mSharedGameType)
            {
                mSharedGameService.BroadcastMove(inMove, currentPlayer);
            }
            else if (EGameType.eJoined == mSharedGameType)
            {
                if (!mJoinedGameClient.BroadcastMove(inMove, currentPlayer))
                {
                    mJoinedGameClient.StopCalling();
                    MessageBox.Show(mJoinedGameClient.mLastConnectionErrorMessage, "Error in broadcasting move");
                    DisconnectFromJoinedGame();
                }
            }
        }

        private void ExecuteExternalMove(MatixMove inMove)
        {
            if (mMatixGame.ExecuteExternalMove(inMove))
            {
                DisplayMoveExecution(inMove);
                if (mMatixGame.IsGameEnded())
                    DisplayGameEnded();
                SetupGUIForCurrentPlayer();
            }
        }

        private void CalculateAndExecuteComputerPlayerMove()
        {
            MatixMove matixMove;
            MatixPlayer currentPlayer = mMatixGame.GetCurrentPlayer();
            if(mMatixGame.ExecuteComputedMove(out matixMove))
            {
                DisplayMoveExecution(matixMove);
                if (mMatixGame.IsGameEnded())
                    DisplayGameEnded();
                SetupGUIForCurrentPlayer();
                if (EGameType.eSharing == mSharedGameType)
                {
                    mSharedGameService.BroadcastMove(matixMove, currentPlayer);
                }
                else if (EGameType.eJoined == mSharedGameType)
                {
                    if (!mJoinedGameClient.BroadcastMove(matixMove, currentPlayer))
                    {
                        mJoinedGameClient.StopCalling();
                        MessageBox.Show(mJoinedGameClient.mLastConnectionErrorMessage, "Error in broadcasting move");
                        DisconnectFromJoinedGame();
                    }
                }
            }
        }

        private void DisplayMoveExecution(MatixMove inMatixMove)
        {
            // Show newly selected column/row
            ColorLabels(
                GetLabelsOfType(mMatixGame.CurrentGameStage.LatestSelectionType,inMatixMove.SelectionIndex),
                Color.Cyan);
            Refresh();
            System.Threading.Thread.Sleep(250);
            
            // Show newly selected cell, blink it
            Label selectedGridLabel =
                GetLabel(mMatixGame.CurrentGameStage.LatestSelectionType, inMatixMove.SelectionIndex,
                             mPreviousSelectionType, mPreviousSelectionIndex);
            selectedGridLabel.BackColor = Color.Red;
            selectedGridLabel.Refresh();
            System.Threading.Thread.Sleep(200);
            selectedGridLabel.BackColor = Color.Cyan;
            selectedGridLabel.Refresh();
            System.Threading.Thread.Sleep(200);
            selectedGridLabel.BackColor = Color.Red;
            selectedGridLabel.Refresh();
            System.Threading.Thread.Sleep(250);

            // unhighlight previous selection
            ColorLabels(
                GetLabelsOfType(mPreviousSelectionType, mPreviousSelectionIndex),
                Color.FromKnownColor(KnownColor.Control));

            // disable all of its cells
            foreach (Label label in GetLabelsOfType(mPreviousSelectionType, mPreviousSelectionIndex))
                label.Enabled = false;

            // show newly selected row/column and save its values
            ShowLatestSelection();
            ShowPlayersScore();
            ShowPlayersProfiledLevel();

            if(!this.Focused)
                FlashWindow(this.Handle, true);
        }


        private Label GetLabel(EMatixPlayerType inFirstPlayerType,
                       int inFirstPlayerSelection,
                       EMatixPlayerType inSecondPlayerType,
                       int inSecondPlayerSelection)
        {
            int columnIndex, rowIndex;

            mMatixGame.BoardGame.GetIndexesAt(inFirstPlayerType,
                                              inFirstPlayerSelection,
                                              inSecondPlayerType,
                                              inSecondPlayerSelection,
                                              out columnIndex,
                                              out rowIndex);

            return mBoardLabelsGrid[columnIndex, rowIndex];
        }

        private void SetupGUIForCurrentPlayer()
        {
            if (!mMatixGame.IsGameEnded())
            {
                MatixPlayer currentPlayer = mMatixGame.GetCurrentPlayer();
                DisplayPlayerTurn(currentPlayer);
                if (!currentPlayer.IsRemote) // if the current player is remote will be waiting for a move
                {
                    if (EMatixMoveType.eMatixMoveAutomatic == currentPlayer.MoveType)
                    {
                        mRecievingHumanInteraction = false;
                        CalculateAndExecuteComputerPlayerMove();
                    }
                    else
                    {
                        mRecievingHumanInteraction = true;
                    }
                }
                else
                    mRecievingHumanInteraction = false;
            }
            else
                mRecievingHumanInteraction = false;
        }

        private void DisplayPlayerTurn(MatixPlayer currentPlayer)
        {
            LabelGameStatus.Text =
                (currentPlayer.PlayerType == EMatixPlayerType.eMatixPlayerColumns ? "Column" : "Row") +
                    " player turn";
            LabelGameStatus.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartLocalGame();
        }

        private void StartLocalGame()
        {
            StopEarlyGameConnection();
            mSharedGameType = EGameType.eLocal;
            SetupLocalGameSpecificGui();
            SetupPlayersForLocalGame();
            StartNewGame();
        }

        private void SetupLocalGameSpecificGui()
        {
            SetupTitleForLocalGame();
            panelSharedPlayers.Visible = false;
        }

        private void SetupTitleForLocalGame()
        {
            Text = "Matix";
        }

        private void StopEarlyGameConnection()
        {
            if (mSharedGameType == EGameType.eSharing)
            {
                StopSharedGame();
            }
            else if(mSharedGameType == EGameType.eJoined)
            {
                StopJoinedGame();
            }
        }

        private void StopSharedGame()
        {
            mSharedGameService.Disconnect();
            mSharedGameService.StopListening();
        }

        private void StopJoinedGame()
        {
            bool result = mJoinedGameClient.Disconnect();
            mJoinedGameClient.StopCalling();
            if (!result)
                MessageBox.Show(mJoinedGameClient.mLastConnectionErrorMessage, "Unable to disconnect from game");
        }

        private void SetupPlayersForLocalGame()
        {
            mRowPlayer.IsRemote = false;
            mRowPlayer.IsRemoteAvailable = false;
            mColumnPlayer.IsRemote = false;
            mColumnPlayer.IsRemoteAvailable = false;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void recieveHintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mRecievingHumanInteraction && !mMatixGame.IsGameEnded())
            {
                MatixMove hintMove;
                mMatixGame.ComputeBestMoveForCurrentPlayer(mMatixOptions.mMaximumHintMoveSearchLevel,out hintMove);

                DisplayHintMove(hintMove);
            }
        }

        private void DisplayHintMove(MatixMove inHintMove)
        {
            Label selectedGridLabel =
                GetLabel(mMatixGame.CurrentGameStage.LatestSelectionType, mMatixGame.CurrentGameStage.LatestSelectionIndex,
                             mMatixGame.CurrentGameStage.CurrentTurnPlayerType, inHintMove.SelectionIndex);
            selectedGridLabel.BackColor = Color.Green;
            selectedGridLabel.Refresh();
            System.Threading.Thread.Sleep(200);
            selectedGridLabel.BackColor = Color.Cyan;
            selectedGridLabel.Refresh();
            System.Threading.Thread.Sleep(200);
            selectedGridLabel.BackColor = Color.Green;
            selectedGridLabel.Refresh();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatixOptionsView matixOptionsView = new MatixOptionsView();
            matixOptionsView.mMatixOptions = mMatixOptions;
            matixOptionsView.ShowDialog();
        }

        private void DisplayGameEnded()
        {
            string endingMessage;

            recieveHintToolStripMenuItem.Enabled = false;

            if (mMatixGame.GetGameWinner() == mRowPlayer)
                endingMessage = "The game was won\n by the rows player.";
            else if (mMatixGame.GetGameWinner() == mColumnPlayer)
                endingMessage = "The game was won\n by the columns player.";
            else
                endingMessage = "Alas! a tie.\n try again.";
            LabelGameStatus.Text = endingMessage;
            LabelGameStatus.ForeColor = Color.Purple;
        }

        private void newSharedGamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSharedGameDialog sharedGameDialog = new NewSharedGameDialog();
            sharedGameDialog.SharedGameOptions = mSharedGameOptions;
            DialogResult dialogResult = sharedGameDialog.ShowDialog();
            if (DialogResult.OK == dialogResult)
            {
                StartSharedGame();
            }
        }

        private void StartSharedGame()
        {
            StopEarlyGameConnection();
            mSharedGameType = EGameType.eSharing;
            SetupPlayersForSharedGame();
            SetupSharedGameSpecificGUI();
            StartNewGame();
            StartSharedGameServiceWithThrad();
        }

        private void StartSharedGameServiceWithThrad()
        {
            mConnectionStartThread = new Thread(new ThreadStart(GameServiceThreadMethod));
            mConnectionStartThread.Start();
        }

        delegate void VoidCaller();

        private void GameServiceThreadMethod()
        {
            if (!StartSharedGameService())
                Invoke(new VoidCaller(StartLocalGame)); // start local game, just as a default action.

        }

        private void SetupPlayersForSharedGame()
        {
            mRowPlayer.IsRemote = (mSharedGameOptions.mSharedPlayers != ESharedPlayersOptions.eColumnShared);
            mRowPlayer.IsRemoteAvailable = !mRowPlayer.IsRemote;
            mColumnPlayer.IsRemote = (mSharedGameOptions.mSharedPlayers != ESharedPlayersOptions.eRowShared);
            mColumnPlayer.IsRemoteAvailable = !mColumnPlayer.IsRemote;
        }

        private void SetupSharedGameSpecificGUI()
        {
            SetupTitleToSharedGame();
            SetupSharedPlayersPanel();
        }

        private void SetupTitleToSharedGame()
        {
            Text = "Matix (Sharing " + mSharedGameOptions.mSharedGameName + ")";
        }

        private void SetupSharedPlayersPanel()
        {
            SetupSharedPlayerLabel(labelColumnShared, mColumnPlayer);
            SetupSharedPlayerLabel(labelRowShared, mRowPlayer);
            panelSharedPlayers.Visible = true;
        }

        private void SetupSharedPlayerLabel(Label inLabel, MatixPlayer inPlayer)
        {
            if (inPlayer.IsRemote)
            {
                if (inPlayer.IsAvailable)
                {
                    inLabel.Text = inPlayer.RemotePlayerName;
                    inLabel.ForeColor = Color.Green;
                }
                else
                {
                    inLabel.Text = "Disconnected";
                    inLabel.ForeColor = Color.Red;
                }
            }
            else
            {
                inLabel.Text = "Local";
                inLabel.ForeColor = Color.Blue;
            }
        }

        private bool StartSharedGameService()
        {
            if (!mSharedGameService.StartListening(GenerateRemotePlayerConnectionInformation()))
            {
                MessageBox.Show(mSharedGameService.mLastConnectionErrorMessage, "Start sharing error");
                return false;
            }
            else
                return true;
        }

        public bool IsGameEnded()
        {
            return mMatixGame.IsGameEnded();
        }

        internal EMatixPlayerType GetCurrentPlayerType()
        {
            return mMatixGame.GetCurrentPlayer().PlayerType;
        }

        internal MatixSharedGameOptions SharedGameOptions
        {
            get { return mSharedGameOptions; }
        }

        internal MatixSharedGameInformation GenerateRemotePlayerConnectionInformation()
        {
            MatixSharedGameInformation gameInformation = new MatixSharedGameInformation();
            gameInformation.GameStage = mMatixGame.CurrentGameStage;
            gameInformation.SharedBoard = mMatixGame.CurrentGameStage.Board;
            gameInformation.StartingPlayer = mStartingPlayer;
            return gameInformation;
        }
        
        internal void ExecuteRemoteMove(MatixMove inMatixMove)
        {
            ExecuteExternalMove(inMatixMove);
        }

        private void connectToSharedGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectToSharedGameDialog connectGameDialog = new ConnectToSharedGameDialog();
            DialogResult dialogResult = connectGameDialog.ShowDialog();
            if (DialogResult.OK == dialogResult)
            {
                StartConnectedGame(connectGameDialog.mGameServerName,
                                   connectGameDialog.mSelectedPlayerType,
                                   connectGameDialog.mConnectedGameName,
                                   connectGameDialog.mSharedPlayerName);
            }
        }

        private void StartConnectedGame(string inGameServerName, 
                                        EMatixPlayerType inMatixPlayerType,
                                        string inConnectedGameName,
                                        string inSharedPlayerName)
        {
            bool joiningSucceeded;
            
            StopEarlyGameConnection();
            if (mJoinedGameClient.StartCalling(inGameServerName))
            {
                mConnectedGameName = inConnectedGameName;
                mSharedGameType = EGameType.eJoined;
                SetupPlayersForJoinedGame(inMatixPlayerType, inSharedPlayerName);

                if (mJoinedGameClient.JoinGame(inMatixPlayerType == EMatixPlayerType.eMatixPlayerRows ? mRowPlayer : mColumnPlayer, out joiningSucceeded))
                {
                    if (!joiningSucceeded)
                        MessageBox.Show("Can not join game. Probably because other players are already connected.", "Connect to remote game");
                }
                else
                    MessageBox.Show(mJoinedGameClient.mLastConnectionErrorMessage, "Error in joining the game.");
            }
            else
                MessageBox.Show(mJoinedGameClient.mLastConnectionErrorMessage);
        }

        internal void DisplayRefusedJoining()
        {
            MessageBox.Show("Can not join game. Probably because other players are already connected.", "Connect to remote game");
            RowPlayer.IsRemoteAvailable = !RowPlayer.IsRemote;
            ColumnPlayer.IsRemoteAvailable = !ColumnPlayer.IsRemote;
        }

        private void SetupPlayersForJoinedGame(EMatixPlayerType inMatixPlayerType,
                                               string inRemotePlayerName)
        {
            mRowPlayer.IsRemote = (inMatixPlayerType != EMatixPlayerType.eMatixPlayerRows);
            mColumnPlayer.IsRemote = (inMatixPlayerType != EMatixPlayerType.eMatixPlayerColumns);
            mColumnPlayer.IsRemoteAvailable = true; // irrelevant values for joined game. while there is a game going
                                                    // on consider both as available.
            mRowPlayer.IsRemoteAvailable = true;
            if (!mRowPlayer.IsRemote)
                mRowPlayer.RemotePlayerName = inRemotePlayerName;
            else
                mColumnPlayer.RemotePlayerName = inRemotePlayerName;
         }

        
        internal void SetupGameWithDetails(MatixSharedGameServices.MatixSharedGameInformation inSharedGameInformation)
        {
            SetupTitleForJoinedGame();
            SetupParametersFromOptions();
            mStartingPlayer = (inSharedGameInformation.StartingPlayer == MatixSharedGameServices.EMatixPlayerType.eMatixPlayerColumns ?
                                EMatixPlayerType.eMatixPlayerColumns :
                                EMatixPlayerType.eMatixPlayerRows);

            mMatixGame.StartGameFromJoinedGame(mRowPlayer, 
                                                mColumnPlayer, 
                                                mStartingPlayer, 
                                                mMatixOptions.mMaximumMoveSearchLevel,
                                                mMatixOptions.mMaximumProfilerSearchLevel,
                                                inSharedGameInformation);

            ShowInitialBoard();
            ShowPlayersScore();
            SetupGUIForCurrentPlayer();
            recieveHintToolStripMenuItem.Enabled = true;           
        }

        private void SetupTitleForJoinedGame()
        {
            Text = "Matix (Joined " + mConnectedGameName + ")";
        }

        internal void ContinueGameIfPossible()
        {
            SetupSharedPlayersPanel();
            SetupGUIForCurrentPlayer();
        }

        private void MatixBoardView_Load(object sender, EventArgs e)
        {

        }

        private void MatixBoardView_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopEarlyGameConnection();
            if (mJoinedGameClient != null)
                mJoinedGameClient.Terminate();
            if (mSharedGameService != null)
                mSharedGameService.Terminate();
            if (mConnectionStartThread != null && mConnectionStartThread.IsAlive)
                mConnectionStartThread.Join();
         }

        internal void DisconnectFromJoinedGame()
        {
            Text = "Matix (Joined game Disconnected)";
            SetupGUIForCurrentPlayer();
        }

        private void runHostsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HostAllServicesView hostsForm = new HostAllServicesView();
            hostsForm.ShowDialog();
        }

        private void resetProfilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mMatixGame.ResetProfiler();
        }
    }

    internal enum EGameType
    {
        eLocal,
        eSharing,
        eJoined
    }
}