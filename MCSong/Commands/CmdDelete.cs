using System;
using System.Collections.Generic;

namespace MCSong
{
    public class CmdDelete : Command
    {
        public override string name { get { return "delete"; } }
        public override string[] aliases { get { return new string[] { "d" }; } }
        public override CommandType type { get { return CommandType.Building; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdDelete() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }

            p.deleteMode = !p.deleteMode;
            Player.SendMessage(p, "Delete mode: &a" + p.deleteMode);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/delete - Deletes any block you click");
            Player.SendMessage(p, "\"any block\" meaning door_air, portals, mb's, etc");
        }
    }
}