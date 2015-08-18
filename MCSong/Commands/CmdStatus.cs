using System;

namespace MCSong
{
    class CmdStatus : Command
    {
        public override string name { get { return "status"; } }
        public override string[] aliases { get { return new string[] { "stat" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            if (!p.extensions.Contains(Extension.MessageTypes)) { Help(p); return; }
            string[] args = message.ToLower().Split(' ');
            if (args.Length < 2) { Help(p); return; }
            if (args[1] == "custom")
            {
                if (args.Length < 3) { Help(p); return; }
                string msg = message.Substring(message.IndexOf(' ', message.IndexOf(args[1])));
            }
            else if (args[1] == "compass")
            {

            }
            else if (args[1] == "default")
            {

            }
            else if (args[1] == "game")
            {

            }
            else if (args[1] == "")
            {

            }
            else
            {

            }
        }

        public override void Help(Player p)
        {
            if (p.extensions.Contains(Extension.MessageTypes))
            {
                Player.SendMessage(p, "/status <1/2/3> [type] - Sets one of your status messages to a server preset");
                Player.SendMessage(p, "/status <1/2/3> custom <message> - Sets one of your status messages to a custom message");
            }
            else
            {
                Player.SendMessage(p, "This command requires the MessageTypes extension.");
                Player.SendMessage(p, "See /cpe for more information.");
            }
        }
    }
}
