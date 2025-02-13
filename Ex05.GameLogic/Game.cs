using Ex05.Enums;
using System;
using System.Collections.Generic;

namespace Ex05.GameLogic
{
    public enum eGameStatusOptions
    {
        Running,
        Win,
        Draw,
        Exit,
    }


    public class Game
    {
        public event Action<Point, Point> MoveExecuted;
        public event Action ActivePlayerChanged;
        public event Action <Point> PieceEaten;
        public event Action <Point, Enums.ePieceColor> BecameKing;



        private GameBoard m_GameBoard;
        private Player m_CurrentPlayer;
        private Player m_NextPlayer;
        private eGameStatusOptions m_Status = eGameStatusOptions.Running;
        private List<Move> m_PlayerRegularMoves = new List<Move>();
        private List<Move> m_PlayerEatingMoves = new List<Move>();
        public static Random s_Rand = new Random();


        protected virtual void OnMoveExecuted(Point i_From, Point i_To)
        {
            MoveExecuted?.Invoke(i_From, i_To);
        }

        protected virtual void OnActivePlayerChanged()
        {
            ActivePlayerChanged?.Invoke();
        }

        protected virtual void OnPieceEaten(Point i_EatenPieceLocation)
        {
            PieceEaten?.Invoke(i_EatenPieceLocation);
        }

        protected virtual void OnBecameKing(Point i_NewKingLocation, ePieceColor i_KingColor)
        {
            BecameKing?.Invoke(i_NewKingLocation, i_KingColor);
        }


