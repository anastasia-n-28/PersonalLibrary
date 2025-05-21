namespace PersonalLibrary
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtPublisher = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // txtTitle
            this.txtTitle.Location = new System.Drawing.Point(30, 30);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.PlaceholderText = "Назва книги";
            this.txtTitle.Size = new System.Drawing.Size(200, 23);

            // txtAuthor
            this.txtAuthor.Location = new System.Drawing.Point(30, 70);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.PlaceholderText = "Автор";
            this.txtAuthor.Size = new System.Drawing.Size(200, 23);

            // txtPublisher
            this.txtPublisher.Location = new System.Drawing.Point(30, 110);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.PlaceholderText = "Видавництво";
            this.txtPublisher.Size = new System.Drawing.Size(200, 23);

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(30, 150);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(200, 50);
            this.btnSearch.Text = "Пошук";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // dgvBooks
            this.dgvBooks.Location = new System.Drawing.Point(270, 30);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.Size = new System.Drawing.Size(1200, 800);
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(30, 210);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(200, 50);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Додати книгу";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(30, 270);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(200, 50);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Редагувати книгу";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(30, 330);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(200, 50);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Видалити книгу";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(1950, 950);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtPublisher);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Name = "Form1";
            this.Text = "Бібліотека";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
