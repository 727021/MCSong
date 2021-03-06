using System;
using System.Data;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCSong
{
    public class CmdClearBlockChanges : Command
    {
        public override string name { get { return "clearblockchanges"; } }
        public override string[] aliases { get { return new string[] { "cbc" }; } }
        public override CommandType type { get { return CommandType.Moderation; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdClearBlockChanges() { }

        public override void Use(Player p, string message)
        {
            Level l = Level.Find(message);
            if (l == null && message != "") { Player.SendMessage(p, "Could not find level."); return; }
            if (l == null && p != null) l = p.level;
            if (l == null && p == null) { Player.SendMessage(p, "Could not find level."); return; }

            int rowsAffected = SQLiteHelper.ExecuteQuery($@"DELETE FROM Blocks{l.name}").rowsAffected;
            Server.s.Debug($"Deleted {rowsAffected} rows from Blocks{l.name}");
            Player.SendMessage(p, "Cleared &cALL" + Server.DefaultColor + " recorded block changes in: &d" + l.name);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clearblockchanges <map> - Clears the block changes stored in /about for <map>.");
            Player.SendMessage(p, "&cUSE WITH CAUTION");
        }
    }
}