using System;

namespace Ex05.GameLogic
{
    public class InputValidator
    {
        public const int k_SmallBoardSize = 6;
        public const int k_MediumBoardSize = 8;
        public const int k_LargeBoardSize = 10;

        public static bool IsPlayerNameValid(string i_input)
        {
            bool isValid = !string.IsNullOrEmpty(i_input) && IsOnlyLetters(i_input) && i_input.Length <= 20 && !i_input.Contains(" ");

            if (!isValid)
            {
                UserInterface.PrintInvalidPlayerNameMessage();
            }

            return isValid;
        }

        public static bool IsOnlyLetters(string i_String)
        {
            bool isOnlyLetters = true;
            int stringLength = i_String.Length;

            for (int i = 0; i < stringLength && isOnlyLetters; i++)
            {
                isOnlyLetters = char.IsLetter(i_String[i]);
            }

            return isOnlyLetters;
        }

        private static bool isBoardSizeInputInOptions(int i_BoardSizeInput)
        {
            bool isValid = i_BoardSizeInput == k_SmallBoardSize || i_BoardSizeInput == k_MediumBoardSize || i_BoardSizeInput == k_LargeBoardSize;

            return isValid;
        }

        public static bool IsBoardSizeInputValid(string i_UserInput, out int o_BoardSize)
        {
            bool isBoardSizeValid;
            bool isNumber = int.TryParse(i_UserInput, out o_BoardSize);

            if (isNumber)
            {
                isBoardSizeValid = isBoardSizeInputInOptions(o_BoardSize);

                if (!isBoardSizeValid)
                {
                    UserInterface.PrintOutOfRangeMessage();
                }
            }
            else
            {
                isBoardSizeValid = false;
                UserInterface.PrintInvalidInputMessage();
            }

            return isBoardSizeValid;
        }

        public static bool IsGameModeValid(string i_UserInput, out eGameModeOptions o_GameMode)
        {
            bool isValid = Enum.TryParse(i_UserInput, out o_GameMode) && Enum.IsDefined(typeof(eGameModeOptions), o_GameMode);

            if (!isValid)
            {
                UserInterface.PrintInvalidGameModeMessage();
            }

            return isValid;
        }

        public static bool IsMoveStringValid(string i_MoveString)
        {
            bool isValid = (i_MoveString.Length == 5 && i_MoveString[2] == '>' && char.IsUpper(i_MoveString[0]) && char.IsLower(i_MoveString[1]) && char.IsUpper(i_MoveString[3]) && char.IsLower(i_MoveString[4]));

            return isValid;
        }
    }
}
