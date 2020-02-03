using System;
using System.Collections.Generic;
using System.Data;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCSong
{
    public class CmdSend : Command
    {
        public override string name { get { return "send"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdSend() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1) { Help(p); return; }

            Player who = Player.Find(message.Split(' ')[0]);
            string whoFrom;
            string whoTo;
            if (who != null) whoTo = who.name;
            else whoTo = message.Split(' ')[0];
            if (p != null) whoFrom = p.name;
            else whoFrom = "Console";

            message = message.Substring(message.IndexOf(' ') + 1);

            SQLiteHelper.ExecuteQuery($@"CREATE TABLE IF NOT EXISTS Inbox (id INTEGER PRIMARY KEY, " +
                $@"to INTEGER, from INTEGER, sent TEXT, message TEXT, " +
                $@"FOREIGN KEY (to) REFERENCES Player(id), FOREIGN KEY (from) REFERENCES Player(id));");

            SQLiteHelper.ExecuteQuery($@"INSERT INTO Inbox (to, from, sent, message) VALUES " +
                $@"((SELECT id FROM Players WHERE name = '{whoTo}'), {p.userID}, {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}, {message})");

            Player.SendMessage(p, "Message sent to &5" + whoTo + ".");
            if (who != null) who.SendMessage("Message recieved from &5" + whoFrom + Server.DefaultColor + ".");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/send [name] <message> - Sends <message> to [name].");
        }
    }
}