using System.Collections.Generic;

namespace Ex02
{
    internal class GameBoard
    {
        private readonly int r_BoardSize;
        private readonly Piece[,] m_Board;

        internal GameBoard(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            m_Board = new Piece[r_BoardSize, r_BoardSize];
        }

        internal int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }

        internal Piece GetPieceInCell(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col];
        }

        internal Piece GetPieceInCellByPosition(Point i_Position)
        {
            return GetPieceInCell(i_Position.X, i_Position.Y);
        }

        internal bool IsOpponentPieceInCell(Point i_Position, Piece i_CurrentPiece)
        {
            Piece pieceAtGivenPosition = GetPieceInCell(i_Position.X, i_Position.Y);
            bool isOpponentPiece;
        
            if (pieceAtGivenPosition == null)
            {
                isOpponentPiece = false;
            }
            else
            {
                isOpponentPiece = i_CurrentPiece.IsOpponentPiece(pieceAtGivenPosition.Type);
            }

            return isOpponentPiece;
        }

        internal void CheckIfPieceInKingPositionAndPromote(Piece i_Piece)
        {
            if (i_Piece.Type == ePieceType.X && i_Piece.Position.X == 0)
            {
                i_Piece.PromoteToKing();
            }
            else if (i_Piece.Type == ePieceType.O && i_Piece.Position.X == BoardSize - 1)
            {
                i_Piece.PromoteToKing();
            }
        }

        internal bool IsCellEmpty(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col] == null;
        }

        internal bool IsCellInBoard(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < r_BoardSize && i_Col >= 0 && i_Col < r_BoardSize;
        }

        internal void UpdateMoveOnBoard(Move i_Move, Piece i_PieceToMove)
        {
            m_Board[i_Move.From.X, i_Move.From.Y] = null;
            m_Board[i_Move.To.X, i_Move.To.Y] = i_PieceToMove;
            i_PieceToMove.Position = i_Move.To;
        }

        internal void RemovePieceFromBoard(Piece i_PieceToRemove)
        {
            m_Board[i_PieceToRemove.Position.X, i_PieceToRemove.Position.Y] = null;
        }

        internal void InitializeStartingBoard(Player i_Player1, Player i_Player2)
        {
            i_Player1.PlayerPieces = new List<Piece>();
            i_Player2.PlayerPieces = new List<Piece>();

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if (row < r_BoardSize / 2 - 1 && (row + col) % 2 != 0)
                    {
                        Piece piece = new Piece(ePieceType.O, row, col);
                        m_Board[row, col] = piece;
                        i_Player2.PlayerPieces.Add(piece);
                    }
                    else if (row > r_BoardSize / 2 && (row + col) % 2 != 0)
                    {
                        Piece piece = new Piece(ePieceType.X, row, col);
                        m_Board[row, col] = piece;
                        i_Player1.PlayerPieces.Add(piece);
                    }
                    else
                    {
                        m_Board[row, col] = null;
                    }
                }
            }
        }
    }
}
