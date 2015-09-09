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

            //DB
            //MySQL.executeQuery("CREATE TABLE if not exists `Inbox" + whoTo + "` (PlayerFrom CHAR(20), TimeSent DATETIME, Contents VARCHAR(255));");
            try { Server.s.database.CreateTable("Inbox" + whoTo, new List<string> { "PlayerFrom", "TimeSent", "Contents" }); }
            catch { }
            Server.s.database.GetTable("Inbox" + whoTo).AddRow(new List<string> { whoFrom, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message });
            //MySQL.executeQuery("INSERT INTO `Inbox" + whoTo + "` (PlayerFrom, TimeSent, Contents) VALUES ('" + p.name + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + message.Replace("'", "\\'") + "')");
            //DB

            Player.SendMessage(p, "Message sent to &5" + whoTo + ".");
            if (who != null) who.SendMessage("Message recieved from &5" + whoFrom + Server.DefaultColor + ".");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/send [name] <message> - Sends <message> to [name].");
        }
    }
}