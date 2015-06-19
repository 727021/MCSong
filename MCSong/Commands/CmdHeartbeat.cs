/*
	Copyright 2010 MCSong Team - Written by Valek
 
    Licensed under the
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
using System.IO;

namespace MCSong
{
    class CmdHeartbeat : Command
    {
        public override string name { get { return "heartbeat"; } }
        public override string[] aliases { get { return new string[] { "beat" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }
        public CmdHeartbeat() { }

        public override void Use(Player p, string message)
        {
            if (p != null && !Server.devs.Contains(p.name.ToLower()))
            {
                Player.SendMessage(p, "This command can only be used by developers!");
                return;
            }
            if (p == null && Directory.GetCurrentDirectory() != "FROSTEDBUTTS")
            {
                Player.SendMessage(null, "This command can only be used by developers!");
                return;
            }
            try
            {
                SongBeat.Pump(BeatType.MCSong);
            }
            catch (Exception e)
            {
                Server.s.Log("Error with MCSong pump.");
                Server.ErrorLog(e);
            }
            Player.SendMessage(p, "Heartbeat pump sent.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/heartbeat - Forces a pump for the MCSong heartbeat.  DEBUG PURPOSES ONLY.");
        }
    }
}
