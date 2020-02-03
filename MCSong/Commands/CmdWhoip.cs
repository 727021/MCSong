using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MCSong
{
    public class CmdWhoip : Command
    {
        public override string name { get { return "whoip"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdWhoip() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            SQLiteHelper.SQLResult ipQuery = SQLiteHelper.ExecuteQuery($@"SELECT name FROM Players WHERE ip = '{message}';");
            if (ipQuery.rowsAffected <= 0)
            {
                Player.SendMessage(p, "Could not find anyone with this IP.");
                return;
            }
            List<string> players = new List<string>();
            foreach (SQLiteHelper.SQLRow row in ipQuery)
                players.Add(row["name"]);
            Player.SendMessage(p, $"Found {players.Count} player{(players.Count > 1 ? "s" : "")} with this IP: {string.Join(", ", players)}");
        }
        public override void Help(Player p)
        {
            p.SendMessage("/whoip <ip address> - Displays players associated with a given IP address.");
        }
    }
}