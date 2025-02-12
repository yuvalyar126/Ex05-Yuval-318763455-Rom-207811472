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
        private string playerName;
        private int score;
        private bool isCurrentTurn;

        public PlayerControl()
        {
            InitializeComponent();
        }

        public void InitializePlayer(string name)
        {
            playerName = name;
            score = 0;
            isCurrentTurn = false;
            UpdateUI();
        }

        public void SetScore(int newScore)
        {
            score = newScore;
            lblScore.Text = $"Score: {score}";
        }

        public void SetTurn(bool isTurn)
        {
            isCurrentTurn = isTurn;
            this.BackColor = isTurn ? Color.LightBlue : SystemColors.Control;
            lblPlayerName.Font = isTurn ? new Font(lblPlayerName.Font, FontStyle.Bold) : new Font(lblPlayerName.Font, FontStyle.Regular);
        }

        private void UpdateUI()
        {
            lblPlayerName.Text = playerName;
            lblScore.Text = $"Score: {score}";
            SetTurn(isCurrentTurn);
        }

    }
}
