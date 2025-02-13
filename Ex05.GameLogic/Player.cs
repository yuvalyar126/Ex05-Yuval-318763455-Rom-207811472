using Ex05.Enums;
using System.Collections.Generic;

namespace Ex05.GameLogic
{
    public enum ePlayerType
    {
        FirstPlayer,
        SecondPlayer,
    }

    public class Player
    {
        private readonly string r_PlayerName;
        private readonly ePieceType r_PlayerPieceType;
        private readonly ePieceColor r_PlayerColor;
        private int m_Points = 0;
        private bool m_IsComputer;
        private bool m_MustEat = false;
        private List<Piece> m_PlayerPieces;
        

        public Player(string i_PlayerName, ePieceType i_PlayerPieceType, bool i_IsComputer, ePieceColor i_PlayerPieceColor)
        {
            r_PlayerName = i_PlayerName;
            r_PlayerPieceType = i_PlayerPieceType;
            m_IsComputer = i_IsComputer;
            r_PlayerColor = i_PlayerPieceColor;
        }

        public string PlayerName
        {
            get
            {
                return r_PlayerName;
            }
        }

        public ePieceType PlayerPieceType
        {
            get
            {
                return r_PlayerPieceType;
            }
        }

        public int Points
        {
            get
            {
                return m_Points;
            }

            set
            {
                m_Points = value;
            }
        }

        public bool MustEatAgain
        {
            get
            {
                return m_MustEat;
            }

            set
            {
                m_MustEat = value;
            }
        }

        public List<Piece> PlayerPieces
        {
            get
            {
                return m_PlayerPieces;
            }

            set
            {
                m_PlayerPieces = value;
            }
        }

        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
        }

        public ePieceColor PlayerColor
        {
            get
            {
                return r_PlayerColor;
            }
        }

        public void CreateMovesLists(GameBoard i_GameBoard, List<Move> i_RegularMovesList, List<Move> i_EatingMovesList)
        {
            foreach (Piece piece in m_PlayerPieces)
            {
                piece.AddMovesToLists(i_GameBoard, i_RegularMovesList, i_EatingMovesList);
            }
        }

        public void RemovePieceFromList(Piece i_PieceToRemove)
        {
            m_PlayerPieces.Remove(i_PieceToRemove);
        }

        public bool IsPlayerHasMoves(GameBoard i_GameBoard)
        {
            bool isPlayerHasMoves = false;
            List<Move> regularMovesList = new List<Move>();
            List<Move> eatingMovesList = new List<Move>();

            CreateMovesLists(i_GameBoard, regularMovesList, eatingMovesList);
            if (eatingMovesList.Count > 0 || regularMovesList.Count > 0)
            {
                isPlayerHasMoves = true;
            }

            return isPlayerHasMoves;
        }

        public int CalculatePiecesValues()
        {
            int Total = 0;

            foreach (Piece piece in m_PlayerPieces)
            {
                Total += piece.IsKing ? 4 : 1;
            }

            return Total;
        }
    }
}
