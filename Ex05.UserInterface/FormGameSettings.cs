using Ex05.GameLogic;
using Ex05.Enums;
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
        private const string m_DefaultPlayer2Name = "Computer";

        public FormGameSettings()
        {
            InitializeComponent();
        }

   
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

        public string Player1Name
        {
            get { return textBoxPlayer1.Text; }
        }

   
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

    
        public bool IsPlayer2Computer
        {
            get { return !checkBoxPlayer2.Checked; }
        }

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
