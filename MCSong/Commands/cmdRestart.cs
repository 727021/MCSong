using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCSong.Gui;

namespace MCSong
{
    public class CmdRestart : Command
    {
        public override string name { get { return "restart"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Moderation; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdRestart() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            MCLawl_.Gui.Program.restartMe();
            //MCLawl_.Gui.Program.ExitProgram(true);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/restart - Restarts the server!  Use carefully!");
        }
    }
}
