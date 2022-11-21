namespace MatixGameForm
{
    partial class NewSharedGameDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxGameName = new System.Windows.Forms.TextBox();
            this.radioButtonRowRemote = new System.Windows.Forms.RadioButton();
            this.radioButtonColumnRemote = new System.Windows.Forms.RadioButton();
            this.radioButtonBothRemote = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shared game name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Choose Remote Players:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxGameName
            // 
            this.textBoxGameName.Location = new System.Drawing.Point(121, 14);
            this.textBoxGameName.Name = "textBoxGameName";
            this.textBoxGameName.Size = new System.Drawing.Size(124, 20);
            this.textBoxGameName.TabIndex = 2;
            // 
            // radioButtonRowRemote
            // 
            this.radioButtonRowRemote.AutoSize = true;
            this.radioButtonRowRemote.Checked = true;
            this.radioButtonRowRemote.Location = new System.Drawing.Point(66, 80);
            this.radioButtonRowRemote.Name = "radioButtonRowRemote";
            this.radioButtonRowRemote.Size = new System.Drawing.Size(123, 17);
            this.radioButtonRowRemote.TabIndex = 3;
            this.radioButtonRowRemote.TabStop = true;
            this.radioButtonRowRemote.Text = "Row player is remote";
            this.radioButtonRowRemote.UseVisualStyleBackColor = true;
            // 
            // radioButtonColumnRemote
            // 
            this.radioButtonColumnRemote.AutoSize = true;
            this.radioButtonColumnRemote.Location = new System.Drawing.Point(66, 103);
            this.radioButtonColumnRemote.Name = "radioButtonColumnRemote";
            this.radioButtonColumnRemote.Size = new System.Drawing.Size(136, 17);
            this.radioButtonColumnRemote.TabIndex = 4;
            this.radioButtonColumnRemote.TabStop = true;
            this.radioButtonColumnRemote.Text = "Column player is remote";
            this.radioButtonColumnRemote.UseVisualStyleBackColor = true;
            // 
            // radioButtonBothRemote
            // 
            this.radioButtonBothRemote.AutoSize = true;
            this.radioButtonBothRemote.Location = new System.Drawing.Point(66, 127);
            this.radioButtonBothRemote.Name = "radioButtonBothRemote";
            this.radioButtonBothRemote.Size = new System.Drawing.Size(136, 17);
            this.radioButtonBothRemote.TabIndex = 5;
            this.radioButtonBothRemote.TabStop = true;
            this.radioButtonBothRemote.Text = "Both players are remote";
            this.radioButtonBothRemote.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(170, 162);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(89, 162);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // NewSharedGameDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(253, 196);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.radioButtonBothRemote);
            this.Controls.Add(this.radioButtonColumnRemote);
            this.Controls.Add(this.radioButtonRowRemote);
            this.Controls.Add(this.textBoxGameName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewSharedGameDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Start a Shared Game";
            this.Load += new System.EventHandler(this.NewSharedGameDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxGameName;
        private System.Windows.Forms.RadioButton radioButtonRowRemote;
        private System.Windows.Forms.RadioButton radioButtonColumnRemote;
        private System.Windows.Forms.RadioButton radioButtonBothRemote;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}