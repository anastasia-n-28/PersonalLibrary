using PersonalLibrary.Models;
using PersonalLibrary.Forms;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace PersonalLibrary
{
    public partial class Form1 : Form
    {
        private Library _library;
        private const string DataFile = "library.json";
        private ContextMenuStrip dgvContextMenu;
        private BindingSource booksBindingSource = new BindingSource();
        
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            saveToolStripMenuItem.Click += (s, e) => { SyncBooksFromGrid(); SaveLibrary(); };
            loadToolStripMenuItem.Click += (s, e) => LoadLibrary();
            exitToolStripMenuItem.Click += (s, e) => Close();
            addToolStripMenuItem.Click += btnAdd_Click;
            editToolStripMenuItem.Click += btnEdit_Click;
            deleteToolStripMenuItem.Click += btnDelete_Click;
            manageSectionsToolStripMenuItem.Click += btnManageSections_Click;
            helpToolStripMenuItem.Click += btnHelp_Click;
            reportToolStripMenuItem.Click += btnReport_Click;
            dgvBooks.ReadOnly = false;
            dgvBooks.CellValidating += dgvBooks_CellValidating;
            dgvBooks.CellEndEdit += dgvBooks_CellEndEdit;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(DataFile))
            {
                _library = Library.DeserializeData(DataFile);
                if (_library == null)
                    _library = TestDataGenerator.Generate();
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
            var result = MessageBox.Show("Зберегти зміни перед виходом?", "Зберегти зміни", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
            UpdateDataGridView(results);
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
            using (var form = new AddBookForm(_library))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var section = _library.Sections.FirstOrDefault(s => s.Name == form.cmbSection.SelectedItem.ToString());
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
                MessageBox.Show("Оберіть книгу для редагування.", "Жодна книга не обрана", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedView = dgvBooks.SelectedRows[0].DataBoundItem as BookView;
            if (selectedView == null) return;
            var selectedBook = selectedView.BookRef;
            using (var form = new EditBookForm(selectedBook, _library))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Оберіть книгу для видалення.", "Жодна книга не обрана", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedView = dgvBooks.SelectedRows[0].DataBoundItem as BookView;
            if (selectedView == null) return;
            var selectedBook = selectedView.BookRef;
            var confirm = MessageBox.Show("Ви дійсно хочете видалити цю книгу?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            var section = _library.Sections.FirstOrDefault(s => s.Books.Any(b => b.ISBN == selectedBook.ISBN));
            if (section != null)
            {
                var bookToRemove = section.Books.FirstOrDefault(b => b.ISBN == selectedBook.ISBN);
                if (bookToRemove != null)
                {
                    section.Books.Remove(bookToRemove);
                    UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                    SaveLibrary();
                }
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            var bookViews = booksBindingSource.DataSource as List<BookView>;
            if (bookViews == null || bookViews.Count == 0)
            {
                MessageBox.Show("Немає даних для звіту!", "Звіт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
            sfd.Title = "Зберегти звіт";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var sw = new StreamWriter(sfd.FileName))
                    {
                        sw.WriteLine("Назва;Автор(и);Видавництво;Рік;ISBN;Походження;Статус;Оцінка;Відгук;Розділ");
                        foreach (var bookView in bookViews)
                        {
                            sw.WriteLine($"{bookView.Title};{bookView.Authors};{bookView.Publisher};{bookView.Year};{bookView.ISBN};{bookView.Origin};{bookView.Status};{bookView.Rating};{bookView.Review};{bookView.Section}");
                        }
                    }
                    MessageBox.Show("Звіт збережено у файл:\n" + sfd.FileName, "Звіт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при збереженні звіту:\n{ex.Message}\nШлях до файлу: {sfd.FileName}", "Помилка збереження звіту", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public class BookView
        {
            public string Section { get; set; }
            public string Title { get; set; }
            public string Authors { get; set; }
            public string Publisher { get; set; }
            public int Year { get; set; }
            public string ISBN { get; set; }
            public string Origin { get; set; }
            public BookStatus Status { get; set; }
            public int Rating { get; set; }
            public string Review { get; set; }
            public Book BookRef { get; set; } // для редагування/видалення
        }

        private void UpdateDataGridView(IEnumerable<Book> books)
        {
            var bookViews = books.Select(book => new BookView
            {
                Section = _library.Sections.FirstOrDefault(s => s.Books.Contains(book))?.Name ?? "",
                Title = book.Title,
                Authors = book.Authors,
                Publisher = book.Publisher,
                Year = book.Year,
                ISBN = book.ISBN,
                Origin = book.Origin,
                Status = book.Status,
                Rating = book.Rating?.Score ?? 0,
                Review = book.Rating?.Review ?? "",
                BookRef = book
            }).ToList();
            booksBindingSource.DataSource = bookViews;
            dgvBooks.DataSource = booksBindingSource;
            dgvBooks.Columns.Clear();
            dgvBooks.AutoGenerateColumns = false;
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Section", HeaderText = "Розділ", DataPropertyName = "Section" });
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Title", DataPropertyName = "Title", HeaderText = "Назва" });
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Authors", DataPropertyName = "Authors", HeaderText = "Автори" });
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Publisher", DataPropertyName = "Publisher", HeaderText = "Видавництво" });
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Year", DataPropertyName = "Year", HeaderText = "Рік" });
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "ISBN", DataPropertyName = "ISBN", HeaderText = "ISBN" });
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Origin", DataPropertyName = "Origin", HeaderText = "Походження" });
            dgvBooks.Columns.Add(new DataGridViewComboBoxColumn { Name = "Status", DataPropertyName = "Status", HeaderText = "Статус", DataSource = Enum.GetValues(typeof(BookStatus)) });
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Rating", HeaderText = "Оцінка", DataPropertyName = "Rating" });
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Review", HeaderText = "Відгук", DataPropertyName = "Review" });
        }

        private void dgvBooks_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var colName = dgvBooks.Columns[e.ColumnIndex].Name;
            if (colName == "Year")
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int year) || year <= 0)
                {
                    dgvBooks.Rows[e.RowIndex].ErrorText = "Рік має бути додатнім числом";
                    e.Cancel = true;
                }
            }
            if (colName == "Rating")
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int score) || score < 1 || score > 5)
                {
                    dgvBooks.Rows[e.RowIndex].ErrorText = "Оцінка має бути від 1 до 5";
                    e.Cancel = true;
                }
            }
        }

        private void dgvBooks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvBooks.Rows[e.RowIndex].ErrorText = string.Empty;
            /* Оновлення даних у _library на основі змін у DataGridView одразу
            var row = dgvBooks.Rows[e.RowIndex];
            if (row.DataBoundItem is Book book)
            {
                var colName = dgvBooks.Columns[e.ColumnIndex].Name;
                var value = row.Cells[e.ColumnIndex].Value;
                switch (colName)
                {
                    case "Title": book.Title = value?.ToString(); break;
                    case "Authors": book.Authors = value?.ToString(); break;
                    case "Publisher": book.Publisher = value?.ToString(); break;
                    case "Year": book.Year = int.TryParse(value?.ToString(), out int year) ? year : 0; break;
                    case "ISBN": book.ISBN = value?.ToString(); break;
                    case "Origin": book.Origin = value?.ToString(); break;
                    case "Status": book.Status = (BookStatus)value; break;
                    case "Rating":
                        if (book.Rating == null) book.Rating = new UserRating();
                        book.Rating.Score = int.TryParse(value?.ToString(), out int score) ? score : 0;
                        break;
                    case "Review":
                        if (book.Rating == null) book.Rating = new UserRating();
                        book.Rating.Review = value?.ToString();
                        break;
                }
            }
            */
        }
        
        private void SaveLibrary()
        {
            try
            {
                SyncBooksFromGrid();
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
                    _library = Library.DeserializeData(DataFile);
                    if (_library == null)
                        _library = TestDataGenerator.Generate();
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

        private void SyncBooksFromGrid()
        {
            // Оновлюємо дані у _library на основі DataGridView
            foreach (DataGridViewRow row in dgvBooks.Rows)
            {
                if (row.DataBoundItem is BookView bookView)
                {
                    var book = bookView.BookRef;
                    book.Title = row.Cells["Title"].Value?.ToString();
                    book.Authors = row.Cells["Authors"].Value?.ToString();
                    book.Publisher = row.Cells["Publisher"].Value?.ToString();
                    book.Year = int.TryParse(row.Cells["Year"].Value?.ToString(), out int year) ? year : 0;
                    book.ISBN = row.Cells["ISBN"].Value?.ToString();
                    book.Origin = row.Cells["Origin"].Value?.ToString();
                    book.Status = (BookStatus)row.Cells["Status"].Value;
                    if (book.Rating == null) book.Rating = new UserRating();
                    book.Rating.Score = int.TryParse(row.Cells["Rating"].Value?.ToString(), out int score) ? score : 0;
                    book.Rating.Review = row.Cells["Review"].Value?.ToString();
                }
            }
        }
    }
}
