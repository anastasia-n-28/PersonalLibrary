using System;
using System.IO;
using System.Windows.Forms;

namespace PersonalLibrary.Forms
{
    /// <summary>
    /// Представляє форму довідки для програми.
    /// </summary>
    public class HelpForm : Form
    {
        private readonly RichTextBox richTextBox;
        /// <summary>
        /// Ініціалізує новий екземпляр класу HelpForm.
        /// </summary>
        public HelpForm()
        {
            this.Text = "Довідка";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            if (Screen.PrimaryScreen != null)
            {
                Size = Screen.PrimaryScreen.WorkingArea.Size;
            }
            else
            {
                Size = new System.Drawing.Size(800, 600);
            }

            richTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new System.Drawing.Font("Segoe UI", 12),
                WordWrap = true,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };
            this.Controls.Add(richTextBox);
            LoadHelpText();
        }
        /// <summary>
        /// Завантажує текст довідки в RichTextBox.
        /// </summary>
        private void LoadHelpText()
        {
            richTextBox.Text = "Інструкція до використання програми:\n" +
                "\nЗверніться до розробника за детальнішою інформацією.";
        }
    }
}