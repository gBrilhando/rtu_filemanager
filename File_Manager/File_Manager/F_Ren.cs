using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace File_Manager
{
    public partial class F_Ren : Form
    {
        Stopwatch stopWatch = new Stopwatch();
        bool err_stat = false;
        public F_Ren()
        {
            InitializeComponent();
        }

        private  void button2_Click(object sender, EventArgs e)
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

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") label1.Text = "Введите имя файла!!!";
            else if (textBox2.Text == "") label2.Text = "Введите новое имя файла!!!";
            else
            {
                if (!System.IO.File.Exists($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt")) label1.Text = "Файл не суещствует. Введите действительный файл";
                else if (System.IO.File.Exists($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox2.Text}.txt")) label2.Text = "Файл уже существует. Выберите другое название или удалите старый файл";
                else
                {
                    Og_log.Log_new(1, textBox1.Text, textBox2.Text);
                    stopWatch.Start();
                    await Task.Delay(3000);
                    if (!err_stat)
                    {
                        File.Move($"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox1.Text}.txt", $"D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\{textBox2.Text}.txt");
                        stopWatch.Stop();
                        TimeSpan ts = stopWatch.Elapsed;
                        Og_log.Log_time(ts, 3000);
                        textBox1.Text = "";
                        label1.Text = "Введите имя файла";
                        textBox2.Text = "";
                        label2.Text = "Введите новое имя файла";
                        button2_Click(sender, e);
                    }
                }
            }
        }
    }
}
