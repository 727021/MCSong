using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSong
{
    public class PluginList
    {
        public List<Plugin> plugins = new List<Plugin>();
        public PluginList() { }
        public void Add(Plugin plugin) { plugins.Add(plugin); }
        public void AddRange(List<Plugin> plugins)
        {
            plugins.ForEach(delegate(Plugin p) { this.plugins.Add(p); });
        }
        public bool Remove(Plugin plugin) { return plugins.Remove(plugin); }
        public bool Contains(Plugin plugin) { return plugins.Contains(plugin); }
        public bool Contains(string name)
        {
            foreach (Plugin p in plugins)
                if (p.Name.ToLower() == name.ToLower())
                    return true;
            return false;
        }
        public Plugin Find(string name)
        {
            foreach (Plugin p in plugins)
                if (p.Name.ToLower() == name.ToLower())
                    return p;
            return null;
        }
        public void ForEach(Action<Plugin> action)
        {
            plugins.ForEach(action);
        }
    }
}
