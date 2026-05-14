using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager
{
    static public class Og_log
    {
        static public bool Log_ex()
        {
            return System.IO.File.Exists("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt");
        }
        static public void New_log()
        {
            System.IO.File.Create("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt").Close();
        }
        static public void Log_new(int sv_vo, string filename, string filename_res)
        {
            if (!Log_ex()) New_log();
            switch (sv_vo) {
                case 0:
                    System.IO.File.AppendAllText("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", $"{DateTime.Now:G} user created {filename}.txt");
                    break;
                case 1:
                    System.IO.File.AppendAllText("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", $"{DateTime.Now:G} user renamed {filename}.txt to {filename_res}.txt");
                    break;
                case 2:
                    System.IO.File.AppendAllText("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", $"{DateTime.Now:G} user read {filename}.txt");
                    break;
                case 3:
                    System.IO.File.AppendAllText("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", $"{DateTime.Now:G} user changed {filename}.txt");
                    break;
                case 4:
                    System.IO.File.AppendAllText("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", $"{DateTime.Now:G} user deleted {filename}.txt");
                    break;
            }
        }

        static public void Log_time(TimeSpan ts, int a)
        {
            if (a != 0) System.IO.File.AppendAllText("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", $"({ts.Milliseconds+a}ms)");
            else System.IO.File.AppendAllText("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", $"(0ms)");
        }
        static public void Log_err()
        {
            System.IO.File.AppendAllText("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", $" - err");
        }
        static public void Log_enter()
        {
            System.IO.File.AppendAllText("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", "\n");
        }
    }
}
