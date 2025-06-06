using System;
using System.Windows.Forms;
using PersonalLibrary.Models;
using System.Linq;
using System.Collections.Generic;

namespace PersonalLibrary.Forms
{
    /// <summary>
    /// Представляє форму для редагування існуючої книги в бібліотеці.
    /// </summary>
    public partial class EditBookForm : Form
    {
        private readonly Book _book;
        private readonly Library _library;
        private readonly List<LibrarySection> _sections;

        /// <summary>
        /// Ініціалізує новий екземпляр класу EditBookForm.
        /// </summary>
        /// <param name="book">Об'єкт книги для редагування.</param>
        /// <param name="library">Інстанс бібліотеки, що містить книгу.</param>
        /// <param name="sections">Список доступних розділів бібліотеки.</param>
        public EditBookForm(Book book, Library library, List<LibrarySection> sections)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
            _library = library ?? throw new ArgumentNullException(nameof(library));
            _sections = sections ?? throw new ArgumentNullException(nameof(sections));

            InitializeComponent();
            PopulateSectionsDropdown();
            LoadBookData();
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        /// <summary>
        /// Завантажує дані з об'єкта книги в контролі форми.
        /// </summary>
        private void LoadBookData()
        {
            txtTitle!.Text = _book.Title ?? "";
            txtAuthors!.Text = _book.Authors ?? "";
            txtPublisher!.Text = _book.Publisher ?? "";
            txtYear!.Text = _book.Year.ToString();
            txtISBN!.Text = _book.ISBN ?? "";
            txtOrigin!.Text = _book.Origin ?? "";

            cmbStatus!.DataSource = Enum.GetValues(typeof(BookStatus));
            cmbStatus.SelectedItem = _book.Status;

            cmbSection!.Items.AddRange([.. _library.Sections.Select(s => s.Name)]);
            var currentSection = _library.Sections.FirstOrDefault(s => s.Books.Contains(_book));
            if (currentSection != null)
            {
                cmbSection.SelectedItem = currentSection.Name;
            }
            else if (cmbSection.Items.Count > 0)
            {
                cmbSection.SelectedIndex = 0;
            }

            if (_book.Rating != null)
            {
                numRating!.Value = _book.Rating.Score;
                txtReview!.Text = _book.Rating.Review ?? "";
            }
            else
            {
                numRating!.Value = numRating!.Minimum;
                txtReview!.Text = "";
            }

            btnOK!.Click += BtnOK_Click;
            btnCancel!.Click += BtnCancel_Click;
            btnAddSection!.Click += BtnAddSection_Click;
        }

        /// <summary>
        /// Заповнює випадаючий список розділів доступними розділами бібліотеки.
        /// </summary>
        private void PopulateSectionsDropdown()
        {
            cmbSection.Items.Clear();
            foreach (var section in _sections)
            {
                cmbSection.Items.Add(section.Name);
            }
        }

        /// <summary>
        /// Обробляє подію Click кнопки "Додати розділ".
        /// Відкриває форму для управління розділами і оновлює випадаючий список.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void BtnAddSection_Click(object? sender, EventArgs e)
        {
            using var form = new ManageSectionsForm(_library);
            form.ShowDialog();
            cmbSection!.Items.Clear();
            cmbSection.Items.AddRange([.. _library.Sections.Select(s => s.Name)]);
            if (cmbSection.Items.Count > 0) cmbSection.SelectedIndex = 0;
        }

        /// <summary>
        /// Обробляє подію Click кнопки OK.
        /// Перевіряє валідність введених даних і оновлює об'єкт книги.
        /// Встановлює DialogResult на OK, якщо успішно, інакше Cancel.
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
            if (cmbSection?.SelectedIndex < 0)
            {
                MessageBox.Show("Оберіть розділ!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                return;
            }
            if (numRating?.Value < 1 || numRating?.Value > 5)
            {
                MessageBox.Show("Оцінка має бути від 1 до 5!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                return;
            }

            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                return;
            }

            BookStatus selectedStatus = cmbStatus?.SelectedItem is BookStatus status ? status : BookStatus.Available;
            int ratingScore = (int)(numRating?.Value ?? 0);

            _book.Title = txtTitle?.Text;
            _book.Authors = txtAuthors?.Text;
            _book.Publisher = txtPublisher?.Text;
            _book.Year = year;
            _book.ISBN = txtISBN?.Text;
            _book.Origin = txtOrigin?.Text;
            _book.Status = selectedStatus;
            if (_book.Rating == null)
            {
                _book.Rating = new UserRating { Score = ratingScore, Review = txtReview?.Text ?? "" };
            }
            else
            {
                _book.Rating.Score = ratingScore;
                _book.Rating.Review = txtReview?.Text ?? "";
            }

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Обробляє подію Click кнопки Відміна.
        /// Встановлює DialogResult на Cancel і закриває форму.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
} 