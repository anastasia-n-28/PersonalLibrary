using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PersonalLibrary.Models;

namespace PersonalLibrary.Forms
{
    /// <summary>
    /// Представляє форму для управління розділами бібліотеки.
    /// </summary>
    public partial class ManageSectionsForm : Form
    {
        private readonly Library _library;
        private ListBox? lstSections;
        private Button? btnAdd;
        private Button? btnEdit;
        private Button? btnDelete;
        private Button? btnClose;

        /// <summary>
        /// Ініціалізує новий екземпляр класу ManageSectionsForm.
        /// </summary>
        /// <param name="library">Екземпляр бібліотеки для управління розділами.</param>
        public ManageSectionsForm(Library library)
        {
            InitializeComponent();
            _library = library;
            InitializeControls();
            LoadSections();
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Size = new System.Drawing.Size(800, 500);
            this.CancelButton = btnClose; // Esc
            this.KeyPreview = true;
            this.KeyPress += ManageSectionsForm_KeyPress; // Enter
        }

        /// <summary>
        /// Ініціалізує контролі на формі.
        /// </summary>
        private void InitializeControls()
        {
            this.Text = "Управління розділами";
            this.Size = new System.Drawing.Size(400, 300);

            lstSections = new ListBox
            {
                Dock = DockStyle.Fill,
                SelectionMode = SelectionMode.One,
                TabIndex = 0
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
                Width = 120,
                TabIndex = 4
            };
            btnClose.Click += (s, e) => this.Close();

            btnDelete = new Button
            {
                Text = "Видалити",
                Height = 40,
                Width = 150,
                TabIndex = 3
            };
            btnDelete.Click += BtnDelete_Click;

            btnEdit = new Button
            {
                Text = "Редагувати",
                Height = 40,
                Width = 150,
                TabIndex = 2
            };
            btnEdit.Click += BtnEdit_Click;

            btnAdd = new Button
            {
                Text = "Додати",
                Height = 40,
                Width = 120,
                TabIndex = 1
            };
            btnAdd.Click += BtnAdd_Click;

            buttonPanel.Controls.AddRange([btnClose, btnDelete, btnEdit, btnAdd]);

            this.Controls.Add(lstSections);
            this.Controls.Add(buttonPanel);
        }

        /// <summary>
        /// Завантажує розділи бібліотеки в ListBox.
        /// </summary>
        /// <exception cref="InvalidOperationException">Викидається попередження, якщо список розділів не ініціалізований.</exception>
        private void LoadSections()
        {
            if (lstSections == null)
                throw new InvalidOperationException("lstSections is not initialized.");

            lstSections.Items.Clear();
            foreach (var section in _library.Sections)
            {
                lstSections.Items.Add(section.Name);
            }
        }

        /// <summary>
        /// Обробляє подію Click кнопки Додати.
        /// Відкриває діалогове вікно для додавання нового розділу.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            using var inputForm = new InputDialog("Нова секція", "Введіть назву секції:");
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

        /// <summary>
        /// Обробляє подію Click кнопки Редагувати.
        /// Відкриває діалогове вікно для перейменування вибраного розділу.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (lstSections == null || lstSections.SelectedIndex == -1) return;

            var oldName = lstSections.SelectedItem?.ToString();
            if (oldName == null) return;

            using var inputForm = new InputDialog("Редагування секції", "Введіть нову назву секції:", oldName);
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

        /// <summary>
        /// Обробляє подію Click кнопки Видалити.
        /// Видаляє вибраний розділ після підтвердження.
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (lstSections == null || lstSections.SelectedIndex == -1) return;

            var sectionName = lstSections.SelectedItem?.ToString();
            if (sectionName == null) return;

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

        /// <summary>
        /// Обробляє подію KeyPress для форми.
        /// Дозволяє додавання або редагування розділів за допомогою клавіші Enter. 
        /// </summary>
        /// <param name="sender">Джерело події.</param>
        /// <param name="e">Екземпляр, що містить дані події.</param>
        private void ManageSectionsForm_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                if (lstSections != null && lstSections.SelectedIndex != -1)
                {
                    BtnEdit_Click(lstSections, EventArgs.Empty);
                }
                else
                {
                    BtnAdd_Click(lstSections, EventArgs.Empty);
                }
            }
        }
    }

    /// <summary>
    /// A simple dialog form for getting text input from the user.
    /// </summary>
    public class InputDialog : Form
    {
        /// <summary>
        /// Gets the text entered by the user.
        /// </summary>
        public string InputText { get; private set; } = string.Empty;
        private readonly TextBox txtInput;
        private readonly Button btnOK;
        private readonly Button btnCancel;

        /// <summary>
        /// Initializes a new instance of the InputDialog class.
        /// </summary>
        /// <param name="title">The title of the dialog window.</param>
        /// <param name="prompt">The text prompt to display to the user.</param>
        /// <param name="defaultValue">The default text value for the input field.</param>
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
                Margin = new Padding(10),
                TabIndex = 0 
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
                DialogResult = DialogResult.Cancel,
                TabIndex = 2
            };

            btnOK = new Button
            {
                Text = "OK",
                Height = 40,
                Width = 80,
                DialogResult = DialogResult.OK,
                TabIndex = 1
            };
            btnOK.Click += (s, e) =>
            {
                InputText = txtInput.Text;
            };

            buttonPanel.Controls.AddRange([btnCancel, btnOK]);

            this.Controls.AddRange([label, txtInput, buttonPanel]);
            this.AcceptButton = btnOK; // Enter
            this.CancelButton = btnCancel; // Esc
        }
    }
}