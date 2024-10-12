using System;
using System.Windows.Forms;
using HanoiTowerApp.Properties;

namespace HanoiTowerApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Form1 f1;
        private void ханойскиеБашенкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f1 = new Form1();
            f1.Show();
        }

        Fractal f;
        private void треугольникСерпинскогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f = new Fractal();
            f.Show();
        }
    }
}