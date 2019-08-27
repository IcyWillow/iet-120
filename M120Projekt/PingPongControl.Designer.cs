namespace M120Projekt
{
    partial class PingPongControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlField = new System.Windows.Forms.Panel();
            this.pctBall = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.pnlField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctBall)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlField
            // 
            this.pnlField.BackColor = System.Drawing.Color.Green;
            this.pnlField.Controls.Add(this.pctBall);
            this.pnlField.Location = new System.Drawing.Point(23, 17);
            this.pnlField.Name = "pnlField";
            this.pnlField.Size = new System.Drawing.Size(200, 148);
            this.pnlField.TabIndex = 0;
            // 
            // pctBall
            // 
            this.pctBall.BackColor = System.Drawing.Color.Yellow;
            this.pctBall.Location = new System.Drawing.Point(92, 53);
            this.pctBall.Name = "pctBall";
            this.pctBall.Size = new System.Drawing.Size(31, 31);
            this.pctBall.TabIndex = 0;
            this.pctBall.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(23, 171);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(87, 46);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(134, 171);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(89, 46);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // tmrGame
            // 
            this.tmrGame.Tick += new System.EventHandler(this.TmrGame_Tick);
            // 
            // DialogButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pnlField);
            this.Name = "DialogButton";
            this.Size = new System.Drawing.Size(254, 228);
            this.Load += new System.EventHandler(this.DialogButton_Load);
            this.pnlField.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctBall)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlField;
        private System.Windows.Forms.PictureBox pctBall;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer tmrGame;
    }
}
