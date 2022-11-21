using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MatixGameForm
{
    internal partial class NewSharedGameDialog : Form
    {
        internal MatixSharedGameOptions mSharedGameOptions;

        public NewSharedGameDialog()
        {
            InitializeComponent();
        }

        public MatixSharedGameOptions SharedGameOptions
        {
            get { return mSharedGameOptions; }
            set { mSharedGameOptions = value; }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void NewSharedGameDialog_Load(object sender, EventArgs e)
        {
            if (null == mSharedGameOptions)
                mSharedGameOptions = new MatixSharedGameOptions();
            textBoxGameName.Text = mSharedGameOptions.mSharedGameName;
            switch (mSharedGameOptions.mSharedPlayers)
            {
                case ESharedPlayersOptions.eRowShared:
                    radioButtonRowRemote.Checked = true;
                    break;
                case ESharedPlayersOptions.eColumnShared:
                    radioButtonColumnRemote.Checked = true;
                    break;
                case ESharedPlayersOptions.eBothShared:
                    radioButtonBothRemote.Checked = true;
                    break;
            }
        }

        private bool ValidateForm()
        {
            if (textBoxGameName.Text.Length > 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please choose a name for the shared game.",
                                "New shared game");
                return false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                mSharedGameOptions.mSharedGameName = textBoxGameName.Text;
                if (radioButtonRowRemote.Checked)
                    mSharedGameOptions.mSharedPlayers = ESharedPlayersOptions.eRowShared;
                else if (radioButtonColumnRemote.Checked)
                    mSharedGameOptions.mSharedPlayers = ESharedPlayersOptions.eColumnShared;
                else
                    mSharedGameOptions.mSharedPlayers = ESharedPlayersOptions.eBothShared;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}