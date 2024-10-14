using System;
using System.Drawing;
using System.Windows.Forms;

namespace HanoiTowerApp.Properties
{
    public partial class Fractal : Form
    {
        private int currentDepth = 0; // Текущий уровень рекурсии
        private int maxDepth = 0; // Максимальный уровень рекурсии
        private Timer timer;
        
        public Fractal()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Fractal_Paint);
            CreateBuildButton();
            CreateStopButton(); // Создание кнопки остановки
            InitializeTimer();
        }
        
        private void CreateBuildButton()
        {
            Button buildButton = new Button();
            buildButton.Text = "Построить фрактал";
            buildButton.Click += new EventHandler(BuildButton_Click);
            Controls.Add(buildButton);
        }
        
        // Новый метод для создания кнопки остановки
        private void CreateStopButton()
        {
            Button stopButton = new Button();
            stopButton.Text = "Остановить";
            stopButton.Click += new EventHandler(StopButton_Click);
            stopButton.Location = new Point(0, 75);
            Controls.Add(stopButton);
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 100; // Интервал обновления в миллисекундах
            timer.Tick += new EventHandler(Timer_Tick);
        }
        
        private void BuildButton_Click(object sender, EventArgs e)
        {
            currentDepth = 0; // Сброс уровня рекурсии
            
            // Проверяем, что введенное значение является числом
            if (int.TryParse(textBox1.Text, out int newMaxDepth) && newMaxDepth > 0)
            {
                maxDepth = newMaxDepth;
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите натуральное максимальное число рекурсии."); // Сообщение об ошибке
            }
            
            this.Invalidate(); // Перерисовка формы
            timer.Start(); // Запуск таймера
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            currentDepth = 0;
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentDepth < maxDepth)
            {
                currentDepth++;
                this.Invalidate(); // Перерисовка формы для отрисовки фрактала на следующем уровне
            }
            else
            {
                timer.Stop(); // Остановка таймера, если достигнут максимум
            }
        }
        
        private void Fractal_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Определяем начальные точки треугольника
            PointF p1 = new PointF(ClientSize.Width / 2, 10);
            PointF p2 = new PointF(10, ClientSize.Height - 10);
            PointF p3 = new PointF(ClientSize.Width - 10, ClientSize.Height - 10);

            // Рисуем треугольник Серпинского
            DrawSierpinskiTriangle(g, p1, p2, p3, currentDepth);
        }
        
        private void DrawSierpinskiTriangle(Graphics g, PointF p1, PointF p2, PointF p3, int depth)
        {
            if (depth == 0)
            {
                PointF[] trianglePoints = { p1, p2, p3 };
                g.FillPolygon(Brushes.Black, trianglePoints);
            }
            else
            {
                // Находим середины сторон
                PointF mid1 = Midpoint(p1, p2);
                PointF mid2 = Midpoint(p2, p3);
                PointF mid3 = Midpoint(p1, p3);

                // Рекурсивно рисуем три меньших треугольника
                DrawSierpinskiTriangle(g, p1, mid1, mid3, depth - 1);
                DrawSierpinskiTriangle(g, mid1, p2, mid2, depth - 1);
                DrawSierpinskiTriangle(g, mid3, mid2, p3, depth - 1);
            }
        }
        
        private PointF Midpoint(PointF p1, PointF p2)
        {
            return new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}