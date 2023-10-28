using System.Data;
using ProgressBar = System.Windows.Forms.ProgressBar;

namespace WinFormsApp5
{
    public partial class Form2 : Form
    {
        private ProgressBar[] horseBars;
        private CancellationTokenSource cancellationTokenSource;

        public Form2()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Add("HorseName", "Кінь");
            dataGridView1.Columns.Add("Position", "Позиція");

            Controls.Add(dataGridView1);
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();

            button1.Enabled = false;
            button2.Enabled = true;
            cancellationTokenSource = new CancellationTokenSource();
            horseBars = new ProgressBar[] { progressBar1, progressBar2, progressBar3, progressBar4, progressBar5 };
            Task[] horseTasks = new Task[5];
            for (int i = 0; i < 5; i++)
            {
                int horseNumber = i + 1;
                horseTasks[i] = RaceHorse(horseNumber, cancellationTokenSource.Token);
            }
            await Task.WhenAny(horseTasks);

            DisplayResults();
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private async Task RaceHorse(int horseNumber, CancellationToken cancellationToken)
        {
            Random random = new Random();

            while (!cancellationToken.IsCancellationRequested && horseBars[horseNumber - 1].Value < 100)
            {
                int step = random.Next(1, 6);
                int newValue = horseBars[horseNumber - 1].Value + step;
                if (newValue <= horseBars[horseNumber - 1].Maximum)
                {
                    horseBars[horseNumber - 1].Invoke((Action)(() =>
                    {
                        horseBars[horseNumber - 1].Value = newValue;
                    }));
                }
                await Task.Delay(100);
            }
        }

        private void DisplayResults()
        {

            dataGridView1.Rows.Clear();

            var horsePositions = new List<Tuple<int, int>>();

            for (int i = 0; i < 5; i++)
            {
                int horseNumber = i + 1;
                int currentPosition = horseBars[i].Value;
                horsePositions.Add(new Tuple<int, int>(horseNumber, currentPosition));
            }

            var sortedPositions = horsePositions.OrderBy(t => t.Item2).ToList();

            foreach (var position in sortedPositions)
            {
                string horseName = $"Кінь {position.Item1}";
                string positionString = (sortedPositions.IndexOf(position) + 1).ToString();

                dataGridView1.Rows.Add(horseName, positionString);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
            button2.Enabled = false;
            button1.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                horseBars[i].Value = 0;
            }

            dataGridView1.Rows.Clear();
        }
    }

}
