﻿using System;
using System.Collections;

namespace MCSong
{
    public class Extension
    {
        public string name { get; private set; }
        public int version { get; private set; }
        private Extension(string n, int v)
        {
            name = n;
            version = v;
            all.Add(this);
        }
        public static readonly ExtensionList all = new ExtensionList();
        
        // Supported
        public static readonly Extension ClickDistance = new Extension("ClickDistance", 1);
        public static readonly Extension CustomBlocks = new Extension("CustomBlocks", 1);
        public static readonly Extension HackControl = new Extension("HackControl", 1);
        public static readonly Extension MessageTypes = new Extension("MessageTypes", 1);

        // Unsupported
        /*
        public static readonly Extension HeldBlock = new Extension("HeldBlock", 1);
        public static readonly Extension EmoteFix = new Extension("EmoteFix", 1);
        public static readonly Extension TextHotKey = new Extension("TextHotKey", 1);
        public static readonly Extension ExtPlayerList = new Extension("ExtPlayerList", 2);
        public static readonly Extension EnvColors = new Extension("EnvColors", 1);
        public static readonly Extension SelectionCuboid = new Extension("SelectionCuboid", 1);
        public static readonly Extension BlockPermissions = new Extension("BlockPermissions", 1);
        public static readonly Extension ChangeModel = new Extension("ChangeModel", 1);
        public static readonly Extension EnvMapAppearance = new Extension("EnvMapAppearance", 1);
        public static readonly Extension EnvWeatherType = new Extension("EnvWeatherType", 1);
        public static readonly Extension PlayerClick = new Extension("PlayerClick", 1);
        */

        public override bool Equals(object obj)
        {
            if (obj is Extension)
            {
                Extension e = (Extension)obj;
                return (e.name == name && e.version == version);
            }
            return false;
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