using System;
using System.Collections.Generic;
using System.Data;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCSong
{
    public class CmdInbox : Command
    {
        public override string name { get { return "inbox"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdInbox() { }

        public override void Use(Player p, string message)
        {
            try
            {
                //MySQL.executeQuery("CREATE TABLE if not exists `Inbox" + p.name + "` (PlayerFrom CHAR(20), TimeSent DATETIME, Contents VARCHAR(255));");
                try { Server.s.database.CreateTable("Inbox" + p.name, new List<string> { "PlayerFrom", "TimeSent", "Contents" }); }
                catch { }

                if (message == "")
                {
                    //DataTable Inbox = MySQL.fillData("SELECT * FROM `Inbox" + p.name + "` ORDER BY TimeSent");

                    Table inbox = Server.s.database.GetTable("Inbox" + p.name);
                    List <List<string>> rows = inbox.Rows;

                    //if (Inbox.Rows.Count == 0) { Player.SendMessage(p, "No messages found."); Inbox.Dispose(); return; }

                    if (rows.Count <= 1) { Player.SendMessage(p, "No messages found."); return; }
                    /*
                    for (int i = 0; i < Inbox.Rows.Count; ++i)
                    {
                        Player.SendMessage(p, i + ": From &5" + Inbox.Rows[i]["PlayerFrom"].ToString() + Server.DefaultColor + " at &a" + Inbox.Rows[i]["TimeSent"].ToString());
                    }
                    Inbox.Dispose();*/
                    foreach (List<string> row in rows)
                    {
                        int i = rows.IndexOf(row);
                        Player.SendMessage(p, i + ": From &5" + inbox.GetValue(i, "PlayerFrom") + Server.DefaultColor + " at &a" + inbox.GetValue(i, "TimeSent"));
                    }
                }
                else if (message.Split(' ')[0].ToLower() == "del" || message.Split(' ')[0].ToLower() == "delete")
                {
                    int FoundRecord = 0;

                    if (message.Split(' ')[1].ToLower() != "all")
                    {
                        try
                        {
                            FoundRecord = int.Parse(message.Split(' ')[1]);
                        }
                        catch { Player.SendMessage(p, "Incorrect number given."); return; }

                        if (FoundRecord < 1) { Player.SendMessage(p, "Cannot delete records below 1"); return; }
                    }

                    //DataTable Inbox = MySQL.fillData("SELECT * FROM `Inbox" + p.name + "` ORDER BY TimeSent");

                    Table inbox = Server.s.database.GetTable("Inbox" + p.name);
                    List<List<string>> rows = inbox.Rows;

                    if (rows.Count < FoundRecord + 1 || rows.Count <= 1)
                    {
                        Player.SendMessage(p, "\"" + FoundRecord + "\" does not exist."); return;
                    }

                    if (FoundRecord == 0)
                    {
                        inbox.Truncate();
                        Player.SendMessage(p, "Deleted all messages.");
                    }
                    else
                    {
                        inbox.DeleteRow(FoundRecord);
                        Player.SendMessage(p, "Deleted message " + FoundRecord + ".");
                    }
                    /*
                    string queryString;
                    if (FoundRecord == 0)
                        queryString = "TRUNCATE TABLE `Inbox" + p.name + "`";
                    else
                        queryString = "DELETE FROM `Inbox" + p.name + "` WHERE PlayerFrom='" + Inbox.Rows[FoundRecord]["PlayerFrom"] + "' AND TimeSent='" + Convert.ToDateTime(Inbox.Rows[FoundRecord]["TimeSent"]).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                
                    MySQL.executeQuery(queryString);

                    if (FoundRecord == -1)
                        Player.SendMessage(p, "Deleted all messages.");
                    else
                        Player.SendMessage(p, "Deleted message.");

                    Inbox.Dispose();*/
                }
                else
                {
                    int FoundRecord;

                    try
                    {
                        FoundRecord = int.Parse(message);
                    }
                    catch { Player.SendMessage(p, "Incorrect number given."); return; }

                    if (FoundRecord < 1) { Player.SendMessage(p, "Cannot read records below 1"); return; }

                    Table inbox = Server.s.database.GetTable("Inbox" + p.name);
                    List<List<string>> rows = inbox.Rows;

                    Player.SendMessage(p, "Message from &5" + inbox.GetValue(FoundRecord, "PlayerFrom") + Server.DefaultColor + " sent at &a" + inbox.GetValue(FoundRecord, "TimeSent") + Server.DefaultColor + ":");
                    Player.SendMessage(p, inbox.GetValue(FoundRecord, "Contents"));

                    //DataTable Inbox = MySQL.fillData("SELECT * FROM `Inbox" + p.name + "` ORDER BY TimeSent");
                    
                    if (rows.Count - 1 < FoundRecord || rows.Count <= 0)
                    {
                        Player.SendMessage(p, "\"" + FoundRecord + "\" does not exist."); return;
                    }
                    /*
                    Player.SendMessage(p, "Message from &5" + Inbox.Rows[FoundRecord]["PlayerFrom"] + Server.DefaultColor + " sent at &a" + Inbox.Rows[FoundRecord]["TimeSent"] + ":");
                    Player.SendMessage(p, Inbox.Rows[FoundRecord]["Contents"].ToString());
                    Inbox.Dispose();*/
                }
            }
            catch
            {
                Player.SendMessage(p, "Error accessing inbox. You may have no mail, try again.");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/inbox - Displays all your messages.");
            Player.SendMessage(p, "/inbox [num] - Displays the message at [num]");
            Player.SendMessage(p, "/inbox <del> [\"all\"/num] - Deletes the message at Num or All if \"all\" is given.");
        }
    }
}