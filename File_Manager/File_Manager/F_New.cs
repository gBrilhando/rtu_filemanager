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
using System.Threading;

namespace File_Manager
{
    public partial class F_New : Form
    {
        Stopwatch stopWatch1 = new Stopwatch();
        bool err_stat = false;
        public F_New()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") label1.Text = "Введите имя нового файла!!!";
            else
            {
                if (!System.IO.File.Exists($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt"))
                {
                    Og_log.Log_new(0, textBox1.Text, "");
                    stopWatch1.Start();
                    await Task.Delay(3000);
                    if (!err_stat)
                    {
                        System.IO.File.Create($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt").Close();
                        stopWatch1.Stop();
                        TimeSpan ts = stopWatch1.Elapsed;
                        Og_log.Log_time(ts, 3000);
                        textBox1.Text = "";
                        label1.Text = "Введите имя нового файла";
                        button2_Click(sender, e);
                    }
                }
                else label1.Text = "Файл уже существует. Введите имя нового файла";
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
