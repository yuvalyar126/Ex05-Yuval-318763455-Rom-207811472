using Ex05.Enums;
using System;

namespace Ex05.GameLogic
{
    public class UserInterface
    {
        public static string GetPlayerNameFromUser(ePlayerType i_PlayerType)
        {
            string playerName;
            bool isValid;
            string playerPrompt = i_PlayerType == ePlayerType.FirstPlayer ? "Enter first player name please: " : "Enter second player name please: ";

            do
            {
                Console.Write(playerPrompt);

                playerName = Console.ReadLine();
                isValid = InputValidator.IsPlayerNameValid(playerName);
            } while (!isValid);

            return playerName;
        }

        public static void StartGame()
        {
            string firstPlayerName = GetPlayerNameFromUser(ePlayerType.FirstPlayer);
            int boardSize = GetBoardSizeFromUser();
            eGameMode gameMode = GetGameModeFromUser();
            string secondPlayerName;

            if (gameMode == eGameMode.PlayerAgainstPlayer)
            {
                secondPlayerName = GetPlayerNameFromUser(ePlayerType.SecondPlayer);
            }
            else
            {
                secondPlayerName = "Computer";
            }

            GameManager GameManager = new GameManager(gameMode, firstPlayerName, secondPlayerName, boardSize);
            GameManager.playNewGame();
        }

        public static int GetBoardSizeFromUser()
        {
            int boardSize = 0;
            bool isValid;

            do
            {
                Console.Write("Enter the board size (6, 8, or 10): ");
                string boardSizeInput = Console.ReadLine();
                isValid = InputValidator.IsBoardSizeInputValid(boardSizeInput, out boardSize);
            } while (!isValid);

            return boardSize;
        }

        public static eGameMode GetGameModeFromUser()
        {
            eGameMode gameMode;
            bool isValid;

            do
            {
                Console.Write("Enter 1 for playing against the computer or 2 for two players: ");
                string gameModeInput = Console.ReadLine();
                isValid = InputValidator.IsGameModeValid(gameModeInput, out gameMode);
            } while (!isValid);

            return gameMode;
        }

        public static eGameStatusOptions AskForNewGame(Game i_Game)
        {
            eGameStatusOptions gameStatus;

            Console.WriteLine("Would you like to play another game? press Y for a new game or any other key to exit");
            string userChoice = Console.ReadLine();
            if (userChoice == "Y")
            {
                gameStatus = eGameStatusOptions.Running;
            }
            else
            {
                Player winner = GameManager.GetWinner(i_Game);
                Console.WriteLine("The Winner is {0} with {1} points!!!", winner.PlayerName, winner.Points);
                gameStatus = eGameStatusOptions.Exit;
            }

            return gameStatus;
        }

        public static void PrintBoard(GameBoard i_Board)
        {
            int boardSize = i_Board.BoardSize;

            string columnHeaders = string.Empty;
            for (int i = 0; i < boardSize; i++)
            {
                columnHeaders += (char)('a' + i);
            }

            string header = "   " + string.Join("   ", columnHeaders.ToCharArray());
            string separator = " " + new string('=', boardSize * 4 + 1);
            Console.WriteLine(header);
            Console.WriteLine(separator);
            for (int row = 0; row < boardSize; row++)
            {
                Console.Write("{0}|", (char)(row + 'A'));
                for (int col = 0; col < boardSize; col++)
                {
                    Piece cellPiece = i_Board.GetPieceInCell(row, col);
                    if (cellPiece == null)
                    {
                        Console.Write("   |");
                        continue;
                    }

                    ePieceType pieceType = cellPiece.Type;
                    switch (pieceType)
                    {
                        case ePieceType.X:
                            Console.Write(" X |");
                            break;
                        case ePieceType.O:
                            Console.Write(" O |");
                            break;
                        case ePieceType.K:
                            Console.Write(" K |");
                            break;
                        case ePieceType.U:
                            Console.Write(" U |");
                            break;

                        default:
                            break;
                    }
                }

                Console.WriteLine();
                Console.WriteLine(separator);
            }
        }

        public static void PrintMove(Move i_Move, Game i_Game)
        {
            string from = string.Format("{0}{1}", (char)(i_Move.From.X + 'A'), (char)(i_Move.From.Y + 'a'));
            string to = string.Format("{0}{1}", (char)(i_Move.To.X + 'A'), (char)(i_Move.To.Y + 'a'));
            string nameToPrint = i_Game.NextPlayer.PlayerName;
            ePieceType typeToPrint = i_Game.NextPlayer.PlayerPieceType;

            if (i_Move.IsEatingMove && i_Game.CurrentPlayer.MustEatAgain)
            {
                nameToPrint = i_Game.CurrentPlayer.PlayerName;
                typeToPrint = i_Game.CurrentPlayer.PlayerPieceType;
            }

            Console.WriteLine("{0}'s move was ({1}): {2}>{3}", nameToPrint, typeToPrint, from, to);
        }

        public static bool ShowComputerMove()
        {
            bool showComputerMove = false;

            Console.WriteLine("Computer’s Turn (press ‘enter’ to see it’s move)");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {

            }

            showComputerMove = true;
            return showComputerMove;
        }

        public static void PrintEndGameMessage(string i_EndGameMessage)
        {
            if (i_EndGameMessage != null)
            {
                Console.WriteLine(i_EndGameMessage);
            }
        }

        public static void PrintCurrentPlayerTurnMessage(Player i_CurrentPlayer)
        {
            Console.WriteLine("{0}'s turn ({1}): ", i_CurrentPlayer.PlayerName, i_CurrentPlayer.PlayerPieceType);
        }

        public static string GetMoveStringFromUser(Player i_CurrentPlayer)
        {
            string move;

            PrintCurrentPlayerTurnMessage(i_CurrentPlayer);
            move = Console.ReadLine();
            return move;
        }

        public static void PrintInvalidPlayerNameMessage()
        {
            Console.WriteLine("Invalid input. Please enter a name up to 20 characters without spaces.");
        }

        public static void PrintOutOfRangeMessage()
        {
            Console.WriteLine("Invalid input. Board size must be 6, 8, or 10.");
        }

        public static void PrintInvalidInputMessage()
        {
            Console.WriteLine("Invalid input. Please try again.");
        }

        public static void PrintInvalidGameModeMessage()
        {
            Console.WriteLine("Invalid choice. Please enter 1 for playing against the computer or 2 for two players.");
        }

        public static void PrintInvalidMoveStringMessage()
        {
            Console.WriteLine("Invalid move. Please enter a move in the format: 'Ac>Bd'.");
        }

        public static void PrintInvalidMoveMessage()
        {
            Console.WriteLine("Invalid move. Please try again.");
        }

        public static void PrintInvalidMustEatMessage()
        {
            Console.WriteLine("Invalid move. You must eat if you can.");
        }
    }
}
