using System;
using System.Windows.Forms;
using PersonalLibrary.Models;
using System.Linq;

namespace PersonalLibrary.Forms
{
    public partial class EditBookForm : Form
    {
        private Book _book;
        private Library _library;

        public EditBookForm(Book book, Library library)
        {
            InitializeComponent();
            _book = book;
            _library = library;
            txtTitle.Text = book.Title;
            txtAuthors.Text = book.Authors;
            txtPublisher.Text = book.Publisher;
            txtYear.Text = book.Year.ToString();
            txtISBN.Text = book.ISBN;
            txtOrigin.Text = book.Origin;
            cmbStatus.DataSource = Enum.GetValues(typeof(BookStatus));
            cmbStatus.SelectedItem = book.Status;
            cmbSection.Items.AddRange(_library.Sections.Select(s => s.Name).ToArray());
            int sectionIndex = _library.Sections.FindIndex(s => s.Books.Contains(book));
            cmbSection.SelectedIndex = sectionIndex >= 0 ? sectionIndex : 0;
            numRating.Minimum = 1;
            numRating.Maximum = 5;
            numRating.Value = book.Rating?.Score ?? 5;
            txtReview.Text = book.Rating?.Review ?? "";
            btnAddSection.Click += BtnAddSection_Click;
        }

        private void BtnAddSection_Click(object sender, EventArgs e)
        {
            using (var form = new ManageSectionsForm(_library))
            {
                form.ShowDialog();
                cmbSection.Items.Clear();
                cmbSection.Items.AddRange(_library.Sections.Select(s => s.Name).ToArray());
                if (cmbSection.Items.Count > 0) cmbSection.SelectedIndex = 0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) error += "Title is required.\n";
            if (string.IsNullOrWhiteSpace(txtAuthors.Text)) error += "Authors are required.\n";
            if (string.IsNullOrWhiteSpace(txtPublisher.Text)) error += "Publisher is required.\n";
            if (string.IsNullOrWhiteSpace(txtISBN.Text)) error += "ISBN is required.\n";
            if (string.IsNullOrWhiteSpace(txtOrigin.Text)) error += "Origin is required.\n";
            if (!int.TryParse(txtYear.Text, out int year) || year <= 0) error += "Year must be a positive number.\n";
            if (cmbSection.SelectedIndex < 0)
            {
                MessageBox.Show("Оберіть розділ!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                return;
            }
            if (numRating.Value < 1 || numRating.Value > 5)
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

            _book.Title = txtTitle.Text;
            _book.Authors = txtAuthors.Text;
            _book.Publisher = txtPublisher.Text;
            _book.Year = year;
            _book.ISBN = txtISBN.Text;
            _book.Origin = txtOrigin.Text;
            _book.Status = (BookStatus)cmbStatus.SelectedItem;
            _book.Rating = new UserRating { Score = (int)numRating.Value, Review = txtReview.Text };
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
} 