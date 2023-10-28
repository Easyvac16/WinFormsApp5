using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        private ProgressBar[] progressBars;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int numBars;
            if (!int.TryParse(numericUpDown1.Text, out numBars) || numBars <= 0)
            {
                MessageBox.Show("Please enter a valid number for bars.");
                return;
            }

            progressBars = new ProgressBar[numBars];

            tableLayoutPanel1.Controls.Clear();

            for (int i = 0; i < numBars; i++)
            {
                progressBars[i] = new ProgressBar();
                progressBars[i].Maximum = 100;
                tableLayoutPanel1.Controls.Add(progressBars[i]);
            }

            await StartProgress();
        }
        private async Task StartProgress()
        {
            Random random = new Random();
            bool allBarsMaxed = false;

            while (!allBarsMaxed)
            {
                allBarsMaxed = true;

                for (int i = 0; i < progressBars.Length; i++)
                {
                    if (progressBars[i].Value < progressBars[i].Maximum)
                    {
                        int value = random.Next(progressBars[i].Value, progressBars[i].Maximum + 1);
                        progressBars[i].Invoke((Action)(() =>
                        {
                            progressBars[i].Value = value;
                            progressBars[i].BackColor = GetRandomColor(random);
                        }));

                        allBarsMaxed = false;
                    }
                }

                await Task.Delay(200);
            }
        }
        private Color GetRandomColor(Random random)
        {
            Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            return color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 newForm = new Form4();
            newForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5();
            newForm.Show();
        }
    }
}
