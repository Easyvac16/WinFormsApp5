namespace WinFormsApp5
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string wordToSearch = textBox1.Text;
            string filePath = textBox2.Text;

            if (string.IsNullOrEmpty(wordToSearch) || string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Будь ласка, введіть слово і шлях до файлу.");
                return;
            }
            try
            {
                string fileContent = File.ReadAllText(filePath);
                int wordCount = await CountOccurrences(fileContent, wordToSearch);
                label3.Text = $"Кількість:{wordCount}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

        }
        private async Task<int> CountOccurrences(string text, string word)
        {
            int count = 0;
            int index = 0;

            while ((index = text.IndexOf(word, index)) != -1)
            {
                index += word.Length;
                count++;
            }

            return count;
        }
    }
}

