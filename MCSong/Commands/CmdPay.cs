using System;

namespace MCSong
{
    public class CmdPay : Command
    {
        public override string name { get { return "pay"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdPay() { }

        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') == -1) { Help(p); return; }
            if (message.Split(' ').Length != 2) { Help(p); return; }

            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { Player.SendMessage(p, "Could not find player entered"); return; }
            if (who == p) { Player.SendMessage(p, "Sorry. Can't allow you to pay yourself"); return; }

            int amountPaid;
            try { amountPaid = int.Parse(message.Split(' ')[1]); }
            catch { Player.SendMessage(p, "Invalid amount"); return; }

            if (who.money + amountPaid > 16777215) { Player.SendMessage(p, "Players cannot have over 16777215 " + Server.moneys); return; }
            if (p.money - amountPaid < 0) { Player.SendMessage(p, "You don't have that much " + Server.moneys); return; }
            if (amountPaid < 0) { Player.SendMessage(p, "Cannot pay negative " + Server.moneys); return; }

            who.money += amountPaid;
            p.money -= amountPaid;
            Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " paid " + who.color + who.name + Server.DefaultColor + " " + amountPaid + " " + Server.moneys);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pay [player] <amount> - Pays <amount> of " + Server.moneys + " to [player]");
        }
    }
}