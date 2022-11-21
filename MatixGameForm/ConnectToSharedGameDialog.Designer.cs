namespace MatixGameForm
{
    partial class ConnectToSharedGameDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelStep1 = new System.Windows.Forms.Panel();
            this.radioButtonSearchGames = new System.Windows.Forms.RadioButton();
            this.textBoxRemoteAddress = new System.Windows.Forms.TextBox();
            this.radioRemote = new System.Windows.Forms.RadioButton();
            this.radioButtonLocal = new System.Windows.Forms.RadioButton();
            this.panelStep2 = new System.Windows.Forms.Panel();
            this.textBoxRemotePlayerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelGameName = new System.Windows.Forms.Label();
            this.radioButtonColumns = new System.Windows.Forms.RadioButton();
            this.radioButtonRows = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.panelConnecting = new System.Windows.Forms.Panel();
            this.C = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timerConnecting = new System.Windows.Forms.Timer(this.components);
            this.panelSearchingGames = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.timerSearchingGames = new System.Windows.Forms.Timer(this.components);
            this.panelSelectGame = new System.Windows.Forms.Panel();
            this.listBoxGames = new System.Windows.Forms.ListBox();
            this.labelSelectGame = new System.Windows.Forms.Label();
            this.panelStep1.SuspendLayout();
            this.panelStep2.SuspendLayout();
            this.panelConnecting.SuspendLayout();
            this.panelSearchingGames.SuspendLayout();
            this.panelSelectGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStep1
            // 
            this.panelStep1.Controls.Add(this.radioButtonSearchGames);
            this.panelStep1.Controls.Add(this.textBoxRemoteAddress);
            this.panelStep1.Controls.Add(this.radioRemote);
            this.panelStep1.Controls.Add(this.radioButtonLocal);
            this.panelStep1.Location = new System.Drawing.Point(1, 1);
            this.panelStep1.Name = "panelStep1";
            this.panelStep1.Size = new System.Drawing.Size(269, 180);
            this.panelStep1.TabIndex = 0;
            // 
            // radioButtonSearchGames
            // 
            this.radioButtonSearchGames.AutoSize = true;
            this.radioButtonSearchGames.Location = new System.Drawing.Point(12, 102);
            this.radioButtonSearchGames.Name = "radioButtonSearchGames";
            this.radioButtonSearchGames.Size = new System.Drawing.Size(95, 17);
            this.radioButtonSearchGames.TabIndex = 3;
            this.radioButtonSearchGames.Text = "Search Games";
            this.radioButtonSearchGames.UseVisualStyleBackColor = true;
            this.radioButtonSearchGames.CheckedChanged += new System.EventHandler(this.radioButtonSearchGames_CheckedChanged);
            // 
            // textBoxRemoteAddress
            // 
            this.textBoxRemoteAddress.Enabled = false;
            this.textBoxRemoteAddress.Location = new System.Drawing.Point(63, 73);
            this.textBoxRemoteAddress.Name = "textBoxRemoteAddress";
            this.textBoxRemoteAddress.Size = new System.Drawing.Size(148, 20);
            this.textBoxRemoteAddress.TabIndex = 2;
            // 
            // radioRemote
            // 
            this.radioRemote.AutoSize = true;
            this.radioRemote.Location = new System.Drawing.Point(11, 50);
            this.radioRemote.Name = "radioRemote";
            this.radioRemote.Size = new System.Drawing.Size(109, 17);
            this.radioRemote.TabIndex = 1;
            this.radioRemote.Text = "Remote computer";
            this.radioRemote.UseVisualStyleBackColor = true;
            this.radioRemote.CheckedChanged += new System.EventHandler(this.radioRemote_CheckedChanged);
            // 
            // radioButtonLocal
            // 
            this.radioButtonLocal.AutoSize = true;
            this.radioButtonLocal.Checked = true;
            this.radioButtonLocal.Location = new System.Drawing.Point(11, 27);
            this.radioButtonLocal.Name = "radioButtonLocal";
            this.radioButtonLocal.Size = new System.Drawing.Size(98, 17);
            this.radioButtonLocal.TabIndex = 0;
            this.radioButtonLocal.TabStop = true;
            this.radioButtonLocal.Text = "Local computer";
            this.radioButtonLocal.UseVisualStyleBackColor = true;
            this.radioButtonLocal.CheckedChanged += new System.EventHandler(this.radioButtonLocal_CheckedChanged);
            // 
            // panelStep2
            // 
            this.panelStep2.Controls.Add(this.textBoxRemotePlayerName);
            this.panelStep2.Controls.Add(this.label1);
            this.panelStep2.Controls.Add(this.labelGameName);
            this.panelStep2.Controls.Add(this.radioButtonColumns);
            this.panelStep2.Controls.Add(this.radioButtonRows);
            this.panelStep2.Location = new System.Drawing.Point(0, 0);
            this.panelStep2.Name = "panelStep2";
            this.panelStep2.Size = new System.Drawing.Size(269, 180);
            this.panelStep2.TabIndex = 1;
            // 
            // textBoxRemotePlayerName
            // 
            this.textBoxRemotePlayerName.Location = new System.Drawing.Point(91, 103);
            this.textBoxRemotePlayerName.Name = "textBoxRemotePlayerName";
            this.textBoxRemotePlayerName.Size = new System.Drawing.Size(121, 20);
            this.textBoxRemotePlayerName.TabIndex = 5;
            this.textBoxRemotePlayerName.Text = "My Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Player Name : ";
            // 
            // labelGameName
            // 
            this.labelGameName.AutoSize = true;
            this.labelGameName.Location = new System.Drawing.Point(11, 12);
            this.labelGameName.Name = "labelGameName";
            this.labelGameName.Size = new System.Drawing.Size(91, 13);
            this.labelGameName.TabIndex = 2;
            this.labelGameName.Text = "no game selected";
            // 
            // radioButtonColumns
            // 
            this.radioButtonColumns.AutoSize = true;
            this.radioButtonColumns.Location = new System.Drawing.Point(11, 73);
            this.radioButtonColumns.Name = "radioButtonColumns";
            this.radioButtonColumns.Size = new System.Drawing.Size(88, 17);
            this.radioButtonColumns.TabIndex = 1;
            this.radioButtonColumns.TabStop = true;
            this.radioButtonColumns.Text = "Play Columns";
            this.radioButtonColumns.UseVisualStyleBackColor = true;
            // 
            // radioButtonRows
            // 
            this.radioButtonRows.AutoSize = true;
            this.radioButtonRows.Location = new System.Drawing.Point(11, 50);
            this.radioButtonRows.Name = "radioButtonRows";
            this.radioButtonRows.Size = new System.Drawing.Size(75, 17);
            this.radioButtonRows.TabIndex = 0;
            this.radioButtonRows.TabStop = true;
            this.radioButtonRows.Text = "Play Rows";
            this.radioButtonRows.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(35, 203);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(197, 203);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "Next";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Enabled = false;
            this.buttonPrevious.Location = new System.Drawing.Point(116, 203);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(75, 23);
            this.buttonPrevious.TabIndex = 4;
            this.buttonPrevious.Text = "Previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // panelConnecting
            // 
            this.panelConnecting.Controls.Add(this.C);
            this.panelConnecting.Controls.Add(this.progressBar1);
            this.panelConnecting.Location = new System.Drawing.Point(1, 1);
            this.panelConnecting.Name = "panelConnecting";
            this.panelConnecting.Size = new System.Drawing.Size(269, 180);
            this.panelConnecting.TabIndex = 5;
            // 
            // C
            // 
            this.C.AutoSize = true;
            this.C.Location = new System.Drawing.Point(104, 57);
            this.C.Name = "C";
            this.C.Size = new System.Drawing.Size(61, 13);
            this.C.TabIndex = 1;
            this.C.Text = "Connecting";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(45, 95);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(187, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // timerConnecting
            // 
            this.timerConnecting.Tick += new System.EventHandler(this.timerConnecting_Tick);
            // 
            // panelSearchingGames
            // 
            this.panelSearchingGames.Controls.Add(this.label2);
            this.panelSearchingGames.Controls.Add(this.progressBar2);
            this.panelSearchingGames.Location = new System.Drawing.Point(-1, 2);
            this.panelSearchingGames.Name = "panelSearchingGames";
            this.panelSearchingGames.Size = new System.Drawing.Size(269, 180);
            this.panelSearchingGames.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Searching Games";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(45, 95);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(187, 23);
            this.progressBar2.TabIndex = 0;
            // 
            // timerSearchingGames
            // 
            this.timerSearchingGames.Tick += new System.EventHandler(this.timerSearchingGames_Tick);
            // 
            // panelSelectGame
            // 
            this.panelSelectGame.Controls.Add(this.listBoxGames);
            this.panelSelectGame.Controls.Add(this.labelSelectGame);
            this.panelSelectGame.Location = new System.Drawing.Point(-1, 1);
            this.panelSelectGame.Name = "panelSelectGame";
            this.panelSelectGame.Size = new System.Drawing.Size(269, 180);
            this.panelSelectGame.TabIndex = 7;
            // 
            // listBoxGames
            // 
            this.listBoxGames.FormattingEnabled = true;
            this.listBoxGames.Location = new System.Drawing.Point(65, 40);
            this.listBoxGames.Name = "listBoxGames";
            this.listBoxGames.Size = new System.Drawing.Size(148, 121);
            this.listBoxGames.TabIndex = 1;
            this.listBoxGames.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxGames_MouseDoubleClick);
            this.listBoxGames.SelectedIndexChanged += new System.EventHandler(this.listBoxGames_SelectedIndexChanged);
            // 
            // labelSelectGame
            // 
            this.labelSelectGame.AutoSize = true;
            this.labelSelectGame.Location = new System.Drawing.Point(15, 12);
            this.labelSelectGame.Name = "labelSelectGame";
            this.labelSelectGame.Size = new System.Drawing.Size(105, 13);
            this.labelSelectGame.TabIndex = 0;
            this.labelSelectGame.Text = "Select Game Server:";
            // 
            // ConnectToSharedGameDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 233);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.panelSelectGame);
            this.Controls.Add(this.panelSearchingGames);
            this.Controls.Add(this.panelConnecting);
            this.Controls.Add(this.panelStep2);
            this.Controls.Add(this.panelStep1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectToSharedGameDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Connect to game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectToSharedGameDialog_FormClosing);
            this.Load += new System.EventHandler(this.ConnectToSharedGameDialog_Load);
            this.panelStep1.ResumeLayout(false);
            this.panelStep1.PerformLayout();
            this.panelStep2.ResumeLayout(false);
            this.panelStep2.PerformLayout();
            this.panelConnecting.ResumeLayout(false);
            this.panelConnecting.PerformLayout();
            this.panelSearchingGames.ResumeLayout(false);
            this.panelSearchingGames.PerformLayout();
            this.panelSelectGame.ResumeLayout(false);
            this.panelSelectGame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelStep1;
        private System.Windows.Forms.Panel panelStep2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.RadioButton radioButtonLocal;
        private System.Windows.Forms.TextBox textBoxRemoteAddress;
        private System.Windows.Forms.RadioButton radioRemote;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.RadioButton radioButtonColumns;
        private System.Windows.Forms.RadioButton radioButtonRows;
        private System.Windows.Forms.Label labelGameName;
        private System.Windows.Forms.TextBox textBoxRemotePlayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelConnecting;
        private System.Windows.Forms.Label C;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timerConnecting;
        private System.Windows.Forms.Panel panelSearchingGames;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Timer timerSearchingGames;
        private System.Windows.Forms.Panel panelSelectGame;
        private System.Windows.Forms.Label labelSelectGame;
        private System.Windows.Forms.ListBox listBoxGames;
        private System.Windows.Forms.RadioButton radioButtonSearchGames;
    }
}