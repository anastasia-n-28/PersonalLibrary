using PersonalLibrary.Models;
using PersonalLibrary.Forms;
using Microsoft.VisualBasic;

namespace PersonalLibrary
{
    public partial class Form1 : Form
    {
        private Library _library;
        private const string DataFile = "library.json";
        private ContextMenuStrip dgvContextMenu;
        
        public Form1()
        {
            InitializeComponent();
            InitializeToolStrip();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(DataFile))
            {
                _library = new Library();
                _library.DeserializeData(DataFile);
            }
            else
            {
                _library = TestDataGenerator.Generate();
            }
            UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
            //InitializeDataGridViewContextMenu();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveLibrary();
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var title = txtTitle.Text;
            var author = txtAuthor.Text;
            var publisher = txtPublisher.Text;

            var results = _library.FindBooks(title, author, publisher);
            dgvBooks.DataSource = null;
            dgvBooks.DataSource = results;
        }

        private void btnDeepSearch_Click(object sender, EventArgs e)
        {
            using (var form = new AdvancedSearchForm(_library))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    UpdateDataGridView(form.SearchResults);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new AddBookForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var section = _library.Sections.FirstOrDefault();
                    if (section != null)
                    {
                        section.AddBook(form.Book);
                        UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to edit.", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedBook = dgvBooks.SelectedRows[0].DataBoundItem as Book;
            if (selectedBook == null) return;
            using (var form = new EditBookForm(selectedBook))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    dgvBooks.Refresh();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to delete.", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show("Ви дійсно хочете видалити цю книгу?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            var selectedBook = dgvBooks.SelectedRows[0].DataBoundItem as Book;
            if (selectedBook == null) return;
            var section = _library.Sections.FirstOrDefault(s => s.Books.Contains(selectedBook));
            if (section != null)
            {
                _library.RemoveBook(section.Name, selectedBook);
                UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
            }
        }

        private void btnManageSections_Click(object sender, EventArgs e)
        {
            using (var form = new ManageSectionsForm(_library))
            {
                form.ShowDialog();
                UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
            }
        }
        
        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpText = "Інструкція користувача:\n\n1. Додайте книги...\n2. Використовуйте пошук...\n...";
            MessageBox.Show(helpText, "Довідка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateDataGridView(IEnumerable<Book> books)
        {
            dgvBooks.DataSource = null;
            dgvBooks.DataSource = books;
            if (dgvBooks.Columns["Info"] != null)
                dgvBooks.Columns["Info"].Visible = false;
        }

        private void InitializeToolStrip()
        {
            var toolStrip = new ToolStrip();
            
            var saveButton = new ToolStripButton("Зберегти");
            saveButton.Click += (s, e) => SaveLibrary();
            
            var loadButton = new ToolStripButton("Завантажити");
            loadButton.Click += (s, e) => LoadLibrary();
            
            var exitButton = new ToolStripButton("Вихід");
            exitButton.Click += (s, e) => Application.Exit();
            
            toolStrip.Items.AddRange(new ToolStripItem[] { 
                saveButton, 
                loadButton,
                exitButton
            });
            
            this.Controls.Add(toolStrip);
            toolStrip.Dock = DockStyle.Top;
        }

        //private void SimpleSearch(string searchText)
        //{
        //    if (string.IsNullOrWhiteSpace(searchText))
        //    {
        //        UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
        //        return;
        //    }

        //    var results = _library.Sections
        //        .SelectMany(s => s.Books)
        //        .Where(b => 
        //            b.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
        //            b.Authors.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
        //            b.Publisher.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        //        .ToList();

        //    UpdateDataGridView(results);
        //}

        private void SaveLibrary()
        {
            try
            {
                _library.SerializeData(DataFile);
                MessageBox.Show("Дані успішно збережено!", "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLibrary()
        {
            try
            {
                if (File.Exists(DataFile))
                {
                    _library = new Library();
                    _library.DeserializeData(DataFile);
                    UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                    MessageBox.Show("Дані успішно завантажено!", "Завантаження", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Файл збереження не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
