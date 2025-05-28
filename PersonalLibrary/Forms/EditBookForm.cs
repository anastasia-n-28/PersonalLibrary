using System;
using System.Windows.Forms;
using PersonalLibrary.Models;

namespace PersonalLibrary.Forms
{
    public partial class EditBookForm : Form
    {
        private Book _book;

        public EditBookForm(Book book)
        {
            InitializeComponent();
            _book = book;
            txtTitle.Text = book.Title;
            txtAuthors.Text = book.Authors;
            txtPublisher.Text = book.Publisher;
            txtYear.Text = book.Year.ToString();
            txtISBN.Text = book.ISBN;
            txtOrigin.Text = book.Origin;
            cmbStatus.DataSource = Enum.GetValues(typeof(BookStatus));
            cmbStatus.SelectedItem = book.Status;
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

            _book.Title = txtTitle.Text;
            _book.Authors = txtAuthors.Text;
            _book.Publisher = txtPublisher.Text;
            _book.Year = year;
            _book.ISBN = txtISBN.Text;
            _book.Origin = txtOrigin.Text;
            _book.Status = (BookStatus)cmbStatus.SelectedItem;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
} 