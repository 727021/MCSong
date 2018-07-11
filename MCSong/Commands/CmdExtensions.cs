using System;
using System.Collections.Generic;

namespace MCSong
{
    public class CmdExtensions : Command
    {
        public override string name { get { return "extensions"; } }
        public override string[] aliases { get { return new string[] { "cpe", "extension" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            if (message.Trim() == "")
            {
                Player.SendMessage(p, "Classic Protocol Extension (CPE) is a project to augment the Minecraft Classic network protocol with new and improved functionality.");
                Player.SendMessage(p, "In other words, some clients and servers work together to add new features to Minecraft Classic, to make the game more fun for you!");
                Player.SendMessage(p, "For more information about CPE, visit http://wiki.vg/CPE");
                return;
            }
            else if (message.ToLower() == "list")
            {
                Player.SendMessage(p, "Existing protocol extensions:");
                string[] e = new string[] { };
                foreach (Extension ex in Extension.all)
                {
                    e[e.Length] = ((ex.implemented) ? "&a" : "&c") + ex.name;
                }
                Player.SendMessage(p, string.Join(Server.DefaultColor + ", ", e));
            }
            else if (message.ToLower() == "server")
            {
                string[] ex = new string[] { };
                foreach (Extension e in Extension.all)
                {
                    if (e.implemented)
                        ex[ex.Length] = e.name + "(v" + e.version + ")";
                }
                Player.SendMessage(p, "Supported extensions:");
                Player.SendMessage(p, string.Join(", ", ex));
            }
            else if (Extension.names.Contains(message.ToLower().Trim()))
            {
                Extension e = Extension.all.Find(message.ToLower().Trim());

                Player.SendMessage(p, "-- " + e.name + " --");
                foreach (string s in e.description)
                    Player.SendMessage(p, s);
                Player.SendMessage(p, "MCSong Support: " + ((e.implemented) ? "&aYES(v" + e.version + ")" : "&cNO"));
            }
            else
            {
                Player who = Player.Find(message);
                if (p == null) { Player.SendMessage(p, "Could not find player " + message); return; }
                Player.SendMessage(p, "Extensions supported by " + who.name + ":");
                string temp = "";
                foreach (Extension e in who.extensions)
                {
                    temp += ", " + e.name + " (v" + e.version + ")";
                }
                Player.SendMessage(p, (temp == "") ? "This player's client does not support CPE or has no enabled extensions." : temp.Remove(0, 2));
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/extensions - Displays basic information about Classic Protocol Extension");
            Player.SendMessage(p, "/extensions list - Lists the names of all existing extensions");
            Player.SendMessage(p, "/extensions server - Lists all extensions enabled on the server");
            Player.SendMessage(p, "/extensions [extension] - Displays information about a specific extension");
            Player.SendMessage(p, "/extensions [player] - Lists all extensions enabled for a player");
        }
    }
}