using PersonalLibrary.Models;

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
            UpdateResults(_library.Sections.SelectMany(s => s.Books).ToList());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _library.SerializeData(DataFile);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var title = txtTitle.Text;
            var author = txtAuthor.Text;
            var publisher = txtPublisher.Text;

            var results = _library.FindBooks(title, author, publisher);
            UpdateResults(results);
        }

        private void UpdateResults(List<Book> books)
        {
            lstResults.DataSource = null;
            lstResults.DataSource = books;
            lstResults.DisplayMember = "Title"; // або "GetInfo" для повної інформації
        }
    }
}
