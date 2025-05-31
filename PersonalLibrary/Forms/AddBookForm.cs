using System;
using System.Windows.Forms;
using PersonalLibrary.Models;
using System.Linq;

namespace PersonalLibrary.Forms
{
    public partial class AddBookForm : Form
    {
        private Library _library;
        public Book Book { get; private set; }

        public AddBookForm(Library library)
        {
            InitializeComponent();
            _library = library;
            cmbStatus.DataSource = Enum.GetValues(typeof(BookStatus));
            cmbSection.Items.AddRange(_library.Sections.Select(s => s.Name).ToArray());
            if (cmbSection.Items.Count > 0) cmbSection.SelectedIndex = 0;
            numRating.Minimum = 1;
            numRating.Maximum = 5;
            numRating.Value = 5;
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
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) error += "Назва обов'язкова.\n";
            if (string.IsNullOrWhiteSpace(txtAuthors.Text)) error += "Автор(и) обов'язкові.\n";
            if (string.IsNullOrWhiteSpace(txtPublisher.Text)) error += "Видавництво обов'язкове.\n";
            if (string.IsNullOrWhiteSpace(txtISBN.Text)) error += "ISBN обов'язкове.\n";
            if (string.IsNullOrWhiteSpace(txtOrigin.Text)) error += "Походження обов'язкове.\n";
            if (!int.TryParse(txtYear.Text, out int year) || year <= 0) error += "Рік - позитивне число\n";
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

            Book = new Book
            {
                Title = txtTitle.Text,
                Authors = txtAuthors.Text,
                Publisher = txtPublisher.Text,
                Year = year,
                ISBN = txtISBN.Text,
                Origin = txtOrigin.Text,
                Status = (BookStatus)cmbStatus.SelectedItem,
                Rating = new UserRating { Score = (int)numRating.Value, Review = txtReview.Text }
            };
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
} 