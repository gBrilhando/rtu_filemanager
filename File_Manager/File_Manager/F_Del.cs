using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager
{
    public partial class F_Del : Form
    {
        Stopwatch stopWatch = new Stopwatch();
        bool err_stat = false;
        public F_Del()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") label1.Text = "Введите имя нового файла!!!";
            else
            {
                Og_log.Log_new(4, textBox1.Text, "");
                if (System.IO.File.Exists($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt"))
                {
                    stopWatch.Start();
                    await Task.Delay(3000);
                    if (!err_stat)
                    {
                        System.IO.File.Delete($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt");
                        stopWatch.Stop();
                        TimeSpan ts = stopWatch.Elapsed;
                        Og_log.Log_time(ts, 3000);
                        textBox1.Text = "";
                        label1.Text = "Введите имя файла";
                        button2_Click(sender, e);
                    }
                }
                else label1.Text = "Файл не существует. Введите имя файла";
            }
        }

        private void button2_Click(object sender, EventArgs e)
            {
                if (stopWatch.IsRunning)
                {
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    Og_log.Log_time(ts, 0);
                    err_stat = true;
                    Og_log.Log_err();
                }
                Og_log.Log_enter();
                this.Close();
            }
    }
}
