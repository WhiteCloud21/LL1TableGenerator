namespace TableGenerator
{
    partial class fShowTable
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
            this.f_dgvMain = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.f_dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // f_dgvMain
            // 
            this.f_dgvMain.AllowUserToAddRows = false;
            this.f_dgvMain.AllowUserToDeleteRows = false;
            this.f_dgvMain.AllowUserToResizeColumns = false;
            this.f_dgvMain.AllowUserToResizeRows = false;
            this.f_dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.f_dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.f_dgvMain.Location = new System.Drawing.Point(0, 0);
            this.f_dgvMain.Name = "f_dgvMain";
            this.f_dgvMain.ReadOnly = true;
            this.f_dgvMain.Size = new System.Drawing.Size(821, 458);
            this.f_dgvMain.TabIndex = 0;
            // 
            // fShowTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 458);
            this.Controls.Add(this.f_dgvMain);
            this.Name = "fShowTable";
            this.Text = "Просмотр таблицы разбора";
            ((System.ComponentModel.ISupportInitialize)(this.f_dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView f_dgvMain;
    }
}