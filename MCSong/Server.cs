/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCSong) Licensed under the
	Educational Community License, Version 2.0 (the "License"); you may
	not use this file except in compliance with the License. You may
	obtain a copy of the License at
	
	http://www.osedu.org/licenses/ECL-2.0
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the License is distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the License for the specific language governing
	permissions and limitations under the License.
*/
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SQLite;

using MonoTorrent.Client;

namespace MCSong
{
    public class Server
    {
        public delegate void LogHandler(string message);
        public delegate void HeartBeatHandler();
        public delegate void MessageEventHandler(string message);
        public delegate void PlayerListHandler();
        public delegate void VoidHandler();

        public event LogHandler OnLog;
        public event LogHandler OnOp;
        public event LogHandler OnAdmin;
        public event LogHandler OnGlobal;
        public event LogHandler OnSystem;
        public event LogHandler OnCommand;
        public event LogHandler OnError;
        public event HeartBeatHandler HeartBeatFail;
        public event MessageEventHandler OnURLChange;
        public event PlayerListHandler OnPlayerListChange;
        public event VoidHandler OnSettingsUpdate;

        public static Thread locationChecker;

        public static Thread blockThread;

        public static int speedPhysics = 250;

        public static string Version { get { return "1.0.0.0-pre1"; /*System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();*/ } }

        public static Socket listen;
        public static System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
        public static System.Timers.Timer updateTimer = new System.Timers.Timer(100);
        //static System.Timers.Timer heartbeatTimer = new System.Timers.Timer(60000);     //Every 45 seconds
        static System.Timers.Timer messageTimer = new System.Timers.Timer(60000 * 5);   //Every 5 mins
        public static System.Timers.Timer cloneTimer = new System.Timers.Timer(5000);

        //public static Thread physThread;
        //public static bool physPause;
        //public static DateTime physResume = DateTime.Now;
        //public static System.Timers.Timer physTimer = new System.Timers.Timer(1000);
        // static Thread botsThread;

        //CTF STUFF
        public static List<CTFGame> CTFGames = new List<CTFGame>();

        public static PlayerList bannedIP;
        public static PlayerList whiteList;
        public static PlayerList ircControllers;
        public static PlayerList gcAgreed;

        public static List<string> devs = new List<string>(new string[] { "727021" });
        public static List<string> devnicks = new List<string>(new string[] { "_727021" });

        public static List<TempBan> tempBans = new List<TempBan>();
        public struct TempBan { public string name; public DateTime allowedJoin; }

        public static MapGenerator MapGen;

        public static PerformanceCounter PCCounter = null;
        public static PerformanceCounter ProcessCounter = null;

        public static Level mainLevel;
        public static List<Level> levels = new List<Level>();
        //public static List<levelID> allLevels = new List<levelID>();
        public struct levelID { public int ID; public string name; }

        public static List<string> afkset = new List<string>();
        public static List<string> afkmessages = new List<string>();
        public static List<string> messages = new List<string>();

        public static DateTime timeOnline;

        //auto updater stuff
        public static bool autoupdate = false;
        public static bool autonotify = true;
        public static string restartcountdown = "30";
        public static string selectedrevision = "";
        public static bool autorestart;
        public static DateTime restarttime;

        public static bool chatmod = false;

        public static bool maintenanceMode = false;

        public static string externalURL = "";

        public static bool cancelShutdown = false;

        //Settings
        #region Server Settings
        public const byte version = 7;
        public static string salt = "";

        public static string name = "[MCSong] Default";
        public static string motd = "Welcome!";
        public static byte players = 12;
        public static byte guests = 4;
        public static byte maps = 5;
        public static int port = 25565;
        public static bool pub = true;
        public static bool premium = false;
        public static bool verify = true;
        public static bool worldChat = true;
        public static bool guestGoto = false;

        public static string ZallState = "Alive";

        //public static string[] userMOTD;

        public static string level = "main";
        public static string errlog = "error.log";

        public static bool console = false;
        public static bool reportBack = true;

        public static bool irc = false;
        public static int ircPort = 6667;
        public static string ircNick = "MCSong_Minecraft_Bot";
        public static string ircServer = "irc.esper.net";
        public static string ircChannel = "#changethis";
        public static string ircOpChannel = "#changethistoo";
        public static bool ircIdentify = false;
        public static string ircPassword = "";

        public static bool gc = true;
        public static string gcNick = "SONG_" + new Random().Next(1000, 9999).ToString();
        public static bool gcIdentify = false;
        public static string gcPassword = "";

        public static bool restartOnError = true;

