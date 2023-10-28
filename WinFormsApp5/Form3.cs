using System.Numerics;

namespace WinFormsApp5
{
    public partial class Form3 : Form
    {
        bool genFibonacci = true;
        public Form3()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int limit;
            limit = int.Parse(textBox1.Text);
            if (limit < 0)
            {
                MessageBox.Show("Межа має бути невід'ємним числом.");
                return;
            }

            button1.Enabled = false;

            genFibonacci = true;

            await CalculateFibonacciAsync(limit);

            button1.Enabled = true;

        }
        private async Task CalculateFibonacciAsync(int limit)
        {

            await Task.Run(() =>
            {
                BigInteger a = 0;
                BigInteger b = 1;
                BigInteger result = 0;

                while (genFibonacci)
                {
                    UpdateResultText(result);

                    result = a + b;
                    a = b;
                    b = result;

                    Thread.Sleep(100);
                    if (result > limit)
                    {
                        genFibonacci = false;
                        break;
                    }
                }
            });
        }
        private void UpdateResultText(BigInteger result)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => label1.Text = "Fibonacci:" + result.ToString()));
            }
            else
            {
                label1.Text = "Fibonacci:" + result.ToString();
            }
        }
    }
}
