using System;

namespace MCSong
{
    public class CmdTitle : Command
    {
        public override string name { get { return "title"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdTitle() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            int pos = message.IndexOf(' ');
            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { Player.SendMessage(p, "Could not find player."); return; }
            
            string newTitle = "";
            if (message.Split(' ').Length > 1) newTitle = message.Substring(pos + 1);
            else
            {
                who.title = "";
                who.SetPrefix();
                Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " had their title removed.", false);
                SQLiteHelper.ExecuteQuery($@"UPDATE Players SET title = '' WHERE id = {who.userID};");
                return;
            }

            if (newTitle != "")
            {
                newTitle = newTitle.ToString().Trim().Replace("[", "");
                newTitle = newTitle.Replace("]", "");
            }

            if (newTitle.Length > 17) { Player.SendMessage(p, "Title must be under 17 letters."); return; }
            if (!Server.devs.Contains(p.name))
            {
                if (Server.devs.Contains(who.name) || newTitle.ToLower() == "dev") { Player.SendMessage(p, "Can't let you do that, starfox."); return; }
            }

            if (newTitle != "")
                Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " was given the title of &b[" + newTitle + "]", false);
            else
                Player.GlobalChat(null, who.color + who.prefix + who.name + Server.DefaultColor + " had their title removed.", false);

            who.title = newTitle;
            SQLiteHelper.ExecuteQuery($@"UPDATE Players SET title = '{SQLiteHelper.EscapeQuotes(newTitle)}' WHERE id = {who.userID};");
            who.SetPrefix();
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/title <player> [title] - Gives <player> the [title].");
            Player.SendMessage(p, "If no [title] is given, the player's title is removed.");
        }
    }
}
