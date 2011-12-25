namespace TableGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.f_gbStep1 = new System.Windows.Forms.GroupBox();
            this.f_btnLoadGram = new System.Windows.Forms.Button();
            this.f_btnBrowseFileGram = new System.Windows.Forms.Button();
            this.f_txtFileGram = new System.Windows.Forms.TextBox();
            this.f_gbStep2 = new System.Windows.Forms.GroupBox();
            this.f_rtbGram = new System.Windows.Forms.RichTextBox();
            this.f_gbStep4 = new System.Windows.Forms.GroupBox();
            this.f_btnSaveTable = new System.Windows.Forms.Button();
            this.f_btnBrowseFileTable = new System.Windows.Forms.Button();
            this.f_txtFileTable = new System.Windows.Forms.TextBox();
            this.f_gbStep3 = new System.Windows.Forms.GroupBox();
            this.f_btnViewTable = new System.Windows.Forms.Button();
            this.f_btnGenerateTable = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.f_tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.f_gbStep1.SuspendLayout();
            this.f_gbStep2.SuspendLayout();
            this.f_gbStep4.SuspendLayout();
            this.f_gbStep3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // f_gbStep1
            // 
            this.f_gbStep1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_gbStep1.Controls.Add(this.f_btnLoadGram);
            this.f_gbStep1.Controls.Add(this.f_btnBrowseFileGram);
            this.f_gbStep1.Controls.Add(this.f_txtFileGram);
            this.f_gbStep1.Location = new System.Drawing.Point(13, 13);
            this.f_gbStep1.Name = "f_gbStep1";
            this.f_gbStep1.Size = new System.Drawing.Size(373, 57);
            this.f_gbStep1.TabIndex = 6;
            this.f_gbStep1.TabStop = false;
            this.f_gbStep1.Text = "1. Загрузка файла грамматики";
            // 
            // f_btnLoadGram
            // 
            this.f_btnLoadGram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_btnLoadGram.Location = new System.Drawing.Point(289, 19);
            this.f_btnLoadGram.Name = "f_btnLoadGram";
            this.f_btnLoadGram.Size = new System.Drawing.Size(75, 22);
            this.f_btnLoadGram.TabIndex = 8;
            this.f_btnLoadGram.Text = "Загрузка";
            this.f_btnLoadGram.UseVisualStyleBackColor = true;
            this.f_btnLoadGram.Click += new System.EventHandler(this.f_btnLoadGram_Click);
            // 
            // f_btnBrowseFileGram
            // 
            this.f_btnBrowseFileGram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_btnBrowseFileGram.Location = new System.Drawing.Point(208, 19);
            this.f_btnBrowseFileGram.Name = "f_btnBrowseFileGram";
            this.f_btnBrowseFileGram.Size = new System.Drawing.Size(75, 22);
            this.f_btnBrowseFileGram.TabIndex = 7;
            this.f_btnBrowseFileGram.Text = "Обзор...";
            this.f_btnBrowseFileGram.UseVisualStyleBackColor = true;
            this.f_btnBrowseFileGram.Click += new System.EventHandler(this.f_btnBrowseFileGram_Click);
            // 
            // f_txtFileGram
            // 
            this.f_txtFileGram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_txtFileGram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.f_txtFileGram.Location = new System.Drawing.Point(7, 19);
            this.f_txtFileGram.Name = "f_txtFileGram";
            this.f_txtFileGram.Size = new System.Drawing.Size(195, 22);
            this.f_txtFileGram.TabIndex = 6;
            // 
            // f_gbStep2
            // 
            this.f_gbStep2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_gbStep2.Controls.Add(this.f_rtbGram);
            this.f_gbStep2.Location = new System.Drawing.Point(13, 77);
            this.f_gbStep2.Name = "f_gbStep2";
            this.f_gbStep2.Size = new System.Drawing.Size(373, 207);
            this.f_gbStep2.TabIndex = 7;
            this.f_gbStep2.TabStop = false;
            this.f_gbStep2.Text = "2. Просмотр грамматики";
            // 
            // f_rtbGram
            // 
            this.f_rtbGram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_rtbGram.Location = new System.Drawing.Point(7, 19);
            this.f_rtbGram.Name = "f_rtbGram";
            this.f_rtbGram.ReadOnly = true;
            this.f_rtbGram.Size = new System.Drawing.Size(358, 182);
            this.f_rtbGram.TabIndex = 1;
            this.f_rtbGram.Text = "";
            // 
            // f_gbStep4
            // 
            this.f_gbStep4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_gbStep4.Controls.Add(this.f_btnSaveTable);
            this.f_gbStep4.Controls.Add(this.f_btnBrowseFileTable);
            this.f_gbStep4.Controls.Add(this.f_txtFileTable);
            this.f_gbStep4.Enabled = false;
            this.f_gbStep4.Location = new System.Drawing.Point(13, 353);
            this.f_gbStep4.Name = "f_gbStep4";
            this.f_gbStep4.Size = new System.Drawing.Size(373, 57);
            this.f_gbStep4.TabIndex = 9;
            this.f_gbStep4.TabStop = false;
            this.f_gbStep4.Text = "4. Сохранение таблицы разбора";
            // 
            // f_btnSaveTable
            // 
            this.f_btnSaveTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_btnSaveTable.Location = new System.Drawing.Point(290, 19);
            this.f_btnSaveTable.Name = "f_btnSaveTable";
            this.f_btnSaveTable.Size = new System.Drawing.Size(75, 22);
            this.f_btnSaveTable.TabIndex = 8;
            this.f_btnSaveTable.Text = "Сохранение";
            this.f_btnSaveTable.UseVisualStyleBackColor = true;
            this.f_btnSaveTable.Click += new System.EventHandler(this.f_btnSaveTable_Click);
            // 
            // f_btnBrowseFileTable
            // 
            this.f_btnBrowseFileTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_btnBrowseFileTable.Location = new System.Drawing.Point(208, 19);
            this.f_btnBrowseFileTable.Name = "f_btnBrowseFileTable";
            this.f_btnBrowseFileTable.Size = new System.Drawing.Size(75, 22);
            this.f_btnBrowseFileTable.TabIndex = 7;
            this.f_btnBrowseFileTable.Text = "Обзор...";
            this.f_btnBrowseFileTable.UseVisualStyleBackColor = true;
            this.f_btnBrowseFileTable.Click += new System.EventHandler(this.f_btnBrowseFileTable_Click);
            // 
            // f_txtFileTable
            // 
            this.f_txtFileTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_txtFileTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.f_txtFileTable.Location = new System.Drawing.Point(7, 19);
            this.f_txtFileTable.Name = "f_txtFileTable";
            this.f_txtFileTable.Size = new System.Drawing.Size(195, 22);
            this.f_txtFileTable.TabIndex = 6;
            // 
            // f_gbStep3
            // 
            this.f_gbStep3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.f_gbStep3.Controls.Add(this.f_btnViewTable);
            this.f_gbStep3.Controls.Add(this.f_btnGenerateTable);
            this.f_gbStep3.Enabled = false;
            this.f_gbStep3.Location = new System.Drawing.Point(13, 290);
            this.f_gbStep3.Name = "f_gbStep3";
            this.f_gbStep3.Size = new System.Drawing.Size(373, 57);
            this.f_gbStep3.TabIndex = 10;
            this.f_gbStep3.TabStop = false;
            this.f_gbStep3.Text = "3. Генерация и просмотр таблицы разбора";
            // 
            // f_btnViewTable
            // 
            this.f_btnViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.f_btnViewTable.Location = new System.Drawing.Point(208, 21);
            this.f_btnViewTable.Name = "f_btnViewTable";
            this.f_btnViewTable.Size = new System.Drawing.Size(157, 30);
            this.f_btnViewTable.TabIndex = 11;
            this.f_btnViewTable.Text = "Просмотр";
            this.f_btnViewTable.UseVisualStyleBackColor = true;
            this.f_btnViewTable.Click += new System.EventHandler(this.f_btnViewTable_Click);
            // 
            // f_btnGenerateTable
            // 
            this.f_btnGenerateTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.f_btnGenerateTable.Location = new System.Drawing.Point(7, 21);
            this.f_btnGenerateTable.Name = "f_btnGenerateTable";
            this.f_btnGenerateTable.Size = new System.Drawing.Size(195, 30);
            this.f_btnGenerateTable.TabIndex = 10;
            this.f_btnGenerateTable.Text = "Генерация";
            this.f_btnGenerateTable.UseVisualStyleBackColor = true;
            this.f_btnGenerateTable.Click += new System.EventHandler(this.f_btnGenerateTable_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.f_tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 407);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(398, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "Статусная строка";
            // 
            // f_tsslStatus
            // 
            this.f_tsslStatus.Name = "f_tsslStatus";
            this.f_tsslStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 429);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.f_gbStep3);
            this.Controls.Add(this.f_gbStep4);
            this.Controls.Add(this.f_gbStep2);
            this.Controls.Add(this.f_gbStep1);
            this.MinimumSize = new System.Drawing.Size(414, 467);
            this.Name = "Form1";
            this.Text = "Form1";
            this.f_gbStep1.ResumeLayout(false);
            this.f_gbStep1.PerformLayout();
            this.f_gbStep2.ResumeLayout(false);
            this.f_gbStep4.ResumeLayout(false);
            this.f_gbStep4.PerformLayout();
            this.f_gbStep3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox f_gbStep1;
        private System.Windows.Forms.Button f_btnLoadGram;
        private System.Windows.Forms.Button f_btnBrowseFileGram;
        private System.Windows.Forms.TextBox f_txtFileGram;
        private System.Windows.Forms.GroupBox f_gbStep2;
        private System.Windows.Forms.RichTextBox f_rtbGram;
        private System.Windows.Forms.GroupBox f_gbStep4;
        private System.Windows.Forms.Button f_btnSaveTable;
        private System.Windows.Forms.Button f_btnBrowseFileTable;
        private System.Windows.Forms.TextBox f_txtFileTable;
        private System.Windows.Forms.GroupBox f_gbStep3;
        private System.Windows.Forms.Button f_btnViewTable;
        private System.Windows.Forms.Button f_btnGenerateTable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel f_tsslStatus;
    }
}

