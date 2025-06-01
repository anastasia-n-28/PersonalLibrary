using System;
using System.Windows.Forms;
using PersonalLibrary.Models;
using System.Linq;

namespace PersonalLibrary.Forms
{
    public partial class EditBookForm : Form
    {
        private readonly Book _book;
        private readonly Library _library;

        public EditBookForm(Book book, Library library)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
            _library = library ?? throw new ArgumentNullException(nameof(library));

            InitializeComponent();
            LoadBookData();
        }

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
            if (cmbSection.Items.Count > 0 && cmbSection.SelectedItem == null) cmbSection.SelectedIndex = 0;

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

        private void BtnAddSection_Click(object? sender, EventArgs e)
        {
            using var form = new ManageSectionsForm(_library);
            form.ShowDialog();
            cmbSection!.Items.Clear();
            cmbSection.Items.AddRange([.. _library.Sections.Select(s => s.Name)]);
            if (cmbSection.Items.Count > 0) cmbSection.SelectedIndex = 0;
        }

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

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
} 