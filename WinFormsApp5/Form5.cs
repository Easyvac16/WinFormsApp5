using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp5
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            string searchWord = textBox1.Text;
            string directoryPath = textBox2.Text;

            if (string.IsNullOrEmpty(searchWord) || string.IsNullOrEmpty(directoryPath))
            {
                MessageBox.Show("Будь ласка, введіть слово та шлях до директорії.");
                return;
            }


            await SearchFilesAsync(directoryPath, searchWord);
        }
        private async Task SearchFilesAsync(string directoryPath, string searchWord)
        {
            try
            {
                var files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    string content = await ReadFileAsync(file);
                    int count = CountOccurrences(content, searchWord);

                    if (count > 0)
                    {
                        listBox1.Items.Add($"Назва файлу: {Path.GetFileName(file)}");
                        listBox1.Items.Add($"Шлях до файлу: {file}");
                        listBox1.Items.Add($"Кількість входжень слова: {count}");
                        listBox1.Items.Add("");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }
        private async Task<string> ReadFileAsync(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private int CountOccurrences(string content, string searchWord)
        {
            return content.Split(new[] { ' ', '\r', '\n', '\t', '.', ',' }, StringSplitOptions.RemoveEmptyEntries)
                          .Count(word => word.Equals(searchWord, StringComparison.OrdinalIgnoreCase));
        }

    }
}
