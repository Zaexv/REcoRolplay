namespace REcoSample
{
    partial class Formulario_Rolplay
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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxGameOver = new System.Windows.Forms.PictureBox();
            this.pictureBoxIA = new System.Windows.Forms.PictureBox();
            this.hint_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameOver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIA)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 93);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 54);
            this.label1.TabIndex = 0;
            // 
            // pictureBoxGameOver
            // 
            this.pictureBoxGameOver.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxGameOver.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxGameOver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxGameOver.Image = global::REcoSample.Properties.Resources.gameover;
            this.pictureBoxGameOver.Location = new System.Drawing.Point(486, 237);
            this.pictureBoxGameOver.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxGameOver.Name = "pictureBoxGameOver";
            this.pictureBoxGameOver.Size = new System.Drawing.Size(760, 473);
            this.pictureBoxGameOver.TabIndex = 3;
            this.pictureBoxGameOver.TabStop = false;
            // 
            // pictureBoxIA
            // 
            this.pictureBoxIA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxIA.Image = global::REcoSample.Properties.Resources.ia;
            this.pictureBoxIA.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxIA.Name = "pictureBoxIA";
            this.pictureBoxIA.Size = new System.Drawing.Size(1283, 675);
            this.pictureBoxIA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxIA.TabIndex = 1;
            this.pictureBoxIA.TabStop = false;
            // 
            // hint_lbl
            // 
            this.hint_lbl.AutoSize = true;
            this.hint_lbl.Location = new System.Drawing.Point(42, 607);
            this.hint_lbl.Name = "hint_lbl";
            this.hint_lbl.Size = new System.Drawing.Size(0, 17);
            this.hint_lbl.TabIndex = 4;
            // 
            // Formulario_Rolplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 675);
            this.Controls.Add(this.hint_lbl);
            this.Controls.Add(this.pictureBoxGameOver);
            this.Controls.Add(this.pictureBoxIA);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Formulario_Rolplay";
            this.Text = "Minijuego de Rol";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameOver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBoxIA;
        private System.Windows.Forms.PictureBox pictureBoxGameOver;
        private System.Windows.Forms.Label hint_lbl;
    }
}

