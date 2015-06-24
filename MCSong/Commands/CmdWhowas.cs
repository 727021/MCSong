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
using System.Collections.Generic;
using System.Data;
using System.IO;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;

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

            OfflinePlayer off = new OfflinePlayer(message);

            if (!off.seen/* || (off.seen & String.IsNullOrEmpty(off.ip))*/) { Player.SendMessage(p, Group.Find(FoundRank).color + message + Server.DefaultColor + " has the rank of " + Group.Find(FoundRank).color + FoundRank); return; }
            if (String.IsNullOrEmpty(off.title))
                Player.SendMessage(p, off.color + message + Server.DefaultColor + " has:");
            else
                Player.SendMessage(p, off.color + "[" + off.tcolor + off.title + off.color + "] " + message + Server.DefaultColor + " has:");
            Player.SendMessage(p, "> > the rank of " + Group.Find(FoundRank).color + FoundRank);
            try
            {
                if (!Group.Find("Nobody").commands.Contains("pay") && !Group.Find("Nobody").commands.Contains("give") && !Group.Find("Nobody").commands.Contains("take")) Player.SendMessage(p, "> > &a" + off.money + Server.DefaultColor + " " + Server.moneys);
            }
            catch { }
            Player.SendMessage(p, "> > &cdied &a" + off.deaths + Server.DefaultColor + " times");
            Player.SendMessage(p, "> > &bmodified &a" + off.blocks + Server.DefaultColor + " blocks.");
            Player.SendMessage(p, "> > was last seen on &a" + off.llogin);
            Player.SendMessage(p, "> > first logged into the server on &a" + off.flogin);
            Player.SendMessage(p, "> > logged in &a" + off.logins + Server.DefaultColor + " times, &c" + off.kicks + Server.DefaultColor + " of which ended in a kick.");
            Player.SendMessage(p, "> > " + Awards.awardAmount(message) + " awards");

            if (Server.bannedIP.Contains(off.ip))
                off.ip = "&8" + off.ip + ", which is banned";
            Player.SendMessage(p, "> > the IP of " + off.ip);
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