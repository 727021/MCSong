using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

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

            string players = "Players with this IP: ";
            jDatabase.Table plrs = Server.s.database.GetTable("Players");
            foreach (int i in plrs.GetRowNumbers("IP", message))
            {
                players += plrs.GetValue(i, "Name") + ", ";
            }
            if (players == "Players with this IP: ") { Player.SendMessage(p, "Could not find anyone with this IP"); return; }
            players = players.Remove(players.Length - 2);
            Player.SendMessage(p, players);
        }
        public override void Help(Player p)
        {
            p.SendMessage("/whoip <ip address> - Displays players associated with a given IP address.");
        }
    }
}