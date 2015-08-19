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
            string custom = "";
            string type = "";
            switch (args[1])
            {
                case "custom":
                    if (args.Length < 3) { Help(p); return; }
                    custom = message.Substring(message.IndexOf(' ', message.IndexOf(args[2]) - 1));
                    type = args[1];
                    break;
                case "compass":
                case "default":
                case "game":
                case "motd":
                case "block":
                case "clear":
                    type = args[1];
                    break;
                default:
                    type = "default";
                    break;
            }
            int i = 0;
            try
            {
                i = int.Parse(args[0]);
                if (i < 1 || 1 > 3) { Help(p); return; }
            }
            catch { Help(p); return; }
            switch (i)
            {
                case 1:
                    p.status1 = type;
                    p.status1c = (type == "custom") ? custom : "";
                    break;
                case 2:
                    p.status2 = type;
                    p.status2c = (type == "custom") ? custom : "";
                    break;
                case 3:
                    p.status3 = type;
                    p.status3c = (type == "custom") ? custom : "";
                    break;
            }
            p.UpdateStatusMessages();
            Player.SendMessage(p, "Your status message was changed to " + type + ((type == "custom") ? ": " + custom + "." : "."));
        }

        public override void Help(Player p)
        {
            if (p.extensions.Contains(Extension.MessageTypes))
            {
                Player.SendMessage(p, "/status <1/2/3> [type] - Sets one of your status messages to a server preset");
                Player.SendMessage(p, "/status <1/2/3> custom <message> - Sets one of your status messages to a custom message");
                Player.SendMessage(p, "Valid types: compass, game, motd, block, default, clear");
            }
            else
            {
                Player.SendMessage(p, "This command requires the MessageTypes extension.");
                Player.SendMessage(p, "See /cpe for more information.");
            }
        }
    }
}
