using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MCSong
{
    class CmdClones : Command
    {

        public override string name { get { return "clones"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdClones() { }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p == null)
                {
                    Help(p);
                    return;
                }
                message = p.name;
            }

            string originalName = message.ToLower();

            Player who = Player.Find(message);
            if (who == null)
            {
                Player.SendMessage(p, "Could not find player. Searching database.");

                SQLiteHelper.SQLResult ipQuery = SQLiteHelper.ExecuteQuery($@"SELECT ip FROM Players WHERE name = '{message}';");
                if (ipQuery.rowsAffected <= 0)
                {
                    Player.SendMessage(p, "Could not find any player by the name entered.");
                    return;
                }
                else
                    message = ipQuery[0]["ip"];
            }
            else
            {
                message = who.ip;
            }

            SQLiteHelper.SQLResult nameQuery = SQLiteHelper.ExecuteQuery($@"SELECT name FROM Players WHERE ip = '{message}';");
            List<string> foundNames = new List<string>();
            for (int i = 0; i < nameQuery.rowsAffected; i++)
                foundNames.Add(nameQuery[i]["name"]);
            if (foundNames.Count <= 1)
                Player.SendMessage(p, $"{originalName} has no clones.");
            else
            {
                Player.SendMessage(p, "These people have the same IP address:");
                Player.SendMessage(p, string.Join(", ", foundNames.ToArray()));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clones <name> - Finds everyone with the same IP as <name>");
        }
    }
}
