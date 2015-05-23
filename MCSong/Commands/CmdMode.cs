using System;
using System.IO;

namespace MCSong
{
    public class CmdMode : Command
    {
        public override string name { get { return "mode"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Building; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdMode() { }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p.modeType != 0)
                {
                    Player.SendMessage(p, "&b" + Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor + " mode: &cOFF");
                    p.modeType = 0;
                    p.BlockAction = 0;
                }
                else
                {
                    Help(p); return;
                }
            }
            else
            {
                byte b = Block.Byte(message);
                if (b == Block.Zero) { Player.SendMessage(p, "Could not find block given."); return; }
                if (b == Block.air) { Player.SendMessage(p, "Cannot use Air Mode."); return; }
                if (!Block.canPlace(p, b)) { Player.SendMessage(p, "Cannot place this block at your rank."); return; }

                if (p.modeType == b)
                {
                    Player.SendMessage(p, "&b" + Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor + " mode: &cOFF");
                    p.modeType = 0;
                    p.BlockAction = 0;
                }
                else
                {
                    p.BlockAction = 6;
                    p.modeType = b;
                    Player.SendMessage(p, "&b" + Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor + " mode: &aON");
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mode [block] - Makes every block placed into [block].");
            Player.SendMessage(p, "/[block] also works");
        }
    }
}