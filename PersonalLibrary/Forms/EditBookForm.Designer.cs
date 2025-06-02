namespace PersonalLibrary.Forms
{
    partial class EditBookForm
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
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.NumericUpDown numRating;
        private System.Windows.Forms.TextBox txtReview;
        private System.Windows.Forms.Button btnAddSection;

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
            this.numRating = new System.Windows.Forms.NumericUpDown();
            this.txtReview = new System.Windows.Forms.TextBox();
            this.btnAddSection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(20, 20);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 23);
            this.txtTitle.TabIndex = 0;
            this.txtTitle.PlaceholderText = "Title";
            // 
            // txtAuthors
            // 
            this.txtAuthors.Location = new System.Drawing.Point(20, 60);
            this.txtAuthors.Name = "txtAuthors";
            this.txtAuthors.Size = new System.Drawing.Size(200, 23);
            this.txtAuthors.TabIndex = 1;
            this.txtAuthors.PlaceholderText = "Authors";
            // 
            // txtPublisher
            // 
            this.txtPublisher.Location = new System.Drawing.Point(20, 100);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.Size = new System.Drawing.Size(200, 23);
            this.txtPublisher.TabIndex = 2;
            this.txtPublisher.PlaceholderText = "Publisher";
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(20, 140);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(200, 23);
            this.txtYear.TabIndex = 3;
            this.txtYear.PlaceholderText = "Year";
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
            this.txtOrigin.PlaceholderText = "Origin";
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
            this.cmbStatus.TabIndex = 8;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // numRating
            // 
            this.numRating.Location = new System.Drawing.Point(20, 340);
            this.numRating.Name = "numRating";
            this.numRating.Size = new System.Drawing.Size(200, 23);
            this.numRating.TabIndex = 9;
            this.numRating.Minimum = 1;
            this.numRating.Maximum = 5;
            // 
            // txtReview
            // 
            this.txtReview.Location = new System.Drawing.Point(20, 380);
            this.txtReview.Name = "txtReview";
            this.txtReview.Size = new System.Drawing.Size(200, 23);
            this.txtReview.TabIndex = 10;
            this.txtReview.PlaceholderText = "Відгук (опціонально)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(20, 440);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 30);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(130, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // EditBookForm
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
            this.Controls.Add(this.numRating);
            this.Controls.Add(this.txtReview);
            this.Name = "EditBookForm";
            this.Text = "Редагувати книгу";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
} 