/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCSong) Licensed under the
	Educational Community License, Version 2.0 (the "License"); you may
	not use this file except in compliance with the License. You may
	obtain a copy of the License at
	
	http://www.osedu.org/licenses/ECL-2.0
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the License is distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the License for the specific language governing
	permissions and limitations under the License.
*/
using System;
using System.Data;
using System.Collections.Generic;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

namespace MCSong
{
    public class CmdAbout : Command
    {
        public override string name { get { return "about"; } }
        public override string[] aliases { get { return new string[] { "b" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdAbout() { }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "Break/build a block to display information.");
            p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(AboutBlockchange);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/about - Displays information about a block.");
        }

        public void AboutBlockchange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands) p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            if (b == Block.Zero) { Player.SendMessage(p, "Invalid Block(" + x + "," + y + "," + z + ")!"); return; }
            p.SendBlockchange(x, y, z, b);

            string message = "Block (" + x + "," + y + "," + z + "): ";
            message += "&f" + b + " = " + Block.Name(b);
            Player.SendMessage(p, message + Server.DefaultColor + ".");
            message = p.level.foundInfo(x, y, z);
            if (message != "") Player.SendMessage(p, "Physics information: &a" + message);

            string Username, TimePerformed, BlockUsed;
            bool Deleted, foundOne = false;

            SQLiteHelper.SQLResult blockQuery = SQLiteHelper.ExecuteQuery($@"SELECT username,when,x,y,z,type,deleted FROM Blocks{p.level.name} WHERE x = {x} AND y = {y} AND z = {z};");
            for (int i = 0; i < blockQuery.rowsAffected; i++)
            {
                foundOne = true;
                Player.SendMessage(p, (blockQuery[i]["deleted"].ToLower() == "true" ? "&3Created by " : "&4Deleted by ") + $"{Server.FindColor(blockQuery[i]["username"])}{blockQuery[i]["username"]}{Server.DefaultColor}, using &3{Block.Name(byte.Parse(blockQuery[i]["type"]))}");
            }

            List<Level.BlockPos> inCache = p.level.blockCache.FindAll(bP => bP.x == x && bP.y == y && bP.z == z);

            for (int i = 0; i < inCache.Count; i++)
            {
                foundOne = true;
                Deleted = inCache[i].deleted;
                Username = inCache[i].name;
                TimePerformed = inCache[i].TimePerformed.ToString("yyyy-MM-dd HH:mm:ss");
                BlockUsed = Block.Name(inCache[i].type);

                if (!Deleted)
                    Player.SendMessage(p, "&3Created by " + Server.FindColor(Username.Trim()) + Username.Trim() + Server.DefaultColor + ", using &3" + BlockUsed);
                else
                    Player.SendMessage(p, "&4Destroyed by " + Server.FindColor(Username.Trim()) + Username.Trim() + Server.DefaultColor + ", using &3" + BlockUsed);
                Player.SendMessage(p, "Date and time modified: &2" + TimePerformed);
            }

            if (!foundOne)
                Player.SendMessage(p, "This block has not been modified since the map was cleared.");
 
            //Blocks.Dispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}