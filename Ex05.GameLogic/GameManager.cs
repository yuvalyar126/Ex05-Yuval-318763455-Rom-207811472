using Ex05.Enums;
using System;

namespace Ex05.GameLogic
{
    public class GameManager
    {
        private readonly int r_BoardSize;
        private readonly Player r_PlayerOne;
        private readonly Player r_PlayerTwo;
        private Game m_Game = null;

        
        



        

        //protected virtual void OnCellChanged(Point i_From, Point i_To)
        //{
        //    CellChanged?.Invoke(i_From, i_To);
        //}

        public GameManager(eGameMode i_GameMode, string i_FirstPlayerName, string i_SecondPlayerName, int i_BoardSize)
        {
            bool isComputer = i_GameMode == eGameMode.PlayerAgainstComputer;

            r_BoardSize = i_BoardSize;
            r_PlayerOne = new Player(i_FirstPlayerName, ePieceType.X, false, ePieceColor.Black);
            r_PlayerTwo = new Player(i_SecondPlayerName, ePieceType.O, isComputer, ePieceColor.Red);
            m_Game = new Game(r_BoardSize, r_PlayerOne, r_PlayerTwo);
        }


        public Game Game
        {
            get
            {
                return m_Game;
            }
        }


       


        //public void playNewGame()
        //{
        //    eGameStatusOptions gameStatus;
        //    do
        //    {
        //        gameStatus = GameLoop();
        //    } while (gameStatus != eGameStatusOptions.Exit);
        //}




        public eGameStatusOptions GameLoop(Point i_From, Point i_To)
        {
            eGameStatusOptions gameStatus = eGameStatusOptions.Running;
            
            Move lastMove = null;
            string endGameMessage = string.Empty;

            if (gameStatus == eGameStatusOptions.Running)
            {
                
                if (!m_Game.CurrentPlayer.IsComputer || UserInterface.ShowComputerMove())
                {
                    lastMove = m_Game.PlayTurn(i_From, i_To,out endGameMessage);
                  //  OnMoveChosen();
                    gameStatus = m_Game.Status;
                }

                
            }

           // UserInterface.PrintEndGameMessage(endGameMessage);
            //gameStatus = UserInterface.AskForNewGame(m_Game);
            return gameStatus;
        }

        public static Player GetWinner(Game i_Game)
        {
            Player winner = null;

            if (i_Game.CurrentPlayer.Points > i_Game.NextPlayer.Points)
            {
                winner = i_Game.CurrentPlayer;
            }
            else if (i_Game.CurrentPlayer.Points < i_Game.NextPlayer.Points)
            {
                winner = i_Game.NextPlayer;
            }

            return winner;
        }
    }
}
