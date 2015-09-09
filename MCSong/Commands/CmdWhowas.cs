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

            jDatabase.Table op = Server.s.database.GetTable("Players");
            int i;
            try { i = op.Rows.IndexOf(op.GetRow(new string[] { "Name" }, new string[] { message })); }
            catch { i = 0; }

            //OfflinePlayer off = new OfflinePlayer(message);

            if (i <= 0) { Player.SendMessage(p, Group.Find(FoundRank).color + message + Server.DefaultColor + " has the rank of " + Group.Find(FoundRank).color + FoundRank); return; }

            string title = op.GetValue(i, "Title");
            string color = op.GetValue(i, "Color");
            string tcolor = op.GetValue(i, "TColor");
            string money = op.GetValue(i, "Money");
            string deaths = op.GetValue(i, "TotalDeaths");
            string blocks = op.GetValue(i, "TotalBlocks");
            string llogin = op.GetValue(i, "LastLogin");
            string flogin = op.GetValue(i, "FirstLogin");
            string logins = op.GetValue(i, "TotalLogins");
            string kicks = op.GetValue(i, "TotalKicks");
            string ip = op.GetValue(i, "IP");

            if (String.IsNullOrEmpty(title))
                Player.SendMessage(p, color + message + Server.DefaultColor + " has:");
            else
                Player.SendMessage(p, color + "[" + tcolor + title + color + "] " + message + Server.DefaultColor + " has:");
            Player.SendMessage(p, "> > the rank of " + Group.Find(FoundRank).color + FoundRank);
            try
            {
                if (!Group.Find("Nobody").commands.Contains("pay") && !Group.Find("Nobody").commands.Contains("give") && !Group.Find("Nobody").commands.Contains("take")) Player.SendMessage(p, "> > &a" + money + Server.DefaultColor + " " + Server.moneys);
            }
            catch { }
            Player.SendMessage(p, "> > &cdied &a" + deaths + Server.DefaultColor + " times");
            Player.SendMessage(p, "> > &bmodified &a" + blocks + Server.DefaultColor + " blocks.");
            Player.SendMessage(p, "> > was last seen on &a" + llogin);
            Player.SendMessage(p, "> > first logged into the server on &a" + flogin);
            Player.SendMessage(p, "> > logged in &a" + logins + Server.DefaultColor + " times, &c" + kicks + Server.DefaultColor + " of which ended in a kick.");
            Player.SendMessage(p, "> > " + Awards.awardAmount(message) + " awards");

            if (Server.bannedIP.Contains(ip))
                ip = "&8" + ip + ", which is banned";
            Player.SendMessage(p, "> > the IP of " + ip);
            if (Server.useWhitelist)
            {
                if (Server.whiteList.Contains(message.ToLower()))
                {
                    Player.SendMessage(p, "> > Player is &fWhitelisted");
                }
            }
            if (Server.devs.Contains(message.ToLower()))
            {
                Player.SendMessage(p, Server.DefaultColor + "> > Player is a &9Developer");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whowas <name> - Displays information about someone who left.");
        }
    }
}