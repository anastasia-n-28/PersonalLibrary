namespace PersonalLibrary.Forms
{
    partial class AddBookForm
    {
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthors;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.TextBox txtOrigin;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbStatus;
        public System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.ComboBox cmbRating;
        private System.Windows.Forms.TextBox txtReview;
        private System.Windows.Forms.Button btnAddSection;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthors = new System.Windows.Forms.TextBox();
            this.txtPublisher = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.txtOrigin = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.btnAddSection = new System.Windows.Forms.Button();
            this.cmbRating = new System.Windows.Forms.ComboBox();
            this.txtReview = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(20, 20);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 23);
            this.txtTitle.TabIndex = 0;
            this.txtTitle.PlaceholderText = "Назва";
            // 
            // txtAuthors
            // 
            this.txtAuthors.Location = new System.Drawing.Point(20, 60);
            this.txtAuthors.Name = "txtAuthors";
            this.txtAuthors.Size = new System.Drawing.Size(200, 23);
            this.txtAuthors.TabIndex = 1;
            this.txtAuthors.PlaceholderText = "Автор(и)";
            // 
            // txtPublisher
            // 
            this.txtPublisher.Location = new System.Drawing.Point(20, 100);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.Size = new System.Drawing.Size(200, 23);
            this.txtPublisher.TabIndex = 2;
            this.txtPublisher.PlaceholderText = "Видавництво";
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(20, 140);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(200, 23);
            this.txtYear.TabIndex = 3;
            this.txtYear.PlaceholderText = "Рік";
            // 
            // txtISBN
            // 
            this.txtISBN.Location = new System.Drawing.Point(20, 180);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(200, 23);
            this.txtISBN.TabIndex = 4;
            this.txtISBN.PlaceholderText = "ISBN";
            // 
            // txtOrigin
            // 
            this.txtOrigin.Location = new System.Drawing.Point(20, 220);
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.Size = new System.Drawing.Size(200, 23);
            this.txtOrigin.TabIndex = 5;
            this.txtOrigin.PlaceholderText = "Походження";
            // 
            // cmbSection
            // 
            this.cmbSection.Location = new System.Drawing.Point(20, 260);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(200, 23);
            this.cmbSection.TabIndex = 6;
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // btnAddSection
            // 
            this.btnAddSection.Location = new System.Drawing.Point(230, 260);
            this.btnAddSection.Name = "btnAddSection";
            this.btnAddSection.Size = new System.Drawing.Size(120, 23);
            this.btnAddSection.TabIndex = 7;
            this.btnAddSection.Text = "Додати розділ";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Location = new System.Drawing.Point(20, 300);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 23);
            this.cmbStatus.TabIndex = 6;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // cmbRating
            // 
            this.cmbRating.Location = new System.Drawing.Point(20, 340);
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.Size = new System.Drawing.Size(200, 23);
            this.cmbRating.TabIndex = 8;
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // txtReview
            // 
            this.txtReview.Location = new System.Drawing.Point(20, 380);
            this.txtReview.Name = "txtReview";
            this.txtReview.Size = new System.Drawing.Size(200, 23);
            this.txtReview.TabIndex = 9;
            this.txtReview.PlaceholderText = "Відгук (опціонально)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(20, 440);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 30);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(130, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // AddBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 500);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtAuthors);
            this.Controls.Add(this.txtPublisher);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtISBN);
            this.Controls.Add(this.txtOrigin);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.cmbSection);
            this.Controls.Add(this.btnAddSection);
            this.Controls.Add(this.cmbRating);
            this.Controls.Add(this.txtReview);
            this.Name = "AddBookForm";
            this.Text = "Додати книгу";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
} 