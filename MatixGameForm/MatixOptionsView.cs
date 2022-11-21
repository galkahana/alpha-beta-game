using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MatixGameForm
{
    internal partial class MatixOptionsView : Form
    {
        public MatixOptions mMatixOptions;

        public MatixOptionsView()
        {
            InitializeComponent();
        }

        private void MatixOptionsView_Load(object sender, EventArgs e)
        {
            if (null == mMatixOptions)
                mMatixOptions = new MatixOptions();
            if (EMatixMoveType.eMatixMoveAutomatic == mMatixOptions.mColumnMoveType)
                comboBoxColumnPlayer.SelectedIndex = 0;
            else
                comboBoxColumnPlayer.SelectedIndex = 1;

            if (EMatixMoveType.eMatixMoveAutomatic == mMatixOptions.mRowMoveType)
                comboBoxRowPlayer.SelectedIndex = 0;
            else
                comboBoxRowPlayer.SelectedIndex = 1;

            numericUpDownMaxSearchLevel.Value = mMatixOptions.mMaximumMoveSearchLevel;
            numericUpDownHintSearchLevel.Value = mMatixOptions.mMaximumHintMoveSearchLevel;
            numericUpDownProfilerSearchLevel.Value = mMatixOptions.mMaximumProfilerSearchLevel;

            if (EMatixPlayerType.eMatixPlayerColumns == mMatixOptions.mWhoStartsTheGame)
                comboBoxStartingGame.SelectedIndex = 1;
            else
                comboBoxStartingGame.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            mMatixOptions.mColumnMoveType = (0 == comboBoxColumnPlayer.SelectedIndex ? 
                                             EMatixMoveType.eMatixMoveAutomatic : 
                                             EMatixMoveType.eMatixMoveManual);

            mMatixOptions.mRowMoveType = (0 == comboBoxRowPlayer.SelectedIndex ?
                                             EMatixMoveType.eMatixMoveAutomatic :
                                             EMatixMoveType.eMatixMoveManual);

            mMatixOptions.mMaximumMoveSearchLevel = (int)numericUpDownMaxSearchLevel.Value;
            mMatixOptions.mMaximumHintMoveSearchLevel = (int)numericUpDownHintSearchLevel.Value;
            mMatixOptions.mMaximumProfilerSearchLevel = (int)numericUpDownProfilerSearchLevel.Value;


            mMatixOptions.mWhoStartsTheGame = (0 == comboBoxStartingGame.SelectedIndex ?
                                               EMatixPlayerType.eMatixPlayerRows :
                                               EMatixPlayerType.eMatixPlayerColumns);

            MessageBox.Show("All changes will take effect in the next game");
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}