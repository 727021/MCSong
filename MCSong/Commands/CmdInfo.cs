/*Q
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
    public class CmdInfo : Command
    {
        public override string name { get { return "info"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Information; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdInfo() { }

        public override void Use(Player p, string message)
        {
            if (message != "") 
            { 
                Help(p); 
            }
            else
            {
                Player.SendMessage(p, "This server runs on &bMCSong" + Server.DefaultColor + ", a fork of MCLawl, which was developed by Lawlcat, Valek, and Zallist.");
                Player.SendMessage(p, "This server's version: &a" + Server.Version);
                Player.SendMessage(p, "MCSong Webite/Forums: http://mcsong.x10.mx/");
                string devlist = "";
                string temp;
                foreach (string dev in Server.devs)
                {
                    temp = dev.Substring(0, 1);
                    temp = temp.ToUpper() + dev.Remove(0, 1);
                    devlist += temp + ", ";
                }
                devlist = devlist.Remove(devlist.Length - 2);
                Player.SendMessage(p, "&9MCSong Development Team: " + Server.DefaultColor + devlist);

                TimeSpan up = DateTime.Now - Server.timeOnline;
                string upTime = "Time online: &b";
                if (up.Days == 1) upTime += up.Days + " day, ";
                else if (up.Days > 0) upTime += up.Days + " days, ";
                if (up.Hours == 1) upTime += up.Hours + " hour, ";
                else if (up.Days > 0 || up.Hours > 0) upTime += up.Hours + " hours, ";
                if (up.Minutes == 1) upTime += up.Minutes + " minute and ";
                else if (up.Hours > 0 || up.Days > 0 || up.Minutes > 0) upTime += up.Minutes + " minutes and ";
                if (up.Seconds == 1) upTime += up.Seconds + " second";
                else upTime += up.Seconds + " seconds";
                Player.SendMessage(p, upTime);

                if (Server.updateTimer.Interval > 1000) Player.SendMessage(p, "Server is currently in &5Low Lag" + Server.DefaultColor + " mode.");
                Player.SendMessage(p, c.purple + "MAINTENANCE MODE " + Server.DefaultColor + "is currently " + ((Server.maintenanceMode) ? c.green + "ON" : c.red + "OFF") + Server.DefaultColor + ".");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/info - Displays the server information.");
        }
    }
}
