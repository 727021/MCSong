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

namespace MCSong
{
    public class CmdWhowas : Command
    {
        public override string name { get { return "whowas"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdWhowas() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player pl = Player.Find(message);
            if (pl != null && !pl.hidden)
            {
                Player.SendMessage(p, pl.color + pl.name + Server.DefaultColor + " is online, using /whois instead.");
                Command.all.Find("whois").Use(p, message);
                return;
            }

            string FoundRank = Group.findPlayer(message.ToLower());

            SQLiteHelper.SQLResult playerQuery = SQLiteHelper.ExecuteQuery($@"SELECT title, color, tcolor, money, deaths, blocks, first_login, last_login, logins, kicks, ip FROM Players WHERE name = '{message}';");
            if (playerQuery.rowsAffected <= 0)
            {
                Player.SendMessage(p, $"{Group.Find(FoundRank).color}{message}{Server.DefaultColor} has the rank of {Group.Find(FoundRank).color}{FoundRank}{Server.DefaultColor}.");
                return;
            }
            SQLiteHelper.SQLRow row = playerQuery[0];
            Player.SendMessage(p, $"{row["color"]}{(string.IsNullOrEmpty(row["title"]) ? "" : $"[{row["tcolor"]}{row["title"]}{row["color"]}] ")}{message}{Server.DefaultColor}:");
            Player.SendMessage(p, $"> > has the rank of {Group.Find(FoundRank).color}{FoundRank}");
            Player.SendMessage(p, $"> > $a{row["money"]}{Server.DefaultColor} {Server.moneys}");
            Player.SendMessage(p, $"> > &cdied &a{row["deaths"]}{Server.DefaultColor} times");
            Player.SendMessage(p, $"> > was last seen on &a{row["last_login"]}");
            Player.SendMessage(p, $"> > first logged into the server on &a{row["first_login"]}");
            Player.SendMessage(p, $"> > logged in &a{row["logins"]}{Server.DefaultColor} times, &c{row["kicks"]}{Server.DefaultColor} of which ended in a kick");
            Player.SendMessage(p, $"> > has &a{Awards.awardAmount(message)}{Server.DefaultColor} awards");
            Player.SendMessage(p, $"> > has the IP of {(Server.bannedIP.Contains(row["ip"]) ? $"&8{row["ip"]}{Server.DefaultColor}, which is banned" : row["ip"])}");
            if (Server.useWhitelist && Server.whiteList.Contains(message.ToLower()))
                Player.SendMessage(p, "> > Player is &fwhitelisted");
            if (Server.devs.Contains(message.ToLower()))
                Player.SendMessage(p, "> > Player is a &9developer");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whowas <name> - Displays information about someone who left.");
        }
    }
}