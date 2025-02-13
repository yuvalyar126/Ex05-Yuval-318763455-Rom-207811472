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
    public partial class PlayerControl : UserControl
    {
        //private string m_PlayerName;
        //private int m_Score;
        private bool m_IsPlayingNow;

        public PlayerControl()
        {
            InitializeComponent();
        }

        public string PlayerName
        {
            get
            {
                return lblPlayerName.Text;
            }
            set
            {
                lblPlayerName.Text = value + ":";
            }
        }
        public string PlayerScore
        {
            get
            {
                return lblScore.Text;
            }

            set
            {
                lblScore.Text = value;
            }
        }

        public bool IsPlayingNow
        {
            get
            {
                return m_IsPlayingNow;
            }
        }

     

        public void SetTurn(bool isTurn)
        {
            m_IsPlayingNow = isTurn;
            this.BackColor = isTurn ? Color.LightBlue : SystemColors.Control;
            lblPlayerName.Font = isTurn ? new Font(lblPlayerName.Font, FontStyle.Bold) : new Font(lblPlayerName.Font, FontStyle.Regular);
        }

        public void FinishTurn()
        {
            m_IsPlayingNow = false;
            this.BackColor = SystemColors.Control;
            lblPlayerName.Font = new Font(lblPlayerName.Font, FontStyle.Regular);
        }
    }
}
