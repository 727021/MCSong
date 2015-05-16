using System;

namespace MCSong
{
    public class CmdRepeat : Command
    {
        public override string name { get { return "repeat"; } }
        public override string[] aliases { get { return new string[] { "m" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdRepeat() { }

        public override void Use(Player p, string message)
        {
            try
            {
                if (p.lastCMD == "") { Player.SendMessage(p, "No commands used yet."); return; }
                if (p.lastCMD.Length > 5)
                    if (p.lastCMD.Substring(0, 6) == "static") { Player.SendMessage(p, "Can't repeat static"); return; }

                Player.SendMessage(p, "Using &b/" + p.lastCMD);

                if (p.lastCMD.IndexOf(' ') == -1)
                {
                    Command.all.Find(p.lastCMD).Use(p, "");
                }
                else
                {
                    Command.all.Find(p.lastCMD.Substring(0, p.lastCMD.IndexOf(' '))).Use(p, p.lastCMD.Substring(p.lastCMD.IndexOf(' ') + 1));
                }
            }
            catch { Player.SendMessage(p, "An error occured!"); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/repeat - Repeats the last used command");
        }
    }
}