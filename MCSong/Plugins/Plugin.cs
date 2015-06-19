using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSong
{
    public abstract class Plugin
    {
        public abstract string Name { get; }
        public abstract string Author { get; }
        public abstract string Description { get; }
        public abstract string Version { get; }
        public abstract string SongVersion { get; }
        public abstract void OnLoad();
        public abstract void OnUnload();
    }
}
