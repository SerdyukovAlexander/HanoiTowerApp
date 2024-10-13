using System.ComponentModel;

namespace HanoiTowerApp
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ханойскиеБашенкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.треугольникСерпинскогоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.ханойскиеБашенкиToolStripMenuItem, this.треугольникСерпинскогоToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(311, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ханойскиеБашенкиToolStripMenuItem
            // 
            this.ханойскиеБашенкиToolStripMenuItem.Name = "ханойскиеБашенкиToolStripMenuItem";
            this.ханойскиеБашенкиToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.ханойскиеБашенкиToolStripMenuItem.Text = "Ханойские башенки";
            this.ханойскиеБашенкиToolStripMenuItem.Click += new System.EventHandler(this.ханойскиеБашенкиToolStripMenuItem_Click);
            // 
            // треугольникСерпинскогоToolStripMenuItem
            // 
            this.треугольникСерпинскогоToolStripMenuItem.Name = "треугольникСерпинскогоToolStripMenuItem";
            this.треугольникСерпинскогоToolStripMenuItem.Size = new System.Drawing.Size(165, 20);
            this.треугольникСерпинскогоToolStripMenuItem.Text = "Треугольник Серпинского";
            this.треугольникСерпинскогоToolStripMenuItem.Click += new System.EventHandler(this.треугольникСерпинскогоToolStripMenuItem_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 106);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "Form2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ханойскиеБашенкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem треугольникСерпинскогоToolStripMenuItem;

        #endregion
    }
}