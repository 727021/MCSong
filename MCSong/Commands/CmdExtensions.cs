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
                Player.SendMessage(p, "For more information about CPE, visit http://wiki.vg/CPE");
                return;
            }
            else if (message.ToLower() == "list")
            {
                Player.SendMessage(p, "Existing protocol extensions:");
                string[] e = new string[] { };
                exts.ForEach(delegate (string s)
                {
                    Extension ex = Extension.all.Find(s);
                    e[e.Length] = ((ex != null) ? "&a" : "&c") + ex.name;
                });
                Player.SendMessage(p, string.Join(", ", e));
            }
            else if (message.ToLower() == "server")
            {
                string[] ex = new string[] { };
                foreach (Extension e in Extension.all)
                {
                    ex[ex.Length] = e.name + "(v" + e.version + ")";
                }
                Player.SendMessage(p, "Supported extensions:");
                Player.SendMessage(p, string.Join(", ", ex));
            }
            else if (exts.Contains(message.ToLower().Trim()))
            {
                Extension e = Extension.all.Find(message.ToLower().Trim());
                switch (message.ToLower().Trim())
                {
                    case "clickdistance":
                        Player.SendMessage(p, "-- ClickDistance --");
                        Player.SendMessage(p, "-Can restrict or extend a player's reach, up to 1023 blocks or down to 0");
                        break;
                    case "customblocks":
                        Player.SendMessage(p, "-- CustomBlocks --");
                        Player.SendMessage(p, "-Adds new visually distinct blocks to the game");
                        Player.SendMessage(p, "-There are currently 16 custom blocks:");
                        Player.SendMessage(p, "-Cobblestone Slab, Rope, Sandstone, Snow, Fire, Light Pink Wool, Forest Green Wool, Brown Wool, Deep Blue Wool, Turquoise Wool, Ice, Ceramic Tile, Magma, Pillar, Crate, Stone Brick");
                        break;
                    case "heldblock":
                        Player.SendMessage(p, "-- HeldBlock --");
                        Player.SendMessage(p, "-Allows the server to know which block a player is holding, to improve features like /cuboid and /follow");
                        break;
                    case "emotefix":
                        Player.SendMessage(p, "-- EmoteFix --");
                        Player.SendMessage(p, "-Improves the appearance of emotes (smileys) in chat");
                        break;
                    case "texthotkey":
                        Player.SendMessage(p, "-- TextHotKey --");
                        Player.SendMessage(p, "-Allows players to set hotkeys for certain server commands");
                        break;
                    case "extplayerlist":
                        Player.SendMessage(p, "-- ExtPlayerList --");
                        Player.SendMessage(p, "-Provides more flexibility in naming of players and loading of skins, autocompletion, and player tab-list display");
                        break;
                    case "envcolors":
                        Player.SendMessage(p, "-- EnvColors --");
                        Player.SendMessage(p, "-Allows the server to make maps more unique by altering environment colors");
                        break;
                    case "selectioncuboid":
                        Player.SendMessage(p, "-- SelectionCuboid --");
                        Player.SendMessage(p, "-Allows players to see highlighted selections in the map when using building commands");
                        break;
                    case "blockpermissions":
                        Player.SendMessage(p, "-- BlockPermissions --");
                        Player.SendMessage(p, "-Prevents players from placing/breaking certain block types");
                        break;
                    case "changemodel":
                        Player.SendMessage(p, "-- ChangeModel --");
                        Player.SendMessage(p, "-Lets servers spawn animal and monster models");
                        break;
                    case "envmapappearance":
                        Player.SendMessage(p, "-- EnvMapAppearance --");
                        Player.SendMessage(p, "-Allows customization of map edge textures");
                        break;
                    case "envweathertype":
                        Player.SendMessage(p, "-- EnvWeatherType --");
                        Player.SendMessage(p, "-Allows servers to create rain and snow");
                        break;
                    case "hackcontrol":
                        Player.SendMessage(p, "-- HackControl --");
                        Player.SendMessage(p, "-Allows servers to control which client cheats/hacks can be used");
                        break;
                    case "messagetypes":
                        Player.SendMessage(p, "-- MessageTypes --");
                        Player.SendMessage(p, "-Adds new on-screen message types, including centered announcements and status messages in the top right corner");
                        break;
                    case "playerclick":
                        Player.SendMessage(p, "-- PlayerClick --");
                        Player.SendMessage(p, "-Allows servers to receive details of every mouse click players make");
                        break;
                }

                Player.SendMessage(p, "MCSong Support: " + ((e != null) ? "&aYES(v" + e.version + ")" : "&cNO"));
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