using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PersonalLibrary.Models;

namespace PersonalLibrary.Forms
{
    public partial class AdvancedSearchForm : Form
    {
        private Library _library;
        private DataGridView dgvResults;
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private TextBox txtPublisher;
        private NumericUpDown numYearFrom;
        private NumericUpDown numYearTo;
        private ComboBox cmbSection;
        private ComboBox cmbStatus;
        private ComboBox cmbRating;
        private Button btnSearch;
        private Button btnClose;

        public List<Book> SearchResults { get; private set; }

        public AdvancedSearchForm(Library library)
        {
            if (library == null)
                throw new ArgumentNullException(nameof(library));

            InitializeComponent();
            _library = library;
            SearchResults = new List<Book>();
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
            txtTitle = new TextBox { Dock = DockStyle.Fill, Height = 40, Width = 120 };
            mainPanel.Controls.Add(txtTitle, 1, 0);

            // Автор
            mainPanel.Controls.Add(new Label { Text = "Автор:", Height = 40 }, 0, 1);
            txtAuthor = new TextBox { Dock = DockStyle.Fill, Height = 40 };
            mainPanel.Controls.Add(txtAuthor, 1, 1);

            // Видавництво
            mainPanel.Controls.Add(new Label { Text = "Видавництво:", Height = 40, Width = 180 }, 0, 2);
            txtPublisher = new TextBox { Dock = DockStyle.Fill, Height = 40, Width = 180 };
            mainPanel.Controls.Add(txtPublisher, 1, 2);

            // Рік видання
            var yearPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, Height = 40 };
            mainPanel.Controls.Add(new Label { Text = "Рік видання:", Height = 40 }, 0, 3);
            numYearFrom = new NumericUpDown { Minimum = 0, Maximum = DateTime.Now.Year, Width = 100, Height = 40 };
            numYearTo = new NumericUpDown { Minimum = 0, Maximum = DateTime.Now.Year, Width = 100, Height = 40 };
            yearPanel.Controls.AddRange(new Control[] { numYearFrom, new Label { Text = " - ", Height = 40 }, numYearTo });
            mainPanel.Controls.Add(yearPanel, 1, 3);

            // Розділ
            mainPanel.Controls.Add(new Label { Text = "Розділ:", Height = 40 }, 0, 4);
            cmbSection = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Height = 40 };
            cmbSection.Items.Add("Всі розділи");
            foreach (var section in _library.Sections)
            {
                cmbSection.Items.Add(section.Name);
            }
            cmbSection.SelectedIndex = 0;
            mainPanel.Controls.Add(cmbSection, 1, 4);

            // Статус
            mainPanel.Controls.Add(new Label { Text = "Статус:", Height = 40 }, 0, 5);
            cmbStatus = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Height = 40 };
            cmbStatus.Items.Add("Будь-який");
            cmbStatus.Items.Add("Наявна");
            cmbStatus.Items.Add("Відсутня");
            cmbStatus.SelectedIndex = 0;
            mainPanel.Controls.Add(cmbStatus, 1, 5);

            // Оцінка
            mainPanel.Controls.Add(new Label { Text = "Оцінка:", Height = 40 }, 0, 6);
            cmbRating = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Height = 40 };
            cmbRating.Items.Add("Будь-який");
            for (int i = 1; i <= 5; i++)
                cmbRating.Items.Add(new string('★', i));
            cmbRating.SelectedIndex = 0;
            mainPanel.Controls.Add(cmbRating, 1, 6);

            // Кнопки
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Height = 60
            };

            btnClose = new Button
            {
                Text = "Закрити",
                Width = 120,
                Height = 40
            };
            btnClose.Click += (s, e) => this.Close();

            btnSearch = new Button
            {
                Text = "Пошук",
                Width = 120,
                Height = 40
            };
            btnSearch.Click += BtnSearch_Click;

            buttonPanel.Controls.AddRange(new Control[] { btnClose, btnSearch });

            // Таблиця результатів
            dgvResults = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            var resultsPanel = new Panel { Dock = DockStyle.Fill };
            resultsPanel.Controls.Add(dgvResults);

            this.Controls.Add(mainPanel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(resultsPanel);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Отримуємо всі книги
                var results = _library.Sections.SelectMany(s => s.Books).ToList();

                // Фільтруємо за назвою
                if (!string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    results = results.Where(b => b.Title != null && 
                        b.Title.Contains(txtTitle.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Фільтруємо за автором
                if (!string.IsNullOrWhiteSpace(txtAuthor.Text))
                {
                    results = results.Where(b => b.Authors != null && 
                        b.Authors.Contains(txtAuthor.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Фільтруємо за видавництвом
                if (!string.IsNullOrWhiteSpace(txtPublisher.Text))
                {
                    results = results.Where(b => b.Publisher != null && 
                        b.Publisher.Contains(txtPublisher.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Фільтруємо за роком
                if (numYearFrom.Value > 0)
                {
                    results = results.Where(b => b.Year >= (int)numYearFrom.Value).ToList();
                }
                if (numYearTo.Value > 0)
                {
                    results = results.Where(b => b.Year <= (int)numYearTo.Value).ToList();
                }

                // Фільтруємо за розділом
                if (cmbSection.SelectedIndex > 0)
                {
                    var sectionName = cmbSection.SelectedItem.ToString();
                    results = results.Where(b => _library.Sections
                        .Any(s => s.Name == sectionName && s.Books.Contains(b))).ToList();
                }

                // Фільтруємо за статусом
                if (cmbStatus.SelectedIndex > 0)
                {
                    var status = cmbStatus.SelectedItem.ToString();
                    results = results.Where(b => b.Status != null && 
                        ((status == "Наявна" && b.Status.IsAvailable) || 
                         (status == "Відсутня" && !b.Status.IsAvailable))).ToList();
                }

                // Фільтруємо за оцінкою
                if (cmbRating.SelectedIndex > 0)
                {
                    var rating = cmbRating.SelectedIndex;
                    results = results.Where(b => b.Rating != null && b.Rating.Score == rating).ToList();
                }

                // Оновлюємо результати
                SearchResults = results;
                dgvResults.DataSource = null;
                dgvResults.DataSource = SearchResults;

                // Встановлюємо DialogResult.OK, щоб форма повернула результати
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при пошуку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 