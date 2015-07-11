using System;
using System.Collections.Generic;

namespace MCSong
{
    public class CmdExtensions : Command
    {
        public override string name { get { return "extensions"; } }
        public override string[] aliases { get { return new string[] { "cpe" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            List<string> exts = new List<string> { "clickdistance", "customblocks", "heldblock", "emotefix", "texthotkey", "extplayerlist", "envcolors", "selectioncuboid", "blockpermissions", "changemodel", "envmapappearance", "envweathertype", "hackcontrol", "messagetypes", "playerclick" };
            if (message.Trim() == "")
            {
                Player.SendMessage(p, "Classic Protocol Extension (CPE) is a project to augment the Minecraft Classic network protocol with new and improved functionality.");
                Player.SendMessage(p, "In other words, some clients and servers work together to add new features to Minecraft Classic, to make the game more fun for you!");
                Player.SendMessage(p, "For more information about CPE, visit http://wiki.vg/Classic_Protocol_Extension");
                Help(p);
                return;
            }
            else if (message.ToLower() == "list")
            {
                Player.SendMessage(p, "Existing protocol extensions:");
                // List ALL cpe extensions and wether mcsong supports them
            }
            else if (message.ToLower() == "server")
            {
                if (Server.cpe.Count == 0) { Player.SendMessage(p, "CPE is disabled on the server."); return; }
                Player.SendMessage(p, "Server-enabled extensions:");
                // List support extensions enabled by the server
            }
            else if (exts.Contains(message.ToLower().Trim()))
            {
                switch (message.ToLower().Trim())
                {
                    case "clickdistance":
                        Player.SendMessage(p, "");
                        break;
                }
            }
            else
            {
                Player who = Player.Find(message);
                if (p == null) { Player.SendMessage(p, "Could not find player " + message); return; }
                if (!who.cpe) { Player.SendMessage(p, ""); return; }
                Player.SendMessage(p, "Extensions enabled for " + who.color + who.name + Server.DefaultColor + ":");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/extensions - Displays basic information about Classic Protocol Extension");
            Player.SendMessage(p, "/extensions list - Lists the names of support existing extensions");
            Player.SendMessage(p, "/extensions server - Lists support extensions enabled on the server");
            Player.SendMessage(p, "/extensions [extension] - Displays information about a specific extension");
            Player.SendMessage(p, "/extensions [player] - Lists support extensions enabled for a player");
        }
    }
}