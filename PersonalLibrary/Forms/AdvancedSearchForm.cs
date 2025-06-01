using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PersonalLibrary.Models;

namespace PersonalLibrary.Forms
{
    public partial class AdvancedSearchForm : Form
    {
        private readonly Library _library;
        private DataGridView dgvResults = null!;
        private TextBox txtTitle = null!;
        private TextBox txtAuthor = null!;
        private TextBox txtPublisher = null!;
        private NumericUpDown numYearFrom = null!;
        private NumericUpDown numYearTo = null!;
        private ComboBox cmbSection = null!;
        private ComboBox cmbStatus = null!;
        private ComboBox cmbRating = null!;
        private Button btnSearch = null!;
        private Button btnClose = null!;

        public List<Book>? SearchResults { get; private set; }

        public AdvancedSearchForm(Library library)
        {
            _library = library ?? throw new ArgumentNullException(nameof(library));

            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            this.Text = "Розширений пошук";
            this.Size = new System.Drawing.Size(800, 600);

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 8,
                Padding = new Padding(10)
            };

            // Заголовок
            mainPanel.Controls.Add(new Label { Text = "Назва:", Height = 40, Width = 120 }, 0, 0);
            txtTitle = new() { Dock = DockStyle.Fill, Height = 40, Width = 120 };
            mainPanel.Controls.Add(txtTitle, 1, 0);

            // Автор
            mainPanel.Controls.Add(new Label { Text = "Автор:", Height = 40 }, 0, 1);
            txtAuthor = new() { Dock = DockStyle.Fill, Height = 40 };
            mainPanel.Controls.Add(txtAuthor, 1, 1);

            // Видавництво
            mainPanel.Controls.Add(new Label { Text = "Видавництво:", Height = 40, Width = 180 }, 0, 2);
            txtPublisher = new() { Dock = DockStyle.Fill, Height = 40, Width = 180 };
            mainPanel.Controls.Add(txtPublisher, 1, 2);

            // Рік видання
            var yearPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, Height = 40 };
            mainPanel.Controls.Add(new Label { Text = "Рік видання:", Height = 40 }, 0, 3);
            numYearFrom = new() { Minimum = 0, Maximum = DateTime.Now.Year, Width = 100, Height = 40 };
            numYearTo = new() { Minimum = 0, Maximum = DateTime.Now.Year, Width = 100, Height = 40 };
            yearPanel.Controls.AddRange([numYearFrom, new Label { Text = " - ", Height = 40 }, numYearTo]);
            mainPanel.Controls.Add(yearPanel, 1, 3);

