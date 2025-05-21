using PersonalLibrary.Models;
using PersonalLibrary.Forms;

namespace PersonalLibrary
{
    public partial class Form1 : Form
    {
        private Library _library;
        private const string DataFile = "library.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(DataFile))
            {
                _library = new Library();
                _library.DeserializeData(DataFile);
            }
            else
            {
                _library = TestDataGenerator.Generate();
            }
            //UpdateResults(_library.Sections.SelectMany(s => s.Books).ToList());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _library.SerializeData(DataFile);
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var title = txtTitle.Text;
            var author = txtAuthor.Text;
            var publisher = txtPublisher.Text;

            var results = _library.FindBooks(title, author, publisher);
            dgvBooks.DataSource = null;
            dgvBooks.DataSource = results;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new AddBookForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Додаємо книгу до секції (наприклад, перша секція)
                    var section = _library.Sections.FirstOrDefault();
                    if (section != null)
                    {
                        section.AddBook(form.Book);
                        RefreshBookList();
                    }
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Інструкція користувачу буде додана пізніше.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to edit.", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedBook = dgvBooks.SelectedRows[0].DataBoundItem as Book;
            if (selectedBook == null) return;
            using (var form = new EditBookForm(selectedBook))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Книга вже оновлена через посилання, просто оновлюємо таблицю
                    dgvBooks.Refresh();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to delete.", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show("Ви дійсно хочете видалити цю книгу?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            var selectedBook = dgvBooks.SelectedRows[0].DataBoundItem as Book;
            if (selectedBook == null) return;
            var section = _library.Sections.FirstOrDefault(s => s.Books.Contains(selectedBook));
            if (section != null)
            {
                _library.RemoveBook(section.Name, selectedBook);
                dgvBooks.DataSource = null;
                dgvBooks.DataSource = _library.Sections.SelectMany(s => s.Books).ToList();
            }
        }

        private void RefreshBookList()
        {
            dgvBooks.DataSource = null;
            dgvBooks.DataSource = _library.Sections.SelectMany(s => s.Books).ToList();
        }
    }
}
