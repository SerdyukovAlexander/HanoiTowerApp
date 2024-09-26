using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HanoiTowerApp
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private int numberOfDisks = 5; // Количество дисков
        private Stack<int>[] rods; // Стержни
        private int movesMade = 0;

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

        private void MoveDisk(int fromRod, int toRod)
        {
            // Перемещение диска
            if (rods[fromRod].Count > 0)
            {
                int disk = rods[fromRod].Pop();
                rods[toRod].Push(disk);
                movesMade++;
                this.Invalidate(); // Перерисовка формы
            }
        }

        private void SolveHanoi(int n, int fromRod, int toRod, int tempRod)
        {
            if (n > 0)
            {
                SolveHanoi(n - 1, fromRod, tempRod, toRod);
                MoveDisk(fromRod, toRod);
                SolveHanoi(n - 1, tempRod, toRod, fromRod);
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            // Решение задачи при нажатии на кнопку
            movesMade = 0;
            SolveHanoi(numberOfDisks, 0, 2, 1);
            MessageBox.Show($"Сделано движений: {movesMade}");
        }
    }
}
