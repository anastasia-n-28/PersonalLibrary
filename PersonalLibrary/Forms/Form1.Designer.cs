namespace PersonalLibrary
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDeepSearch;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageSectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.Panel searchPanel;

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
            this.btnDeepSearch = new System.Windows.Forms.Button();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageSectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // txtTitle
            this.txtTitle.Location = new System.Drawing.Point(30, 140);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.PlaceholderText = "Назва книги";
            this.txtTitle.Size = new System.Drawing.Size(200, 23);

            // txtAuthor
            this.txtAuthor.Location = new System.Drawing.Point(30, 180);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.PlaceholderText = "Автор";
            this.txtAuthor.Size = new System.Drawing.Size(200, 23);

            // txtPublisher
            this.txtPublisher.Location = new System.Drawing.Point(30, 220);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.PlaceholderText = "Видавництво";
            this.txtPublisher.Size = new System.Drawing.Size(200, 23);

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(30, 260);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(200, 50);
            this.btnSearch.Text = "Пошук";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            // btnDeepSearch
            this.btnDeepSearch.Location = new System.Drawing.Point(30, 310);
            this.btnDeepSearch.Name = "btnDeepSearch";
            this.btnDeepSearch.Size = new System.Drawing.Size(200, 100);
            this.btnDeepSearch.Text = "Розширений пошук";
            this.btnDeepSearch.Click += new System.EventHandler(this.BtnDeepSearch_Click);

            // dgvBooks
            this.dgvBooks.Location = new System.Drawing.Point(270, 100);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.Size = new System.Drawing.Size(1800, 800);
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBooks.Dock = System.Windows.Forms.DockStyle.Fill;

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.addToolStripMenuItem,
                this.editToolStripMenuItem,
                this.deleteToolStripMenuItem,
                this.manageSectionsToolStripMenuItem,
                this.reportToolStripMenuItem,
                this.helpToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2100, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Top;

            // fileToolStripMenuItem
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.saveToolStripMenuItem,
                this.loadToolStripMenuItem,
                this.exitToolStripMenuItem });

            // addToolStripMenuItem
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.addToolStripMenuItem.Text = "Додати книгу";

            // editToolStripMenuItem
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.editToolStripMenuItem.Text = "Редагувати";

            // deleteToolStripMenuItem
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.deleteToolStripMenuItem.Text = "Видалити";

            // manageSectionsToolStripMenuItem
            this.manageSectionsToolStripMenuItem.Name = "manageSectionsToolStripMenuItem";
            this.manageSectionsToolStripMenuItem.Size = new System.Drawing.Size(150, 20);
            this.manageSectionsToolStripMenuItem.Text = "Управління розділами";

            // helpToolStripMenuItem
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.helpToolStripMenuItem.Text = "Довідка";

            // saveToolStripMenuItem
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Зберегти";

            // loadToolStripMenuItem
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Завантажити";

            // exitToolStripMenuItem
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Вихід";

            // reportToolStripMenuItem
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.reportToolStripMenuItem.Text = "Звіт";

            // searchPanel
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.searchPanel.Width = 250;
            this.searchPanel.Controls.Add(this.txtTitle);
            this.searchPanel.Controls.Add(this.txtAuthor);
            this.searchPanel.Controls.Add(this.txtPublisher);
            this.searchPanel.Controls.Add(this.btnSearch);
            this.searchPanel.Controls.Add(this.btnDeepSearch);

            // Form1
            this.ClientSize = new System.Drawing.Size(2100, 1000);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Бібліотека";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}