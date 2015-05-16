using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCSong
{
    class CmdMaintenance : Command
    {
        public override string name { get { return "maintenance"; } }
        public override string[] aliases { get { return new string[] { "maint" }; } }
        public override CommandType type { get { return CommandType.Moderation; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdMaintenance() { }
        public override void Use(Player p, string message)
        {
            if (!Server.maintenanceMode)
            {
                Server.maintenanceMode = true;
                Player.GlobalMessage(c.purple + "MAINTENANCE MODE " + Server.DefaultColor + "has been turned " + c.green + "ON");
                Server.s.Log("MAINTENANCE MODE has been turned ON");
                if (Server.maintKick)
                {
                    Player.GlobalMessage("Kicking all players ranked below " + Level.PermissionToName(Server.maintPerm));
                    Server.s.Log("Kicking all players ranked below" + Level.PermissionToName(Server.maintPerm));
                    foreach (Player pl in Player.players)
                    {
                        if (p.group.Permission < Server.maintPerm)
                        {
                            p.Kick("Kicked for server maintenance!");
                        }
                    }
                }
                if (!Server.console)
                {
                    MCSong.Gui.Window.thisWindow.chkMaintenance.CheckState = CheckState.Checked;
                    MCSong.Gui.Window.thisWindow.chkMaintenance.Update();
                }
                
            }
            else if (Server.maintenanceMode)
            {
                Server.maintenanceMode = false;
                Player.GlobalMessage(c.purple + "MAINTENANCE MODE " + Server.DefaultColor + "has been turned " + c.red + "OFF");
                Server.s.Log("MAINTENANCE MODE has been turned OFF");
                if (!Server.console)
                {
                    MCSong.Gui.Window.thisWindow.chkMaintenance.CheckState = CheckState.Unchecked;
                    MCSong.Gui.Window.thisWindow.chkMaintenance.Update();
                }
            }
        }
        public override void Help(Player p)
        {
            p.SendMessage("/maintenance - Toggles maintenance mode" + ((Server.maintKick) ? " and kicks all players ranked below " + Level.PermissionToName(Server.maintPerm) : ""));
            p.SendMessage(c.purple + "MAINTENANCE MODE " + Server.DefaultColor + "is currently " + ((Server.maintenanceMode) ? c.green + "ON" : c.red + "OFF") + Server.DefaultColor + ".");
        }
    }
}
