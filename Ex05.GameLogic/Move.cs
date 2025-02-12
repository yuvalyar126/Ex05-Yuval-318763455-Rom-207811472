namespace Ex02
{
    internal class Move
    {
        private Point m_From;
        private Point m_To;
        private Point m_EatenPieceLocation;
        private bool m_IsEatingMove = false;

        internal Move(){} // Empty ctor

        internal Move(Point i_From, Point i_To)
        {
            m_From = i_From;
            m_To = i_To;
        }

        internal Move(string i_MoveString)
        {
            m_From = new Point(i_MoveString[0] - 'A', i_MoveString[1] - 'a');
            m_To = new Point(i_MoveString[3] - 'A', i_MoveString[4] - 'a');
        }

        internal Move(Point i_From, Point i_To, Point i_EatenPieceLocation)
        {
            m_From = i_From;
            m_To = i_To;
            m_EatenPieceLocation = i_EatenPieceLocation;
            m_IsEatingMove = true;
        }

        internal Point From
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

        internal Point To
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

        internal bool IsEatingMove
        {
            get
            {
                return m_IsEatingMove;
            }
        }

        internal Point EatenPieceLocation
        {
            get
            {
                return m_EatenPieceLocation;
            }
        }
    }
}