using System;

namespace MCSong
{
    class CmdWhere : Command
    {
        public override string[] aliases { get { return new string[] { }; } }

        public override bool consoleUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool museumUsable { get { return true; } }

        public override string name { get { return "where"; } }

        public override CommandType type { get { return CommandType.Information; } }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/where - Tells you your current position");
        }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "Level: " + p.level.name);
            Player.SendMessage(p, "Coordinates: " + Math.Truncate((double)p.pos[0] / 32) + ", " + Math.Truncate((double)p.pos[1] / 32) + ", " + Math.Truncate((double)p.pos[2]/32));
            Player.SendMessage(p, "Rotation: " + p.rot[0] + ", " + p.rot[1]);
        }
    }
}
