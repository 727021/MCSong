using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCSong
{
    class CmdListcommands : Command
    {
        public override string name { get { return "listcommands"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }
        public CmdListcommands() { }

        public override void Use(Player p, string message)
        {
            if (p != null) return;

            List<String> Commands = new List<String>();
            switch (message.ToLower().Trim())
            {
                case "build":
                case "b":

                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type == CommandType.Building)
                        {
                            Commands.Add("support.Add(new " + c.GetType().Name + "());");
                        }
                    }

                    break;
                case "moderation":
                case "mod":
                case "m":

                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type == CommandType.Moderation)
                        {
                            Commands.Add("support.Add(new " + c.GetType().Name + "());");
                        }
                    }
                
                    break;
                case "information":
                case "info":
                case "i":

                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type == CommandType.Information)
                        {
                            Commands.Add("support.Add(new " + c.GetType().Name + "());");
                        }
                    }
                
                    break;
                case "other":
                case "o":
                default:

                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type == CommandType.Other)
                        {
                            Commands.Add("support.Add(new " + c.GetType().Name + "());");
                        }
                    }
                
                    break;
            }
            Commands.Sort();
            foreach (String s in Commands)
            {
                Server.s.Log(s);
            }

        }
        
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/listcommands <type> - Development command; not intended for player use.");
        }
    }
}
