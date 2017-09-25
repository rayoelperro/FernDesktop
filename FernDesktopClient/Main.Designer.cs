namespace FernDesktopClient
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ToFill = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ToFill)).BeginInit();
            this.SuspendLayout();
            // 
            // ToFill
            // 
            this.ToFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToFill.Location = new System.Drawing.Point(0, 0);
            this.ToFill.Name = "ToFill";
            this.ToFill.Size = new System.Drawing.Size(802, 476);
            this.ToFill.TabIndex = 0;
            this.ToFill.TabStop = false;
            this.ToFill.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToFill_MouseDown);
            this.ToFill.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToFill_MouseMove);
            this.ToFill.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ToFill_MouseUp);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(802, 476);
            this.Controls.Add(this.ToFill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FernDesktop";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ToFill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ToFill;
    }
}

