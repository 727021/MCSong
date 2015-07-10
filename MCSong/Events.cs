using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSong
{
    public sealed partial class Player
    {
        public bool noKick = false, noSendMessage = false, noJoin = false, noBlockchange = false;

        // Kick
        public delegate void OnPlayerKickedEventHandler(Player p, string reason);
        public static event OnPlayerKickedEventHandler OnPlayerKickedEvent = null;
        public delegate void OnKickedEventHandler(string reason);
        public event OnKickedEventHandler OnKickedEvent = null;
        // SendMessage
        public delegate void OnPlayerSendMessageEventHandler(Player p, string message);
        public static event OnPlayerSendMessageEventHandler OnPlayerSendMessageEvent = null;
        public delegate void OnSendMessageEventHandler(string message);
        public event OnSendMessageEventHandler OnSendMessageEvent = null;
        // Join
        public delegate void OnPlayerJoinEventHandler(Player p);
        public static event OnPlayerJoinEventHandler OnPlayerJoinEvent = null;
        // Blockchange
        public delegate void OnPlayerBlockchangeEventHandler(Player p, ushort x, ushort y, ushort z, byte type);
        public static event OnPlayerBlockchangeEventHandler OnPlayerBlockchangeEvent = null;
        public delegate void OnBlockchangeEventHandler(ushort x, ushort y, ushort z, byte type);
        public event OnBlockchangeEventHandler OnBlockchangeEvent = null;
    }

    public partial class Level
    {
        // Load
        public delegate void OnLevelLoadEventHandler(string name);
        public static event OnLevelLoadEventHandler OnLevelLoadEvent = null;
        // Unload
        public delegate void OnLevelUnloadEventHandler(string name);
        public static event OnLevelUnloadEventHandler OnLevelUnloadEvent = null;
        public delegate void OnUnloadEventHandler();
        public event OnUnloadEventHandler OnUnloadEvent = null;
        // Physics
        public delegate void OnPhysChangeEventHandler(int level);
        public event OnPhysChangeEventHandler OnPhysChangeEvent = null;
        public delegate void OnLevelPhysChangeEventHandler();// [TODO] add name/phys level to this one?
        public static event OnLevelPhysChangeEventHandler OnLevelPhysChangeEvent = null;
    }
    /*
     * 
     * Player Events:
     * MCSong.Player.OnPlayerKickedEvent(Player p, string reason)
     * #MCSong.Player.OnKickedEvent(string reason)
     * MCSong.Player.OnPlayerSendMessageEvent(Player p, string message)
     * #MCSong.Player.OnSendMessageEvent(string message)
     * MCSong.Player.OnPlayerJoinEvent(Player p)
     * MCSong.Player.OnPlayerBlockchangeEvent(Player p, ushort x, ushort y, ushort z, byte type)
     * #MCSong.Player.OnBlockchangeEvent(ushort x, ushort y, ushort z, byte type)
     * Booleans: noKick, noSendMessage, noJoin, noBlockchange
     * 
     * Level Events:
     * MCSong.Level.OnLevelLoadEvent(string name)
     * MCSong.Level.OnLevelUnloadEvent(string name)
     * #MCSong.Level.OnUnloadEvent()
     * MCSong.Level.OnLevelPhysChangeEvent(string name)
     * #MCSong.Level.OnPhysChangeEvent(int level)
     * Booleans:
     * 
     * Server Events:
     * 
     */
}
