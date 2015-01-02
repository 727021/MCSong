using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCSong
{
    class CmdListcommands : Command
    {
        public override string name { get { return "listcommands"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
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
                        if (c.type.StartsWith("b"))
                        {
                            Commands.Add("all.Add(new " + c.GetType().Name + "());");
                        }
                    }

                    break;
                case "moderation":
                case "mod":
                case "m":

                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type.StartsWith("m"))
                        {
                            Commands.Add("all.Add(new " + c.GetType().Name + "());");
                        }
                    }
                
                    break;
                case "information":
                case "info":
                case "i":

                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type.StartsWith("i"))
                        {
                            Commands.Add("all.Add(new " + c.GetType().Name + "());");
                        }
                    }
                
                    break;
                case "other":
                case "o":
                default:

                    foreach (Command c in Command.all.commands)
                    {
                        if (c.type.StartsWith("o"))
                        {
                            Commands.Add("all.Add(new " + c.GetType().Name + "());");
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