            // Розділ
            mainPanel.Controls.Add(new Label { Text = "Розділ:", Height = 40 }, 0, 4);
            cmbSection = new() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Height = 40 };
            cmbSection.Items.Add("Будь-який");
            cmbSection.Items.AddRange([.. _library.Sections.Select(s => s.Name)]);
            cmbSection.SelectedIndex = 0;
            mainPanel.Controls.Add(cmbSection, 1, 4);

            // Статус
            mainPanel.Controls.Add(new Label { Text = "Статус:", Height = 40 }, 0, 5);
            cmbStatus = new() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Height = 40 };
            cmbStatus.Items.Add("Будь-який");
            foreach (BookStatus status in Enum.GetValues(typeof(BookStatus)))
                cmbStatus.Items.Add(status);
            cmbStatus.SelectedIndex = 0;
            mainPanel.Controls.Add(cmbStatus, 1, 5);

            // Оцінка
            mainPanel.Controls.Add(new Label { Text = "Оцінка:", Height = 40 }, 0, 6);
            cmbRating = new() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Height = 40 };
            cmbRating.Items.Add("Будь-яка");
            for (int i = 1; i <= 5; i++)
                cmbRating.Items.Add(i);
            cmbRating.SelectedIndex = 0;
            mainPanel.Controls.Add(cmbRating, 1, 6);

            // Кнопки
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Height = 60
            };

            btnClose = new()
            {
                Text = "Закрити",
                Width = 120,
                Height = 40
            };
            btnClose.Click += BtnClose_Click;

            btnSearch = new()
            {
                Text = "Пошук",
                Width = 120,
                Height = 40
            };
            btnSearch.Click += BtnSearch_Click;

            buttonPanel.Controls.AddRange([btnClose, btnSearch]);

            // Таблиця результатів
            dgvResults = new()
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoGenerateColumns = false
            };

            // Додаємо колонки вручну для відображення BookView
            dgvResults.Columns.AddRange(
                [
                    new DataGridViewTextBoxColumn { Name = "Section", HeaderText = "Розділ", DataPropertyName = "Section" },
                    new DataGridViewTextBoxColumn { Name = "Title", DataPropertyName = "Title", HeaderText = "Назва" },
                    new DataGridViewTextBoxColumn { Name = "Authors", DataPropertyName = "Authors", HeaderText = "Автори" },
                    new DataGridViewTextBoxColumn { Name = "Publisher", DataPropertyName = "Publisher", HeaderText = "Видавництво" },
                    new DataGridViewTextBoxColumn { Name = "Year", DataPropertyName = "Year", HeaderText = "Рік" },
                    new DataGridViewTextBoxColumn { Name = "ISBN", DataPropertyName = "ISBN", HeaderText = "ISBN" },
                    new DataGridViewTextBoxColumn { Name = "Origin", DataPropertyName = "Origin", HeaderText = "Походження" },
                    new DataGridViewTextBoxColumn { Name = "Status", DataPropertyName = "Status", HeaderText = "Статус" },
                    new DataGridViewTextBoxColumn { Name = "Rating", HeaderText = "Оцінка", DataPropertyName = "Rating" },
                    new DataGridViewTextBoxColumn { Name = "Review", HeaderText = "Відгук", DataPropertyName = "Review" }
                ]
            );

            var resultsPanel = new Panel { Dock = DockStyle.Fill };
            resultsPanel.Controls.Add(dgvResults);

            this.Controls.Add(mainPanel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(resultsPanel);
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                // Отримуємо всі книги
                var results = _library.Sections.SelectMany(s => s.Books).ToList();

                // Фільтруємо за назвою
                if (!string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    results = [.. results.Where(b => b.Title?.Contains(txtTitle.Text, StringComparison.OrdinalIgnoreCase) == true)];
                }

                // Фільтруємо за автором
                if (!string.IsNullOrWhiteSpace(txtAuthor.Text))
                {
                    results = [.. results.Where(b => b.Authors?.Contains(txtAuthor.Text, StringComparison.OrdinalIgnoreCase) == true)];
                }

                // Фільтруємо за видавництвом
                if (!string.IsNullOrWhiteSpace(txtPublisher.Text))
                {
                    results = [.. results.Where(b => b.Publisher?.Contains(txtPublisher.Text, StringComparison.OrdinalIgnoreCase) == true)];
                }

                // Фільтруємо за роком
                if (numYearFrom.Value > 0)
                {
                    results = [.. results.Where(b => b.Year >= (int)numYearFrom.Value)];
                }
                if (numYearTo.Value > 0)
                {
                    results = [.. results.Where(b => b.Year <= (int)numYearTo.Value)];
                }

                // Фільтруємо за розділом
                if (cmbSection.SelectedIndex > 0)
                {
                    var sectionName = cmbSection.SelectedItem?.ToString();
                    if (sectionName != null)
                    {
                         results = [.. results.Where(b => _library.Sections
                            .Any(s => s.Name == sectionName && s.Books.Contains(b)))];
                    }
                }

                // Фільтруємо за статусом
                if (cmbStatus.SelectedIndex > 0)
                {
                    if (cmbStatus.SelectedItem is BookStatus status)
                    {
                        results = [.. results.Where(b => b.Status == status)];
                    }
                }

                // Фільтруємо за оцінкою
                if (cmbRating.SelectedIndex > 0)
                {
                    if (cmbRating.SelectedItem is int rating)
                    {
                        results = [.. results.Where(b => b.Rating != null && b.Rating.Score == rating)];
                    }
                }

                // Оновлюємо результати
                SearchResults = results;
                var bookViews = SearchResults.Select(book => new BookView
                {
                    Section = _library.Sections.FirstOrDefault(s => s.Books.Contains(book))?.Name ?? "",
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

                dgvResults.DataSource = null;
                dgvResults.DataSource = bookViews;

                // Встановлюємо DialogResult.OK, щоб форма повернула результати
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при пошуку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        // Додаємо клас BookView для відображення у DataGridView
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
    }
} 