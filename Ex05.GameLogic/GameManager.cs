namespace Ex05.GameLogic
{
    public class GameManager
    {
        private readonly int r_BoardSize;
        private readonly Player r_PlayerOne;
        private readonly Player r_PlayerTwo;

        public GameManager(eGameModeOptions i_GameMode, string i_FirstPlayerName, string i_SecondPlayerName, int i_BoardSize)
        {
            bool isComputer = i_GameMode == eGameModeOptions.PlayerVsComputer;

            r_BoardSize = i_BoardSize;
            r_PlayerOne = new Player(i_FirstPlayerName, ePieceType.X, false);
            r_PlayerTwo = new Player(i_SecondPlayerName, ePieceType.O, isComputer);
        }

        public void playNewGame()
        {
            eGameStatusOptions gameStatus;
            do
            {
                gameStatus = gameLoop();
            } while (gameStatus != eGameStatusOptions.Exit);
        }

        private eGameStatusOptions gameLoop()
        {
            eGameStatusOptions gameStatus = eGameStatusOptions.Running;
            Game game = new Game(r_BoardSize, r_PlayerOne, r_PlayerTwo);
            Move lastMove = null;
            string endGameMessage = string.Empty;

            while (gameStatus == eGameStatusOptions.Running)
            {
                
                if (!game.CurrentPlayer.IsComputer || UserInterface.ShowComputerMove())
                {
                    UserInterface.PrintBoard(game.GameBoard);
                    if (lastMove != null)
                    {
                        UserInterface.PrintMove(lastMove, game);
                    }
                }

                lastMove = game.PlayTurn(out endGameMessage);
                gameStatus = game.Status;
            }

            UserInterface.PrintEndGameMessage(endGameMessage);
            gameStatus = UserInterface.AskForNewGame(game);
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
