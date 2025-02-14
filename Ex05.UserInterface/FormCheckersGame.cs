using Ex05.Enums;
using Ex05.GameLogic;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.UserInterface
{
    public partial class FormCheckersGame : Form
    {

        private enum eButtonData
        {
            
            ButtonStartPositionAxisX = 13,
            ButtonStartPositionAxisY = 50,
            ButtonSize = 50,
        }

        private readonly FormGameSettings r_FormGameSettings = new FormGameSettings();
        private PictureBoxPiece m_SelectedPiece;
        private GameManager m_GameManager;
        private int m_BoardSize;

        public FormCheckersGame()
        {
            InitializeComponent();
        }

        public void ShowDialog()
        {
            r_FormGameSettings.ShowDialog();

            if (r_FormGameSettings.DialogResult == DialogResult.OK)
            {
                initStartingGame();
            }
        }

        public void initNewGame(int i_Player1Score, int i_Player2Score)
        {
            initForm(i_Player1Score, i_Player2Score);
            int boardSize = (int)r_FormGameSettings.BoardSize;
            string player1Name = r_FormGameSettings.Player1Name;
            string player2Name = r_FormGameSettings.Player2Name;

            Enums.eGameMode gameMode = r_FormGameSettings.IsPlayer2Computer ? Enums.eGameMode.PlayerAgainstComputer : Enums.eGameMode.PlayerAgainstPlayer;

            m_GameManager = new GameManager(gameMode, player1Name, player2Name, boardSize);
            
            m_GameManager.Game.MoveExecuted += Game_MoveExecuted;
            m_GameManager.Game.ActivePlayerChanged += Game_ActivePlayerChanged;
            m_GameManager.Game.PieceEaten += Game_PieceEaten;
            m_GameManager.Game.BecameKing += Game_BecameKing;
            m_GameManager.Game.ComputerPieceSelected+= Game_ComputerPieceSelected;
            m_GameManager.Game.ScoreAdded+= Game_ScoreAdded;
        }

        private void initStartingGame()
        {
            initNewGame(0, 0);
            base.ShowDialog();
        }

        private void Game_ScoreAdded(int i_ScoreToAdd, string i_WinnerName)
        {
            if (playerControl1.PlayerName == i_WinnerName + ':')
            {
                playerControl1.PlayerScore = i_ScoreToAdd.ToString();
            }
            else
            {
                playerControl2.PlayerScore = i_ScoreToAdd.ToString();
            }
        }

        private void Game_ComputerPieceSelected(GameLogic.Point i_SelectedComputerPieceLocation)
        {
            for (int i = 0; i < m_NumberOfPiecesForEachPlayer; i++)
            {
                if (playerControl2.IsPlayingNow)
                {
                    if (m_RedPiecesPictureBox[i] != null)
                    {
                        if (m_RedPiecesPictureBox[i].CurrentCell.LocationInBoard.X == i_SelectedComputerPieceLocation.X && m_RedPiecesPictureBox[i].CurrentCell.LocationInBoard.Y == i_SelectedComputerPieceLocation.Y) 
                        {
                            m_SelectedPiece = m_RedPiecesPictureBox[i];
                        }
                    }
                }
            }
        }

        private void Game_BecameKing(GameLogic.Point i_KingLocation, ePieceColor i_kingColor)
        {
            ButtonCell kingCell = m_ButtonCells[i_KingLocation.X, i_KingLocation.Y];
            for (int i = 0; i < m_NumberOfPiecesForEachPlayer; i++)
            {
                if (i_kingColor == Enums.ePieceColor.Red)
                {
                    if (m_RedPiecesPictureBox[i] != null)
                    {
                        if (m_RedPiecesPictureBox[i].CurrentCell == kingCell)
                        {
                            m_RedPiecesPictureBox[i].Image = Properties.Resources.red_king;
                        }
                    }

                }
                else if (i_kingColor == Enums.ePieceColor.Black)
                {
                    if (m_BlackPiecesPictureBox[i] != null)
                    {
                        if (m_BlackPiecesPictureBox[i].CurrentCell == kingCell)
                        {
                            m_BlackPiecesPictureBox[i].Image = Properties.Resources.black_king;
                        }
                    }
                }
            }
        }

        private void Game_PieceEaten(GameLogic.Point i_EatenPieceLocation)
        {
            if (playerControl1.IsPlayingNow)
            {
                removePieceFromBoard(i_EatenPieceLocation.X, i_EatenPieceLocation.Y, m_RedPiecesPictureBox);
            }
            else
            {
                removePieceFromBoard(i_EatenPieceLocation.X, i_EatenPieceLocation.Y, m_BlackPiecesPictureBox);
            }
        }

        private void Game_ActivePlayerChanged()
        {
            if (playerControl1.IsPlayingNow)
            {
                playerControl1.SetTurn(false);
                playerControl2.SetTurn(true);
            }
            else
            {
                playerControl1.SetTurn(true);
                playerControl2.SetTurn(false);
            }
        }

        private void Game_MoveExecuted(GameLogic.Point from, GameLogic.Point to)
        {
            MoveSelectedPiece(to.X, to.Y);
        }

        public void MoveSelectedPiece(int i_RowOfNewSquare, int i_ColumnOfNewSquare)
        {
            if (m_SelectedPiece != null)
            {
                m_SelectedPiece.CurrentCell = m_ButtonCells[i_RowOfNewSquare, i_ColumnOfNewSquare];
                markSelectedPiece(m_SelectedPiece);
            }
        }

        public void initForm(int i_Player1Score, int i_Player2Score)
        {
            m_BoardSize = (int)r_FormGameSettings.BoardSize;
            m_NumberOfPiecesForEachPlayer = (m_BoardSize / 2) * ((m_BoardSize / 2) - 1);
            m_ButtonCells = new ButtonCell[m_BoardSize, m_BoardSize];
            m_RedPiecesPictureBox = new PictureBoxPiece[m_NumberOfPiecesForEachPlayer];
            m_BlackPiecesPictureBox = new PictureBoxPiece[m_NumberOfPiecesForEachPlayer];
            initPlayerControl(i_Player1Score, i_Player2Score);
            initPieces();
            initButtonsBoard(m_BoardSize);
            initPiecesOnBoard();
            addControlsButtonCellToForm();
            playerControl1.SetTurn(true);
            playerControl2.SetTurn(false);
            resizeClient();
        }


        private void initPlayerControl(int i_Player1Score, int i_Player2Score)
        {
            playerControl1.PlayerName = r_FormGameSettings.Player1Name;
            playerControl1.PlayerScore = i_Player1Score.ToString();
            playerControl2.PlayerName = r_FormGameSettings.Player2Name;
            playerControl2.PlayerScore = i_Player2Score.ToString();
        }

        private void initPieces()
        {
            for (int i = 0; i < m_NumberOfPiecesForEachPlayer; i++)
            {
                m_RedPiecesPictureBox[i] = new PictureBoxPiece(Enums.ePieceColor.Red);
                m_RedPiecesPictureBox[i].Image = Properties.Resources.red_piece;
                initPictureBoxPiece(m_RedPiecesPictureBox[i]);

                m_BlackPiecesPictureBox[i] = new PictureBoxPiece(Enums.ePieceColor.Black);
                m_BlackPiecesPictureBox[i].Image = Properties.Resources.black_piece;
                initPictureBoxPiece(m_BlackPiecesPictureBox[i]);
            }
        }

        private void initPictureBoxPiece(PictureBoxPiece i_PictureBoxPiece)
        {
            i_PictureBoxPiece.Size = new System.Drawing.Size(50, 50);
            i_PictureBoxPiece.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            i_PictureBoxPiece.BackColor = System.Drawing.Color.White;
            this.Controls.Add(i_PictureBoxPiece);
            i_PictureBoxPiece.Click += new EventHandler(this.piece_Clicked);
        }

        private void piece_Clicked(object sender, EventArgs e)
        {
            markSelectedPiece(sender);
        }

        private void playerControl2_Click(object sender, EventArgs e)
        {
            if (r_FormGameSettings.IsPlayer2Computer && playerControl2.IsPlayingNow)
            {
                string endGameMessage = string.Empty;
                eGameStatusOptions gameStatus = m_GameManager.GameLoop(null, null, out endGameMessage);
                gameEndingHandler(gameStatus, endGameMessage);
            }
        }

        private void gameEndingHandler(eGameStatusOptions i_GameStatus, string i_EndGameMessage)
        {
            string messageBoxTitle = "Damka";
            string messageBoxText = i_EndGameMessage;
            
            if (i_GameStatus != eGameStatusOptions.Running)
            {
                DialogResult userChoice = MessageBox.Show(messageBoxText, messageBoxTitle, MessageBoxButtons.YesNo);
                if (userChoice == DialogResult.Yes)
                {
                    removeAllPiecesFromBoard();
                    m_GameManager.ResetGame();
                    initNewGame(int.Parse(playerControl1.PlayerScore), int.Parse(playerControl2.PlayerScore));
                    
                }
                else
                {
                    Close();
                }
            }

        }

        private void buttonCell_Clicked(object sender, EventArgs e)
        {

            if (m_SelectedPiece == null)
                return; 

            ButtonCell targetCell = sender as ButtonCell;
            Ex05.GameLogic.Point from = m_SelectedPiece.CurrentCell.LocationInBoard;
            Ex05.GameLogic.Point to = targetCell.LocationInBoard;
            string endGameMessage = string.Empty;

            if (r_FormGameSettings.IsPlayer2Computer)
            { 
                if(playerControl1.IsPlayingNow)
                {
                    eGameStatusOptions gameStatus = m_GameManager.GameLoop(from, to, out endGameMessage);
                    gameEndingHandler(gameStatus, endGameMessage);
                }
               
            }
            else
            {
                eGameStatusOptions gameStatus = m_GameManager.GameLoop(from, to, out endGameMessage);
                gameEndingHandler(gameStatus, endGameMessage);
            }
        }

        private void initButtonsBoard(int i_BoardSize)
        {
            int xCoordinate = (int)eButtonData.ButtonStartPositionAxisX;
            int yCoordinate = (int)eButtonData.ButtonStartPositionAxisY;

            for (byte row = 0; row < m_BoardSize; row++)
            {
                for (byte column = 0; column < m_BoardSize; column++)
                {
                    m_ButtonCells[row, column] = new ButtonCell(row, column);
                    m_ButtonCells[row, column].LocationInBoard = new Ex05.GameLogic.Point(row, column);

                    if (((column % 2) == 0 && (row % 2) == 0) || ((column % 2) != 0 && (row % 2) != 0))
                    {
                        m_ButtonCells[row, column].BackColor = System.Drawing.Color.Black;
                        m_ButtonCells[row, column].Enabled = false;
                    }
                    else
                    {
                        m_ButtonCells[row, column].BackColor = System.Drawing.Color.White;
                        m_ButtonCells[row, column].Click += new EventHandler(this.buttonCell_Clicked);
                    }

                    this.m_ButtonCells[row, column].Location = new System.Drawing.Point(xCoordinate, yCoordinate);
                    this.m_ButtonCells[row, column].Size = new System.Drawing.Size((int)eButtonData.ButtonSize, (int)eButtonData.ButtonSize);
                    this.m_ButtonCells[row, column].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    xCoordinate += (int)eButtonData.ButtonSize;

                }

                yCoordinate += (int)eButtonData.ButtonSize;
                xCoordinate = (int)eButtonData.ButtonStartPositionAxisX;
            }
        }

        private void initPiecesOnBoard()
        {
            initOneSidePiecesOnBoard(0, (m_BoardSize / 2) - 1, m_RedPiecesPictureBox);
            initOneSidePiecesOnBoard((m_BoardSize / 2) + 1, m_BoardSize, m_BlackPiecesPictureBox);
        }

        private void initOneSidePiecesOnBoard(int i_StartRow, int i_EndRow, PictureBoxPiece[] i_Pieces)
        {
            int index = 0;

            for (int row = i_StartRow; row < i_EndRow; row++)
            {
                int startColumn = row % 2 == 0 ? 1 : 0;

                for (int column = startColumn; column < m_BoardSize; column += 2)
                {
                    i_Pieces[index].CurrentCell = m_ButtonCells[row, column];
                    i_Pieces[index].BringToFront();
                    index++;
                }
            }
        }

        private void resizeClient()
        {
            int clientSizeWidth = ((int)eButtonData.ButtonSize * m_BoardSize) + (2 * (int)eButtonData.ButtonStartPositionAxisX);
            int clientSizeHeight = ((int)eButtonData.ButtonSize * m_BoardSize) + (int)eButtonData.ButtonStartPositionAxisY + (int)eButtonData.ButtonStartPositionAxisX;
            ClientSize = new System.Drawing.Size(clientSizeWidth, clientSizeHeight);
        }

        private void addControlsButtonCellToForm()
        {
            foreach (ButtonCell square in m_ButtonCells)
            {
                this.Controls.Add(square);
            }
        }

        private void removePieceFromBoard(int i_CurrentCellPieceRow, int i_CurrentCellPieceColumn, PictureBoxPiece[] i_Pieces)
        {
            ButtonCell currentButtonCell = m_ButtonCells[i_CurrentCellPieceRow, i_CurrentCellPieceColumn];

            for (int i = 0; i < m_NumberOfPiecesForEachPlayer; i++)
            {
                if (i_Pieces[i] != null)
                {
                    if (i_Pieces[i].CurrentCell == currentButtonCell)
                    {
                        Controls.Remove(i_Pieces[i]);
                        i_Pieces[i] = null;
                        break;
                    }
                }
            }
        }

        private void removeAllPiecesFromBoard()
        {
            for (int i = 0; i < m_NumberOfPiecesForEachPlayer; i++)
            {
                if (m_RedPiecesPictureBox[i] != null)
                {
                    Controls.Remove(m_RedPiecesPictureBox[i]);
                    m_RedPiecesPictureBox[i] = null;
                }

                if (m_BlackPiecesPictureBox[i] != null)
                {
                    Controls.Remove(m_BlackPiecesPictureBox[i]);
                    m_BlackPiecesPictureBox[i] = null;
                }
            }
        }

        private void markSelectedPiece(object i_PieceSender)
        {
            PictureBoxPiece selectedPiece = i_PieceSender as PictureBoxPiece;

            if (m_SelectedPiece != null)
            {
                m_SelectedPiece.BackColor = Color.White; 

                if (m_SelectedPiece != selectedPiece)
                {
                    m_SelectedPiece = selectedPiece;    
                    m_SelectedPiece.BackColor = Color.DodgerBlue;
                }
                else
                {
                    m_SelectedPiece = null;  
                }
            }
            else
            {
                if (selectedPiece != null)
                {
                    m_SelectedPiece = selectedPiece;
                    m_SelectedPiece.BackColor = Color.DodgerBlue;
                }
            }
        }
    }
}
