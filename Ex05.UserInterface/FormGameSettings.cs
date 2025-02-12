using Ex05.GameLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.UserInterface
{
    public partial class FormGameSettings : Form
    {
        // member field
        private const string m_DefaultPlayer2Name = "Computer";

        public FormGameSettings()
        {
            InitializeComponent();
        }

        /*
         * Get and Set the BoardSize.
         */
        public eBoardSize BoardSize
        {
            get
            {
                eBoardSize BoardSize;

                if (radioButton6X6.Checked)
                {
                    BoardSize = eBoardSize.Small;
                }
                else if (radioButton8X8.Checked)
                {
                    BoardSize = eBoardSize.Medium;
                }
                else
                {
                    BoardSize = eBoardSize.Large;
                }

                return BoardSize;
            }
        }

        /*
         * Get the Player1Name.
         */
        public string Player1Name
        {
            get { return textBoxPlayer1.Text; }
        }

        /*
         * Get the Player2Name.
         */
        public string Player2Name
        {
            get
            {
                string returnName;

                if (checkBoxPlayer2.Checked)
                {
                    returnName = textBoxPlayer2.Text;
                }
                else
                {
                    returnName = m_DefaultPlayer2Name;
                }

                return returnName;
            }
        }

        /*
         * Get if checkbox player2 is checked.
         */
        public bool CheckBoxPlayer2Checked
        {
            get { return checkBoxPlayer2.Checked; }
        }

        /*
         *  checkBoxPlayer2_CheckedChanged is an event handler method that will execute when player2 check box is checked.
         */
        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                string defaultName = string.Format("[{0}]", m_DefaultPlayer2Name);
                textBoxPlayer2.Text = defaultName;
            }
        }
    }
}
