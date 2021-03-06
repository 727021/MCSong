using System;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using MCSong;

namespace MCSong_.Gui
{
    public static class Program
    {
        public static bool usingConsole = false;

        [DllImport("kernel32")]
        public static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static void GlobalExHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Server.ErrorLog(ex);
            Thread.Sleep(500);

            if (!Server.restartOnError)
                Program.restartMe();
            else
                Program.restartMe(false);
        }

        public static void ThreadExHandler(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Server.ErrorLog(ex);
            Thread.Sleep(500);

            if (!Server.restartOnError)
                Program.restartMe();
            else
                Program.restartMe(false);
        }

        public static void WriteLine(string message = "")
        {
            if (message == "") { Console.WriteLine(); return; }
            try { Console.WriteLine(Server.StripColors(message)); }
            catch { Console.WriteLine(message); }
        }

        public static void Main(string[] args)
        {
            if (Process.GetProcessesByName("MCSong").Length != 1)
            {
                foreach (Process pr in Process.GetProcessesByName("MCSong"))
                {
                    if (pr.MainModule.BaseAddress == Process.GetCurrentProcess().MainModule.BaseAddress)
                        if (pr.Id != Process.GetCurrentProcess().Id)
                            pr.Kill();
                }
            }

            PidgeonLogger.Init();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.GlobalExHandler);
            Application.ThreadException += new ThreadExceptionEventHandler(Program.ThreadExHandler);
            bool skip = false;
        remake:
            try
            {
                if (!File.Exists("Viewmode.cfg") || skip)
                {
                    StreamWriter SW = new StreamWriter(File.Create("Viewmode.cfg"));
                    SW.WriteLine("#This file controls how the console window is shown to the server host");
                    SW.WriteLine("#cli:             True or False (Determines whether a CLI interface is used) (Set True if on Mono)");
                    SW.WriteLine("#high-quality:    True or false (Determines whether the Gui interface uses higher quality objects)");
                    SW.WriteLine();
                    SW.WriteLine("cli = false");
                    SW.WriteLine("high-quality = true");
                    SW.Flush();
                    SW.Close();
                    SW.Dispose();
                }

                if (File.ReadAllText("Viewmode.cfg") == "") { skip = true; goto remake; }

                string[] foundView = File.ReadAllLines("Viewmode.cfg");
                if (foundView[0][0] != '#') { skip = true; goto remake; }

                if (foundView[4].Split(' ')[2].ToLower() == "true")
                {
                    Server s = new Server();
                    s.OnLog += WriteLine;
                    s.OnCommand += WriteLine;
                    s.OnSystem += WriteLine;
                    s.Start();

                    Console.Title = Server.name + " - MCSong Version: " + Server.Version;
                    
                    usingConsole = true;
                    handleComm(Console.ReadLine());

                    //Application.Run();
                }
                else
                {

                    IntPtr hConsole = GetConsoleWindow();
                    if (IntPtr.Zero != hConsole)
                    {
                        ShowWindow(hConsole, 0);
                    }
                    UpdateCheck(true);
                    if (foundView[5].Split(' ')[2].ToLower() == "true")
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                    }

                    updateTimer.Elapsed += delegate { UpdateCheck(); }; updateTimer.Start();

                    Application.Run(new MCSong.Gui.Window());
                }
            }
            catch (Exception e) { Server.ErrorLog(e); return; }
        }

        public static void handleComm(string s)
        {
            if (s == "") goto handle;
            switch (s[0])
            {
                case '/':
                case '!':// Command
                    string cmd = "", msg = "";
                    s = s.Remove(0, 1);
                    if (s.IndexOf(' ') != -1)
                    {
                        cmd = s.Split(' ')[0];
                        msg = s.Substring(s.IndexOf(' ') + 1);
                    }
                    else if (s != "")
                    {
                        cmd = s;
                    }
                    else
                    {
                        WriteLine("CONSOLE: Whitespace commands are not allowed");
                        goto handle;
                    }

                    try
                    {
                        Command c = Command.all.Find(cmd);
                        if (c != null)
                        {
                            if (c.consoleUsable)
                            {
                                c.Use(null, msg);
                                WriteLine("CONSOLE: USED /" + cmd + " " + msg);
                            }
                            else
                            {
                                WriteLine("CONSOLE: Cannot use /" + cmd);
                            }
                        }
                        else
                        {
                            WriteLine("CONSOLE: Command /" + cmd + " not found");
                        }
                    }
                    catch (Exception e)
                    {
                        Server.ErrorLog(e);
                        WriteLine("CONSOLE: Failed command");

                    }
                    break;
                case '#':// OP Chat
                    s = s.Remove(0, 1);
                    Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + s);
                    Server.s.Log("(OPs): Console: " + s);
                    //IRCBot.Say("Console: " + s, true);
                    break;
                case ';':// Admin Chat
                    s = s.Remove(0, 1);
                    Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + s);
                    Server.s.Log("(Admins): Console: " + s);
                    //IRCBot.Say("Console: " + s, true);
                    break;
                case '\\':// Global Chat
                    s = s.Remove(0, 1);
                    Player.GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] Console: &f" + s);
                    Server.s.Log("[Global][" + Server.gcNick + "] Console: " + s);
                    //GlobalBot.Say("Console: " + s);
                    break;
                default:// Chat
                    Player.GlobalMessage("Console [&a" + Server.ZallState + Server.DefaultColor + "]: &f" + s);
                    //IRCBot.Say("Console [" + Server.ZallState + "]: " + s);
                    WriteLine("<CONSOLE> " + s);
                    break;
            }
            handle:
            handleComm(Console.ReadLine());

            // OLD (MCLawl) MESSAGE HANDLING
            /*
            string sentCmd = "", sentMsg = "";

            if (s.IndexOf(' ') != -1)
            {
                sentCmd = s.Split(' ')[0];
                sentMsg = s.Substring(s.IndexOf(' ') + 1);
            }
            else if (s != "")
            {
                sentCmd = s;
            }
            else
            {
                goto talk;
            }

            try
            {
                Command cmd = Command.all.Find(sentCmd);
                if (cmd != null)
                {
                    if (cmd.consoleUsable)
                    {
                        cmd.Use(null, sentMsg);
                        WriteLine("CONSOLE: USED /" + sentCmd + " " + sentMsg);
                        handleComm(Console.ReadLine());
                    }
                    else
                    {
                        WriteLine("CONSOLE: Cannot use /" + sentCmd);
                        handleComm(Console.ReadLine());
                    }
                    return;
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                WriteLine("CONSOLE: Failed command.");
                handleComm(Console.ReadLine());
                return;
            }
            

        talk: handleComm("say " + MCSong.Group.findPerm(LevelPermission.Admin).color + "Console: &f" + s);
            handleComm(Console.ReadLine());
            */
        }

        public static bool CurrentUpdate = false;
        static bool msgOpen = false;
        public static System.Timers.Timer updateTimer = new System.Timers.Timer(120 * 60 * 1000);

        public static void UpdateCheck(bool wait = false, Player p = null)
        {

            CurrentUpdate = true;
            Thread updateThread = new Thread(new ThreadStart(delegate
            {
                WebClient Client = new WebClient();

                if (wait) { if (!Server.checkUpdates) return; Thread.Sleep(10000); }
                try
                {
                    if (Client.DownloadString("http://updates.mcsong.x10.mx/curversion.txt") != Server.Version)
                    {
                        if (Server.autoupdate == true || p != null)
                        {
                            if (Server.autonotify == true || p != null)
                            {
                                if (p != null) Server.restartcountdown = "20";
                                Player.GlobalMessage("Update found. Prepare for restart in &f" + Server.restartcountdown + Server.DefaultColor + " seconds.");
                                Server.s.Log("Update found.  Prepare for restart in " + Server.restartcountdown + " seconds.");
                                double nxtTime = Convert.ToDouble(Server.restartcountdown);
                                DateTime nextupdate = DateTime.Now.AddMinutes(nxtTime);
                                int timeLeft = Convert.ToInt32(Server.restartcountdown);
                                System.Timers.Timer countDown = new System.Timers.Timer();
                                countDown.Interval = 1000;
                                countDown.Start();
                                countDown.Elapsed += delegate
                                {
                                    if (Server.autoupdate == true || p != null)
                                    {
                                        Player.GlobalMessage("Updating in &f" + timeLeft + Server.DefaultColor + " seconds.");
                                        Server.s.Log("Updating in " + timeLeft + " seconds.");
                                        timeLeft = timeLeft - 1;
                                        if (timeLeft < 0)
                                        {
                                            Player.GlobalMessage("---UPDATING SERVER---");
                                            Server.s.Log("---UPDATING SERVER---");
                                            countDown.Stop();
                                            countDown.Dispose();
                                            PerformUpdate(false);
                                        }
                                    }
                                    else
                                    {
                                        Player.GlobalMessage("Stopping auto restart.");
                                        Server.s.Log("Stopping auto restart.");
                                        countDown.Stop();
                                        countDown.Dispose();
                                    }
                                };
                            }
                            else
                            {
                                PerformUpdate(false);
                            }

                        }
                        else
                        {
                            if (!msgOpen && !usingConsole)
                            {
                                msgOpen = true;
                                if (MessageBox.Show("New version found. Would you like to update?", "Update?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    PerformUpdate(false);
                                }
                                msgOpen = false;
                            }
                            else
                            {
                                ConsoleColor prevColor = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Red;
                                WriteLine("An update was found!");
                                WriteLine("Update using the file at updates.mcsong.x10.mx/MCSong_.dll and placing it over the top of your current MCSong_.dll!");
                                Console.ForegroundColor = prevColor;
                            }
                        }
                    }
                    else
                    {
                        Player.SendMessage(p, "No update found!");
                    }
                }
                catch { Server.s.Log("No web server found to update on."); }
                Client.Dispose();
                CurrentUpdate = false;
            })); updateThread.Start();
        }

        public static void PerformUpdate(bool oldrevision)
        {
            try
            {
                StreamWriter SW;
                if (!Server.mono)
                {
                    if (!File.Exists("Update.bat"))
                        SW = new StreamWriter(File.Create("Update.bat"));
                    else
                    {
                        if (File.ReadAllLines("Update.bat")[0] != "::Version 3")
                        {
                            SW = new StreamWriter(File.Create("Update.bat"));
                        }
                        else
                        {
                            SW = new StreamWriter(File.Create("Update_generated.bat"));
                        }
                    }
                    SW.WriteLine("::Version 3");
                    SW.WriteLine("TASKKILL /pid %2 /F");
                    SW.WriteLine("if exist MCSong_.dll.backup (erase MCSong_.dll.backup)");
                    SW.WriteLine("if exist MCSong_.dll (rename MCSong_.dll MCSong_.dll.backup)");
                    SW.WriteLine("if exist MCSong.new (rename MCSong.new MCSong_.dll)");
                    SW.WriteLine("start MCSong.exe");
                }
                else
                {
                    if (!File.Exists("Update.sh"))
                        SW = new StreamWriter(File.Create("Update.sh"));
                    else
                    {
                        if (File.ReadAllLines("Update.sh")[0] != "#Version 2")
                        {
                            SW = new StreamWriter(File.Create("Update.sh"));
                        }
                        else
                        {
                            SW = new StreamWriter(File.Create("Update_generated.sh"));
                        }
                    }
                    SW.WriteLine("#Version 2");
                    SW.WriteLine("#!/bin/bash");
                    SW.WriteLine("kill $2");
                    SW.WriteLine("rm MCSong_.dll.backup");
                    SW.WriteLine("mv MCSong_.dll MCSong.dll_.backup");
                    SW.WriteLine("wget http://updates.mcsong.x10.mx/MCSong_.dll");
                    SW.WriteLine("mono MCSong.exe");
                }

                SW.Flush(); SW.Close(); SW.Dispose();

                string filelocation = "";
                string verscheck = "";
                Process proc = Process.GetCurrentProcess();
                string assemblyname = proc.ProcessName + ".exe";
                if (!oldrevision)
                {
                    WebClient client = new WebClient();
                    Server.selectedrevision = client.DownloadString("http://updates.mcsong.x10.mx/curversion.txt");
                    client.Dispose();
                }
                verscheck = Server.selectedrevision.TrimStart('r');
                int vers = int.Parse(verscheck.Split('.')[0]);
                if (oldrevision) { filelocation = ("http://updates.mcsong.x10.mx/archives/exe/" + Server.selectedrevision + ".exe"); }
                if (!oldrevision) { filelocation = ("http://updates.mcsong.x10.mx/MCSong_.dll"); }
                WebClient Client = new WebClient();
                Client.DownloadFile(filelocation, "MCSong.new");
                Client.DownloadFile("http://updates.mcsong.x10.mx/changelog.txt", "extra/Changelog.txt");
                foreach (Level l in Server.levels) l.Save();
                foreach (Player pl in Player.players) pl.save();

                string fileName;
                if (!Server.mono) fileName = "Update.bat";
                else fileName = "Update.sh";

                try
                {
                    if (MCSong.Gui.Window.thisWindow.notifyIcon1 != null)
                    {
                        MCSong.Gui.Window.thisWindow.notifyIcon1.Icon = null;
                        MCSong.Gui.Window.thisWindow.notifyIcon1.Visible = false;
                    }
                }
                catch { }

                Process p = Process.Start(fileName, "main " + System.Diagnostics.Process.GetCurrentProcess().Id.ToString());
                p.WaitForExit();
            }
            catch (Exception e) { Server.ErrorLog(e); }
        }

        static public void ExitProgram(Boolean AutoRestart)
        {
            Thread exitThread;
            Server.Exit();

            exitThread = new Thread(new ThreadStart(delegate
            {
                try
                {
                    if (MCSong.Gui.Window.thisWindow.notifyIcon1 != null)
                    {
                        MCSong.Gui.Window.thisWindow.notifyIcon1.Icon = null;
                        MCSong.Gui.Window.thisWindow.notifyIcon1.Visible = false;
                    }
                }
                catch { }

                try
                {
                    saveAll();

                    if (AutoRestart == true) restartMe();
                    else Server.process.Kill();
                }
                catch
                {
                    Server.process.Kill();
                }
            })); exitThread.Start();
        }

        static public void restartMe(bool fullRestart = true)
        {
            Thread restartThread = new Thread(new ThreadStart(delegate
            {
                saveAll();

                Server.shuttingDown = true;
                try
                {
                    if (MCSong.Gui.Window.thisWindow.notifyIcon1 != null)
                    {
                        MCSong.Gui.Window.thisWindow.notifyIcon1.Icon = null;
                        MCSong.Gui.Window.thisWindow.notifyIcon1.Visible = false;
                    }
                }
                catch { }

                if (Server.listen != null) Server.listen.Close();
                if (!Server.mono || fullRestart)
                {
                    Application.Restart();
                    Server.process.Kill();
                }
                else
                {
                    Server.s.Start();
                }
            }));
            restartThread.Start();
        }
        static public void saveAll()
        {
            try
            {
                List<Player> kickList = new List<Player>();
                kickList.AddRange(Player.players);
                foreach (Player p in kickList) { p.Kick("Server restarted! Rejoin!"); }
            }
            catch (Exception exc) { Server.ErrorLog(exc); }

            try
            {
                string level = null;
                foreach (Level l in Server.levels)
                {
                    level = level + l.name + "=" + l.physics + System.Environment.NewLine;
                    l.Save();
                    l.saveChanges();
                }

                File.WriteAllText("text/autoload.txt", level);
            }
            catch (Exception exc) { Server.ErrorLog(exc); }
        }
    }
}
