using PersonalLibrary.Models;

namespace PersonalLibrary
{
    public partial class Form1 : Form
    {
        private Library _library;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _library = TestDataGenerator.Generate();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var title = txtTitle.Text;
            var author = txtAuthor.Text;
            var publisher = txtPublisher.Text;

            var results = _library.FindBooks(title, author, publisher);
            lstResults.DataSource = results;
            lstResults.DisplayMember = "Title"; // або "GetInfo"
        }
    }
}
