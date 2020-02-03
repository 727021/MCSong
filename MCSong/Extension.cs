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
using System.Collections;
using System.Collections.Generic;

namespace MCSong
{
    public class Extension
    {
        public string name { get; private set; }
        public int version { get; private set; }
        public string[] description { get; private set; }
        public bool implemented { get; private set; }
        private Extension(string n, int v, string[] d, bool i = true)
        {
            name = n;
            version = v;
            description = d;
            implemented = i;
            all.Add(this);
            names.Add(n.ToLower());
        }
        public static readonly ExtensionList all = new ExtensionList();
        public static readonly List<string> names = new List<string>();
        
        // Supported
        public static readonly Extension ClickDistance = new Extension("ClickDistance", 1, new string[] { "-Can restrict or extend a player's reach, up to 1023 blocks or down to 0" });
        public static readonly Extension CustomBlocks = new Extension("CustomBlocks", 1, new string[] { "-Adds new visually distinct blocks to the game", "-There are currently 16 custom blocks:", "-Cobblestone Slab, Rope, Sandstone, Snow, Fire, Light Pink Wool, Forest Green Wool, Brown Wool, Deep Blue Wool, Turquoise Wool, Ice, Ceramic Tile, Magma, Pillar, Crate, Stone Brick" });
        public static readonly Extension HackControl = new Extension("HackControl", 1, new string[] { "-Allows servers to control which client cheats/hacks can be used" });
        public static readonly Extension MessageTypes = new Extension("MessageTypes", 1, new string[] { "-Adds new on-screen message types, including centered announcements and status messages in the top right corner" });
        public static readonly Extension EnvWeatherType = new Extension("EnvWeatherType", 1, new string[] { "-Allows servers to create rain and snow" });
        public static readonly Extension HeldBlock = new Extension("HeldBlock", 1, new string[] { "-Allows the server to know which block a player is holding, to improve features like /cuboid and /follow" });
        public static readonly Extension EnvColors = new Extension("EnvColors", 1, new string[] { "-Allows the server to make maps more unique by altering environment colors" });

        // Unsupported
        
        public static readonly Extension EmoteFix = new Extension("EmoteFix", 1, new string[] { "-Improves the appearance of emotes (smileys) in chat" }, false);
        public static readonly Extension TextHotKey = new Extension("TextHotKey", 1, new string[] { "-Allows players to set hotkeys for certain server commands" }, false);
        public static readonly Extension ExtPlayerList = new Extension("ExtPlayerList", 2, new string[] { "-Provides more flexibility in naming of players and loading of skins, autocompletion, and player tab-list display" }, false);
        public static readonly Extension SelectionCuboid = new Extension("SelectionCuboid", 1, new string[] { "-Allows players to see highlighted selections in the map when using building commands" }, false);
        public static readonly Extension BlockPermissions = new Extension("BlockPermissions", 1, new string[] { "-Prevents players from placing/breaking certain block types" }, false);
        public static readonly Extension ChangeModel = new Extension("ChangeModel", 1, new string[] { "-Lets servers spawn animal and monster models" }, false);
        public static readonly Extension EnvMapAppearance = new Extension("EnvMapAppearance", 1, new string[] { "-Allows customization of map edge textures" }, false);
        public static readonly Extension PlayerClick = new Extension("PlayerClick", 1, new string[] { "-Allows servers to receive details of every mouse click players make" }, false);
        

        public override bool Equals(object obj)
        {
            if (obj is Extension)
            {
                Extension e = (Extension)obj;
                return (e.name == name && e.version == version);
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1545369197;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + version.GetHashCode();
            return hashCode;
        }
    }
    public class ExtensionList : CollectionBase
    {
        public void Add(Extension e) { List.Add(e); }
        public Extension Find(string name)
        {
            foreach (Extension e in List)
            {
                if (e.name.ToLower() == name.ToLower())
                    return e;
            }
            return null;
        }
        public bool Contains(Extension e)
        {
            return List.Contains(e);
        }
        public override string ToString()
        {
            if (List.Count == 0)
                return "none";
            string temp = "";
            foreach (Extension e in this)
            {
                temp += "," + e.name;
            }
            return temp.Remove(0, 1);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}