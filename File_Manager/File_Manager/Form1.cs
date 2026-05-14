using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager
{
    public partial class File_M : Form
    {
        public File_M()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_New F_New = new F_New();
            F_New.Show();
        }

        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Ren F_Ren = new F_Ren();
            F_Ren.Show();
        }

        private void прочитатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Red F_Red = new F_Red();
            F_Red.Show();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Chg F_Chg = new F_Chg();
            F_Chg.Show();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Del F_Del = new F_Del();
            F_Del.Show();
        }

        private void прочитатьЛогToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!richTextBox1.Visible)
            {
                richTextBox1.Visible = true;
                richTextBox1.Text = System.IO.File.ReadAllText($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt");
                прочитатьЛогToolStripMenuItem.Text = "Убрать лог";
            } else
            {
                richTextBox1.Text = "";
                richTextBox1.Visible = false;
                прочитатьЛогToolStripMenuItem.Text = "Показать лог";
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version version = new Version();
            version.Show();
        }
    }
}
