using Ex05.Enums;
using System.Collections.Generic;

namespace Ex05.GameLogic
{
    public enum ePieceType
    {
        X,
        O,
        U,
        K,
    }

    public enum eValue
    {
        Regular = 1,
        King = 4,
    }

    public class Piece
    {
        private ePieceType m_Type;
        private ePieceColor m_Color;
        private bool m_IsKing = false;
        private Point m_Position;


        public Piece(ePieceType type, int row, int column)
        {
            m_Type = type;
            m_Position = new Point(row, column);
        }

        public ePieceType Type
        {
            get
            {
                return m_Type;
            }
            set
            {
                m_Type = value;
            }
        }

        public bool IsKing
        {
            get
            {
                return m_IsKing;
            }
            set
            {
                m_IsKing = value;
            }
        }

        public Point Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        public ePieceColor Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        public void PromoteToKing()
        {
            m_IsKing = true;
            m_Type = m_Type == ePieceType.X ? ePieceType.K : ePieceType.U;
        }

        public bool IsOpponentPiece(ePieceType i_CurrentPieceType)
        {
            bool isOpponentPiece = false;

            if (i_CurrentPieceType == ePieceType.X || i_CurrentPieceType == ePieceType.K)
            {
                isOpponentPiece = m_Type == ePieceType.O || m_Type == ePieceType.U;
            }
            else if (i_CurrentPieceType == ePieceType.O || i_CurrentPieceType == ePieceType.K)
            {
                isOpponentPiece = m_Type == ePieceType.X || m_Type == ePieceType.K;
            }

            return isOpponentPiece;
        }

        private int getDirection()
        {
            int direction = m_Type == ePieceType.X || m_Type == ePieceType.K ? -1 : 1;

            return direction;
        }

        private bool canMove(GameBoard i_GameBoard, Point i_NextMoveCell)
        {
            int nextMoveRow = i_NextMoveCell.X, nextMoveCol = i_NextMoveCell.Y;

            return i_GameBoard.IsCellInBoard(nextMoveRow, nextMoveCol) && i_GameBoard.IsCellEmpty(nextMoveRow, nextMoveCol);
        }

        public void AddMovesToLists(GameBoard i_GameBoard, List<Move> i_RegularMovesList, List<Move> i_EatingMovesList)
        {
            addRegularMovesToList(i_GameBoard, i_RegularMovesList);
            AddEatingMovesToList(i_GameBoard, i_EatingMovesList);
        }

        private void addRegularMovesToList(GameBoard i_GameBoard, List<Move> i_RegularMovesList)
        {
            int moveDirection = getDirection();

            addRegularMovesPerDirection(i_GameBoard, i_RegularMovesList, moveDirection);
            if (m_IsKing)
            {
                moveDirection *= -1;
                addRegularMovesPerDirection(i_GameBoard, i_RegularMovesList, moveDirection);
            }
        }

        public void AddEatingMovesToList(GameBoard i_GameBoard, List<Move> i_EatingMovesList)
        {
            int moveDirection = getDirection();

            addEatingMovesPerDirection(i_GameBoard, i_EatingMovesList, moveDirection);
            if (m_IsKing)
            {
                moveDirection *= -1;
                addEatingMovesPerDirection(i_GameBoard, i_EatingMovesList, moveDirection);
            }
        }

        private void addRegularMovesPerDirection(GameBoard i_GameBoard, List<Move> i_RegularMovesList, int i_Direction)
        {
            for (int colOffset = -1; colOffset <= 1; colOffset += 2)
            {
                int newCol = m_Position.Y + colOffset;
                int newRow = m_Position.X + i_Direction;
                Point newPosition = new Point(newRow, newCol);
                Move move = new Move(m_Position, newPosition);

                if (canMove(i_GameBoard, move.To))
                {
                    i_RegularMovesList.Add(move);
                }
            }
        }

        private void addEatingMovesPerDirection(GameBoard i_GameBoard, List<Move> i_EatingMovesList, int i_Direction)
        {
            for (int colOffset = -2; colOffset <= 2; colOffset += 4)
            {
                int newCol = m_Position.Y + colOffset;
                int newRow = m_Position.X + 2 * i_Direction;
                Point newPosition = new Point(newRow, newCol);
                Point eatenPiecePosition = new Point(m_Position.X + i_Direction, m_Position.Y + colOffset / 2);

                if (canMove(i_GameBoard, newPosition) && i_GameBoard.IsOpponentPieceInCell(eatenPiecePosition, this))
                {
                    Move move = new Move(m_Position, newPosition, eatenPiecePosition);
                    i_EatingMovesList.Add(move);
                }
            }
        }
    }
}