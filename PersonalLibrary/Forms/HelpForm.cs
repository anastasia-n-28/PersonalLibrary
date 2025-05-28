using System;
using System.IO;
using System.Windows.Forms;

namespace PersonalLibrary.Forms
{
    public class HelpForm : Form
    {
        private RichTextBox richTextBox;
        public HelpForm()
        {
            this.Text = "Довідка";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
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
        private void LoadHelpText()
        {
            string helpFile = "help.txt";
            if (File.Exists(helpFile))
            {
                richTextBox.Text = File.ReadAllText(helpFile);
            }
            else
            {
                richTextBox.Text = "Файл довідки help.txt не знайдено.\n\n1. Додайте книги...\n2. Використовуйте пошук...\n...";
            }
        }
    }
} 