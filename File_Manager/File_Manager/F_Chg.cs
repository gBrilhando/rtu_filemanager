using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Threading;

namespace File_Manager
{
    public partial class F_Chg : Form
    {
        Stopwatch stopWatch1 = new Stopwatch();
        bool err_stat = false;
        public F_Chg()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!button2.Visible)
            {
                if (textBox1.Text == "") label1.Text = "Введите имя файла!!!";
                else if (!System.IO.File.Exists($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt")) label1.Text = "Файла не существует. Создайте файл или измените имя файла";
                else
                {
                    Og_log.Log_new(2, textBox1.Text, "");
                    stopWatch1.Start();
                    await Task.Delay(3000);
                    if (!err_stat)
                    {
                        richTextBox1.Visible = true;
                        button2.Visible = true;
                        button1.Text = "Сбросить";
                        richTextBox1.Text = System.IO.File.ReadAllText($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt");
                        stopWatch1.Stop();
                        TimeSpan ts = stopWatch1.Elapsed;
                        Og_log.Log_time(ts, 3000);
                        Og_log.Log_enter();
                    }
                }
            }
            else
            {
                textBox1.Text = "";
                richTextBox1.Text = "";
                richTextBox1.Visible = false;
                button2.Visible = false;
                button1.Text = "Открыть";
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Og_log.Log_new(3, textBox1.Text, "");
            stopWatch1.Start();
            await Task.Delay(3000);
            if (!err_stat)
            {
                System.IO.File.WriteAllText($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt", richTextBox1.Text);
                stopWatch1.Stop();
                TimeSpan ts = stopWatch1.Elapsed;
                Og_log.Log_time(ts, 3000);
                button3_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                if (stopWatch1.IsRunning)
                {
                    stopWatch1.Stop();
                    TimeSpan ts = stopWatch1.Elapsed;
                    Og_log.Log_time(ts, 0);
                    err_stat = true;
                    Og_log.Log_err();
                }
                Og_log.Log_enter();
                this.Close();
            }
        }
    }
}