        public static bool antiTunnel = true;
        public static byte maxDepth = 4;
        public static int Overload = 1500;
        public static int rpLimit = 500;
        public static int rpNormLimit = 10000;

        public static int backupInterval = 300;
        public static int blockInterval = 60;
        public static string backupLocation = Application.StartupPath + "/levels/backups";

        public static bool physicsRestart = true;
        public static bool deathcount = true;
        public static bool AutoLoad = false;
        public static int physUndo = 60000;
        public static int totalUndo = 200;
        public static bool rankSuper = true;
        public static bool oldHelp = false;
        public static bool parseSmiley = true;
        public static bool useWhitelist = false;
        public static bool forceCuboid = false;
        public static bool repeatMessage = false;

        public static bool checkUpdates = true;

        public static string DefaultColor = "&e";
        public static string IRCColour = "&5";
        public static string gcColor = "&6";

        public static int afkminutes = 10;
        public static int afkkick = 45;

        public static string defaultRank = "guest";

        public static bool dollardollardollar = true;

        public static bool cheapMessage = true;
        public static string cheapMessageGiven = " is now being cheap and being immortal";
        public static bool customBan = false;
        public static string customBanMessage = "You're banned!";
        public static bool customShutdown = false;
        public static string customShutdownMessage = "Server shutdown. Rejoin in 10 seconds.";
        public static string moneys = "moneys";
        public static LevelPermission opchatperm = LevelPermission.Operator;// #
        public static LevelPermission adminchatperm = LevelPermission.Admin;// ;

        public static bool logbeat = false;

        public static LevelPermission maintPerm = LevelPermission.Admin;
        public static bool maintKick = true;

        // CPE
        public static readonly byte CustomBlockSupportLevel = 1;

        public static bool mono = false;

        public static bool flipHead = false;

        public static bool shuttingDown = false;

        public static bool debugMode = false;

        public static bool upnp = false;
        public static bool upnpRunning = false;
        #endregion

