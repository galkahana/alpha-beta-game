namespace MatixGameForm
{
    partial class MatixOptionsView
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxStartingGame = new System.Windows.Forms.ComboBox();
            this.comboBoxRowPlayer = new System.Windows.Forms.ComboBox();
            this.comboBoxColumnPlayer = new System.Windows.Forms.ComboBox();
            this.numericUpDownMaxSearchLevel = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHintSearchLevel = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownProfilerSearchLevel = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxSearchLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHintSearchLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProfilerSearchLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(173, 217);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(35, 217);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Player Starting The Game:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Row Player Identity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Column Player Identity:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Maximum Computer Search Level:";
            // 
            // comboBoxStartingGame
            // 
            this.comboBoxStartingGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartingGame.FormattingEnabled = true;
            this.comboBoxStartingGame.Items.AddRange(new object[] {
            "Row",
            "Column"});
            this.comboBoxStartingGame.Location = new System.Drawing.Point(192, 26);
            this.comboBoxStartingGame.Name = "comboBoxStartingGame";
            this.comboBoxStartingGame.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStartingGame.TabIndex = 6;
            // 
            // comboBoxRowPlayer
            // 
            this.comboBoxRowPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRowPlayer.FormattingEnabled = true;
            this.comboBoxRowPlayer.Items.AddRange(new object[] {
            "Computer",
            "Human"});
            this.comboBoxRowPlayer.Location = new System.Drawing.Point(192, 56);
            this.comboBoxRowPlayer.Name = "comboBoxRowPlayer";
            this.comboBoxRowPlayer.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRowPlayer.TabIndex = 7;
            // 
            // comboBoxColumnPlayer
            // 
            this.comboBoxColumnPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColumnPlayer.FormattingEnabled = true;
            this.comboBoxColumnPlayer.Items.AddRange(new object[] {
            "Computer",
            "Human"});
            this.comboBoxColumnPlayer.Location = new System.Drawing.Point(192, 89);
            this.comboBoxColumnPlayer.Name = "comboBoxColumnPlayer";
            this.comboBoxColumnPlayer.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColumnPlayer.TabIndex = 8;
            // 
            // numericUpDownMaxSearchLevel
            // 
            this.numericUpDownMaxSearchLevel.Location = new System.Drawing.Point(192, 123);
            this.numericUpDownMaxSearchLevel.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownMaxSearchLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxSearchLevel.Name = "numericUpDownMaxSearchLevel";
            this.numericUpDownMaxSearchLevel.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownMaxSearchLevel.TabIndex = 9;
            this.numericUpDownMaxSearchLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownHintSearchLevel
            // 
            this.numericUpDownHintSearchLevel.Location = new System.Drawing.Point(192, 153);
            this.numericUpDownHintSearchLevel.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownHintSearchLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHintSearchLevel.Name = "numericUpDownHintSearchLevel";
            this.numericUpDownHintSearchLevel.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownHintSearchLevel.TabIndex = 11;
            this.numericUpDownHintSearchLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Maximum Hint Search Level:";
            // 
            // numericUpDownProfilerSearchLevel
            // 
            this.numericUpDownProfilerSearchLevel.Location = new System.Drawing.Point(191, 185);
            this.numericUpDownProfilerSearchLevel.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownProfilerSearchLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownProfilerSearchLevel.Name = "numericUpDownProfilerSearchLevel";
            this.numericUpDownProfilerSearchLevel.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownProfilerSearchLevel.TabIndex = 13;
            this.numericUpDownProfilerSearchLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Maximum Profiler Search Level:";
            // 
            // MatixOptionsView
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(336, 271);
            this.Controls.Add(this.numericUpDownProfilerSearchLevel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDownHintSearchLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownMaxSearchLevel);
            this.Controls.Add(this.comboBoxColumnPlayer);
            this.Controls.Add(this.comboBoxRowPlayer);
            this.Controls.Add(this.comboBoxStartingGame);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MatixOptionsView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Matix Options";
            this.Load += new System.EventHandler(this.MatixOptionsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxSearchLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHintSearchLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProfilerSearchLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxStartingGame;
        private System.Windows.Forms.ComboBox comboBoxRowPlayer;
        private System.Windows.Forms.ComboBox comboBoxColumnPlayer;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxSearchLevel;
        private System.Windows.Forms.NumericUpDown numericUpDownHintSearchLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownProfilerSearchLevel;
        private System.Windows.Forms.Label label6;
    }
}