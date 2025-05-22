using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PersonalLibrary.Models;

namespace PersonalLibrary.Forms
{
    public partial class ManageSectionsForm : Form
    {
        private Library _library;
        private ListBox lstSections;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClose;

        public ManageSectionsForm(Library library)
        {
            InitializeComponent();
            _library = library;
            InitializeControls();
            LoadSections();
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Size = new System.Drawing.Size(800, 500);
        }

        private void InitializeControls()
        {
            this.Text = "Управління розділами";
            this.Size = new System.Drawing.Size(400, 300);

            lstSections = new ListBox
            {
                Dock = DockStyle.Fill,
                SelectionMode = SelectionMode.One
            };

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                FlowDirection = FlowDirection.RightToLeft
            };

            btnClose = new Button
            {
                Text = "Закрити",
                Height = 40,
                Width = 120
            };
            btnClose.Click += (s, e) => this.Close();

            btnDelete = new Button
            {
                Text = "Видалити",
                Height = 40,
                Width = 150
            };
            btnDelete.Click += BtnDelete_Click;

            btnEdit = new Button
            {
                Text = "Редагувати",
                Height = 40,
                Width = 150
            };
            btnEdit.Click += BtnEdit_Click;

            btnAdd = new Button
            {
                Text = "Додати",
                Height = 40,
                Width = 120
            };
            btnAdd.Click += BtnAdd_Click;

            buttonPanel.Controls.AddRange(new Control[] { btnClose, btnDelete, btnEdit, btnAdd });

            this.Controls.Add(lstSections);
            this.Controls.Add(buttonPanel);
        }

        private void LoadSections()
        {
            lstSections.Items.Clear();
            foreach (var section in _library.Sections)
            {
                lstSections.Items.Add(section.Name);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var inputForm = new InputDialog("Нова секція", "Введіть назву секції:"))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    var sectionName = inputForm.InputText;
                    if (!string.IsNullOrWhiteSpace(sectionName))
                    {
                        _library.AddSection(sectionName);
                        LoadSections();
                    }
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (lstSections.SelectedIndex == -1) return;

            var oldName = lstSections.SelectedItem.ToString();
            using (var inputForm = new InputDialog("Редагування секції", "Введіть нову назву секції:", oldName))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    var newName = inputForm.InputText;
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        _library.RenameSection(oldName, newName);
                        LoadSections();
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstSections.SelectedIndex == -1) return;

            var sectionName = lstSections.SelectedItem.ToString();
            var result = MessageBox.Show(
                $"Ви впевнені, що хочете видалити розділ '{sectionName}'?\nВсі книги в цьому розділі будуть видалені.",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _library.RemoveSection(sectionName);
                LoadSections();
            }
        }
    }

    public class InputDialog : Form
    {
        public string InputText { get; private set; }
        private TextBox txtInput;
        private Button btnOK;
        private Button btnCancel;

        public InputDialog(string title, string prompt, string defaultValue = "")
        {
            this.Text = title;
            this.Size = new System.Drawing.Size(600, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var label = new Label
            {
                Text = prompt,
                Dock = DockStyle.Top,
                Padding = new Padding(10)
            };

            txtInput = new TextBox
            {
                Text = defaultValue,
                Dock = DockStyle.Top,
                Margin = new Padding(10)
            };

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Height = 80
            };

            btnCancel = new Button
            {
                Text = "Скасувати",
                Height = 40,
                Width = 150,
                DialogResult = DialogResult.Cancel
            };

            btnOK = new Button
            {
                Text = "OK",
                Height = 40,
                Width = 80,
                DialogResult = DialogResult.OK
            };
            btnOK.Click += (s, e) =>
            {
                InputText = txtInput.Text;
            };

            buttonPanel.Controls.AddRange(new Control[] { btnCancel, btnOK });

            this.Controls.AddRange(new Control[] { label, txtInput, buttonPanel });
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
    }
} 