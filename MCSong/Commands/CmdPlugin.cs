using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSong
{
    public class CmdPlugin : Command
    {
        public override string name { get { return "plugin"; } }
        public override string[] aliases { get { return new string[] { "plugins" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool consoleUsable { get { return true; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            string[] args = message.Split(' ');
            if (args.Length < 1) { Help(p); return; }
            args[0] = args[0].ToLower();
            if (args[0] == "load" || args[0] == "l")
            {
                if (args.Length != 2) { Help(p); return; }
                try
                {
                    PluginManager.Load(args[1]);
                }
                catch (Exception e)
                {
                    Player.SendMessage(p, "Failed to load plugin:");
                    Player.SendMessage(p, e.Message);
                    return;
                }
            }
            if (args[0] == "unload" || args[0] == "ul" || args[0] == "u")
            {
                if (args.Length != 2) { Help(p); return; }
                if (PluginManager.Loaded(args[1]))
                {
                    try
                    {
                        PluginManager.Unload(PluginManager.loaded.Find(args[1]));
                    }
                    catch (Exception e)
                    {
                        Player.SendMessage(p, "Failed to unload plugin:");
                        Player.SendMessage(p, e.Message);
                    }
                }
            }
            if (args[0] == "info" || args[0] == "i" || args[0] == "about" || args[0] == "a")
            {
                if (args.Length == 1)
                {
                    Player.SendMessage(p, PluginManager.loaded.plugins.Count.ToString() + " plugins loaded:");
                    PluginManager.loaded.ForEach(delegate(Plugin pl)
                    {
                        Player.SendMessage(p, (PluginManager.loaded.plugins.IndexOf(pl) + 1).ToString() + " - " + pl.Name + " (" + pl.Version + ")");
                    });
                }
                else if (args.Length == 2)
                {
                    Plugin pl = PluginManager.loaded.Find(args[1]);
                    if (pl == null)
                    {
                        Player.SendMessage(p, "Could not find plugin.");
                        return;
                    }
                    Player.SendMessage(p, "Found plugin information:");
                    Player.SendMessage(p, pl.Name + " (" + pl.Version + ")");
                    if (!String.IsNullOrEmpty(pl.Author)) Player.SendMessage(p, "Author: " + pl.Author);
                    if (!String.IsNullOrEmpty(pl.Description)) Player.SendMessage(p, "Description: " + pl.Description);
                }
                else { Help(p); return; }
            }
            if (args[0] == "create" || args[0] == "n" || args[0] == "new")
            {
                if (args.Length != 2) { Help(p); return; }
                PluginManager.Create(args[1]);
                Player.SendMessage(p, "New skeleton plugin saved to extra/plugins/source/" + args[1] + "/");
            }
            if (args[0] == "compile" || args[0] == "comp")
            {
                if (args.Length != 2) { Help(p); return; }
                try
                {
                    PluginManager.Compile(args[1]);
                    Player.SendMessage(p, "Compiled successfully.");
                }
                catch (Exception e)
                {
                    Server.ErrorLog(e);
                    Player.SendMessage(p, "Compile error. Please check compile.log for more information.");
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/plugin <load/unload/create/compile/info> - Plugin system interaction");
        }

    }
}
