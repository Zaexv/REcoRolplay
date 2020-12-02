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
            this.pictureBoxIA = new System.Windows.Forms.PictureBox();
            this.pictureBoxGameOver = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameOver)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 146);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 85);
            this.label1.TabIndex = 0;
            // 
            // pictureBoxIA
            // 
            this.pictureBoxIA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxIA.Image = global::REcoSample.Properties.Resources.ia;
            this.pictureBoxIA.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxIA.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxIA.Name = "pictureBoxIA";
            this.pictureBoxIA.Size = new System.Drawing.Size(2564, 1417);
            this.pictureBoxIA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxIA.TabIndex = 1;
            this.pictureBoxIA.TabStop = false;
            // 
            // pictureBoxGameOver
            // 
            this.pictureBoxGameOver.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxGameOver.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxGameOver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxGameOver.Location = new System.Drawing.Point(729, 370);
            this.pictureBoxGameOver.Name = "pictureBoxGameOver";
            this.pictureBoxGameOver.Size = new System.Drawing.Size(1140, 739);
            this.pictureBoxGameOver.TabIndex = 3;
            this.pictureBoxGameOver.TabStop = false;
            // 
            // Formulario_Rolplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2564, 1417);
            this.Controls.Add(this.pictureBoxGameOver);
            this.Controls.Add(this.pictureBoxIA);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Formulario_Rolplay";
            this.Text = "Minijuego de Rol";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameOver)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBoxIA;
        private System.Windows.Forms.PictureBox pictureBoxGameOver;
    }
}