        public Game(int i_BoardSize, Player i_CurrentPlayer, Player i_NextPlayer)
        {
            m_GameBoard = new GameBoard(i_BoardSize);
            m_CurrentPlayer = i_CurrentPlayer;
            m_NextPlayer = i_NextPlayer;
            m_GameBoard.InitializeStartingBoard(i_CurrentPlayer, i_NextPlayer);
            CurrentPlayer.CreateMovesLists(m_GameBoard, m_PlayerRegularMoves, m_PlayerEatingMoves);
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public Player NextPlayer
        {
            get
            {
                return m_NextPlayer;
            }
        }

        public eGameStatusOptions Status
        {
            get
            {
                return m_Status;
            }

            set
            {
                m_Status = value;
            }

        }

        public GameBoard GameBoard
        {
            get
            {
                return m_GameBoard;
            }
        }

        private void switchTurnsBetweenPlayers()
        {
            Player temp = m_CurrentPlayer;
            m_CurrentPlayer = m_NextPlayer;
            m_NextPlayer = temp;
        }

        private void executeMove(Move i_MoveToExecute, Piece i_PieceToMove)
        {
            m_GameBoard.UpdateMoveOnBoard(i_MoveToExecute, i_PieceToMove);
            OnMoveExecuted(i_MoveToExecute.From, i_MoveToExecute.To);

            CheckIfPieceInKingPositionAndPromote(i_PieceToMove);

            if (i_MoveToExecute.IsEatingMove)
            {
                Piece eatenPiece = m_GameBoard.GetPieceInCellByPosition(i_MoveToExecute.EatenPieceLocation);
                m_GameBoard.RemovePieceFromBoard(eatenPiece);
                m_NextPlayer.RemovePieceFromList(eatenPiece);
                OnPieceEaten(i_MoveToExecute.EatenPieceLocation);
            }
        }


        public void CheckIfPieceInKingPositionAndPromote(Piece i_Piece)
        {
            if (i_Piece.Color == Enums.ePieceColor.Black && i_Piece.Position.X == 0 || i_Piece.Color == Enums.ePieceColor.Red && i_Piece.Position.X == m_GameBoard.BoardSize - 1)
            {
                i_Piece.PromoteToKing();
                OnBecameKing(i_Piece.Position, i_Piece.Color);
            }
        }

        private bool isMoveInList(Move i_Move, List<Move> i_MoveList)
        {
            bool isMoveInList = false;

            foreach (Move move in i_MoveList)
            {
                if (move.From.Equals(i_Move.From) && move.To.Equals(i_Move.To))
                {
                    isMoveInList = true;
                }
            }

            return isMoveInList;
        }

        private Move getMoveFromLists(Move i_Move)
        {
            Move move = null;

            if (isMoveInList(i_Move, m_PlayerRegularMoves))
            {
                move = m_PlayerRegularMoves.Find(m => m.From.Equals(i_Move.From) && m.To.Equals(i_Move.To));
            }
            else if (isMoveInList(i_Move, m_PlayerEatingMoves))
            {
                move = m_PlayerEatingMoves.Find(m => m.From.Equals(i_Move.From) && m.To.Equals(i_Move.To));
            }

            return move;
        }

        private bool isLegalMove(Move i_MoveToCheck)
        {
            bool isLegal = false;

            if (i_MoveToCheck == null)
            {
                isLegal = false;
               // UserInterface.PrintInvalidMoveMessage();

            }
            else if (m_PlayerEatingMoves.Count != 0)
            {
                if (isMoveInList(i_MoveToCheck, m_PlayerEatingMoves))
                {
                    isLegal = true;
                }
                else
                {
                 //   UserInterface.PrintInvalidMustEatMessage();
                }

            }
            else
            {
                if (isMoveInList(i_MoveToCheck, m_PlayerRegularMoves))
                {
                    isLegal = true;
                }
                else
                {
                   // UserInterface.PrintInvalidMoveMessage();
                }
            }

            return isLegal;
        }

        //public Move PlayTurn(out string o_EndGameMessage)
        //{
        //    Move currentMove;
        //    o_EndGameMessage = string.Empty;

        //    if (m_CurrentPlayer.IsComputer)
        //    {
        //        currentMove = getComputerMove();
        //    }
        //    else
        //    {

        //        currentMove = getHumanMove(out o_EndGameMessage);
        //    }

        //    if (currentMove != null)
        //    {
        //        MakeMove(currentMove, m_GameBoard.GetPieceInCellByPosition(currentMove.From), out o_EndGameMessage);
        //    }

        //    return currentMove;
        //}




        public Move PlayTurn(Point i_From, Point i_To, out string o_EndGameMessage)
        {
            Move currentMove = new Move(i_From, i_To);
            currentMove = getMoveFromLists(currentMove);
            o_EndGameMessage = string.Empty;

            if (currentMove != null)
            {
                if (isLegalMove(currentMove))
                {
                    MakeMove(currentMove, m_GameBoard.GetPieceInCellByPosition(currentMove.From), out o_EndGameMessage);
                }
            }

            return currentMove;
        }



        private Move getComputerMove()
        {
            Move computerMove = new Move();
            int randomIndextMove;

            if (m_PlayerEatingMoves.Count != 0)
            {
                randomIndextMove = s_Rand.Next(m_PlayerEatingMoves.Count);
                computerMove = m_PlayerEatingMoves[randomIndextMove];
            }
            else if (m_PlayerRegularMoves.Count != 0)
            {
                randomIndextMove = s_Rand.Next(m_PlayerRegularMoves.Count);
                computerMove = m_PlayerRegularMoves[randomIndextMove];
            }

            return computerMove;
        }

        private Move getHumanMove(out string o_EndGameMessage)
        {
            Move currentMove = new Move();
            bool isQuit = false;
            o_EndGameMessage = string.Empty;

            while (true)
            {
                string moveString = UserInterface.GetMoveStringFromUser(m_CurrentPlayer);
                if (moveString == "Q")
                {
                    currentMove = null;
                    isQuit = true;
                    switchTurnsBetweenPlayers();
                    checkEndGame(out o_EndGameMessage, isQuit);
                    break;
                }

                if (!InputValidator.IsMoveStringValid(moveString))
                {
                    UserInterface.PrintInvalidMoveStringMessage();
                    continue;
                }

                currentMove = new Move(moveString);
                Piece movingPiece = m_GameBoard.GetPieceInCellByPosition(currentMove.From);
                if (movingPiece == null || movingPiece.IsOpponentPiece(m_CurrentPlayer.PlayerPieceType))
                {
                    UserInterface.PrintInvalidMoveMessage();
                    continue;
                }

                currentMove = getMoveFromLists(currentMove);
                if (!isLegalMove(currentMove))
                {
                    continue;
                }

                break;
            }

            return currentMove;
        }

        public void MakeMove(Move io_MoveToPlay, Piece io_PieceToMove, out string o_EndGameMessage)
        {
            executeMove(io_MoveToPlay, io_PieceToMove);
            checkEndGame(out o_EndGameMessage, false);
            if (m_Status != eGameStatusOptions.Running)
            {
                return;
            }
            prepareForExtraTurn(io_PieceToMove, io_MoveToPlay);
            if (m_PlayerEatingMoves.Count > 0 && io_MoveToPlay.IsEatingMove)
            {
                CurrentPlayer.MustEatAgain = true;
                return;
            }
            CurrentPlayer.MustEatAgain = false;
            prepareForNextTurn();
            
        }

        private void prepareForExtraTurn(Piece i_movingPiece, Move i_Move)
        {
            m_PlayerEatingMoves.Clear();
            i_movingPiece.AddEatingMovesToList(m_GameBoard, m_PlayerEatingMoves);
        }

        private void prepareForNextTurn()
        {
            m_PlayerRegularMoves.Clear();
            m_PlayerEatingMoves.Clear();
            switchTurnsBetweenPlayers();
            OnActivePlayerChanged();

            m_CurrentPlayer.CreateMovesLists(m_GameBoard, m_PlayerRegularMoves, m_PlayerEatingMoves);
        }

        private void checkEndGame(out string o_EndGameMessage, bool i_isQuit)
        {
            string endGameMessage = string.Empty;
            int winnerPoints = 0;

            if (!m_CurrentPlayer.IsPlayerHasMoves(m_GameBoard) && !m_NextPlayer.IsPlayerHasMoves(m_GameBoard))
            {
                m_Status = eGameStatusOptions.Draw;
                endGameMessage = "It's a draw!";
            }

            else if (m_NextPlayer.PlayerPieces.Count == 0 || !m_NextPlayer.IsPlayerHasMoves(m_GameBoard) || i_isQuit)
            {
                m_Status = eGameStatusOptions.Win;
                winnerPoints = calculateWinnerPoints();
                m_CurrentPlayer.Points += winnerPoints;
                endGameMessage = (string.Format("{0} Won and got {1} more points! {2} have {3} points now.", m_CurrentPlayer.PlayerName, winnerPoints, m_CurrentPlayer.PlayerName, m_CurrentPlayer.Points));
            }

            o_EndGameMessage = endGameMessage;
        }

        private int calculateWinnerPoints()
        {
            int winnerPointsForRound = 0;
            int currentPlayerTotalValues = CurrentPlayer.CalculatePiecesValues();
            int nextPlayerTotalValues = NextPlayer.CalculatePiecesValues();

            if (m_Status == eGameStatusOptions.Win)
            {
                winnerPointsForRound = Math.Max(currentPlayerTotalValues - nextPlayerTotalValues, 0);
            }

            return winnerPointsForRound;
        }
    }
}



