using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace HanoiTowerApp
{
    public partial class Form1 : Form
    {
        private int numberOfDisks = 4; // Количество дисков
        private Stack<int>[] rods; // Стержни
        private int movesMade = 0;
        private int movingDisk = -1; // Переменная для отслеживания перемещаемого диска
        private int movingDiskX; // X позиция перемещаемого диска
        private int movingDiskY; // Y позиция перемещаемого диска

        public Form1()
        {
            InitializeComponent();

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
        }
        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawRods(e.Graphics);
            if (movingDisk >= 0)
            {
                // Отрисовываем перемещаемый диск
                e.Graphics.FillRectangle(Brushes.Gray, movingDiskX, movingDiskY, movingDisk * 20, 12);
            }
        }

        private void DrawRods(Graphics graphics)
        {
            // Определение позиций стержней
            int rodWidth = 10;
            int rodHeight = 100;
            int baseHeight = 300;
            int spacing = 150;

            // Отрисовка стержней
            for (int i = 0; i < rods.Length; i++)
            {
                int x = i * spacing + 100;
                graphics.FillRectangle(Brushes.Black, x - rodWidth / 2, baseHeight - rodHeight, rodWidth, rodHeight);

                // Отрисовка дисков
                int diskOffset = 0;
                foreach (var disk in rods[i])
                {
                    graphics.FillRectangle(Brushes.Gray, x - disk * 10, baseHeight - rodHeight + diskOffset - 10, disk * 20, 12);
                    diskOffset += 20;
                }
            }
        }

        private async Task MoveDiskWithAnimation(int fromRod, int toRod)
        {
            // Перемещение диска с анимацией
            if (rods[fromRod].Count > 0)
            {
                int disk = rods[fromRod].Pop();
                int targetX = (toRod * 150 + 100) - disk * 10; // Получить целевую позицию X для диска
                int fromX = (fromRod * 150 + 100) - disk * 10; // Начальная позиция X для диска
                movingDiskX = fromX; // Установить начальную позицию перемещаемого диска
                int y = 180; // Начальная вертикальная позиция
                movingDiskY = y; // Запоминаем Y для перемещения

                // Устанавливаем перемещаемый диск для отрисовки
                movingDisk = disk;

                // Анимация перемещения диска
                for (int i = 0; i < 20; i++)
                {
                    y -= 5; // Поднимаем диск
                    movingDiskY = y;
                    this.Invalidate(); // Перерисовываем форму
                    await Task.Delay(30); // Задержка для анимации
                }

                movingDiskX = targetX; // Устанавливаем конечную позицию по X

                for (int i = 0; i < 20; i++)
                {
                    y += 5; // Опускаем диск
                    movingDiskY = y;
                    this.Invalidate(); // Перерисовываем форму
                    await Task.Delay(30); // Задержка для анимации
                }

                // Обновляем расположение диска на роде
                rods[toRod].Push(disk);
                movesMade++;
                movingDisk = -1; // Сбрасываем перемещаемый диск
                this.Invalidate(); // Окончательная перерисовка
            }
        }

        private async Task SolveHanoi(int n, int fromRod, int toRod, int tempRod)
        {
            if (n > 0)
            {
                await SolveHanoi(n - 1, fromRod, tempRod, toRod);
                await MoveDiskWithAnimation(fromRod, toRod);
                await SolveHanoi(n - 1, tempRod, toRod, fromRod);
            }
        }

        private async void buttonMove_Click(object sender, EventArgs e)
        {
            // Решение задачи при нажатии на кнопку
            movesMade = 0;
            await SolveHanoi(numberOfDisks, 0, 2, 1);
            MessageBox.Show($"Сделано движений: {movesMade}");
        }
    }
}