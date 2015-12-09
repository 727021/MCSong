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
    public class CmdWeather : Command
    {
        public override string name { get { return "weather"; } }
        public override string[] aliases { get { return new string[] {  }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            message = message.Trim().ToLower();
            switch (message)
            {
                case "sun":
                    if (p.level.weather == Weather.SUNNY)
                    {
                        Player.SendMessage(p, "It's already sunny!");
                        return;
                    }
                    p.level.weather = Weather.SUNNY;
                    TellWeather(p.level, "The sun came out!");
                    break;
                case "rain":
                    if (p.level.weather == Weather.RAINING)
                    {
                        Player.SendMessage(p, "It's already raining!");
                        return;
                    }
                    p.level.weather = Weather.RAINING;
                    TellWeather(p.level, "It started to rain!");
                    break;
                case "snow":
                    if (p.level.weather == Weather.SNOWING)
                    {
                        Player.SendMessage(p, "It's already snowing!");
                        return;
                    }
                    p.level.weather = Weather.SNOWING;
                    TellWeather(p.level, "It started to snow!");
                    break;
                default:
                    Help(p);
                    return;
            }
            p.level.players.ForEach(delegate (Player pl)
            {
                pl.SendWeather(p.level.weather);
            });
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/weather [sun/rain/snow] - Sets the weather on your map");
        }

        private void TellWeather(Level l, string message)
        {
            l.players.ForEach(delegate (Player p)
            {
                if (p.cpe && p.extensions.Contains(Extension.EnvWeatherType))
                    Player.SendMessage(p, message);
            });
        }
    }
}