using System;
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
        public static readonly Extension ClickDistance = new Extension("ClickDistance", 1);
        public static readonly Extension CustomBlocks = new Extension("CustomBlocks", 1);

        public static bool supported(Extension e) { return all.Contains(e); }
        public static bool enabled(Extension e) { return Server.cpe.Contains(e); }
        public bool enabled() { return Server.cpe.Contains(this); }
        public override bool Equals(object obj)
        {
            if (obj is Extension)
            {
                Extension e = (Extension)obj;
                return (e.name.ToLower() == name.ToLower() && e.version == version);
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