﻿using System;
using System.Collections.Generic;


namespace MCSong
{
    public class CmdDrop : Command
    {
        public override string name { get { return "drop"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return false; } }
        public CmdDrop() { }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            if (p.hasflag != null)
            {
                p.level.ctfgame.DropFlag(p, p.hasflag);
                return;
            }
            else
            {
                Player.SendMessage(p, "You are not carrying a flag.");
            }

        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/drop - Drop the flag if you are carrying it.");
        }
    }
}
