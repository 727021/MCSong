using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MCSong
{
    public enum BeatType
    {
        Mojang,
        ClassiCube,
        MCSong
    }

    public static class SongBeat
    {
        static int timeout = 60 * 1000;
        static string hash;
        public static string serverURL;
        static string staticVars;

        static bool ccSuccess = false;

        static BackgroundWorker worker;
        static HttpWebRequest request;

        static StreamWriter beatLogger;

        public static void Init()
        {
            if (!Directory.Exists("heartbeat"))
                Directory.CreateDirectory("heartbeat");
            if (Server.logbeat)
            {
                if (!File.Exists("heartbeat/beats.log"))
                    File.Create("heartbeat/beats.log").Close();
            }

            // Transfer old logs/remove old responses
            if (File.Exists("heartbeat.log"))
            {
                StreamReader sr = new StreamReader("heartbeat.log");
                StreamWriter sw = new StreamWriter("heartbeat/beats.log");
                sw.Write(sr.ReadToEnd());
                sr.Close();
                sw.Close();
                File.Delete("heartbeat.log");
            }
            if (File.Exists("songbeat.log"))
            {
                StreamReader sr = new StreamReader("songbeat.log");
                StreamWriter sw = new StreamWriter("heartbeat/beats.log");
                sw.Write(sr.ReadToEnd());
                sr.Close();
                sw.Close();
                File.Delete("songbeat.log");
            }
            if (File.Exists("text/externalurl.txt"))
                File.Delete("text/externalurl.txt");

            staticVars = "port=" + Server.port +
                "&max=" + Server.players +
                "&name=" + URLEncode(Server.name) +
                "&public=" + Server.pub +
                "&version=" + Server.version;
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            worker.RunWorkerAsync();
        }

        static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Pump(BeatType.ClassiCube);
            //Pump(BeatType.Mojang);
            //Pump(BeatType.MCSong);
            Thread.Sleep(timeout);
        }

        static void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            worker.RunWorkerAsync();
        }

        public static bool Pump(BeatType type)
        {
            if (staticVars == null)
                Init();

            if (Server.logbeat)
                beatLogger = new StreamWriter("heartbeat/beats.log", File.Exists("heartbeat/beats.log"));

            string postVars = staticVars;

            string url = "http://www.classicube.net/heartbeat.jsp";
            try
            {
                int hidden = 0;

                foreach (Player p in Player.players)
                {
                    if (p.hidden)
                        hidden++;
                }

                switch (type)
                {
                    case BeatType.ClassiCube:
                        ccSuccess = false;
                        postVars += "&salt=" + Server.salt +
                            "&software=MCSong";
                        goto default;
                    case BeatType.Mojang:
                        url = "https://minecraft.net/heartbeat.jsp";
                        postVars += "&salt=" + Server.salt;
                        goto default;
                    case BeatType.MCSong:
                        url = "http://mcsong.x10.mx/heartbeat.php";
                        postVars += "&songversion=" + Server.Version +
                            "&url=" + URLEncode(Server.externalURL) +
                            "&motd=" + URLEncode(Server.motd);
                        goto default;
                    default:
                        postVars += "&users=" + (Player.number - hidden);
                        break;
                }

                request = (HttpWebRequest)WebRequest.Create(new Uri(url + "?" + postVars));
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                byte[] formData = Encoding.ASCII.GetBytes(postVars);
                request.ContentLength = formData.Length;
                request.Timeout = 15000;
                try
                {
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(formData, 0, formData.Length);
                        requestStream.Close();
                    }
                    if (Server.logbeat)
                    {
                        beatLogger.WriteLine(string.Format("[{0}] Request sent at {1}", type, DateTime.Now.ToString()));
                    }
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.Timeout)
                    {
                        throw new WebException("Failed during request.GetRequestStream()", e.InnerException, e.Status, e.Response);
                    }
                }

                using (WebResponse response = request.GetResponse())
                {
                    if (Server.logbeat)
                    {
                        beatLogger.WriteLine(string.Format("[{0}] Response received at {1}", type, DateTime.Now.ToString()));
                    }
                    StreamReader responseReader;
                    switch (type)
                    {
                        case BeatType.ClassiCube:
                            responseReader = new StreamReader(response.GetResponseStream());
                            string resp = responseReader.ReadToEnd();
                            File.WriteAllText("heartbeat/ClassiCube.txt", resp);
                            if (Server.logbeat) Server.s.Log("ClassiCube response saved to heartbeat/ClassiCube.txt");

                            if (resp.StartsWith("http"))
                            {
                                serverURL = resp;
                                Server.s.UpdateUrl(serverURL);
                                Server.externalURL = serverURL;
                                File.WriteAllText("heartbeat/externalurl.txt", resp);
                                if (Server.logbeat) Server.s.Log("URL saved to heartbeat/externalurl.txt...");
                                ccSuccess = true;
                            }
                            else if (resp.StartsWith("{"))
                            {
                                JObject json = JObject.Parse(resp);
                                Server.s.Log("ClassiCube returned an error: " + json["errors"][0][0].ToString().Replace("\"", ""));
                                ccSuccess = false;
                                /*                                
                                {
                                  "errors": [
                                    [
                                       "Server not reachable, port may not be open."
                                    ]
                                  ], 
                                  "response": "", 
                                  "status": "fail"
                                }
                                */
                            }
                            responseReader.Close();
                            break;
                        case BeatType.Mojang:
                            responseReader = new StreamReader(response.GetResponseStream());
                            string line = responseReader.ReadLine();
                            File.WriteAllText("heartbeat/Mojang.txt", line);
                            if (Server.logbeat) Server.s.Log("Mojang response saved to heartbeat/Mojang.txt...");
                            if (!ccSuccess)
                            {
                                hash = line.Substring(line.LastIndexOf('=') + 1);
                                serverURL = line;
                                Server.s.UpdateUrl(serverURL);
                                Server.externalURL = serverURL;
                                File.WriteAllText("heartbeat/externalurl.txt", line);
                                if (Server.logbeat) Server.s.Log("URL saved to heartbeat/externalurl.txt...");
                            }
                            responseReader.Close();
                            break;
                        case BeatType.MCSong:
                            new WebClient().DownloadFile(url + "?" + postVars, "heartbeat/MCSong.txt");
                            if (Server.logbeat) Server.s.Log("MCSong response saved to heartbeat/MCSong.txt");
                            break;
                    }


                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    Server.s.Log(string.Format("Timeout: {0}", type));
                }
                Server.ErrorLog(e);
            }
            catch (Exception e)
            {
                Server.s.Log(string.Format("Error reporting to {0}", type));
                Server.ErrorLog(e);
                return false;
            }
            finally
            {
                request.Abort();
                if (beatLogger != null)
                    beatLogger.Close();
            }

            return true;
        }

        public static string URLEncode(string s)
        {
            StringBuilder o = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i] >= '0' && s[i] <= '9') ||
                    (s[i] >= 'A' && s[i] <= 'Z') ||
                    (s[i] >= 'a' && s[i] <= 'z') ||
                    s[i] == '-' || s[i] == '_' || s[i] == '.' || s[i] == '~')
                {
                    o.Append(s[i]);
                }
                else if (Array.IndexOf<char>(reserved, s[i]) != -1)
                {
                    o.Append("%").Append(((int)s[i]).ToString("X"));
                }
            }
            return o.ToString();
        }

        private static char[] reserved = new char[] { ' ', '!', '*', '\'', '(', ')', ';', ':', '@', '&', '=', '+', '$', ',', '/', '?', '%', '#', '[', ']' };
    }
}
