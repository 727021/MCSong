using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSong
{
    public class CmdAliases : Command
    {
        public override string name { get { return "aliases"; } }
        public override string[] aliases { get { return new string[] { "shortcuts", "shortcut", "alias", "short" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                Help(p);
                return;
            }
            switch (message.ToLower())
            {
                case "build":
                    Player.SendMessage(p, "Building command aliases:");
                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type == CommandType.Building && c.aliases.Length > 0 && !String.IsNullOrWhiteSpace(c.aliases[0]))
                        {
                            string al = "";
                            foreach (string a in c.aliases)
                            {
                                al += ", " + a;
                            }
                            Player.SendMessage(p, "/" + c.name + " - " + al.Remove(0, 2));
                        }
                    }
                    break;
                case "info":
                    Player.SendMessage(p, "Information command aliases:");
                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type == CommandType.Information && c.aliases.Length > 0 && !String.IsNullOrWhiteSpace(c.aliases[0]))
                        {
                            string al = "";
                            foreach (string a in c.aliases)
                            {
                                al += ", " + a;
                            }
                            Player.SendMessage(p, "/" + c.name + " - " + al.Remove(0, 2));
                        }
                    }
                    break;
                case "mod":
                    Player.SendMessage(p, "Moderation command aliases:");
                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type == CommandType.Moderation && c.aliases.Length > 0 && !String.IsNullOrWhiteSpace(c.aliases[0]))
                        {
                            string al = "";
                            foreach (string a in c.aliases)
                            {
                                al += ", " + a;
                            }
                            Player.SendMessage(p, "/" + c.name + " - " + al.Remove(0, 2));
                        }
                    }
                    break;
                case "other":
                    Player.SendMessage(p, "Other command aliases:");
                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type == CommandType.Other && c.aliases.Length > 0 && !String.IsNullOrWhiteSpace(c.aliases[0]))
                        {
                            string al = "";
                            foreach (string a in c.aliases)
                            {
                                al += ", " + a;
                            }
                            Player.SendMessage(p, "/" + c.name + " - " + al.Remove(0, 2));
                        }
                    }
                    break;
                default:
                    Command cmd = Command.all.Find(message.ToLower());
                    if (cmd == null)
                    {
                        Player.SendMessage(p, "Command not found!");
                    }
                    else
                    {
                        if (cmd.aliases.Length > 0 && !String.IsNullOrWhiteSpace(cmd.aliases[0]))
                        {
                            string al = "";
                            foreach (string a in cmd.aliases)
                            {
                                al += ", " + a;
                            }
                            Player.SendMessage(p, "Aliases for /" + cmd.name + " - " + al.Remove(0, 2));
                        }
                        else
                        {
                            Player.SendMessage(p, "No aliases found for /" + cmd.name);
                        }
                    }
                    break;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/aliases [build/info/mod/other] - Displays aliases for commands of a certain type");
            Player.SendMessage(p, "/aliases [command] - Displays aliases for a specific command");
        }

    }
}
