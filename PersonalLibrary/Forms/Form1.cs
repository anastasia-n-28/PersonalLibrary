using PersonalLibrary.Models;
using PersonalLibrary.Forms;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace PersonalLibrary
{
    public partial class Form1 : Form
    {
        private Library? _library;
        private const string DataFile = "library.json";
        private readonly BindingSource booksBindingSource = [];
        private bool _isLibraryModified = false;
        private bool _isAdvancedSearchFormOpen = false;
        
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            saveToolStripMenuItem.Click += (s, e) => { SyncBooksFromGrid(); SaveLibrary(); };
            loadToolStripMenuItem.Click += (s, e) => LoadLibrary();
            exitToolStripMenuItem.Click += (s, e) => Close();
            addToolStripMenuItem.Click += BtnAdd_Click;
            editToolStripMenuItem.Click += BtnEdit_Click;
            deleteToolStripMenuItem.Click += BtnDelete_Click;
            manageSectionsToolStripMenuItem.Click += BtnManageSections_Click;
            helpToolStripMenuItem.Click += BtnHelp_Click;
            reportToolStripMenuItem.Click += BtnReport_Click;
            dgvBooks.ReadOnly = false;
            dgvBooks.CellValidating += DgvBooks_CellValidating;
            dgvBooks.CellEndEdit += DgvBooks_CellEndEdit;
            btnSearch.Click += BtnSearch_Click;
            btnDeepSearch.Click += BtnDeepSearch_Click;
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            if (File.Exists(DataFile))
            {
                _library = Library.DeserializeData(DataFile);
                _library ??= TestDataGenerator.Generate();
            }
            else
            {
                _library = TestDataGenerator.Generate();
            }
            _isLibraryModified = false;

            UpdateDataGridView(_library?.Sections.SelectMany(s => s.Books));
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_isLibraryModified)
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
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            var title = txtTitle.Text;
            var author = txtAuthor.Text;
            var publisher = txtPublisher.Text;

            var results = _library?.FindBooks(title, author, publisher);
            UpdateDataGridView(results);

            if (results == null || !results.Any())
            {
                MessageBox.Show("Книг не знайдено", "Пошук", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void BtnDeepSearch_Click(object? sender, EventArgs e)
        {
            if (_isAdvancedSearchFormOpen) return;

            if (_library != null)
            {
                _isAdvancedSearchFormOpen = true;
                using var form = new AdvancedSearchForm(_library);
                DialogResult result = form.ShowDialog();

                await Task.Delay(100);

                _isAdvancedSearchFormOpen = false;

                if (result == DialogResult.OK)
                {
                    UpdateDataGridView(form.SearchResults);
                }
            }
            else
            {
                _isAdvancedSearchFormOpen = false;
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (_library != null)
            {
                using var form = new AddBookForm(_library);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.Book != null)
                    {
                        var sectionName = form.cmbSection.SelectedItem?.ToString();
                        if (sectionName != null)
                        {
                            var section = _library.Sections.FirstOrDefault(s => s.Name == sectionName);
                            if (section != null)
                    {
                        section.AddBook(form.Book);
                        UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                                SaveLibrary();
                                _isLibraryModified = true;
                            }
                        }
                    }
                }
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Оберіть книгу для редагування.", "Жодна книга не обрана", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvBooks.SelectedRows[0].DataBoundItem is BookView selectedView)
            {
                var selectedBook = selectedView.BookRef;
                if (selectedBook != null && _library != null)
                {
                    using var form = new EditBookForm(selectedBook, _library, _library.Sections);
                    if (form.ShowDialog() == DialogResult.OK)
                {
                    UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                        SaveLibrary();
                        _isLibraryModified = true;
                    }
                }
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Оберіть книгу для видалення.", "Жодна книга не обрана", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvBooks.SelectedRows[0].DataBoundItem is BookView selectedView)
            {
                var selectedBook = selectedView.BookRef;
                if (selectedBook != null && _library != null)
                {
            var confirm = MessageBox.Show("Ви дійсно хочете видалити цю книгу?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

                    if (selectedBook.ISBN != null)
                    {
                        var section = _library.Sections.FirstOrDefault(s => s.Books.Any(b => b.ISBN == selectedBook.ISBN));
            if (section != null)
            {
                            var bookToRemove = section.Books.FirstOrDefault(b => b.ISBN == selectedBook.ISBN);
                            if (bookToRemove != null)
                            {
                                section.Books.Remove(bookToRemove);
                UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                                SaveLibrary();
                                _isLibraryModified = true;
                            }
                        }
                    }
                }
            }
        }

        private void BtnManageSections_Click(object? sender, EventArgs e)
        {
            if (_library != null)
            {
                using var form = new ManageSectionsForm(_library);
                form.ShowDialog();
                UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                _isLibraryModified = true;
            }
        }
        
        private void BtnHelp_Click(object? sender, EventArgs e)
        {
            string helpText = "Інструкція користувача:\n\n1. Додайте книги...\n2. Використовуйте пошук...\n...";
            MessageBox.Show(helpText, "Довідка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReport_Click(object? sender, EventArgs e)
        {
            if (booksBindingSource.DataSource is List<BookView> bookViews)
            {
                if (bookViews.Count == 0)
                {
                    MessageBox.Show("Немає даних для звіту!", "Звіт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using var sfd = new SaveFileDialog();
                sfd.Filter = "Text files (*.txt)|*.txt";
                sfd.Title = "Зберегти звіт";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using var sw = new StreamWriter(sfd.FileName);
                        sw.WriteLine("Book Info");
                        foreach (var bookView in bookViews)
                        {
                            var bookInfo = bookView.BookRef?.GetInfo() ?? "";
                            sw.WriteLine(bookInfo.Replace(";", ","));
                        }
                        MessageBox.Show("Звіт збережено у файл:\n" + sfd.FileName, "Звіт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при збереженні звіту:\n{ex.Message}\nШлях до файлу: {sfd.FileName}", "Помилка збереження звіту", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Немає даних для звіту!", "Звіт", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public class BookView
        {
            public string? Section { get; set; }
            public string? Title { get; set; }
            public string? Authors { get; set; }
            public string? Publisher { get; set; }
            public int Year { get; set; }
            public string? ISBN { get; set; }
            public string? Origin { get; set; }
            public BookStatus Status { get; set; }
            public int Rating { get; set; }
            public string? Review { get; set; }
            public Book? BookRef { get; set; }
        }

        private void UpdateDataGridView(IEnumerable<Book>? books)
        {
            if (books == null)
            {
                booksBindingSource.DataSource = new List<BookView>();
                if (dgvBooks != null)
                {
                    dgvBooks.DataSource = booksBindingSource;
                }
                return;
            }

            var bookViews = books.Select(book => new BookView
            {
                Section = _library?.Sections.FirstOrDefault(s => s.Books.Contains(book))?.Name ?? "",
                Title = book.Title ?? "",
                Authors = book.Authors ?? "",
                Publisher = book.Publisher ?? "",
                Year = book.Year,
                ISBN = book.ISBN ?? "",
                Origin = book.Origin ?? "",
                Status = book.Status,
                Rating = book.Rating?.Score ?? 0,
                Review = book.Rating?.Review ?? "",
                BookRef = book
            }).ToList();
            booksBindingSource.DataSource = bookViews;
            
            if (dgvBooks != null)
            {
                dgvBooks.DataSource = booksBindingSource;
                dgvBooks.Columns.Clear();
                dgvBooks.AutoGenerateColumns = false;
                dgvBooks.Columns.AddRange(
                    [
                        new DataGridViewTextBoxColumn { Name = "Section", HeaderText = "Розділ", DataPropertyName = "Section" },
                        new DataGridViewTextBoxColumn { Name = "Title", DataPropertyName = "Title", HeaderText = "Назва" },
                        new DataGridViewTextBoxColumn { Name = "Authors", DataPropertyName = "Authors", HeaderText = "Автори" },
                        new DataGridViewTextBoxColumn { Name = "Publisher", DataPropertyName = "Publisher", HeaderText = "Видавництво" },
                        new DataGridViewTextBoxColumn { Name = "Year", DataPropertyName = "Year", HeaderText = "Рік" },
                        new DataGridViewTextBoxColumn { Name = "ISBN", DataPropertyName = "ISBN", HeaderText = "ISBN" },
                        new DataGridViewTextBoxColumn { Name = "Origin", DataPropertyName = "Origin", HeaderText = "Походження" },
                        new DataGridViewComboBoxColumn { Name = "Status", DataPropertyName = "Status", HeaderText = "Статус", DataSource = Enum.GetValues(typeof(BookStatus)) },
                        new DataGridViewTextBoxColumn { Name = "Rating", HeaderText = "Оцінка", DataPropertyName = "Rating" },
                        new DataGridViewTextBoxColumn { Name = "Review", HeaderText = "Відгук", DataPropertyName = "Review" }
                    ]
                );
            }
        }

        private void DgvBooks_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvBooks?.Columns == null) return;

            var colName = dgvBooks.Columns[e.ColumnIndex]?.Name;

            if (colName == null) return;

            if (dgvBooks.Rows == null || e.RowIndex < 0 || e.RowIndex >= dgvBooks.Rows.Count || dgvBooks.Rows[e.RowIndex] == null) return;

            if (colName == "Year")
            {
                if (!int.TryParse(e.FormattedValue?.ToString(), out int year) || year <= 0)
                {
                    dgvBooks.Rows[e.RowIndex].ErrorText = "Рік має бути додатнім числом";
                    e.Cancel = true;
                }
            }
            if (colName == "Rating")
            {
                if (!int.TryParse(e.FormattedValue?.ToString(), out int score) || score < 1 || score > 5)
                {
                    dgvBooks.Rows[e.RowIndex].ErrorText = "Оцінка має бути від 1 до 5";
                    e.Cancel = true;
                }
            }
        }

        private void DgvBooks_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (dgvBooks?.Rows == null || e.RowIndex < 0 || e.RowIndex >= dgvBooks.Rows.Count || dgvBooks.Rows[e.RowIndex] == null) return;

            dgvBooks.Rows[e.RowIndex].ErrorText = string.Empty;
        }
        
        private void SaveLibrary()
        {
            try
            {
                SyncBooksFromGrid();
                if (_library != null)
                {
                _library.SerializeData(DataFile);
                MessageBox.Show("Дані успішно збережено!", "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isLibraryModified = false;
                }
                else
                {
                    MessageBox.Show("Помилка: об'єкт бібліотеки відсутній.", "Помилка збереження", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                    {
                        MessageBox.Show("Помилка десеріалізації даних. Завантажено тестові дані.", "Помилка завантаження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _library = TestDataGenerator.Generate();
                    }
                    else
                    {
                        MessageBox.Show("Дані успішно завантажено!", "Завантаження", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (_library != null)
                    {
                    UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                    }
                }
                else
                {
                    MessageBox.Show("Файл збереження не знайдено! Створено нову бібліотеку.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _library = TestDataGenerator.Generate();
                    if (_library != null)
                    {
                        UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                    }
                }
                _isLibraryModified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _library = TestDataGenerator.Generate();
                if (_library != null)
                {
                    UpdateDataGridView(_library.Sections.SelectMany(s => s.Books));
                }
                _isLibraryModified = false;
            }
        }

        private void SyncBooksFromGrid()
        {
            if (dgvBooks?.Rows == null || _library == null) return;

            foreach (DataGridViewRow row in dgvBooks.Rows)
            {
                if (row.DataBoundItem is BookView bookView)
                {
                    var book = bookView.BookRef;
                    if (book != null)
                    {
                        if (row.Cells == null) continue;

                        book.Title = row.Cells["Title"]?.Value?.ToString() ?? "";
                        book.Authors = row.Cells["Authors"]?.Value?.ToString() ?? "";
                        book.Publisher = row.Cells["Publisher"]?.Value?.ToString() ?? "";

                        if (row.Cells["Year"]?.Value != null && int.TryParse(row.Cells["Year"].Value.ToString(), out int yearResult))
                        {
                            book.Year = yearResult;
                        } else {
                        }

                        book.ISBN = row.Cells["ISBN"]?.Value?.ToString() ?? "";
                        book.Origin = row.Cells["Origin"]?.Value?.ToString() ?? "";
                        
                        if (row.Cells["Status"]?.Value is BookStatus status) book.Status = status;

                        book.Rating ??= new UserRating();
                        
                        if (row.Cells["Rating"]?.Value != null && int.TryParse(row.Cells["Rating"].Value.ToString(), out int scoreResult))
                        {
                            book.Rating.Score = scoreResult;
                        } else {
                        }
                        
                        book.Rating.Review = row.Cells["Review"]?.Value?.ToString() ?? "";
                        _isLibraryModified = true;
                    }
                }
            }
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BtnDelete_Click(dgvBooks, EventArgs.Empty);
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (dgvBooks.ContainsFocus || dgvBooks.Focused)
                {
                     if (dgvBooks.SelectedRows.Count > 0)
                    {
                        BtnEdit_Click(dgvBooks, EventArgs.Empty);
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
