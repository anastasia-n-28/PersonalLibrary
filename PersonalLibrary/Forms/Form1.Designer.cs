namespace PersonalLibrary
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstResults;

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
            this.lstResults = new System.Windows.Forms.ListBox();
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

            // lstResults
            this.lstResults.FormattingEnabled = true;
            this.lstResults.ItemHeight = 15;
            this.lstResults.Location = new System.Drawing.Point(250, 30);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(500, 274);

            // Form1
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtPublisher);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lstResults);
            this.Name = "Form1";
            this.Text = "Бібліотека";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
