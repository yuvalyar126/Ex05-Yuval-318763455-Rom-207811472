using System.Collections.Generic;

namespace Ex02
{
    internal enum ePlayerType
    {
        FirstPlayer,
        SecondPlayer,
    }

    internal class Player
    {
        private readonly string r_PlayerName;
        private readonly ePieceType r_PlayerPieceSymbol;
        private int m_Points = 0;
        private bool m_IsComputer;
        private bool m_MustEat = false;
        private List<Piece> m_PlayerPieces;
        

        internal Player(string i_PlayerName, ePieceType i_PlayerCoinSymbol, bool i_IsComputer)
        {
            r_PlayerName = i_PlayerName;
            r_PlayerPieceSymbol = i_PlayerCoinSymbol;
            m_IsComputer = i_IsComputer;
        }

        internal string PlayerName
        {
            get
            {
                return r_PlayerName;
            }
        }

        internal ePieceType PlayerPieceSymbol
        {
            get
            {
                return r_PlayerPieceSymbol;
            }
        }

        internal int Points
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

        internal bool MustEatAgain
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

        internal List<Piece> PlayerPieces
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

        internal bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
        }

        internal void CreateMovesLists(GameBoard i_GameBoard, List<Move> i_RegularMovesList, List<Move> i_EatingMovesList)
        {
            foreach (Piece piece in m_PlayerPieces)
            {
                piece.AddMovesToLists(i_GameBoard, i_RegularMovesList, i_EatingMovesList);
            }
        }

        internal void RemovePieceFromList(Piece i_PieceToRemove)
        {
            m_PlayerPieces.Remove(i_PieceToRemove);
        }

        internal bool IsPlayerHasMoves(GameBoard i_GameBoard)
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

        internal int CalculatePiecesValues()
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
