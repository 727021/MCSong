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
using System.Text;

namespace MCSong
{
    class CmdResetBot : Command
    {
        public override string name { get { return "resetbot"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Moderation; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdResetBot() { }

        public override void Use(Player p, string message)
        {
            if (message.ToLower() == "global")
            {
                Player.SendMessage(p, "Reloading the Global Chat bot...");
                //GlobalBot.Reset();
                Player.SendMessage(p, "Global Chat bot was reloaded.");
                return;
            }
            Player.SendMessage(p, "Reloading the IRCBot...");
            //IRCBot.Reset();
            Player.SendMessage(p, "IRCBot was reloaded.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/resetbot - reloads the IRCBot. FOR EMERGENCIES ONLY!");
            Player.SendMessage(p, "/resetbot global - reloads the Global Chat bot.");
        }
    }
}
