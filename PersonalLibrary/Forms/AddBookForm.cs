using System;
using System.Windows.Forms;
using PersonalLibrary.Models;
using System.Linq;
using Microsoft.VisualBasic;

namespace PersonalLibrary.Forms
{
    /// <summary>
    /// Представляє форму для додавання нової книги до бібліотеки.
    /// </summary>
    public partial class AddBookForm : Form
    {
        private readonly Library _library;
        /// <summary>
        /// Отримує об'єкт Book, створений цією формою, якщо операція була успішною.
        /// </summary>
        public Book? Book { get; private set; }

        /// <summary>
        /// Ініціалізує новий екземпляр класу AddBookForm.
        /// </summary>
        /// <param name="library">Екземпляр бібліотеки для додавання книги.</param>
        public AddBookForm(Library library)
        {
            _library = library ?? throw new ArgumentNullException(nameof(library));

            InitializeComponent();
            cmbStatus.DataSource = Enum.GetValues(typeof(BookStatus));

            PopulateSectionsDropdown();

            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            btnAddSection.Click += BtnAddSection_Click;

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            this.Shown += AddBookForm_Shown;

            numRating.ValueChanged += NumRating_ValueChanged;
        }

        /// <summary>
        /// Заповнює випадаючий список розділів доступними розділами бібліотеки.
        /// </summary>
        private void PopulateSectionsDropdown()
        {
            cmbSection.Items.Clear();
            cmbSection.Items.Add("Оберіть розділ");
            cmbSection.Items.AddRange([.. _library.Sections.Select(s => s.Name)]);
            if (cmbSection.Items.Count > 0) cmbSection.SelectedIndex = 0;
        }

        /// <summary>
        /// Обробляє подію Click кнопки Додати розділ.
        /// Відкриває форму керування розділами для додавання нового розділу.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void BtnAddSection_Click(object? sender, EventArgs e)
        {
            using var form = new ManageSectionsForm(_library);
            form.ShowDialog();
            cmbSection.Items.Clear();
            cmbSection.Items.Add("Оберіть розділ");
            cmbSection.Items.AddRange([.. _library.Sections.Select(s => s.Name)]);
            if (cmbSection.Items.Count > 0) cmbSection.SelectedIndex = 0;
        }

        /// <summary>
        /// Обробляє подію Click кнопки OK.
        /// Перевіряє вхідні дані та створює новий об'єкт Book.
        /// Встановлює DialogResult на OK, якщо операція була успішною.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void BtnOK_Click(object? sender, EventArgs e)
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(txtTitle?.Text)) error += "Назва обов'язкова.\n";
            if (string.IsNullOrWhiteSpace(txtAuthors?.Text)) error += "Автор(и) обов'язкові.\n";
            if (string.IsNullOrWhiteSpace(txtPublisher?.Text)) error += "Видавництво обов'язкове.\n";
            if (string.IsNullOrWhiteSpace(txtISBN?.Text)) error += "ISBN обов'язкове.\n";
            if (string.IsNullOrWhiteSpace(txtOrigin?.Text)) error += "Походження обов'язкове.\n";
            if (!int.TryParse(txtYear?.Text, out int year) || year <= 0) error += "Рік - позитивне число\n";

            if (cmbSection.SelectedIndex <= 0)
            {
                MessageBox.Show("Оберіть розділ!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numRating.Value < 0 || numRating.Value > 5)
            {
                 MessageBox.Show("Оцінка має бути від 0 до 5!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
            }

            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BookStatus selectedStatus = cmbStatus?.SelectedItem is BookStatus status ? status : BookStatus.Available;

            Book = new Book
            {
                Title = txtTitle?.Text,
                Authors = txtAuthors?.Text,
                Publisher = txtPublisher?.Text,
                Year = year,
                ISBN = txtISBN?.Text,
                Origin = txtOrigin?.Text,
                Status = selectedStatus,
                Rating = numRating.Value > 0 ? new UserRating { Score = (int)numRating.Value, Review = txtReview?.Text ?? string.Empty } : null
            };

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Обробляє подію Click кнопки Скасувати.
        /// Встановлює DialogResult на Скасувати.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Обробляє подію ValueChanged контролу Rating NumericUpDown.
        /// Вмикає/вимикає текстове поле відгуку на основі значення оцінки.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void NumRating_ValueChanged(object? sender, EventArgs e)
        {
            txtReview.Enabled = numRating.Value > 0;
            numRating.Text = numRating.Value.ToString();
            numRating.Refresh();
        }

        /// <summary>
        /// Обробляє подію Shown форми.
        /// Відповідає за вигляд контролу Rating.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void AddBookForm_Shown(object? sender, EventArgs e)
        {
            numRating.BringToFront();
            numRating.Select();
            numRating.Update();
            numRating.Invalidate();
            numRating.PerformLayout();

            NumRating_ValueChanged(numRating, EventArgs.Empty);
        }
    }
} 