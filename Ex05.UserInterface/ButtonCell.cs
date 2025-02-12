using Ex05.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.UserInterface
{
    public class ButtonCell : Button
    {
        private Point m_LocationInBoard;

        public ButtonCell(int i_Row, int i_Column)
        {
            m_LocationInBoard = new Point(i_Column, i_Row);
        }

        public Point LocationInBoard
        {
            get
            {
                return m_LocationInBoard;
            }

            set
            {
                m_LocationInBoard = value;
            }
        }
    }
}
