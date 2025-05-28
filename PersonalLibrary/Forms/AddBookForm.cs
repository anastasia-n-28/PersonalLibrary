using System;
using System.Windows.Forms;
using PersonalLibrary.Models;

namespace PersonalLibrary.Forms
{
    public partial class AddBookForm : Form
    {
        public Book Book { get; private set; }

        public AddBookForm()
        {
            InitializeComponent();
            cmbStatus.DataSource = Enum.GetValues(typeof(BookStatus));
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
                Rating = new UserRating { Score = 0, Review = "" }
            };
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
} 