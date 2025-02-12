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
            this.playerControl1 = new Ex05.UserInterface.PlayerControl();
            this.SuspendLayout();
            // 
            // playerControl1
            // 
            this.playerControl1.BackColor = System.Drawing.SystemColors.Control;
            this.playerControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playerControl1.Location = new System.Drawing.Point(3, 2);
            this.playerControl1.Name = "playerControl1";
            this.playerControl1.Size = new System.Drawing.Size(424, 52);
            this.playerControl1.TabIndex = 0;
            // 
            // FormCheckersGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.playerControl1);
            this.Name = "FormCheckersGame";
            this.Text = "FormCheckersGame";
            this.ResumeLayout(false);

        }

        #endregion
        private PlayerControl player1Control;
        private PlayerControl player2Control;
        private PlayerControl playerControl1;
    }
}