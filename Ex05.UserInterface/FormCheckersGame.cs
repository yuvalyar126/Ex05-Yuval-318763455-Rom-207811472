using Ex05.GameLogic;
using Ex05.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Ex05.GameLogic.GameBoard;
using System.Reflection;

namespace Ex05.UserInterface
{
    public partial class FormCheckersGame : Form
    {
        private enum eButtonData
        {
            ButtonSize = 50,
            ButtonXStartPosition = 13,
            ButtonYStartPosition = 50,
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
                initializeGame();
            }
        }

        public void initializeGame()
        {
            initForm();
            int boardSize = (int)r_FormGameSettings.BoardSize;
            string player1Name = r_FormGameSettings.Player1Name;
            string player2Name = r_FormGameSettings.Player2Name;
            Enums.eGameMode gameMode = r_FormGameSettings.IsPlayer2Computer ? Enums.eGameMode.PlayerAgainstComputer : Enums.eGameMode.PlayerAgainstPlayer;

            m_GameManager = new GameManager(gameMode, player1Name, player2Name, boardSize);
            m_GameManager.Game.MoveExecuted += Game_MoveExecuted;
            m_GameManager.Game.ActivePlayerChanged += Game_ActivePlayerChanged;
            m_GameManager.Game.PieceEaten += Game_PieceEaten;
            m_GameManager.Game.BecameKing += Game_BecameKing;



            //game.GameOver += game_GameOver;
            //game.ScoreChanged += game_ScoreChanged;
            //game.BoardChanged += game_BoardChanged;
            //game.InitializeGame();


            base.ShowDialog();
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
                else
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

        public void initForm()
        {
            m_BoardSize = (int)r_FormGameSettings.BoardSize;
            m_NumberOfPiecesForEachPlayer = (m_BoardSize / 2) * ((m_BoardSize / 2) - 1);
            m_ButtonCells = new ButtonCell[m_BoardSize, m_BoardSize];
            m_RedPiecesPictureBox = new PictureBoxPiece[m_NumberOfPiecesForEachPlayer];
            m_BlackPiecesPictureBox = new PictureBoxPiece[m_NumberOfPiecesForEachPlayer];


            initPlayerControl();
            initPieces();
            initButtonsBoard(m_BoardSize);
            initPiecesOnBoard();
            resizeClient();
            addControlsButtonCellToForm();
            playerControl1.SetTurn(true);
        }

        private void initPlayerControl()
        {
            playerControl1.PlayerName = r_FormGameSettings.Player1Name;
            playerControl2.PlayerName = r_FormGameSettings.Player2Name;
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

        //private void piece_Clicked(object sender, EventArgs e)
        //{
        //    PictureBoxPiece selectedPiece = sender as PictureBoxPiece;

        //    if (selectedPiece == null)
        //        return;

        //    if (m_SelectedPiece != null)
        //    {
        //        m_SelectedPiece.BackColor = Color.White; // Reset previous selection

        //        if (m_SelectedPiece == selectedPiece) // Deselect if clicking the same piece again
        //        {
        //            m_SelectedPiece = null;
        //            return;
        //        }
        //    }

        //    // Select new piece
        //    m_SelectedPiece = selectedPiece;
        //    m_SelectedPiece.BackColor = Color.DodgerBlue;
        //}



        private void piece_Clicked(object sender, EventArgs e)
        {
            markSelectedPiece(sender);
        }


        //private void buttonCell_Clicked(object sender, EventArgs e)
        //{
        //    if (m_SelectedPiece == null)
        //        return; // No piece selected, do nothing

        //    ButtonCell targetCell = sender as ButtonCell;
        //    if (targetCell == null)
        //        return; // Ensure the clicked object is a button cell

        //    // Get piece position
        //    int row = targetCell.LocationInBoard.Y;
        //    int column = targetCell.LocationInBoard.X;

        //    // Attempt to make the move
        //    if (true) // Replace with actual move logic
        //    {
        //      MoveSelectedPiece(row, column);
        //    }

        //    // Deselect piece after move attempt
        //    //m_SelectedPiece.BackColor = Color.White;
        //    m_SelectedPiece = null;
        //}


        private void buttonCell_Clicked(object sender, EventArgs e)
        {

            if (m_SelectedPiece == null)
                return; // No piece selected, do nothing

            ButtonCell targetCell = sender as ButtonCell;
            Ex05.GameLogic.Point from = m_SelectedPiece.CurrentCell.LocationInBoard;
            Ex05.GameLogic.Point to = targetCell.LocationInBoard;

            eGameStatusOptions gameStatus = m_GameManager.GameLoop(from, to);


        }




        private void initButtonsBoard(int i_BoardSize)
        {
            // Point for the graphic location:
            int xCoordinate = (int)eButtonData.ButtonXStartPosition;
            int yCoordinate = (int)eButtonData.ButtonYStartPosition;

            for (byte row = 0; row < m_BoardSize; row++)
            {
                for (byte column = 0; column < m_BoardSize; column++)
                {
                    m_ButtonCells[row, column] = new ButtonCell(row, column);
                    m_ButtonCells[row, column].LocationInBoard = new Ex05.GameLogic.Point(row, column);

                    // Check if the current ButtonCell is black or white
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
                xCoordinate = (int)eButtonData.ButtonXStartPosition;

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
            int clientSizeWidth = ((int)eButtonData.ButtonSize * m_BoardSize) + (2 * (int)eButtonData.ButtonXStartPosition);
            int clientSizeHeight = ((int)eButtonData.ButtonSize * m_BoardSize) + (int)eButtonData.ButtonYStartPosition + (int)eButtonData.ButtonXStartPosition;
            ClientSize = new System.Drawing.Size(clientSizeWidth, clientSizeHeight);
        }

        private void addControlsButtonCellToForm()
        {
            foreach (ButtonCell square in m_ButtonCells)
            {
                this.Controls.Add(square);
            }
        }

        //private void move_piece(Move i_move)
        //{
        //    updatePiecePosition(i_move.From, i_move.To);
        //}

        private void updatePiecePosition(System.Drawing.Point i_From, System.Drawing.Point i_To)
        {
            m_ButtonCells[i_To.X, i_To.Y].Image = m_ButtonCells[i_From.X, i_From.Y].Image;
            m_ButtonCells[i_From.X, i_From.Y].Image = null;
        }




        private void removePieceFromBoard(int i_CurrentCellPieceRow, int i_CurrentCellPieceColumn, PictureBoxPiece[] i_Pieces)
        {
            ButtonCell currentButtonCell = m_ButtonCells[i_CurrentCellPieceRow, i_CurrentCellPieceColumn];

            // scan array and locate the piece whose cell Point are received in function:
            for (int i = 0; i < m_NumberOfPiecesForEachPlayer; i++)
            {
                if (i_Pieces[i] != null)
                {
                    // Check if the current piece it what we looking for
                    if (i_Pieces[i].CurrentCell == currentButtonCell)
                    {
                        Controls.Remove(i_Pieces[i]);
                        i_Pieces[i] = null;
                        break;
                    }
                }
            }
        }



        private void markSelectedPiece(object i_PieceSender)
        {
            PictureBoxPiece selectedPiece = i_PieceSender as PictureBoxPiece;

            if (m_SelectedPiece != null)
            {
                m_SelectedPiece.BackColor = Color.White; // Return the color of previous selected piece to white.

                if (m_SelectedPiece != selectedPiece)
                {
                    m_SelectedPiece = selectedPiece;        // Now m_CurrentSelectedPiece is selectedPiece.
                    m_SelectedPiece.BackColor = Color.DodgerBlue;
                }
                else
                {
                    m_SelectedPiece = null;  // If we select this piece before it means that now we cancel the select.
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
