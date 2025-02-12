namespace Ex05.GameLogic
{
    public class Point
    {
        private int m_X;
        private int m_Y;

        public Point(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }

        public int X
        {
            get
            {
                return m_X;
            }

            set
            {
                m_X = value;
            }
        }

        public int Y
        {
            get
            {
                return m_Y;
            }

            set
            {
                m_Y = value;
            }
        }

        public override bool Equals(object i_Obj)
        {
            if (!(i_Obj is Point))
            {
                return false;
            }

            Point other = (Point)i_Obj;
            return X == other.X && Y == other.Y;
        }
    }
}