        public static MainLoop ml;
        public static Server s;
        public Server()
        {
            ml = new MainLoop("server");
            s = this;
        }
        public void Start()
        {
            shuttingDown = false;
            Log("Starting Server");

            if (!Directory.Exists("properties")) Directory.CreateDirectory("properties");
            if (!Directory.Exists("bots")) Directory.CreateDirectory("bots");
            if (!Directory.Exists("text")) Directory.CreateDirectory("text");

            if (!Directory.Exists("extra")) Directory.CreateDirectory("extra");
            if (!Directory.Exists("extra/undo")) Directory.CreateDirectory("extra/undo");
            if (!Directory.Exists("extra/undoPrevious")) Directory.CreateDirectory("extra/undoPrevious");
            if (!Directory.Exists("extra/copy/")) { Directory.CreateDirectory("extra/copy/"); }
            if (!Directory.Exists("extra/copyBackup/")) { Directory.CreateDirectory("extra/copyBackup/"); }

            try
            {
                if (File.Exists("server.properties")) File.Move("server.properties", "properties/server.properties");
                if (File.Exists("rules.txt")) File.Move("rules.txt", "text/rules.txt");
                if (File.Exists("welcome.txt")) File.Move("welcome.txt", "text/welcome.txt");
                if (File.Exists("messages.txt")) File.Move("messages.txt", "text/messages.txt");
                if (File.Exists("externalurl.txt")) File.Move("externalurl.txt", "text/externalurl.txt");
                if (File.Exists("autoload.txt")) File.Move("autoload.txt", "text/autoload.txt");
                if (File.Exists("IRC_Controllers.txt")) File.Move("IRC_Controllers.txt", "ranks/IRC_Controllers.txt");
                if (useWhitelist) if (File.Exists("whitelist.txt")) File.Move("whitelist.txt", "ranks/whitelist.txt");
            } catch { }

            ServerProperties.Load("properties/server.properties");
            Updater.Load("properties/update.properties");

            Group.InitAll();
            Command.InitAll();
            GrpCommands.fillRanks();
            Block.SetBlocks();
            Awards.Load();

            if (File.Exists("text/emotelist.txt"))
            {
                foreach (string s in File.ReadAllLines("text/emotelist.txt"))
                {
                    Player.emoteList.Add(s);
                }
            }
            else
            {
                File.Create("text/emotelist.txt");
            }

            timeOnline = DateTime.Now;

            SQLiteHelper.ExecuteQuery(@"CREATE TABLE IF NOT EXISTS Players (id INTEGER PRIMARY KEY ASC, name TEXT, ip TEXT, first_login TEXT, last_login TEXT, total_login INTEGER, title TEXT, deaths INTEGER, money INTEGER, blocks INTEGER, kicks INTEGER, color TEXT, tcolor TEXT);");
            // Insert a console player if there isn't one already (needed for inbox messages)
            if (SQLiteHelper.ExecuteQuery($@"SELECT id FROM Players WHERE name = 'Console';").rowsAffected <= 0)
                SQLiteHelper.ExecuteQuery($@"INSERT INTO Players (name) VALUES ('Console');");

            if (levels != null)
                foreach (Level l in levels) { l.Unload(); }
            ml.Queue(delegate
            {
                try
                {
                    levels = new List<Level>(Server.maps);
                    MapGen = new MapGenerator();

                    Random random = new Random();

                    if (File.Exists("levels/" + Server.level + ".lvl"))
                    {
                        mainLevel = Level.Load(Server.level);
                        mainLevel.unload = false;
                        if (mainLevel == null)
                        {
                            if (File.Exists("levels/" + Server.level + ".lvl.backup"))
                            {
                                Log("Attempting to load backup.");
                                File.Copy("levels/" + Server.level + ".lvl.backup", "levels/" + Server.level + ".lvl", true);
                                mainLevel = Level.Load(Server.level);
                                if (mainLevel == null)
                                {
                                    Log("BACKUP FAILED!");
                                    Console.ReadLine(); return;
                                }
                            }
                            else
                            {
                                Log("mainlevel not found");
                                Level temp = new Level(Server.level, 128, 64, 128, "flat");

                                temp.permissionvisit = LevelPermission.Guest;
                                temp.permissionbuild = LevelPermission.Guest;
                                temp.Save(true, true);
                                mainLevel = Level.Load(temp.name);
                            }
                        }
                    }
                    else
                    {
                        Log("mainlevel not found");
                        Level temp = new Level(Server.level, 128, 64, 128, "flat");

                        temp.permissionvisit = LevelPermission.Guest;
                        temp.permissionbuild = LevelPermission.Guest;
                        temp.Save(true, true);
                        mainLevel = Level.Load(temp.name);
                    }
                    addLevel(mainLevel);
                    mainLevel.physThread.Start();
                } catch (Exception e) { Server.ErrorLog(e); }
            });

            ml.Queue(delegate
            {
                bannedIP = PlayerList.Load("banned-ip.txt", null);
                ircControllers = PlayerList.Load("IRC_Controllers.txt", null);
                gcAgreed = PlayerList.Load("GCAgreed.txt", null);

                foreach (Group grp in Group.GroupList)
                    grp.playerList = PlayerList.Load(grp.fileName, grp);
                if (Server.useWhitelist)
                    whiteList = PlayerList.Load("whitelist.txt", null);
            });

            ml.Queue(delegate
            {
                if (File.Exists("text/autoload.txt"))
                {
                    try
                    {
                        string[] lines = File.ReadAllLines("text/autoload.txt");
                        foreach (string line in lines)
                        {
                            //int temp = 0;
                            string _line = line.Trim();
                            try
                            {
                                if (_line == "") { continue; }
                                if (_line[0] == '#') { continue; }
                                int index = _line.IndexOf("=");

                                string key = _line.Split('=')[0].Trim();
                                string value;
                                try
                                {
                                    value = _line.Split('=')[1].Trim();
                                }
                                catch
                                {
                                    value = "0";
                                }

                                if (!key.Equals(mainLevel.name))
                                {
                                    Command.all.Find("load").Use(null, key + " " + value);
                                    Level l = Level.FindExact(key);
                                }
                                else
                                {
                                    try
                                    {
                                        int temp = int.Parse(value);
                                        if (temp >= 0 && temp <= 3)
                                        {
                                            mainLevel.setPhysics(temp);
                                        }
                                    }
                                    catch
                                    {
                                        Server.s.Log("Physics variable invalid");
                                    }
                                }


                            }
                            catch
                            {
                                Server.s.Log(_line + " failed.");
                            }
                        }
                    }
                    catch
                    {
                        Server.s.Log("autoload.txt error");
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                else
                {
                    Log("autoload.txt does not exist");
                }
            });

            ml.Queue(delegate
            {
                Log("Creating listening socket on port " + Server.port + "... ");
                if (Setup())
                {
                    if (upnp)
                    {
                        if (UpnpSetup())
                        {
                            s.Log("Ports have been forwarded with upnp."); upnpRunning = true;
                        }
                        else
                        {
                            s.Log("Could not auto forward ports. Make sure upnp is enabled on your router."); upnpRunning = false;
                        }
                    }
                    if (!upnp || upnp && upnpRunning)
                        s.Log("Done.");
                }
                else
                {
                    s.Log("Could not create socket connection.  Shutting down.");
                    return;
                }
            });

            ml.Queue(delegate
            {
                updateTimer.Elapsed += delegate
                {
                    Player.GlobalUpdate();
                    PlayerBot.GlobalUpdatePosition();
                };

                updateTimer.Start();
            });


            // Heartbeat code here:

            ml.Queue(delegate
            {
                try
                {
                    SongBeat.Init();
                }
                catch (Exception e)
                {
                    Server.ErrorLog(e);
                }
            });

            // END Heartbeat code

            
            Thread processThread = new Thread(new ThreadStart(delegate
            {
                try
                {
                    Server.s.Log("Starting performance counters...");
                    PCCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                    ProcessCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
                    PCCounter.BeginInit();
                    ProcessCounter.BeginInit();
                    PCCounter.NextValue();
                    ProcessCounter.NextValue();
                }
                catch { }
            }));
            processThread.Start();
            

            ml.Queue(delegate
            {
                messageTimer.Elapsed += delegate
                {
                    RandomMessage();
                };
                messageTimer.Start();

                process = System.Diagnostics.Process.GetCurrentProcess();

                if (File.Exists("text/messages.txt"))
                {
                    StreamReader r = File.OpenText("text/messages.txt");
                    while (!r.EndOfStream)
                        messages.Add(r.ReadLine());
                    r.Dispose();
                }
                else File.Create("text/messages.txt").Close();

                if (Server.irc)
                {
                    new IRCBot();
                }
                if (Server.gc)
                {
                    new GlobalBot();
                }
            

                //      string CheckName = "FROSTEDBUTTS";

                //       if (Server.name.IndexOf(CheckName.ToLower())!= -1){ Server.s.Log("FROSTEDBUTTS DETECTED");}
                new AutoSaver(Server.backupInterval);     //2 and a half mins

                blockThread = new Thread(new ThreadStart(delegate
                {
                    while (true)
                    {
                        Thread.Sleep(blockInterval * 1000);
                        foreach (Level l in levels)
                        {
                            l.saveChanges();
                        }
                    }
                }));
                blockThread.Start();

                locationChecker = new Thread(new ThreadStart(delegate
                {
                    while (true)
                    {
                        Thread.Sleep(3);
                        for (int i = 0; i < Player.players.Count; i++)
                        {
                            try
                            {
                                Player p = Player.players[i];

                                if (p.frozen)
                                {
                                    unchecked { p.SendPos((byte)-1, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]); } continue;
                                }
                                else if (p.following != "")
                                {
                                    Player who = Player.Find(p.following);
                                    if (who == null || who.level != p.level) 
                                    { 
                                        p.following = "";
                                        if (!p.canBuild)
                                        {
                                            p.canBuild = true;
                                        }
                                        if (who != null && who.possess == p.name)
                                        {
                                            who.possess = "";
                                        }
                                        continue; 
                                    }
                                    if (p.canBuild)
                                    {
                                        unchecked { p.SendPos((byte)-1, who.pos[0], (ushort)(who.pos[1] - 16), who.pos[2], who.rot[0], who.rot[1]); }
                                    }
                                    else
                                    {
                                        unchecked { p.SendPos((byte)-1, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1]); }
                                    }
                                } else if (p.possess != "") {
                                    Player who = Player.Find(p.possess);
                                    if (who == null || who.level != p.level)
                                        p.possess = "";
                                }

                                ushort x = (ushort)(p.pos[0] / 32);
                                ushort y = (ushort)(p.pos[1] / 32);
                                ushort z = (ushort)(p.pos[2] / 32);

                                if (p.level.Death) 
                                    p.RealDeath(x, y, z);
                                p.CheckBlock(x, y, z);

                                p.oldBlock = (ushort)(x + y + z);
                            } catch (Exception e) { Server.ErrorLog(e); }
                        }
                    }
                }));

                locationChecker.Start();

                Log("Finished setting up server");
            });

            try
            {
                using (WebClient web = new WebClient())
                {
                    if (new List<string>(web.DownloadString("http://updates.mcsong.x10.mx/hostbans.txt").Split(',')).Contains(web.DownloadString("http://ipinfo.io/ip").Trim()))
                    {
                        s.Log("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !");
                        s.Log("YOUR IP HAS BEEN HOST BANNED. APPEAL AT http://mcsong.x10.mx/forums.");
                        s.Log("! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !");
                        Exit();
                    }
                }
            }
            catch
            {
                // Hard-coded list
            }

            //PluginManager.AutoLoad();
        }
        
