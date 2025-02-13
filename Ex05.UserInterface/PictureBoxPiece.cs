using Ex05.GameLogic;
using Ex05.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.UserInterface
{
    public class PictureBoxPiece : PictureBox
    {
        private ButtonCell m_CurrentCell = null;
        private Enums.ePieceColor m_PieceColor;

        public PictureBoxPiece(Enums.ePieceColor i_PieceColor)
        {
            m_PieceColor = i_PieceColor;
        }


        public ButtonCell CurrentCell
        {
            get
            {
                return m_CurrentCell;
            }

            set
            {
                m_CurrentCell = value;
                this.Location = m_CurrentCell.Location;
            }
        }

        public Enums.ePieceColor PieceColor
        {
            get
            {
                return m_PieceColor;
            }
        }
    }
}
