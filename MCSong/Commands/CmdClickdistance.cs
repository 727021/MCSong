using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSong
{
    class CmdClickdistance : Command
    {
        public override string name { get { return "clickdistance"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override CommandType type { get { return CommandType.Moderation; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (!Server.cpeClickDistance) { Player.SendMessage(p, "Click distance is disabled on the server. See &2/cpe " + Server.DefaultColor + "for more information."); return; }
            string[] args = message.Split(' ');
            if (args.Length != 2) { Help(p); return; }
            Player who = Player.Find(args[0]);
            if (who == null) { Player.SendMessage(p, "Could not find player &2" + args[1]); return; }
            if (!who.cpeClickDistance) { Player.SendMessage(p, ((who == p) ? "Your" : who.color + who.name + Server.DefaultColor + "'s") + " client does not support click distance. See &2/cpe " + Server.DefaultColor + "for more information."); return; }
            double d;
            try { d = Double.Parse(args[1]); }
            catch { Player.SendMessage(p, "Click distance is invalid."); return; }
            short c = ((short)(d * 32));
            who.SendClickDistance(c);
            if (who == p)
            {
                Player.SendMessage(p, "Your click distance was set to " + d + "blocks.");
                return;
            }
            Player.SendMessage(p, who.color + who.name + Server.DefaultColor + "'s click distance was set to " + d + " blocks.");
            Player.SendMessage(who, p.color + p.name + Server.DefaultColor + " set your click distance to " + d + " blocks.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clickdistance <player> <blocks> - Sets a player's click distance");
            if (!Server.cpeClickDistance) { Player.SendMessage(p, "Click distance is disabled on the server. See &2/cpe " + Server.DefaultColor + "for more information."); }
        }
    }
}
