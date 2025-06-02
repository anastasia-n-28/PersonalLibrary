using System;
using System.Windows.Forms;
using PersonalLibrary.Models;
using System.Linq;
using Microsoft.VisualBasic;

namespace PersonalLibrary.Forms
{
    public partial class AddBookForm : Form
    {
        private readonly Library _library;
        public Book? Book { get; private set; }

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

        private void AddBookForm_Shown(object? sender, EventArgs e)
        {
            numRating.BringToFront();
            numRating.Select();
            numRating.Update();
            numRating.Invalidate();
            numRating.PerformLayout();

            NumRating_ValueChanged(numRating, EventArgs.Empty);
        }

        private void PopulateSectionsDropdown()
        {
            cmbSection.Items.Clear();
            cmbSection.Items.Add("Оберіть розділ");
            cmbSection.Items.AddRange([.. _library.Sections.Select(s => s.Name)]);
            if (cmbSection.Items.Count > 0) cmbSection.SelectedIndex = 0;
        }

        private void BtnAddSection_Click(object? sender, EventArgs e)
        {
            using var form = new ManageSectionsForm(_library);
            form.ShowDialog();
            cmbSection.Items.Clear();
            cmbSection.Items.Add("Оберіть розділ");
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

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void NumRating_ValueChanged(object? sender, EventArgs e)
        {
            txtReview.Enabled = numRating.Value > 0;
            numRating.Text = numRating.Value.ToString();
            numRating.Refresh();
        }
    }
} 