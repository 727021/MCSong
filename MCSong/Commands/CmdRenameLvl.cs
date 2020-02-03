using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCSong
{
    public class CmdRenameLvl : Command
    {
        public override string name { get { return "renamelvl"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Moderation; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdRenameLvl() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1) { Help(p); return; }
            Level foundLevel = Level.Find(message.Split(' ')[0]);
            string newName = message.Split(' ')[1];

            if (File.Exists("levels/" + newName)) { Player.SendMessage(p, "Level already exists."); return; }
            if (foundLevel == Server.mainLevel) { Player.SendMessage(p, "Cannot rename the main level."); return; }
            if (foundLevel != null) foundLevel.Unload();

            try
            {
                File.Move("levels/" + foundLevel.name + ".lvl", "levels/" + newName + ".lvl");

                try
                {
                    File.Move("levels/level properties/" + foundLevel.name + ".properties", "levels/level properties/" + newName + ".properties");
                }
                catch { }
                try
                {
                    File.Move("levels/level properties/" + foundLevel.name, "levels/level properties/" + newName + ".properties");
                }
                catch { }

                SQLiteHelper.ExecuteQuery($@"ALTER TABLE Blocks{foundLevel.name} RENAME TO Blocks{newName};");
                SQLiteHelper.ExecuteQuery($@"ALTER TABLE Portals{foundLevel.name} RENAME TO Portals{newName};");
                SQLiteHelper.ExecuteQuery($@"ALTER TABLE Messages{foundLevel.name} RENAME TO Messages{newName};");
                SQLiteHelper.ExecuteQuery($@"ALTER TABLE Zones{foundLevel.name} RENAME TO Zones{newName};");
                Player.GlobalMessage("Renamed " + foundLevel.name + " to " + newName);
            }
            catch (Exception e) { Player.SendMessage(p, "Error when renaming."); Server.ErrorLog(e); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/renamelvl <level> <new name> - Renames <level> to <new name>");
            Player.SendMessage(p, "Portals going to <level> will be lost");
        }
    }
}