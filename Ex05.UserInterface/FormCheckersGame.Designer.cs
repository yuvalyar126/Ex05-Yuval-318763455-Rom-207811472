namespace Ex05.UserInterface
{
    partial class FormCheckersGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.playerControl1 = new Ex05.UserInterface.PlayerControl();
            this.playerControl2 = new Ex05.UserInterface.PlayerControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.playerControl2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.playerControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 32);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // playerControl1
            // 
            this.playerControl1.BackColor = System.Drawing.SystemColors.Control;
            this.playerControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playerControl1.Location = new System.Drawing.Point(3, 3);
            this.playerControl1.Name = "playerControl1";
            this.playerControl1.PlayerName = "Player Name:";
            this.playerControl1.PlayerScore = "0";
            this.playerControl1.Size = new System.Drawing.Size(380, 26);
            this.playerControl1.TabIndex = 0;
            // 
            // playerControl2
            // 
            this.playerControl2.BackColor = System.Drawing.SystemColors.Control;
            this.playerControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playerControl2.Location = new System.Drawing.Point(403, 3);
            this.playerControl2.Name = "playerControl2";
            this.playerControl2.PlayerName = "Player Name:";
            this.playerControl2.PlayerScore = "0";
            this.playerControl2.Size = new System.Drawing.Size(380, 26);
            this.playerControl2.TabIndex = 1;
            // 
            // FormCheckersGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormCheckersGame";
            this.Text = "FormCheckersGame";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private int m_NumberOfPiecesForEachPlayer;
        private ButtonCell[,] m_ButtonCells;
        private PictureBoxPiece[] m_RedPiecesPictureBox;
        private PictureBoxPiece[] m_BlackPiecesPictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private PlayerControl playerControl1;
        private PlayerControl playerControl2;
    }
}