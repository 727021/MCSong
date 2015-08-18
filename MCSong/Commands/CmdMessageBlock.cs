using System;

namespace MCSong
{
    public class CmdMessageBlock : Command
    {
        public override string name { get { return "mb"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Building; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdMessageBlock() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            CatchPos cpos = new CatchPos();
            cpos.message = "";
            bool typeSet = false;
            try
            {
                switch (message.Split(' ')[0].ToLower())
                {
                    case "air": cpos.block = Block.MsgAir; break;
                    case "water": cpos.block = Block.MsgWater; break;
                    case "lava": cpos.block = Block.MsgLava; break;
                    case "black": cpos.block = Block.MsgBlack; break;
                    case "white": cpos.block = Block.MsgWhite; break;
                    case "show": showMBs(p); return;
                    case "chat": typeSet = true; cpos.block = Block.MsgWhite; cpos.type = MessageType.CHAT; cpos.message = message.Replace(message.Split(' ')[0] + " ", ""); break;
                    case "announce": typeSet = true; cpos.block = Block.MsgWhite; cpos.type = MessageType.ANNOUNCEMENT; cpos.message = message.Replace(message.Split(' ')[0] + " ", ""); break;
                    default: cpos.block = Block.MsgWhite; cpos.type = MessageType.CHAT; cpos.message = message; break;
                }
            }
            catch { cpos.block = Block.MsgWhite; cpos.type = MessageType.CHAT; cpos.message = message; }

            if (!typeSet)
            {
                switch(message.Split(' ')[1])
                {
                    case "chat": cpos.type = MessageType.CHAT; break;
                    case "announce": cpos.type = MessageType.ANNOUNCEMENT; break;
                    default: cpos.type = MessageType.CHAT; cpos.message = message; break;
                }
            }

            if (cpos.message == "") cpos.message = message.Substring(message.IndexOf(' ') + 1);
            p.blockchangeObject = cpos;

            Player.SendMessage(p, "Place where you wish the message block to go."); p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mb [block] [type] <message> - Places a message in your next block.");
            Player.SendMessage(p, "Valid blocks: white, black, air, water, lava");
            Player.SendMessage(p, "Valid types: chat, announce");
            Player.SendMessage(p, "/mb show shows or hides MBs");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            CatchPos cpos = (CatchPos)p.blockchangeObject;

            /*
            DataTable Messages = MySQL.fillData("SELECT * FROM `Messages" + p.level.name + "` WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z);
            Messages.Dispose();

            if (Messages.Rows.Count == 0)
            {
                MySQL.executeQuery("INSERT INTO `Messages" + p.level.name + "` (X, Y, Z, Message) VALUES (" + (int)x + ", " + (int)y + ", " + (int)z + ", '" + cpos.message + "')");
            }
            else
            {
                MySQL.executeQuery("UPDATE `Messages" + p.level.name + "` SET Message='" + cpos.message + "' WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z);
            }
            */

            if (p.level.getMB(x, y, z).type <= -1)
            {
                p.level.MBList.Remove(p.level.getMB(x, y, z));
            }
            Level.MessageBlock m = new Level.MessageBlock() { X = x, Y = y, Z = z, type = (int)cpos.type, message = cpos.message.Replace('|', ':') };
            p.level.MBList.Add(m);

            Player.SendMessage(p, "Message block placed.");
            p.level.Blockchange(p, x, y, z, cpos.block);
            p.SendBlockchange(x, y, z, cpos.block);

            if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }

        struct CatchPos { public string message; public byte block; public MessageType type; }

        public void showMBs(Player p)
        {
            p.showMBs = !p.showMBs;

            /*DataTable Messages = new DataTable("Messages");
            Messages = MySQL.fillData("SELECT * FROM `Messages" + p.level.name + "`");

            int i;

            if (p.showMBs)
            {
                for (i = 0; i < Messages.Rows.Count; i++)
                    p.SendBlockchange((ushort)Messages.Rows[i]["X"], (ushort)Messages.Rows[i]["Y"], (ushort)Messages.Rows[i]["Z"], Block.MsgWhite);
                Player.SendMessage(p, "Now showing &a" + i.ToString() + Server.DefaultColor + " MBs.");
            }
            else
            {
                for (i = 0; i < Messages.Rows.Count; i++)
                    p.SendBlockchange((ushort)Messages.Rows[i]["X"], (ushort)Messages.Rows[i]["Y"], (ushort)Messages.Rows[i]["Z"], p.level.GetTile((ushort)Messages.Rows[i]["X"], (ushort)Messages.Rows[i]["Y"], (ushort)Messages.Rows[i]["Z"]));
                Player.SendMessage(p, "Now hiding MBs.");
            }
            Messages.Dispose();*/
            if (p.showMBs)
            {
                foreach (Level.MessageBlock m in p.level.MBList)
                    p.SendBlockchange(m.X, m.Y, m.Z, Block.MsgWhite);
                Player.SendMessage(p, "Now showing &a" + p.level.MBList.Count + Server.DefaultColor + " MBs.");
            }
            else
            {
                foreach (Level.MessageBlock m in p.level.MBList)
                    p.SendBlockchange(m.X, m.Y, m.Z, p.level.GetTile(m.X, m.Y, m.Z));
                Player.SendMessage(p, "Now hiding MBs.");
            }
        }
    }
}