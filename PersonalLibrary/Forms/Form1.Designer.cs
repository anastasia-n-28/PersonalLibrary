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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonManage;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;

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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonManage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.SuspendLayout();

            // txtTitle
            this.txtTitle.Location = new System.Drawing.Point(30, 100);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.PlaceholderText = "Назва книги";
            this.txtTitle.Size = new System.Drawing.Size(200, 23);

            // txtAuthor
            this.txtAuthor.Location = new System.Drawing.Point(30, 140);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.PlaceholderText = "Автор";
            this.txtAuthor.Size = new System.Drawing.Size(200, 23);

            // txtPublisher
            this.txtPublisher.Location = new System.Drawing.Point(30, 180);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.PlaceholderText = "Видавництво";
            this.txtPublisher.Size = new System.Drawing.Size(200, 23);

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(30, 220);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(200, 50);
            this.btnSearch.Text = "Пошук";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // btnDeepSearch
            this.btnDeepSearch.Location = new System.Drawing.Point(30, 270);
            this.btnDeepSearch.Name = "btnDeepSearch";
            this.btnDeepSearch.Size = new System.Drawing.Size(200, 100);
            this.btnDeepSearch.Text = "Розширений пошук";
            this.btnDeepSearch.Click += new System.EventHandler(this.btnDeepSearch_Click);

            // dgvBooks
            this.dgvBooks.Location = new System.Drawing.Point(270, 100);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.Size = new System.Drawing.Size(1800, 800);
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // toolStrip1
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.toolStripButtonAdd,
                this.toolStripButtonEdit,
                this.toolStripButtonDelete,
                this.toolStripButtonManage,
                this.toolStripButtonHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1500, 40);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Stretch = true;

            // toolStripButtonAdd
            this.toolStripButtonAdd.Text = "Додати";
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // toolStripButtonEdit
            this.toolStripButtonEdit.Text = "Редагувати";
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // toolStripButtonDelete
            this.toolStripButtonDelete.Text = "Видалити";
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // toolStripButtonManage
            this.toolStripButtonManage.Text = "Управління розділами";
            this.toolStripButtonManage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonManage.Click += new System.EventHandler(this.btnManageSections_Click);

            // toolStripButtonHelp
            this.toolStripButtonHelp.Text = "Довідка";
            this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonHelp.Click += new System.EventHandler(this.btnHelp_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(2100, 1000);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtPublisher);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnDeepSearch);
            this.Controls.Add(this.dgvBooks);
            this.Name = "Form1";
            this.Text = "Бібліотека";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}