using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Добавляем пространство имен для работы с графиками
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

namespace HanoiTowerApp
{
    public partial class Form1 : Form
    {
        private int numberOfDisks = 0; // Количество дисков
        private Stack<int>[] rods; // Стержни
        private int movesMade = 0;
        private int movingDisk = -1; // Переменная для отслеживания перемещаемого диска
        private int movingDiskX; // X позиция перемещаемого диска
        private int movingDiskY; // Y позиция перемещаемого диска
        private Button buttonStop; // Кнопка остановки
        private CancellationTokenSource cancellationTokenSource; // Токен отмены
        private Chart chart; // Диаграмма для отображения времени

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            // Создание графика
            chart = new Chart();
            chart.Location = new Point(700, 50);
            chart.Size = new Size(350, 200);
            chart.ChartAreas.Add(new ChartArea("MainArea"));
            chart.Series.Add(new Series("TimeSeries")
            {
                ChartType = SeriesChartType.Line
            });
            chart.ChartAreas["MainArea"].AxisX.Title = "Количество дисков";
            chart.ChartAreas["MainArea"].AxisY.Title = "Время (мс)";
            chart.Series["TimeSeries"].BorderWidth = 2;
            chart.Series["TimeSeries"].Color = Color.Gray;
            this.Controls.Add(chart);
            

            buttonStop = new Button();
            buttonStop.Text = "Остановить";
            buttonStop.Location = new Point(253, 500);
            buttonStop.Click += ButtonStop_Click;
            this.Controls.Add(buttonStop);

            // Инициализация стержней
            rods = new Stack<int>[3];
            for (int i = 0; i < 3; i++)
            {
                rods[i] = new Stack<int>();
            }

            // Заполнение первого стержня дисками
            for (int i = numberOfDisks; i > 0; i--)
            {
                rods[0].Push(i);
            }

            // Настройка формы
            this.Paint += new PaintEventHandler(Form1_Paint);

            textBox1.Text = "количество дисков";
            textBox1.ForeColor = Color.Gray;

            textBox1.GotFocus += RemoveText;
            textBox1.LostFocus += AddText;
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawRods(e.Graphics);
            if (movingDisk >= 0)
            {
                e.Graphics.FillRectangle(Brushes.LightSlateGray, movingDiskX, movingDiskY, movingDisk * 20, 12);
            }
        }

        private void DrawRods(Graphics graphics)
        {
            // Определение позиций стержней
            int rodWidth = 10;
            int rodHeight = 150;
            int baseHeight = 300;
            int spacing = 200;

            // Отрисовка стержней
            for (int i = 0; i < rods.Length; i++)
            {
                int x = i * spacing + 100;
                graphics.FillRectangle(Brushes.Black, x - rodWidth / 2, baseHeight - rodHeight, rodWidth, rodHeight);

                int diskOffset = 0;
                foreach (var disk in rods[i])
                {
                    graphics.FillRectangle(Brushes.Gray, x - disk * 10, baseHeight - rodHeight + diskOffset - 10, disk * 20, 12);
                    diskOffset += 14;
                }
            }
        }

        private async Task MoveDiskWithAnimation(int fromRod, int toRod, CancellationToken cancellationToken)
        {
            if (rods[fromRod].Count > 0)
            {
                int disk = rods[fromRod].Pop();
                int targetX = (toRod * 200 + 100) - disk * 10;
                int fromX = (fromRod * 200 + 100) - disk * 10;
                movingDiskX = fromX;
                int y = 130;
                movingDiskY = y;
                movingDisk = disk;

                for (int i = 0; i < 20; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    y -= 5; // Поднимаем диск
                    movingDiskY = y;
                    this.Invalidate(); // Перерисовываем форму
                    await Task.Delay(30);
                }
                movingDiskX = targetX; // Устанавливаем конечную позицию по X
                for (int i = 0; i < 20; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    y += 5; // Опускаем диск
                    movingDiskY = y;
                    this.Invalidate(); // Перерисовываем форму
                    await Task.Delay(30);
                }

                rods[toRod].Push(disk);
                movesMade++;
                movingDisk = -1;
                this.Invalidate();

                await Task.Delay(100); // Задержка для предотвращения слишком быстрого счета движений
            }
        }

        private async Task SolveHanoi(int n, int fromRod, int toRod, int tempRod, CancellationToken cancellationToken)
        {
            if (n > 0)
            {
                await SolveHanoi(n - 1, fromRod, tempRod, toRod, cancellationToken);
                await MoveDiskWithAnimation(fromRod, toRod, cancellationToken);
                await SolveHanoi(n - 1, tempRod, toRod, fromRod, cancellationToken);
            }
        }

        private void PoinsAdd(int cntDisks)
        {
            for (int i = 1; i < cntDisks; i++)
            {
                chart.Series["TimeSeries"].Points.AddXY(i,  (Math.Pow(i, 2) - 1)*1000);
            }
        }

        private async void buttonMove_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int newNumberOfDisks) && newNumberOfDisks > 0)
            {
                numberOfDisks = newNumberOfDisks;
                chart.Series["TimeSeries"].Points.Clear();
                PoinsAdd(numberOfDisks);

                rods = new Stack<int>[3];
                for (int i = 0; i < 3; i++)
                {
                    rods[i] = new Stack<int>();
                }

                for (int i = numberOfDisks; i > 0; i--)
                {
                    rods[0].Push(i);
                }

                movesMade = 0;
                cancellationTokenSource = new CancellationTokenSource();
                await SolveHanoi(numberOfDisks, 0, 2, 1, cancellationTokenSource.Token);
                MessageBox.Show($"Сделано движений: {movesMade}");
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите натуральное число дисков.");
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }

        private void RemoveText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "количество дисков")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void AddText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "количество дисков";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}