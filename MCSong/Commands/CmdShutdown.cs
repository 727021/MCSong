using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MCSong
{
    public class CmdShutdown : Command
    {
        public override string name { get { return "shutdown"; } }
        public override string[] aliases { get { return new string[] { "sd", "close", "stop", "exit" }; } }
        public override CommandType type { get { return CommandType.Moderation; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }

        private Thread th;
        private int seconds = 10;
        private int temp = 10;
        public override void Use(Player p, string message)
        {
            if (message.ToLower().Trim() == "cancel")
            {
                Server.cancelShutdown = true;
                return;
            }
            if (Server.shuttingDown)
            {
                Player.SendMessage(p, "Server is already shutting down.");
                return;
            }
            Server.cancelShutdown = false;
            Server.shuttingDown = true;
            try { seconds = Convert.ToInt32(message); }
            catch { seconds = 10; }

            if (seconds < 0 || seconds > 120)
                seconds = 10;
            temp = seconds;
            Server.s.Log("Shutdown initiated by " + ((p == null) ? "Console" : p.name));
            th = new Thread(new ThreadStart(shutdown));
            th.Start();
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/shutdown [seconds] - Shuts down the server with a countdown");
            Player.SendMessage(p, "/shutdown cancel - Cancels a server shutdown");
            Player.SendMessage(p, "[seconds] must be between 0 and 120, and will default to 10");
        }
        private void shutdown()
        {
            while (seconds > 0)
            {
                if (Server.cancelShutdown)
                {
                    Server.s.Log("SERVER SHUTDOWN CANCELLED");
                    Player.GlobalMessage("SERVER SHUTDOWN CANCELLED");
                    Server.shuttingDown = false;
                    th.Abort();
                    return;
                }
                if (seconds == temp || seconds == 120 || seconds == 90 || seconds == 60 || seconds == 45 || seconds == 30 || seconds <= 15)
                {
                    Server.s.Log("SERVER SHUTDOWN IN " + seconds + " SECONDS");
                    Player.GlobalMessage("SERVER SHUTDOWN IN " + seconds + " SECONDS");
                }
                Thread.Sleep(1000);
                seconds--;
            }
            MCSong_.Gui.Program.ExitProgram(false);
        }
    }
}