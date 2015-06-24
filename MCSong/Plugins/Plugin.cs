using System;

namespace MCSong
{
    public abstract class Plugin
    {
        /// <summary>
        /// The name of the plugin
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// The person (or people) who wrote the plugin
        /// </summary>
        public abstract string[] Authors { get; }
        /// <summary>
        /// What to display under /plugin info
        /// </summary>
        public abstract string Description { get; }
        /// <summary>
        /// The plugin's version
        /// </summary>
        public abstract string Version { get; }
        /// <summary>
        /// The version of MCSong the plugin is designed for
        /// </summary>
        public abstract string SongVersion { get; }
        /// <summary>
        /// Called when the plugin is loaded
        /// This is where event listeners, etc. should go
        /// </summary>
        public abstract void OnLoad();
        /// <summary>
        /// Called when the plugin is unloaded
        /// Save stuff here
        /// </summary>
        public abstract void OnUnload();
    }
}
