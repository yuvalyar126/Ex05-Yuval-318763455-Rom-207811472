namespace Ex05.GameLogic
{
    public class Move
    {
        private Point m_From;
        private Point m_To;
        private Point m_EatenPieceLocation;
        private bool m_IsEatingMove = false;

        public Move(){} // Empty ctor

        public Move(Point i_From, Point i_To)
        {
            m_From = i_From;
            m_To = i_To;
        }

        public Move(string i_MoveString)
        {
            m_From = new Point(i_MoveString[0] - 'A', i_MoveString[1] - 'a');
            m_To = new Point(i_MoveString[3] - 'A', i_MoveString[4] - 'a');
        }

        public Move(Point i_From, Point i_To, Point i_EatenPieceLocation)
        {
            m_From = i_From;
            m_To = i_To;
            m_EatenPieceLocation = i_EatenPieceLocation;
            m_IsEatingMove = true;
        }

        public Point From
        {
            get
            {
                return m_From;
            }
            set
            {
                m_From = value;
            }
        }

        public Point To
        {
            get
            {
                return m_To;
            }
            set
            {
                m_To = value;
            }
        }

        public bool IsEatingMove
        {
            get
            {
                return m_IsEatingMove;
            }
        }

        public Point EatenPieceLocation
        {
            get
            {
                return m_EatenPieceLocation;
            }
        }
    }
}