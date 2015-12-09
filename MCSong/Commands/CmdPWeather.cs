/*
	Copyright 2015 MCSong, Licensed under the
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
    public class CmdPWeather : Command
    {
        public override string name { get { return "pweather"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override void Use(Player p, string message)
        {
            if (!p.cpe || !p.extensions.Contains(Extension.EnvWeatherType)) { Help(p); return; }
            message = message.Trim().ToLower();
            switch (message)
            {
                case "sun":
                    p.SendWeather(Weather.SUNNY);
                    Player.SendMessage(p, "The sun came out!");
                    break;
                case "rain":
                    p.SendWeather(Weather.RAINING);
                    Player.SendMessage(p, "It started to rain!");
                    break;
                case "snow":
                    p.SendWeather(Weather.SNOWING);
                    Player.SendMessage(p, "It started to snow!");
                    break;
                default:
                    Help(p);
                    return;
            }
        }
        public override void Help(Player p)
        {
            if (!p.cpe || !p.extensions.Contains(Extension.EnvWeatherType))
            {
                Player.SendMessage(p, "Your client doesn't support weather! See &2/cpe" + Server.DefaultColor + " for more information.");
                return;
            }
            Player.SendMessage(p, "/pweather [sun/rain/snow] - Changes your weather");
        }
    }
}