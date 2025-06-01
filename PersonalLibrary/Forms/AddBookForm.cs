using System;
using System.Windows.Forms;
using PersonalLibrary.Models;
using System.Linq;
using Microsoft.VisualBasic; // Потрібно для Interaction.InputBox

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
            PopulateRatingDropdown();

            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            btnAddSection.Click += BtnAddSection_Click;
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void PopulateSectionsDropdown()
        {
            cmbSection.Items.Clear();
            cmbSection.Items.Add("Оберіть розділ");
            cmbSection.Items.AddRange([.. _library.Sections.Select(s => s.Name)]);
            if (cmbSection.Items.Count > 0) cmbSection.SelectedIndex = 0;
        }

        private void PopulateRatingDropdown()
        {
            cmbRating.Items.Clear();
            cmbRating.Items.Add("-");
            for (int i = 1; i <= 5; i++)
            {
                cmbRating.Items.Add(i.ToString());
            }
            cmbRating.SelectedIndex = -1;
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
                Rating = cmbRating.SelectedIndex != -1 ? new UserRating { Score = cmbRating.SelectedIndex + 1, Review = txtReview?.Text ?? string.Empty } : null
            };

            var selectedSectionName = cmbSection.SelectedItem?.ToString();
            var targetSection = _library.Sections.FirstOrDefault(s => s.Name == selectedSectionName);

            if (targetSection == null)
            {
                MessageBox.Show("Не вдалося визначити розділ.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                return;
            }

            targetSection.AddBook(Book);

            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void CmbRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReview.Enabled = cmbRating.SelectedIndex != -1;
        }
    }
} 