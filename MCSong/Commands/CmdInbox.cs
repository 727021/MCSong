using System;
using System.Collections.Generic;
using System.Data;

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
                SQLiteHelper.ExecuteQuery($@"CREATE TABLE IF NOT EXISTS Inbox (id INTEGER PRIMARY KEY, to INTEGER, from INTEGER, sent TEXT, message TEXT, FOREIGN KEY (to) REFERENCES Player(id), FOREIGN KEY (from) REFERENCES Player(id));");
                SQLiteHelper.SQLResult inbox = SQLiteHelper.ExecuteQuery($@"SELECT i.id AS id, p1.name AS to, p2.name AS from, i.sent AS sent, i.message AS message FROM Player p1 INNER JOIN Inbox i ON i.to = p1.id INNER JOIN Player p2 ON i.from = p2.id WHERE p1.name = '{p.name}';");
                if (message == "")
                {

                    if (inbox.rowsAffected <= 0) { Player.SendMessage(p, "No messages found."); return; }

                    for (int i = 0; i < inbox.rowsAffected; i++)
                        Player.SendMessage(p, $"{i + 1}: From &5{inbox[i]["from"]}{Server.DefaultColor} at &a{inbox[i]["sent"]}");
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

                    if (inbox.rowsAffected < FoundRecord || inbox.rowsAffected <= 0)
                    {
                        Player.SendMessage(p, "\"" + FoundRecord + "\" does not exist."); return;
                    }

                    if (FoundRecord == 0)
                    {
                        SQLiteHelper.ExecuteQuery($@"DELETE FROM Inbox WHERE to = (SELECT id FROM Player WHERE name = '{p.name}');");
                        Player.SendMessage(p, "Deleted all messages.");
                    }
                    else
                    {
                        SQLiteHelper.ExecuteQuery($@"DELETE FROM Inbox WHERE id = {inbox[FoundRecord - 1]["id"]};");
                        Player.SendMessage(p, "Deleted message " + FoundRecord + ".");
                    }
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
                    
                    if (inbox.rowsAffected < FoundRecord || inbox.rowsAffected <= 0)
                    {
                        Player.SendMessage(p, "\"" + FoundRecord + "\" does not exist."); return;
                    }

                    Player.SendMessage(p, "Message from &5" + inbox[FoundRecord - 1]["from"] + Server.DefaultColor + " sent at &a" + inbox[FoundRecord - 1]["sent"] + Server.DefaultColor + ":");
                    Player.SendMessage(p, inbox[FoundRecord - 1]["message"]);
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
            Player.SendMessage(p, "/inbox [#] - Displays the message at #");
            Player.SendMessage(p, "/inbox <del> [all/#] - Deletes the message at # or all messages.");
        }
    }
}