        public static bool Setup()
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, Server.port);
                listen = new Socket(endpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listen.Bind(endpoint);
                listen.Listen((int)SocketOptionName.MaxConnections);

                listen.BeginAccept(new AsyncCallback(Accept), null);
                return true;
            }
            catch (SocketException e) { ErrorLog(e); return false; }
            catch (Exception e) { ErrorLog(e); return false; }
        }

        public static bool UpnpSetup()
        {
            try
            {
                if (new UpnpHelper().AddMapping(Convert.ToUInt16(port), "TCP", "MCSong"))
                    return true;
                return false;
            }
            catch (Exception e)
            {
                ErrorLog(e);
                s.Log("Failed. Make sure your router supports upnp.");
                return false;
            }
        }

        static void Accept(IAsyncResult result)
        {
            if (shuttingDown == false)
            {
                // found information: http://www.codeguru.com/csharp/csharp/cs_network/sockets/article.php/c7695
                // -Descention
                Player p = null;
                try
                {
                    p = new Player(listen.EndAccept(result));
                    listen.BeginAccept(new AsyncCallback(Accept), null);
                }
                catch (SocketException)
                {
                    if (p != null)
                        p.Disconnect();
                }
                catch (Exception e)
                {
                    ErrorLog(e);
                    if (p != null)
                        p.Disconnect();
                }
            }
        }

        public static void Exit()
        {
            /*PluginManager.loaded.ForEach(delegate(Plugin p)
            {
                try
                {
                    PluginManager.Unload(p);
                }
                catch { }
            });*/
            List<string> players = new List<string>();
            foreach (Player p in Player.players) { p.save(); players.Add(p.name); }
            foreach (string p in players)
            {
                if (!Server.customShutdown)
                {
                    Player.Find(p).Kick("Server shutdown. Rejoin in 10 seconds.");
                }
                else
                {
                    Player.Find(p).Kick(Server.customShutdownMessage);
                }
            }

            Player.connections.ForEach(
            delegate(Player p)
            {
                if (!Server.customShutdown)
                {
                    p.Kick("Server shutdown. Rejoin in 10 seconds.");
                }
                else
                {
                    p.Kick(Server.customShutdownMessage);
                }
            }
            );
            shuttingDown = true;
            if (listen != null)
            {
                listen.Close();
            }
        }

        public static void addLevel(Level level)
        {
            levels.Add(level);
        }

        public void PlayerListUpdate()
        {
            Server.s.OnPlayerListChange?.Invoke();
        }

        public void FailBeat()
        {
            HeartBeatFail?.Invoke();
        }

        public void UpdateUrl(string url)
        {
            OnURLChange?.Invoke(url);
        }

        public void LogOp(string message)
        {
            message = StripColors(message);
            OnOp?.Invoke(message);
            Log("(OPs): " + message);
        }
        public void LogAdmin(string message)
        {
            message = StripColors(message);
            OnAdmin?.Invoke(message);
            Log("(Admin): " + message);
        }
        public void LogGC(string message)
        {
            message = StripColors(message);
            OnGlobal?.Invoke(message);
            Log("[GLOBAL]" + message);
        }
        public void Log(string message, bool systemMsg = false)
        {
            message = StripColors(message);
            string msg = DateTime.Now.ToString("HH:mm:ss) ") + message;
            if (systemMsg)
                OnSystem?.Invoke(msg);
            else
                OnLog?.Invoke(msg);

            Logger.Write(msg + Environment.NewLine);
        }
        public void Debug(string message)
        {
            if (Server.debugMode)
                Log("[Debug]" + message);
        }
        public void ErrorCase(string message)
        {
            OnError?.Invoke(message);
        }

        public void CommandUsed(string message)
        {
            OnCommand?.Invoke(DateTime.Now.ToString("(HH:mm:ss) ") + message);
            Logger.Write(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
        }

        public static void ErrorLog(Exception ex)
        {
            Logger.WriteError(ex);
            try
            {
                s.Log("!!!Error! See " + Logger.ErrorLogPath + " for more information.");
            } catch { }
        }

        public static void RandomMessage()
        {
            if (Player.number != 0 && messages.Count > 0)
                Player.GlobalMessage(messages[new Random().Next(0, messages.Count)]);
        }

        internal void SettingsUpdate()
        {
            OnSettingsUpdate?.Invoke();
        }

        public static string FindColor(string Username)
        {
            foreach (Group grp in Group.GroupList)
            {
                if (grp.playerList.Contains(Username)) return grp.color;
            }
            return Group.standard.color;
        }

        public static string StripColors(string input)
        {
            return new Regex("&[0-9a-f]", RegexOptions.IgnoreCase).Replace(input, "");// Why not use regex?
        }
    }
}