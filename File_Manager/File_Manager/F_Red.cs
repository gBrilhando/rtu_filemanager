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
    public partial class F_Red : Form
    {
        Stopwatch stopWatch1 = new Stopwatch();
        bool err_stat = false;
        public F_Red()
        {
            InitializeComponent();
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
                Og_log.Log_enter();
            }
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") label1.Text = "Введите имя файла!!!";
            else if (!System.IO.File.Exists($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt")) label1.Text = "Файла не существует. Создайте файл или измените имя файла";
            else {
                Og_log.Log_new(2, textBox1.Text, "");
                stopWatch1.Start();
                await Task.Delay(3000);
                if (!err_stat)
                {
                    richTextBox1.Text = System.IO.File.ReadAllText($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt");
                    stopWatch1.Stop();
                    TimeSpan ts = stopWatch1.Elapsed;
                    Og_log.Log_time(ts, 3000);
                    textBox1.Text = "";
                    label1.Text = "Введите имя файла";
                    Og_log.Log_enter();
                }
            }
        }
    }
}
