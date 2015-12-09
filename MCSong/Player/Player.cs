/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCSong) Licensed under the
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
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Data;

namespace MCSong
{
    public enum MessageType
    {
        CHAT = 0,
        STATUS_TOP = 1,
        STATUS_MIDDLE = 2,
        STATUS_BOTTOM = 3,
        BOTTOM_BOTTOM = 11,
        BOTTOM_MIDDLE = 12,
        BOTTOM_TOP = 13,
        ANNOUNCEMENT = 100
    }

    public enum Magic
    {
        IDENTIFICATION = 0,
        PING = 1,
        LEVEL_INIT = 2,
        LEVEL_DATA = 3,
        LEVEL_FINALIZE = 4,
        PLAYER_SET_BLOCK = 5,
        BLOCK_CHANGE = 6,
        SPAWN_PLAYER = 7,
        POSITION_ROTATION = 8,
        POSITION_ROTATION_UPDATE = 9,
        POSITION_UPDATE = 10,
        ROTATION_UPDATE = 11,
        DESPAWN_PLAYER = 12,
        CHAT_MESSAGE = 13,
        DISCONNECT = 14,
        UPDATE_PLAYER_TYPE = 15,
        // CPE
        EXTINFO = 16,
        EXTENTRY = 17,
        CLICK_DISTANCE = 18,
        CUSTOM_BLOCK_SUPPORT_LEVEL = 19,
        HOLD_THIS = 20,
        SET_TEXT_HOTKEY = 21,
        EXT_ADD_PLAYERNAME = 22,
        EXT_ADD_ENTITY = 23,
        EXT_REMOVE_PLAYERNAME = 24,
        ENV_SET_COLOR = 25,
        SELECTION_CUBOID = 26,
        REMOVE_SELECTION_CUBOID = 27,
        SET_BLOCK_PERMISSIONS = 28,
        CHANGE_MODEL = 29,
        ENV_SET_MAP_APPEARANCE = 30,
        ENV_SET_WEATHER_TYPE = 31,
        HACK_CONTROL = 32,
        EXT_ADD_ENTITY2 = 33
    }

    public sealed partial class Player
    {
        public static List<Player> players = new List<Player>();
        public static Dictionary<string, string> left = new Dictionary<string, string>();
        public static List<Player> connections = new List<Player>(Server.players);
        public static List<string> emoteList = new List<string>();
        public static int totalMySQLFailed = 0;
        public static byte number { get { return (byte)players.Count; } }
        static System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        public static bool storeHelp = false;
        public static string storedHelp = "";

        Socket socket;
        System.Timers.Timer loginTimer = new System.Timers.Timer(1000);
        public System.Timers.Timer pingTimer = new System.Timers.Timer(2000);
        System.Timers.Timer extraTimer = new System.Timers.Timer(22000);
        public System.Timers.Timer afkTimer = new System.Timers.Timer(2000);
        public int afkCount = 0;
        public DateTime afkStart;

        public bool megaBoid = false;
        public bool cmdTimer = false;

        byte[] buffer = new byte[0];
        byte[] tempbuffer = new byte[0xFF];
        public bool disconnected = false;

        public string name;
        public string realName;
        public byte id;
        public int userID = -1;
        public string ip;
        public string color = "";
        public Group group;
        public bool hidden = false;
        public bool painting = false;
        public bool muted = false;
        public bool jailed = false;
        public bool invincible = false;
        public string prefix = "";
        public string title = "";
        public string titlecolor = "";

        public bool deleteMode = false;
        public bool ignorePermission = false;
        public bool ignoreGrief = false;
        public bool parseSmiley = true;
        public bool smileySaved = true;
        public bool opchat = false;
        public bool adminchat = false;
        public bool gcRead = false;
        public bool onWhitelist = false;
        public bool whisper = false;
        public string whisperTo = "";

        public string storedMessage = "";

        public bool trainGrab = false;
        public bool onTrain = false;

        public bool frozen = false;
        public string following = "";
        public string possess = "";
        
        // Only used for possession.
        //Using for anything else can cause unintended effects!
        public bool canBuild = true;

        public int money = 0;
        public Int64 overallBlocks = 0;
        public int loginBlocks = 0;

        public DateTime timeLogged;
        public DateTime firstLogin;
        public DateTime lastLogin;
        public int totalLogins = 0;
        public int totalKicked = 0;
        public int overallDeath = 0;

        public string savedcolor = "";

        public bool staticCommands = false;

        public DateTime ZoneSpam;
        public bool ZoneCheck = false;
        public bool zoneDel = false;

        public Thread commThread;
        public bool commUse = false;

        public bool aiming;
        public bool isFlying = false;

        public bool joker = false;

        public bool voice = false;
        public string voicestring = "";

        public string agreestring = "";

        //CTF
        public Team team;
        public Team hasflag;
        public string CTFtempcolor;
        public string CTFtempprefix;
        public bool carryingFlag;
        public bool spawning = false;
        public bool teamchat = false;
        public int health = 100;

        // CPE
        public string clientName = "Mojang Client";
        public bool cpe = false;
        public short cpeCount = 0;
        private int cpeExtSent = 0;

        public ExtensionList extensions = new ExtensionList();
        public byte CustomBlockSupportLevel = 0;

        public short clickDistance = 160;

        public string status1 = "clear";
        public string status1c = "";
        public string status2 = "clear";
        public string status2c = "";
        public string status3 = "clear";
        public string status3c = "";
        public void UpdateStatusMessages()
        {
            string message = "";
            switch (status1.ToLower())
            {
                case "clear":
                    message = " ";
                    break;
                case "custom":
                    message = status1c;
                    break;
                case "compass":
                    message = "[" + Compass(rot[0] / (int)(255 / (compass.Length - 1))) + "]";
                    break;
                case "game":
                    if (level.ctfmode && team != null) message = "&fCTF: " + team.color + team.teamstring + " &fPoints: " + team.color + team.points;
                    else message = "&fNo Team";
                    break;
                case "block":
                    message = BlockInfo();
                    break;
                case "motd":
                    message = (level.motd == "ignore") ? Server.motd : level.motd;
                    break;
                case "default":
                    message = "&f" + Server.moneys + ": &a" + money + " &fLevel: " + Group.findPerm(level.permissionvisit).color + level.name + " (" + level.physics + ")";
                    break;
            }
            SendMessage(this, message, MessageType.STATUS_TOP);
            switch (status2.ToLower())
            {
                case "clear":
                    message = " ";
                    break;
                case "custom":
                    message = status2c;
                    break;
                case "compass":
                    message = "[" + Compass(rot[0] / (int)(255 / (compass.Length - 1))) + "]";
                    break;
                case "game":
                    if (level.ctfmode && team != null) message = "&fCTF: " + team.color + team.teamstring + " &fPoints: " + team.color + team.points;
                    else message = "&fNo Team";
                    break;
                case "block":
                    message = BlockInfo();
                    break;
                case "motd":
                    message = (level.motd == "ignore") ? Server.motd : level.motd;
                    break;
                case "default":
                    message = "&f" + Server.moneys + ": &a" + money + " &fLevel: " + Group.findPerm(level.permissionvisit).color + level.name + " (" + level.physics + ")";
                    break;
            }
            SendMessage(this, message, MessageType.STATUS_MIDDLE);
            switch (status3.ToLower())
            {
                case "clear":
                    message = " ";
                    break;
                case "custom":
                    message = status3c;
                    break;
                case "compass":
                    message = "[" + Compass(rot[0] / (int)(255 / (compass.Length - 1))) + "]";
                    break;
                case "game":
                    if (level.ctfmode && team != null) message = "&fCTF: " + team.color + team.teamstring + " &fPoints: " + team.color + team.points;
                    else message = "&fNo Team";
                    break;
                case "block":
                    message = BlockInfo();
                    break;
                case "motd":
                    message = (level.motd == "ignore") ? Server.motd : level.motd;
                    break;
                case "default":
                    message = "&f" + Server.moneys + ": &a" + money + " &fLevel: " + Group.findPerm(level.permissionvisit).color + level.name + " (" + level.physics + ")";
                    break;
            }

            SendMessage(this, message, MessageType.STATUS_BOTTOM);
        }
        private string compass = " -NW- | -N- | -NE- | -E- | -SE- | -S- | -SW- | -W- |";
        public string Compass(int start)
        {
            int l = 19; //Length of substring
            if (start + l > compass.Length)
            {
                string sub = compass.Substring(start, compass.Length - start);
                sub += compass.Substring(0, l - (compass.Length - start));
                return sub;
            }
            return compass.Substring(start, l);
        }
        public string BlockInfo()
        {
            try
            {
                double x = 0, y = 0, z = 0;
                byte block = 0;
                double a = Math.Sin(((double)(128 - rot[0]) / 256) * 2 * Math.PI);
                double b = Math.Cos(((double)(128 - rot[0]) / 256) * 2 * Math.PI);
                double c = Math.Cos(((double)(rot[1] + 64) / 256) * 2 * Math.PI);
                double d = Math.Cos(((double)(rot[1]) / 256) * 2 * Math.PI);
                for (byte i = 0; i < ((extensions.Contains(Extension.ClickDistance)) ? Math.Round((decimal)clickDistance / 32) : 5); i++)
                {
                    x = Math.Round((pos[0] / 32) + (double)(a * i * d));
                    y = Math.Round((pos[1] / 32) + (double)(c * i));
                    z = Math.Round((pos[2] / 32) + (double)(b * i * d));
                    block = level.GetTile((ushort)x, (ushort)y, (ushort)z);
                    if (block != Block.air && i < ((extensions.Contains(Extension.ClickDistance)) ? Math.Floor((decimal)clickDistance / 32) : 5))
                        break;
                }
                return "&fBlock: &c" + Block.Name((extensions.Contains(Extension.CustomBlocks) ? block : Block.Fallback(block))) + " &fX/Y/Z: &c" + x + "/" + y + "/" + z;
            }
            catch { return ""; }
        }

        //Copy
        public List<CopyPos> CopyBuffer = new List<CopyPos>();
        public struct CopyPos { public ushort x, y, z; public byte type; }
        public bool copyAir = false;
        public int[] copyoffset = new int[3] { 0, 0, 0 };
        public ushort[] copystart = new ushort[3] { 0, 0, 0 };

        //Undo
        public struct UndoPos { public ushort x, y, z; public byte type, newtype; public string mapName; public DateTime timePlaced; }
        public List<UndoPos> UndoBuffer = new List<UndoPos>();
        public List<UndoPos> RedoBuffer = new List<UndoPos>();
        

        public bool showPortals = false;
        public bool showMBs = false;

        public string prevMsg = "";


        //Movement
        public ushort oldBlock = 0;
        public ushort deathCount = 0;
        public byte deathBlock;

        //Games
        public DateTime lastDeath = DateTime.Now;
        
        public byte BlockAction = 0;  //0-Nothing 1-solid 2-lava 3-water 4-active_lava 5 Active_water 6 OpGlass 7 BluePort 8 OrangePort
        public byte modeType = 0;
        public byte[] bindings = new byte[128];
        public string[] cmdBind = new string[10];
        public string[] messageBind = new string[10];
        public string lastCMD = "";

        public Level level = Server.mainLevel;
        public bool Loading = true;     //True if player is loading a map.

        public delegate void BlockchangeEventHandler(Player p, ushort x, ushort y, ushort z, byte type);
        public event BlockchangeEventHandler Blockchange = null;
        public void ClearBlockchange() { Blockchange = null; }
        public bool HasBlockchange() { return (Blockchange == null); }
        public object blockchangeObject = null;
        public ushort[] lastClick = new ushort[3] { 0, 0, 0 };

        public ushort[] pos = new ushort[3] { 0, 0, 0 };
        ushort[] oldpos = new ushort[3] { 0, 0, 0 };
        ushort[] basepos = new ushort[3] { 0, 0, 0 };
        public byte[] rot = new byte[2] { 0, 0 };
        byte[] oldrot = new byte[2] { 0, 0 };

        // grief/spam detection
        public static int spamBlockCount = 200;
        public static int spamBlockTimer = 5;
        Queue<DateTime> spamBlockLog = new Queue<DateTime>(spamBlockCount);

        public static int spamChatCount = 3;
        public static int spamChatTimer = 4;
        Queue<DateTime> spamChatLog = new Queue<DateTime>(spamChatCount);

        public object[] CustomCommandVars = new object[] { };

        public bool loggedIn = false;
        public Player(Socket s)
        {
            try
            {
                socket = s;
                ip = socket.RemoteEndPoint.ToString().Split(':')[0];
                Server.s.Log(ip + " connected to the server.");

                for (byte i = 0; i < 128; ++i) bindings[i] = i;

                socket.BeginReceive(tempbuffer, 0, tempbuffer.Length, SocketFlags.None, new AsyncCallback(Receive), this);

                loginTimer.Elapsed += delegate
                {
                    if (!Loading)
                    {
                        loginTimer.Stop();

                        if (File.Exists("text/welcome.txt"))
                        {
                            try
                            {
                                List<string> welcome = new List<string>();
                                StreamReader wm = File.OpenText("text/welcome.txt");
                                while (!wm.EndOfStream)
                                    welcome.Add(wm.ReadLine());

                                wm.Close();
                                wm.Dispose();

                                foreach (string w in welcome)
                                    SendMessage(w);
                            }
                            catch { }
                        }
                        else
                        {
                            Server.s.Log("Could not find Welcome.txt. Using default.");
                            File.WriteAllText("text/welcome.txt", "Welcome to my server!");
                        }
                        extraTimer.Start();
                    }
                }; loginTimer.Start();

                pingTimer.Elapsed += delegate { SendPing(); };
                pingTimer.Start();

                extraTimer.Elapsed += delegate
                {
                    extraTimer.Stop();

                    try
                    {
                        if (!Group.Find("Nobody").commands.Contains("inbox") && !Group.Find("Nobody").commands.Contains("send"))
                        {
                            //DataTable Inbox = MySQL.fillData("SELECT * FROM `Inbox" + name + "`", true);



                            SendMessage("&cYou have &f" + Server.s.database.GetTable("Inbox" + name).Rows.Count + Server.DefaultColor + " &cmessages in /inbox");
                            //Inbox.Dispose();
                        }
                    }
                    catch { }
                    if (Server.updateTimer.Interval > 1000) SendMessage("Lowlag mode is currently &aON.");
                    try
                    {
                        if (!Group.Find("Nobody").commands.Contains("pay") && !Group.Find("Nobody").commands.Contains("give") && !Group.Find("Nobody").commands.Contains("take")) SendMessage("You currently have &a" + money + Server.DefaultColor + " " + Server.moneys);
                    }
                    catch { }
                    SendMessage("You have modified &a" + overallBlocks + Server.DefaultColor + " blocks!");
                    if (players.Count == 1)
                        SendMessage("There is currently &a" + players.Count + " player online.");
                    else
                        SendMessage("There are currently &a" + players.Count + " players online.");
                    try
                    {
                        if (!Group.Find("Nobody").commands.Contains("award") && !Group.Find("Nobody").commands.Contains("awards") && !Group.Find("Nobody").commands.Contains("awardmod")) SendMessage("You have " + Awards.awardAmount(name) + " awards.");
                    }
                    catch { }

                };

                afkTimer.Elapsed += delegate
                {
                    if (name == "") return;

                    if (Server.afkset.Contains(name))
                    {
                        afkCount = 0;
                        /*if (Server.afkkick > 0 && group.Permission < LevelPermission.Operator)
                            if (afkStart.AddMinutes(Server.afkkick) < DateTime.Now)
                                Kick("Auto-kick, AFK for " + Server.afkkick + " minutes");*/
                        if ((oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2]) && (oldrot[0] != rot[0] || oldrot[1] != rot[1]))
                            Command.all.Find("afk").Use(this, "");
                    }
                    else
                    {
                        if (oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2] && oldrot[0] == rot[0] && oldrot[1] == rot[1])
                            afkCount++;
                        else
                            afkCount = 0;

                        if (afkCount > Server.afkminutes * 30)
                        {
                            Command.all.Find("afk").Use(this, "auto: Not moved for " + Server.afkminutes + " minutes");
                            afkCount = 0;
                        }
                    }
                };
                if (Server.afkminutes > 0) afkTimer.Start();

                connections.Add(this);
            }
            catch (SocketException e) { if (e.Message.Trim() != "An existing connection was forcibly closed by the remote host") Server.ErrorLog(e); Kick("Login failed!"); }
            catch (Exception e) { Kick("Login failed!"); Server.ErrorLog(e); }
        }

        public void save()
        {
            /*string commandString =
                "UPDATE Players SET IP='" + ip + "'" +
                ", LastLogin='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                ", totalLogin=" + totalLogins +
                ", totalDeaths=" + overallDeath +
                ", Money=" + money +
                ", totalBlocks=" + overallBlocks + " + " + loginBlocks +
                ", totalKicked=" + totalKicked +
                " WHERE Name='" + name + "'";

            MySQL.executeQuery(commandString);*/
            string c = "";
            string tc = "";
            string t = "";
            try
            {
                c = Server.s.database.GetTable("Players").GetValue(Server.s.database.GetTable("Players").Rows.IndexOf(Server.s.database.GetTable("Players").GetRow(new string[] { "Name" }, new string[] { name })), "Color");
                tc = Server.s.database.GetTable("Players").GetValue(Server.s.database.GetTable("Players").Rows.IndexOf(Server.s.database.GetTable("Players").GetRow(new string[] { "Name" }, new string[] { name })), "TColor");
                t = Server.s.database.GetTable("Players").GetValue(Server.s.database.GetTable("Players").Rows.IndexOf(Server.s.database.GetTable("Players").GetRow(new string[] { "Name" }, new string[] { name })), "Title");
                Server.s.database.GetTable("Players").DeleteRow(Server.s.database.GetTable("Players").Rows.IndexOf(Server.s.database.GetTable("Players").GetRow(new string[] { "Name" }, new string[] { name })));
                Server.s.Debug("1");
            }
            catch (Exception e) { Server.ErrorLog(e); }
            Server.s.database.GetTable("Players").AddRow(new List<string> { id.ToString(), name, ip, firstLogin.ToString("yyyy-MM-dd HH:mm:ss"), lastLogin.ToString("yyyy-MM-dd HH:mm:ss"), totalLogins.ToString(), title, deathCount.ToString(), money.ToString(), (overallBlocks + loginBlocks).ToString(), totalKicked.ToString(), c, tc });

            try
            {
                if (!smileySaved)
                {
                    if (parseSmiley)
                        emoteList.RemoveAll(s => s == name);
                    else
                        emoteList.Add(name);

                    File.WriteAllLines("text/emotelist.txt", emoteList.ToArray());
                    smileySaved = true;
                }
            }
            catch (Exception e)
            { 
                Server.ErrorLog(e);
            }
        }

        #region == INCOMING ==
        static void Receive(IAsyncResult result)
        {
        //    Server.s.Log(result.AsyncState.ToString());
            Player p = (Player)result.AsyncState;
            if (p.disconnected)
                return;
            try
            {
                int length = p.socket.EndReceive(result);
                if (length == 0) { p.Disconnect(); return; }

                byte[] b = new byte[p.buffer.Length + length];
                Buffer.BlockCopy(p.buffer, 0, b, 0, p.buffer.Length);
                Buffer.BlockCopy(p.tempbuffer, 0, b, p.buffer.Length, length);

                p.buffer = p.HandleMessage(b);
                p.socket.BeginReceive(p.tempbuffer, 0, p.tempbuffer.Length, SocketFlags.None,
                                      new AsyncCallback(Receive), p);
            }
            catch (SocketException)
            {
                p.Disconnect();
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                p.Kick("Error!");
            }
        }
        byte[] HandleMessage(byte[] buffer)
        {
            try
            {
                int length = 0; byte msg = buffer[0];
                // Get the length of the message by checking the first byte
                switch ((Magic)msg)
                {
                    case Magic.IDENTIFICATION:
                        length = 130;
                        break; // login
                    case Magic.PLAYER_SET_BLOCK:
                        if (!loggedIn)
                            goto default;
                        length = 8;
                        break; // blockchange
                    case Magic.POSITION_ROTATION:
                        if (!loggedIn)
                            goto default;
                        length = 9;
                        break; // input
                    case Magic.CHAT_MESSAGE:
                        if (!loggedIn)
                            goto default;
                        length = 65;
                        break; // chat
                    case Magic.EXTINFO:
                        length = 66;
                        break; // extinfo
                    case Magic.EXTENTRY:
                        length = 68;
                        break; // extentry
                    case Magic.CUSTOM_BLOCK_SUPPORT_LEVEL:
                        length = 1;
                        break;
                    default:
                        Server.s.Debug("Unhandled message id \"" + msg + "\"");
                        Kick("Unhandled message id \"" + msg + "\"!");
                        return new byte[0];
                }
                if (buffer.Length > length)
                {
                    byte[] message = new byte[length];
                    Buffer.BlockCopy(buffer, 1, message, 0, length);

                    byte[] tempbuffer = new byte[buffer.Length - length - 1];
                    Buffer.BlockCopy(buffer, length + 1, tempbuffer, 0, buffer.Length - length - 1);

                    buffer = tempbuffer;

                    // Thread thread = null; 
                    switch ((Magic)msg)
                    {
                        case Magic.IDENTIFICATION:
                            HandleLogin(message);
                            break;
                        case Magic.PLAYER_SET_BLOCK:
                            if (!loggedIn)
                                break;
                            HandleBlockchange(message);
                            break;
                        case Magic.POSITION_ROTATION:
                            if (!loggedIn)
                                break;
                            HandleInput(message);
                            break;
                        case Magic.CHAT_MESSAGE:
                            if (!loggedIn)
                                break;
                            HandleChat(message);
                            break;
                        case Magic.EXTINFO:
                            HandleExtInfo(message);
                            break;
                        case Magic.EXTENTRY:
                            HandleExtEntry(message);
                            break;
                        case Magic.CUSTOM_BLOCK_SUPPORT_LEVEL:
                            HandleCustomBlockSupportLevel(message);
                            break;
                    }
                    //thread.Start((object)message);
                    if (buffer.Length > 0)
                        buffer = HandleMessage(buffer);
                    else
                        return new byte[0];
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
            }
            return buffer;
        }
        void HandleLogin(byte[] message)
        {
            try
            {
                //byte[] message = (byte[])m;
                if (loggedIn)
                    return;

                byte version = message[0];
                name = enc.GetString(message, 1, 64).Trim();
                string verify = enc.GetString(message, 65, 32).Trim();
                byte type = message[129];

                Server.s.Debug("Packet Received(0): " + version + " " + name + " " + verify + " " + type);

                if (type == (byte)66)
                    cpe = true;

                if (OnPlayerJoinEvent != null) OnPlayerJoinEvent(this);

                if (noJoin)
                {
                    try { this.Disconnect(); }
                    catch { }
                    Server.s.Debug("PlayerJoinEvent cancelled (noJoin = true)");
                    return;
                }

                try
                {
                    Server.TempBan tBan = Server.tempBans.Find(tB => tB.name.ToLower() == name.ToLower());
                    if (tBan.allowedJoin < DateTime.Now)
                    {
                        Server.tempBans.Remove(tBan);
                    }
                    else
                    {
                        Kick("You're still banned (temporary ban)!");
                    }
                } catch { }
                // OMNI BAN
                bool omnibanned = false;
                /*try
                {//Get omnibans from the devpanel
                    string omnibans = new WebClient().DownloadString("http://dev.mcsong.x10.mx/omnibans.php?action=list");
                    if (omnibanned)
                    {
                        foreach (string s in omnibans.Split(','))
                        {
                            if (this.name.ToLower() == s.ToLower() || this.ip == s || Regex.IsMatch(this.ip, s))
                            {
                                omnibanned = true;
                            }
                        }
                    }
                }
                catch
                {*/ //Hard-coded list of omnibans
                    if (this.name.ToLower() == "soysauceships")
                    {
                        omnibanned = true;
                    }
                //}
                if (omnibanned) { Kick("You have been Omnibanned.  mcsong.x10.mx/forums for appeal."); omnibanned = false; return; }
                // Whitelist check.
                if (Server.useWhitelist)
                {
                    if (Server.verify)
                    {
                        if (Server.whiteList.Contains(name))
                        {
                            onWhitelist = true;
                        }
                    }
                    else
                    {
                        // [TODO] KILL THIS SQL (convert to flatfile)
                        // Verify Names is off.  Gotta check the hard way.
                        /*DataTable ipQuery = MySQL.fillData("SELECT Name FROM Players WHERE IP = '" + ip + "'");

                        if (ipQuery.Rows.Count > 0)
                        {
                            if (ipQuery.Rows.Contains(name) && Server.whiteList.Contains(name))
                            {
                                onWhitelist = true;
                            }
                        }
                        ipQuery.Dispose();*/
                    }
                }

                if (Server.bannedIP.Contains(ip))
                {
                    if (Server.useWhitelist)
                    {
                        if (!onWhitelist)
                        {
                            Kick(Server.customBanMessage);
                            return;
                        }
                    }
                    else
                    {
                        Kick(Server.customBanMessage);
                        return;
                    }
                }
                if (connections.Count >= 5) { Kick("Too many connections!"); return; }

                if (Group.findPlayerGroup(name) == Group.findPerm(LevelPermission.Banned))
                {
                    if (Server.useWhitelist)
                    {
                        if (!onWhitelist)
                        {
                            Kick(Server.customBanMessage);
                            return;
                        }
                    }
                    else
                    {
                        Kick(Server.customBanMessage);
                        return;
                    }
                }

                if (Server.premium && ip != "127.0.0.1" && !Server.devs.Contains(name.ToLower()) && new WebClient().DownloadString("https://minecraft.net/haspaid.jsp?user=" + name).Trim().ToLower() == "false")
                {
                    Kick("Premium (paid) account required!");
                }

                if (Player.players.Count >= Server.players && ip != "127.0.0.1" && !Server.devs.Contains(name.ToLower())) { Kick("Server full!"); return; }
                if (version != Server.version) { Kick("Wrong version!"); return; }
                if (name.Length > 16 || !ValidName(name)) { Kick("Illegal name!"); return; }
                int guests = 0;
                foreach (Player p in players)
                    if (p.group.Permission == LevelPermission.Guest)
                        guests++;
                if (Server.verify)
                {
                    if (verify == "--" || verify != 
                        BitConverter.ToString(md5.ComputeHash(enc.GetBytes(Server.salt + name)))
                        .Replace("-", "").ToLower().TrimStart('0'))
                    {
                        if (ip != "127.0.0.1" && ! ip.StartsWith("192.168."))
                        {
                            Kick("Login failed! Try again."); return;
                        }
                    }
                }

                if (Server.guests > 0 && Group.findPlayerGroup(name).Permission == LevelPermission.Guest && guests >= Server.guests && ip != "127.0.0.1" && !Server.devs.Contains(name.ToLower())) { Kick("Too mmany guests!"); return; }
                if (Server.maintenanceMode && (Group.findPlayerGroup(name).Permission < Server.maintPerm))
                {
                    if (ip != "127.0.0.1" && !ip.StartsWith("192.168.") && !Server.devs.Contains(name.ToLower()))
                    {
                        Kick("The server is in maintenance mode! Come back later."); return;
                    }
                }

                foreach (Player p in players)
                {
                    if (p.name == name)
                    {
                        if (Server.verify)
                        {
                            p.Kick("Someone logged in as you!"); break;
                        }
                        else { Kick("Already logged in!"); return; }
                    }
                }
                
                try { left.Remove(name.ToLower()); }
                catch { }

                group = Group.findPlayerGroup(name);

                if (cpe)
                {
                    Server.s.Debug("Player supports CPE! Starting negotiations...");
                    SendExtInfo();
                    SendExtEntry();
                }
                else
                {
                    FinishLogin();
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                Player.GlobalMessage("An error occurred: " + e.Message);
            }            
        }
        public void FinishLogin()
        {
            Server.s.Debug("Finishing player login...");
            SendMotd();
            SendMap();
            Loading = true;

            if (disconnected) return;

            loggedIn = true;
            id = FreeId();

            players.Add(this);
            connections.Remove(this);

            Server.s.PlayerListUpdate();

            IRCBot.Say(name + " joined the game.");

            //Test code to show when people come back with different accounts on the same IP
            string temp = "Lately known as:";
            bool found = false;
            if (ip != "127.0.0.1")
            {
                foreach (KeyValuePair<string, string> prev in left)
                {
                    if (prev.Value == ip)
                    {
                        found = true;
                        temp += " " + prev.Key;
                    }
                }
                if (found)
                {
                    GlobalMessageOps(temp);
                    Server.s.Log(temp);
                    IRCBot.Say(temp, true);       //Tells people in op channel on IRC
                }
            }
            
            // --------------------

            /*DataTable playerDb = MySQL.fillData("SELECT * FROM Players WHERE Name='" + name + "'");

            if (playerDb.Rows.Count == 0)
            {
                this.prefix = "";
                this.titlecolor = "";
                this.color = group.color;
                this.money = 0;
                this.firstLogin = DateTime.Now;
                this.totalLogins = 1;
                this.totalKicked = 0;
                this.overallDeath = 0;
                this.overallBlocks = 0;
                this.timeLogged = DateTime.Now;
                SendMessage("Welcome " + name + "! This is your first visit.");

                MySQL.executeQuery("INSERT INTO Players (Name, IP, FirstLogin, LastLogin, totalLogin, Title, totalDeaths, Money, totalBlocks, totalKicked)" +
                    "VALUES ('" + name + "', '" + ip + "', '" + firstLogin.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + totalLogins +
                    ", '" + prefix + "', " + overallDeath + ", " + money + ", " + loginBlocks + ", " + totalKicked + ")");

            }
            else
            {
                totalLogins = int.Parse(playerDb.Rows[0]["totalLogin"].ToString()) + 1;
                userID = int.Parse(playerDb.Rows[0]["ID"].ToString());
                firstLogin = DateTime.Parse(playerDb.Rows[0]["firstLogin"].ToString());
                timeLogged = DateTime.Now;
                if (playerDb.Rows[0]["Title"].ToString().Trim() != "")
                {
                    string parse = playerDb.Rows[0]["Title"].ToString().Trim().Replace("[", "");
                    title = parse.Replace("]", "");
                }
                if (playerDb.Rows[0]["title_color"].ToString().Trim() != "")
                {
                    titlecolor = c.Parse(playerDb.Rows[0]["title_color"].ToString().Trim());
                }
                else
                {
                    titlecolor = "";
                }
                if (playerDb.Rows[0]["color"].ToString().Trim() != "")
                {
                    color = c.Parse(playerDb.Rows[0]["color"].ToString().Trim());
                }
                else
                {
                    color = group.color;
                }
                SetPrefix();
                overallDeath = int.Parse(playerDb.Rows[0]["TotalDeaths"].ToString());
                overallBlocks = int.Parse(playerDb.Rows[0]["totalBlocks"].ToString().Trim());
                money = int.Parse(playerDb.Rows[0]["Money"].ToString());
                totalKicked = int.Parse(playerDb.Rows[0]["totalKicked"].ToString());*/
            //PlayerDB.Load(this);

            jDatabase.Table plrs = Server.s.database.GetTable("Players");
            List<List<string>> rows = plrs.Rows;

            try
            {
                int i = rows.IndexOf(plrs.GetRow(new string[] { "Name" }, new string[] { name }));
                title = plrs.GetValue(i, "Title");
                titlecolor = plrs.GetValue(i, "TColor");
                color = (String.IsNullOrWhiteSpace(plrs.GetValue(i, "Color")) ? group.color : plrs.GetValue(i, "Color"));
                money = int.Parse(plrs.GetValue(i, "Money"));
                firstLogin = DateTime.Parse(plrs.GetValue(i, "FirstLogin"));
                lastLogin = DateTime.Parse(plrs.GetValue(i, "LastLogin"));
                totalLogins = int.Parse(plrs.GetValue(i, "TotalLogins"));
                totalKicked = int.Parse(plrs.GetValue(i, "TotalKicks"));
                overallDeath = int.Parse(plrs.GetValue(i, "TotalDeaths"));
                overallBlocks = int.Parse(plrs.GetValue(i, "TotalBlocks"));
                timeLogged = DateTime.Now;
            }
            catch
            {
                title = "";
                titlecolor = "";
                color = group.color;
                money = 0;
                firstLogin = DateTime.Now;
                lastLogin = DateTime.Now;
                totalLogins = 1;
                totalKicked = 0;
                overallDeath = 0;
                overallBlocks = 0;
                timeLogged = DateTime.Now;
                Server.s.database.GetTable("Players").AddRow(new List<string> { id.ToString(), name, ip, firstLogin.ToString("yyyy-MM-dd HH:mm:ss"), lastLogin.ToString("yyyy-MM-dd HH:mm:ss"), totalLogins.ToString(), "", deathCount.ToString(), money.ToString(), (overallBlocks + loginBlocks).ToString(), totalKicked.ToString(), "", "" });
            }

            SendMessage("Welcome back " + color + prefix + name + Server.DefaultColor + "! You've been here " + totalLogins + " times!");
            //}
            //playerDb.Dispose();

            if (Server.devs.Contains(this.name.ToLower()))
            {
                if (color == Group.standard.color)
                {
                    color = "&9";
                }
                if (prefix == "")
                {
                    title = "Dev";
                }
                SetPrefix();
            }

            try
            {
                ushort x = (ushort)((0.5 + level.spawnx) * 32);
                ushort y = (ushort)((1 + level.spawny) * 32);
                ushort z = (ushort)((0.5 + level.spawnz) * 32);
                pos = new ushort[3] { x, y, z }; rot = new byte[2] { level.rotx, level.roty };

                GlobalSpawn(this, x, y, z, rot[0], rot[1], true);
                foreach (Player p in players)
                {
                    if (p.level == level && p != this && !p.hidden)
                        SendSpawn(p.id, p.color + p.name, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]);
                }
                foreach (PlayerBot pB in PlayerBot.playerbots)
                {
                    if (pB.level == level)
                        SendSpawn(pB.id, pB.color + pB.name, pB.pos[0], pB.pos[1], pB.pos[2], pB.rot[0], pB.rot[1]);
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                Server.s.Log("Error spawning player \"" + name + "\"");
            }

            Loading = false;

            if (emoteList.Contains(name)) parseSmiley = false;
            GlobalChat(null, "&a+ " + this.color + this.prefix + this.name + Server.DefaultColor + " has joined the game.", false);
            Server.s.Log(name + " [" + ip + "] has joined the server.");

            if (cpe && extensions.Contains(Extension.ClickDistance))
            {
                SendClickDistance(clickDistance);
            }

        }

        public void HandleExtInfo(byte[] message)
        {
            clientName = enc.GetString(message, 0, 64).Trim();
            cpeCount = NTHOshort(message, 64);
            Server.s.Debug("Packet Received(16): " + clientName + " " + cpeCount);
            if (cpeCount <= 0)
            {
                FinishLogin();
            }
        }

        private bool extraExt = false;
        public void HandleExtEntry(byte[] message)
        {
            string extName = enc.GetString(message, 0, 64).Trim();
            int extVersion = NTHOint(message, 64);//CHECK
            Extension e = Extension.all.Find(extName);

            Server.s.Debug("Packet Received(17): " + extName + " " + extVersion);

            if (extraExt) return;
            cpeExtSent++;
            if (cpeExtSent > cpeCount) { Server.s.Log("Too many ExtEntry packets were received! Ignoring all extras..."); extraExt = true; return; }

            if (e != null)// Is it enabled on the server?
                if (extVersion == e.version)// Do the versions match?
                    extensions.Add(e);

            
            
            if (cpeExtSent == cpeCount)
                if (extensions.Contains(Extension.CustomBlocks))
                    SendCustomBlockSupportLevel();
                else
                    FinishLogin();
        }

        public void HandleCustomBlockSupportLevel(byte[] message)
        {
            Server.s.Debug("Packet Received(19): " + message[0]);
            CustomBlockSupportLevel = (message[0] > Server.CustomBlockSupportLevel) ? Server.CustomBlockSupportLevel : message[0];
            Server.s.Debug("Using support level " + CustomBlockSupportLevel);
            FinishLogin();
        }

        public void SetPrefix()
        {
            prefix = (title == "") ? "" : (titlecolor == "") ? "[" + title + "] " : "[" + titlecolor + title + color + "] ";
        }

        void HandleBlockchange(byte[] message)
        {
            int section = 0;
            try
            {
                //byte[] message = (byte[])m;
                if (!loggedIn)
                    return;
                if (CheckBlockSpam())
                    return;

                section++;
                ushort x = NTHO(message, 0);
                ushort y = NTHO(message, 2);
                ushort z = NTHO(message, 4);
                byte action = message[6];
                byte type = message[7];

                Server.s.Debug("Packet Received(5): " + x + " " + y + " " + z + " " + action + " " + type);

                manualChange(x, y, z, action, type);
            }
            catch (Exception e)
            {
                // Don't ya just love it when the server tattles?
                GlobalMessageOps(name + " has triggered a block change error");
                Server.ErrorLog(e);
            }
        }
        public void manualChange(ushort x, ushort y, ushort z, byte action, byte type)
        {
            if (extensions.Contains(Extension.CustomBlocks))
            {
                if (Block.SupportLevel(type) > CustomBlockSupportLevel)
                {
                    Kick("Unknown block type!");
                    return;
                }
                else
                {
                    if (CustomBlockSupportLevel == 1)
                    {
                        if (type > 65)
                        {
                            Kick("Unknown block type!");
                            return;
                        }
                    }
                    else if (CustomBlockSupportLevel < 1)
                    {
                        if (type > 49)
                        {
                            Kick("Unknown block type!");
                            return;
                        }
                    }
                }
            }
            else
            {
                if (type > 49)
                {
                    Kick("Unknown block type!");
                    return;
                }
            }
            

            if (OnPlayerBlockchangeEvent != null) OnPlayerBlockchangeEvent(this, x, y, z, type);
            if (OnBlockchangeEvent != null) OnBlockchangeEvent(x, y, z, type);
            if (noBlockchange) { Server.s.Debug("BlockchangeEvent cancelled (noBlockchange = true)"); return; }

            byte b = level.GetTile(x, y, z);
            if (b == Block.Zero) { return; }
            if (jailed) { SendBlockchange(x, y, z, b); return; }
            if (level.name.Contains("Museum " + Server.DefaultColor) && Blockchange == null)
            {
                return;
            }

            if (!deleteMode)
            {
                string info = level.foundInfo(x, y, z);
                if (info.Contains("wait")) { return; }
            }

            if (!canBuild)
            {
                Server.s.Debug("canBuild is false");
                SendBlockchange(x, y, z, b);
                return;
            }

            Level.BlockPos bP;
            bP.name = name;
            bP.TimePerformed = DateTime.Now;
            bP.x = x; bP.y = y; bP.z = z;
            bP.type = type;

            lastClick[0] = x;
            lastClick[1] = y;
            lastClick[2] = z;

            if (Blockchange != null)
            {
                if (Blockchange.Method.ToString().IndexOf("AboutBlockchange") == -1 && !level.name.Contains("Museum " + Server.DefaultColor))
                {
                    bP.deleted = true;
                    level.blockCache.Add(bP);
                }

                Blockchange(this, x, y, z, type);
                return;
            }

            if (group.Permission == LevelPermission.Banned) return;
            if (group.Permission == LevelPermission.Guest)
            {
                int Diff = 0;

                Diff = Math.Abs((int)(pos[0] / 32) - x);
                Diff += Math.Abs((int)(pos[1] / 32) - y);
                Diff += Math.Abs((int)(pos[2] / 32) - z);

                if (Diff > 12)
                {
                    Server.s.Log(name + " attempted to build with a " + Diff.ToString() + " distance offset");
                    GlobalMessageOps("To Ops &f-" + color + name + "&f- attempted to build with a " + Diff.ToString() + " distance offset");
                    SendMessage("You can't build that far away.");
                    SendBlockchange(x, y, z, b); return;
                }

                if (Server.antiTunnel)
                {
                    if (!ignoreGrief)
                    {
                        if (y < level.depth / 2 - Server.maxDepth)
                        {
                            SendMessage("You're not allowed to build this far down!");
                            SendBlockchange(x, y, z, b); return;
                        }
                    }
                }
            }

            if (!Block.canPlace(this, b) && !Block.BuildIn(b) && !Block.AllowBreak(b))
            {
                SendMessage("Cannot build here!");
                SendBlockchange(x, y, z, b);
                return;
            }

            if (!Block.canPlace(this, type))
            {
                SendMessage("You can't place this block type!");
                SendBlockchange(x, y, z, b); 
                return;
            }

            if (b >= 200 && b < 220)
            {
                SendMessage("Block is active, you cant disturb it!");
                SendBlockchange(x, y, z, b);
                return;
            }


            if (action > 1) { Kick("Unknown block action!"); }

            byte oldType = type;
            type = bindings[type];
            //Ignores updating blocks that are the same and send block only to the player
            if (b == (byte)((painting || action == 1) ? type : 0))
            {
                Server.s.Debug("Painting or something");
                if (painting || oldType != type) { SendBlockchange(x, y, z, b); } return;
            }
            //else

            if (!painting && action == 0)
            {
                if (!deleteMode)
                {
                    if (Block.portal(b)) { HandlePortal(this, x, y, z, b); return; }
                    if (Block.mb(b)) { HandleMsgBlock(this, x, y, z, b); return; }
                }

                bP.deleted = true;
                level.blockCache.Add(bP);
                deleteBlock(b, type, x, y, z);
            }
            else
            {
                bP.deleted = false;
                level.blockCache.Add(bP);
                placeBlock(b, type, x, y, z);
            }
        }

        public void HandlePortal(Player p, ushort x, ushort y, ushort z, byte b)
        {
            try
            {
                //DataTable Portals = MySQL.fillData("SELECT * FROM `Portals" + level.name + "` WHERE EntryX=" + (int)x + " AND EntryY=" + (int)y + " AND EntryZ=" + (int)z);

                jDatabase.Table portals = Server.s.database.GetTable("Portals" + level.name);
                int i = portals.Rows.IndexOf(portals.GetRow(new string[] { "EntryX", "EntryY", "EntryZ" }, new string[] { x.ToString(), y.ToString(), z.ToString() }));

                if (i > -1)
                {
                    if (level.name != portals.GetValue(i, "ExitMap"))
                    {
                        ignorePermission = true;
                        Level thisLevel = level;
                        Command.all.Find("goto").Use(this, portals.GetValue(i, "ExitMap"));
                        if (thisLevel == level) { Player.SendMessage(p, "The map the portal goes to isn't loaded."); return; }
                        ignorePermission = false;
                    }
                    else
                        SendBlockchange(x, y, z, b);

                    while (p.Loading) { }
                    Command.all.Find("move").Use(this, this.name + " " + portals.GetValue(i, "ExitX") + " " + portals.GetValue(i, "ExitY") + " " + portals.GetValue(i, "ExitZ"));

                }
                else
                    Blockchange(this, x, y, z, (byte)0);

                /*int LastPortal = Portals.Rows.Count - 1;
                if (LastPortal > -1)
                {
                    if (level.name != Portals.Rows[LastPortal]["ExitMap"].ToString())
                    {
                        ignorePermission = true;
                        Level thisLevel = level;
                        Command.all.Find("goto").Use(this, Portals.Rows[LastPortal]["ExitMap"].ToString());
                        if (thisLevel == level) { Player.SendMessage(p, "The map the portal goes to isn't loaded."); return; }
                        ignorePermission = false;
                    }
                    else SendBlockchange(x, y, z, b);

                    while (p.Loading) { }  //Wait for player to spawn in new map
                    Command.all.Find("move").Use(this, this.name + " " + Portals.Rows[LastPortal]["ExitX"].ToString() + " " + Portals.Rows[LastPortal]["ExitY"].ToString() + " " + Portals.Rows[LastPortal]["ExitZ"].ToString());
                }
                else
                {
                    Blockchange(this, x, y, z, (byte)0);
                }
                Portals.Dispose();*/
            }
            catch { Player.SendMessage(p, "Portal had no exit."); return; }
        }

        public void HandleMsgBlock(Player p, ushort x, ushort y, ushort z, byte b)
        {
            try
            {
                /*DataTable Messages = MySQL.fillData("SELECT * FROM `Messages" + level.name + "` WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z);

                int LastMsg = Messages.Rows.Count - 1;
                if (LastMsg > -1)
                {
                    string message = Messages.Rows[LastMsg]["Message"].ToString().Trim();
                    if (message != prevMsg || Server.repeatMessage)
                    {
                        Player.SendMessage(p, message);
                        prevMsg = message;
                    }
                    SendBlockchange(x, y, z, b);
                }
                else
                {
                    Blockchange(this, x, y, z, (byte)0);
                }
                Messages.Dispose();*/
                Level.MessageBlock mb = level.getMB(x, y, z);
                string msg = mb.message;
                MessageType t = (MessageType)mb.type;
                if (!p.extensions.Contains(Extension.MessageTypes))
                    t = MessageType.CHAT;
                Player.SendMessage(p, msg, t);
                if (Server.repeatMessage) SendBlockchange(x, y, z, b);
                else Blockchange(this, x, y, z, 0);
            }
            catch { Player.SendMessage(p, "No message was stored."); return; }
        }

        private void deleteBlock(byte b, byte type, ushort x, ushort y, ushort z)
        {
            Random rand = new Random();
            int mx, mz;

            if (deleteMode) { level.Blockchange(this, x, y, z, Block.air); return; }

            if (Block.tDoor(b)) { SendBlockchange(x, y, z, b); return; }
            if (Block.DoorAirs(b) != 0)
            {
                if (level.physics != 0) level.Blockchange(x, y, z, Block.DoorAirs(b));
                else SendBlockchange(x, y, z, b);
                return;
            }
            if (Block.odoor(b) != Block.Zero)
            {
                if (b == Block.odoor8 || b == Block.odoor8_air)
                {
                    level.Blockchange(this, x, y, z, Block.odoor(b));
                }
                else
                {
                    SendBlockchange(x, y, z, b);
                }
                return;
            }

            switch (b)
            {
                case Block.door_air:   //Door_air
                case Block.door2_air:
                case Block.door3_air:
                case Block.door4_air:
                case Block.door5_air:
                case Block.door6_air:
                case Block.door7_air:
                case Block.door8_air:
                case Block.door9_air:
                case Block.door10_air:
                case Block.door_iron_air:
                case Block.door_dirt_air:
                case Block.door_grass_air:
                case Block.door_blue_air:
                case Block.door_book_air:
                    break;
                case Block.rocketstart:
                    if (level.physics < 2)
                    {
                        SendBlockchange(x, y, z, b);
                    }
                    else
                    {
                        int newZ = 0, newX = 0, newY = 0;

                        SendBlockchange(x, y, z, Block.rocketstart);
                        if (rot[0] < 48 || rot[0] > (256 - 48))
                            newZ = -1;
                        else if (rot[0] > (128 - 48) && rot[0] < (128 + 48))
                            newZ = 1;

                        if (rot[0] > (64 - 48) && rot[0] < (64 + 48))
                            newX = 1;
                        else if (rot[0] > (192 - 48) && rot[0] < (192 + 48))
                            newX = -1;

                        if (rot[1] >= 192 && rot[1] <= (192 + 32))
                            newY = 1;
                        else if (rot[1] <= 64 && rot[1] >= 32)
                            newY = -1;

                        if (192 <= rot[1] && rot[1] <= 196 || 60 <= rot[1] && rot[1] <= 64) { newX = 0; newZ = 0; }

                        level.Blockchange((ushort)(x + newX * 2), (ushort)(y + newY * 2), (ushort)(z + newZ * 2), Block.rockethead);
                        level.Blockchange((ushort)(x + newX), (ushort)(y + newY), (ushort)(z + newZ), Block.fire);
                    }
                    break;
                case Block.firework:
                    if (level.physics != 0)
                    {
                        mx = rand.Next(0, 2); mz = rand.Next(0, 2);

                        level.Blockchange((ushort)(x + mx - 1), (ushort)(y + 2), (ushort)(z + mz - 1), Block.firework);
                        level.Blockchange((ushort)(x + mx - 1), (ushort)(y + 1), (ushort)(z + mz - 1), Block.lavastill, false, "wait 1 dissipate 100");
                    } SendBlockchange(x, y, z, b);

                    break;
                default:
                    level.Blockchange(this, x, y, z, (byte)(Block.air));
                    break;
            }
        }

        public void placeBlock(byte b, byte type, ushort x, ushort y, ushort z)
        {
            if (Block.odoor(b) != Block.Zero) { SendMessage("oDoor here!"); return; }

            switch (BlockAction)
            {
                case 0:     //normal
                    //if (level.physics == 0)
                    //{
                        switch (type)
                        {
                            case Block.dirt: //instant dirt to grass
                                level.Blockchange(this, x, y, z, (byte)(Block.grass));
                                break;
                            case Block.staircasestep:    //stair handler
                                if (level.GetTile(x, (ushort)(y - 1), z) == Block.staircasestep)
                                {
                                    SendBlockchange(x, y, z, Block.air);    //send the air block back only to the user.
                                    //level.Blockchange(this, x, y, z, (byte)(Block.air));
                                    level.Blockchange(this, x, (ushort)(y - 1), z, (byte)(Block.staircasefull));
                                    break;
                                }
                                //else
                                level.Blockchange(this, x, y, z, type);
                                break;
                            case Block.cobbleslab: // Cobble stair handler
                                if (level.GetTile(x, (ushort)(y - 1), z) == Block.cobbleslab)
                                {
                                    SendBlockchange(x, y, z, Block.air);
                                    level.Blockchange(this, x, (ushort)(y - 1), z, (byte)Block.stone);
                                    break;
                                }
                                level.Blockchange(this, x, y, z, type);
                                break;
                            default:
                                level.Blockchange(this, x, y, z, type);
                                break;
                        }
                    //}
                    //else
                    //{
                    //    level.Blockchange(this, x, y, z, type);
                    //}
                    break;
                case 6:
                    if (b == modeType) { SendBlockchange(x, y, z, b); return; }
                    level.Blockchange(this, x, y, z, modeType);
                    break;
                case 13:    //Small TNT
                    level.Blockchange(this, x, y, z, Block.smalltnt);
                    break;
                case 14:    //Small TNT
                    level.Blockchange(this, x, y, z, Block.bigtnt);
                    break;
                default:
                    Server.s.Log(name + " is breaking something");
                    BlockAction = 0;
                    break;
            }
        }

        void HandleInput(object m)
        {
            if (!loggedIn || trainGrab || following != "" || frozen)
                return;

            byte[] message = (byte[])m;
            byte thisid = message[0];

            ushort x = NTHO(message, 1);
            ushort y = NTHO(message, 3);
            ushort z = NTHO(message, 5);
            byte rotx = message[7];
            byte roty = message[8];
            pos = new ushort[3] { x, y, z };
            rot = new byte[2] { rotx, roty };

            UpdateStatusMessages();

            //Server.s.Debug("Packet Received(8): " + thisid + " " + x + " " + y + " " + z + " " + rotx + " " + roty);
        }

        public void RealDeath(ushort x, ushort y, ushort z)
        {
            byte b = level.GetTile(x, (ushort)(y - 2), z);
            byte b1 = level.GetTile(x, y, z);

            if (oldBlock != (ushort)(x + y + z))
            {
                if (Block.Convert(b) == Block.air)
                {
                    deathCount++;
                    deathBlock = Block.air;
                    return;
                }
                else
                {
                    if (deathCount > level.fall && deathBlock == Block.air)
                    {
                        HandleDeath(deathBlock);
                        deathCount = 0;
                    }
                    else if (deathBlock != Block.water)
                    {
                        deathCount = 0;
                    }
                }
            }

            switch (Block.Convert(b1))
            {
                case Block.water:
                case Block.waterstill:
                case Block.lava:
                case Block.lavastill:
                    deathCount++;
                    deathBlock = Block.water;
                    if (deathCount > level.drown * 200)
                    {
                        HandleDeath(deathBlock);
                        deathCount = 0;
                    }
                    break;
                default:
                    deathCount = 0;
                    break;
            }
        }

        public void CheckBlock(ushort x, ushort y, ushort z)
        {
            y = (ushort)Math.Round((decimal)(((y * 32) + 4) / 32));

            byte b = this.level.GetTile(x, y, z);
            byte b1 = this.level.GetTile(x, (ushort)((int)y - 1), z);


            if (Block.Mover(b) || Block.Mover(b1))
            {
                if (Block.DoorAirs(b) != 0)
                    level.Blockchange(x, y, z, Block.DoorAirs(b));
                if (Block.DoorAirs(b1) != 0)
                    level.Blockchange(x, (ushort)(y - 1), z, Block.DoorAirs(b1));

                if ((x + y + z) != oldBlock)
                {
                    if (b == Block.air_portal || b == Block.water_portal || b == Block.lava_portal)
                    {
                        HandlePortal(this, x, y, z, b);
                    }
                    else if (b1 == Block.air_portal || b1 == Block.water_portal || b1 == Block.lava_portal)
                    {
                        HandlePortal(this, x, (ushort)((int)y - 1), z, b1);
                    }

                    if (b == Block.MsgAir || b == Block.MsgWater || b == Block.MsgLava)
                    {
                        HandleMsgBlock(this, x, y, z, b);
                    }
                    else if (b1 == Block.MsgAir || b1 == Block.MsgWater || b1 == Block.MsgLava)
                    {
                        HandleMsgBlock(this, x, (ushort)((int)y - 1), z, b1);
                    }
                    else if (b1 == Block.flagbase)
                    {
                        if (team != null)
                        {
                            y = (ushort)(y - 1);
                            foreach (Team workTeam in level.ctfgame.teams)
                            {
                                if (workTeam.flagLocation[0] == x && workTeam.flagLocation[1] == y && workTeam.flagLocation[2] == z)
                                {
                                    if (workTeam == team)
                                    {
                                        if (!workTeam.flagishome)
                                        {
                                     //       level.ctfgame.ReturnFlag(this, workTeam, true);
                                        }
                                        else
                                        {
                                            if (carryingFlag)
                                            {
                                                level.ctfgame.CaptureFlag(this, workTeam, hasflag);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        level.ctfgame.GrabFlag(this, workTeam);
                                    }
                                }

                            }
                        }
                    }
                }
            }
            if (Block.Death(b)) HandleDeath(b); else if (Block.Death(b1)) HandleDeath(b1);
        }

        public void HandleDeath(byte b, string customMessage = "", bool explode = false)
        {
            ushort x = (ushort)(pos[0] / 32);
            ushort y = (ushort)(pos[1] / 32);
            ushort z = (ushort)(pos[2] / 32);

            if (lastDeath.AddSeconds(2) < DateTime.Now)
            {

                if (level.Killer && !invincible)
                {
                    
                    switch (b)
                    {
                        case Block.tntexplosion: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " &cblew into pieces.", false); break;
                        case Block.deathair: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " walked into &cnerve gas and suffocated.", false); break;
                        case Block.deathwater:
                        case Block.activedeathwater: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " stepped in &dcold water and froze.", false); break;
                        case Block.deathlava:
                        case Block.activedeathlava: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " stood in &cmagma and melted.", false); break;
                        case Block.magma: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " was hit by &cflowing magma and melted.", false); break;
                        case Block.geyser: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " was hit by &cboiling water and melted.", false); break;
                        case Block.birdkill: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " was hit by a &cphoenix and burnt.", false); break;
                        case Block.train: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " was hit by a &ctrain.", false); break;
                        case Block.fishshark: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " was eaten by a &cshark.", false); break;
                        case Block.fire: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " burnt to a &ccrisp.", false); break;
                        case Block.rockethead: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " was &cin a fiery explosion.", false); level.MakeExplosion(x, y, z, 0); break;
                        case Block.zombiebody: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " died due to lack of &5brain.", false); break;
                        case Block.creeper: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " was killed &cb-SSSSSSSSSSSSSS", false); level.MakeExplosion(x, y, z, 1); break;
                        case Block.air: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " hit the floor &chard.", false); break;
                        case Block.water: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " &cdrowned.", false); break;
                        case Block.Zero: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " was &cterminated", false); break;
                        case Block.fishlavashark: GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + " was eaten by a ... LAVA SHARK?!", false); break;
                        case Block.rock:
                            if (explode) level.MakeExplosion(x, y, z, 1);
                            GlobalChat(this, this.color + this.prefix + this.name + Server.DefaultColor + customMessage, false);
                            break;
                        case Block.stone:
                            if (explode) level.MakeExplosion(x, y, z, 1);
                            GlobalChatLevel(this, this.color + this.prefix + this.name + Server.DefaultColor + customMessage, false);
                            break;
                    }
                    if (team != null && this.level.ctfmode)
                    {
                        if (carryingFlag)
                        {
                            level.ctfgame.DropFlag(this, hasflag);
                        }
                        team.SpawnPlayer(this);
                        this.health = 100;
                    }
                    else
                    {
                        Command.all.Find("spawn").Use(this, "");
                        overallDeath++;
                    }

                    if (Server.deathcount)
                        if (overallDeath % 10 == 0) GlobalChat(this, this.color + this.prefix + this.name + Server.DefaultColor + " has died &3" + overallDeath + " times", false);
                }
                lastDeath = DateTime.Now;
                
            }
        }

        /*       void HandleFly(Player p, ushort x, ushort y, ushort z) {
                FlyPos pos;

                ushort xx; ushort yy; ushort zz;

                TempFly.Clear();

                if (!flyGlass) y = (ushort)(y + 1);

                for (yy = y; yy >= (ushort)(y - 1); --yy)
                for (xx = (ushort)(x - 2); xx <= (ushort)(x + 2); ++xx)
                    for (zz = (ushort)(z - 2); zz <= (ushort)(z + 2); ++zz)
                    if (p.level.GetTile(xx, yy, zz) == Block.air) { 
                        pos.x = xx; pos.y = yy; pos.z = zz;
                        TempFly.Add(pos);
                    }

                FlyBuffer.ForEach(delegate(FlyPos pos2) {
                    try { if (!TempFly.Contains(pos2)) SendBlockchange(pos2.x, pos2.y, pos2.z, Block.air); } catch { }
                });

                FlyBuffer.Clear();

                TempFly.ForEach(delegate(FlyPos pos3){
                    FlyBuffer.Add(pos3);
                });

                if (flyGlass) {
                    FlyBuffer.ForEach(delegate(FlyPos pos1) {
                        try { SendBlockchange(pos1.x, pos1.y, pos1.z, Block.glass); } catch { }
                    });
                } else {
                    FlyBuffer.ForEach(delegate(FlyPos pos1) {
                        try { SendBlockchange(pos1.x, pos1.y, pos1.z, Block.waterstill); } catch { }
                    });
                }
            } */

        void HandleChat(byte[] message)
        {
            try
            {
                if (!loggedIn) return;

                //byte[] message = (byte[])m;
                string text = enc.GetString(message, 1, 64).Trim();

                Server.s.Debug("Packet Received(13): " + message[0] + " " + text);

                if (storedMessage != "")
                {
                    if (!text.EndsWith(">") && !text.EndsWith("<"))
                    {
                        text = storedMessage.Replace("|>|", " ").Replace("|<|", "") + text;
                        storedMessage = "";
                    }
                }
                if (text.EndsWith(">"))
                {
                    storedMessage += text.Replace(">", "|>|");
                    SendMessage("Message appended!");
                    return;
                } 
                else if (text.EndsWith("<"))
                {
                    storedMessage += text.Replace("<", "|<|");
                    SendMessage("Message appended!");
                    return;
                }

                text = Regex.Replace(text, @"\s\s+", " ");
                foreach (char ch in text)
                {
                    if (ch < 32 || ch >= 127 || ch == '&')
                    {
                        Kick("Illegal character in chat message!");
                        return;
                    }
                }
                if (text.Length == 0)
                    return;
                afkCount = 0;

                if (text != "/afk")
                {
                    if (Server.afkset.Contains(this.name))
                    {
                        Server.afkset.Remove(this.name);
                        Player.GlobalMessage("-" + this.color + this.name + Server.DefaultColor + "- is no longer AFK");
                        IRCBot.Say(this.name + " is no longer AFK");
                    }
                }

                if (text[0] == '/' || text[0] == '!')
                {
                    text = text.Remove(0, 1);

                    int pos = text.IndexOf(' ');
                    if (pos == -1)
                    {
                        HandleCommand(text.ToLower(), "");
                        return;
                    }
                    string cmd = text.Substring(0, pos).ToLower();
                    string msg = text.Substring(pos + 1);
                    HandleCommand(cmd, msg);
                    return;
                }

                if (Server.chatmod && !this.voice) { this.SendMessage("Chat moderation is on, you cannot speak."); return; }
                if (muted) { this.SendMessage("You are muted."); return; }  //Muted: Only allow commands


                if (text[0] == '@' || whisper)
                {
                    string newtext = text;
                    if (text[0] == '@') newtext = text.Remove(0, 1).Trim();

                    if (whisperTo == "")
                    {
                        int pos = newtext.IndexOf(' ');
                        if (pos != -1)
                        {
                            string to = newtext.Substring(0, pos);
                            string msg = newtext.Substring(pos + 1);
                            HandleQuery(to, msg); return;
                        }
                        else
                        {
                            SendMessage("No message entered");
                            return;
                        }
                    }
                    else
                    {
                        HandleQuery(whisperTo, newtext);
                        return;
                    }
                }
                if (text[0] == '#' || opchat)
                {
                    string newtext = text;
                    if (text[0] == '#') newtext = text.Remove(0, 1).Trim();

                    GlobalMessageOps("To Ops &f-" + color + name + "&f- " + newtext);
                    if (group.Permission < Server.opchatperm && !Server.devs.Contains(name.ToLower()))
                        SendMessage("To Ops &f-" + color + name + "&f- " + newtext);
                    Server.s.LogOp(name + ": " + newtext);
                    IRCBot.Say(name + ": " + newtext, true);
                    return;
                }
                if (text[0] == ';' || adminchat)
                {
                    string newtext = text;
                    if (text[0] == ';') newtext = text.Remove(0, 1).Trim();

                    GlobalMessageAdmins("To Admins &f-" + color + name + "&f- " + newtext);
                    if (group.Permission < Server.adminchatperm && !Server.devs.Contains(name.ToLower()))
                        SendMessage("To Admins &f-" + color + name + "&f- " + newtext);
                    Server.s.LogAdmin(name + ": " + newtext);
                    IRCBot.Say(name + ": " + newtext, true);
                    return;
                }
                if (text[0] == '\\')
                {
                    if (!Server.gc) { SendMessage("Global Chat is currently disabled."); return; }
                    if (!Server.gcAgreed.Contains(name)) { SendMessage("You must agree to the /gcrules before using Global Chat."); return; }
                    string newtext = text.Remove(0, 1).Trim();

                    GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] " + name + ": &f" + newtext);
                    Server.s.LogGC("[" + Server.gcNick + "] " + name + ": " + newtext);
                    GlobalBot.Say(name + ": " + newtext);
                    return;
                }

                if (this.teamchat)
                {
                    if (team == null)
                    {
                        Player.SendMessage(this, "You are not on a team.");
                        return;
                    }
                    foreach (Player p in team.players)
                    {
                        Player.SendMessage(p, "(" + team.teamstring + ") " + this.color + this.name + ":&f " + text);
                    }
                    return;
                }
                if (this.joker)
                {
                    if (File.Exists("text/joker.txt"))
                    {
                        Server.s.Log("<JOKER>: " + this.name + ": " + text);
                        Player.GlobalMessageOps(Server.DefaultColor + "<&aJ&bO&cK&5E&9R" + Server.DefaultColor + ">: " + this.color + this.name + ":&f " + text);
                        FileInfo jokertxt = new FileInfo("text/joker.txt");
                        StreamReader stRead = jokertxt.OpenText();
                        List<string> lines = new List<string>();
                        Random rnd = new Random();
                        int i = 0;

                        while (!(stRead.Peek() == -1))
                            lines.Add(stRead.ReadLine());

                        i = rnd.Next(lines.Count);

                        stRead.Close();
                        stRead.Dispose();
                        text = lines[i];
                    }
                    else { File.Create("text/joker.txt"); }

                }

                if (!level.worldChat)
                {
                    Server.s.Log("<" + name + ">[level] " + text);
                    GlobalChatLevel(this, text, true);
                    return;
                }

                if (text[0] == '%')
                {
                    string newtext = text;
                    if (!Server.worldChat)
                    {
                        newtext = text.Remove(0, 1).Trim();
                        GlobalChatWorld(this, newtext, true);
                    }
                    else
                    {
                        GlobalChat(this, newtext);
                    }
                    Server.s.Log("<" + name + "> " + newtext);
                    IRCBot.Say("<" + name + "> " + newtext);
                    return;
                }
                Server.s.Log("<" + name + "> " + text);

                if (Server.worldChat)
                {
                    GlobalChat(this, text);
                }
                else
                {
                    GlobalChatLevel(this, text, true);
                }

                IRCBot.Say(name + ": " + text);
            }
            catch (Exception e) { Server.ErrorLog(e); Player.GlobalMessage("An error occurred: " + e.Message); }
        }
        public void HandleCommand(string cmd, string message)
        {
            try
            {
                if (cmd == "") { SendMessage("No command entered."); return; }
                if (jailed) { SendMessage("You cannot use any commands while jailed."); return; }
                if (cmd.ToLower() == "care") { SendMessage("Corneria now loves you with all his heart."); return; }
                if (cmd.ToLower() == "facepalm") { SendMessage("Lawlcat's bot army just simultaneously facepalm'd at your use of this command."); return; }
                if (cmd.ToLower() == "dev" && Server.devs.Contains(name.ToLower()))
                {
                    SendMessage("Dev commands not yet implemented");
                    return;
                }
                
                string foundShortcut = Command.all.FindShort(cmd);
                if (foundShortcut != "") cmd = foundShortcut;

                try
                {
                    int foundCb = int.Parse(cmd);
                    if (messageBind[foundCb] == null) { SendMessage("No CMD is stored on /" + cmd); return; }
                    message = messageBind[foundCb] + " " + message;
                    message = message.TrimEnd(' ');
                    cmd = cmdBind[foundCb];
                }
                catch { }

                Command command = Command.all.Find(cmd);
                if (command != null)
                {
                    if (group.CanExecute(command))
                    {
                        if (cmd != "repeat") lastCMD = cmd + " " + message;
                        if (level.name.Contains("Museum " + Server.DefaultColor))
                        {
                            if(!command.museumUsable)
                            {
                                SendMessage("Cannot use this command while in a museum!");
                                return;
                            }
                        }
                        if (this.joker == true || this.muted == true)
                        {
                            if (cmd.ToLower() == "me")
                            {
                                SendMessage("Cannot use /me while muted or jokered.");
                                return;
                            }
                        }

                        Server.s.CommandUsed(name + " used /" + cmd + " " + message);
                        this.commThread = new Thread(new ThreadStart(delegate
                        {
                            try
                            {
                                command.Use(this, message);
                            }
                            catch (Exception e)
                            {
                                Server.ErrorLog(e);
                                Player.SendMessage(this, "An error occured when using the command!");
                            }
                        }));
                        commThread.Start();
                    }
                    else { SendMessage("You are not allowed to use \"" + cmd + "\"!"); }
                }
                else if (Block.Byte(cmd.ToLower()) != Block.Zero)
                {
                    HandleCommand("mode", cmd.ToLower());
                }
                else
                {
                    bool retry = true;

                    switch (cmd.ToLower())
                    {    //Check for command switching
                        case "cut": cmd = "copy"; message = "cut"; goto retry;
                        case "ps": message = "ps " + message; cmd = "map"; goto retry;

                        default: retry = false; break;  //Unknown command, then
                    }

                    foreach (Group grp in Group.GroupList)
                    {
                        if (cmd.ToLower() == grp.name.ToLower())
                        {
                            message = message + " " + cmd.ToLower();
                            cmd = "setrank";
                            retry = true;
                            break;
                        }
                        if (cmd.ToLower() == grp.name.ToLower() + "s")
                        {
                            message = cmd;
                            cmd = "viewranks";
                            retry = true;
                            break;
                        }
                    }

                retry:
                    if (retry) HandleCommand(cmd, message);
                    else SendMessage("Unknown command \"" + cmd + "\"!");
                }
            }
            catch (Exception e) { Server.ErrorLog(e); SendMessage("Command failed."); }
        }
        void HandleQuery(string to, string message)
        {
            Player p = Find(to);
            if (p == this) { SendMessage("Trying to talk to yourself, huh?"); return; }
            if (p != null && !p.hidden)
            {
                Server.s.Log(name + " @" + p.name + ": " + message);
                SendChat(this, Server.DefaultColor + "[<] " + p.color + p.prefix + p.name + ": &f" + message);
                SendChat(p, "&9[>] " + this.color + this.prefix + this.name + ": &f" + message);
            }
            else { SendMessage("Player \"" + to + "\" doesn't exist!"); }
        }
        #endregion
        #region == OUTGOING ==
        public void SendRaw(Magic magic)
        {
            SendRaw(magic, new byte[0]);
        }
        public void SendRaw(Magic magic, byte[] send)
        {
            SendRaw((int)magic, send);
        }
        public void SendRaw(int id)
        {
            SendRaw(id, new byte[0]);
        }
        public void SendRaw(int id, byte[] send)
        {

            byte[] buffer = new byte[send.Length + 1];
            buffer[0] = (byte)id;

            Buffer.BlockCopy(send, 0, buffer, 1, send.Length);
            string TxStr = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                TxStr += buffer[i];
            }
            int tries = 0;
        retry: try
            {
            
                socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, delegate(IAsyncResult result) { }, null);
            }
            catch (SocketException)
            {
                tries++;
                if (tries > 2)
                    Disconnect();
                else goto retry;
            }
        }

        

        public static void SendMessage(Player p, string message, MessageType type = MessageType.CHAT)
        {
            if (p == null) {

                message = Server.stripColors(message);

                if (storeHelp)
                {
                    storedHelp += message + "\r\n";
                }
                else
                {
                    Server.s.Log(message);
                    IRCBot.Say(message, true); 
                }
                return; 
            }
            p.SendMessage((byte)type, Server.DefaultColor + message);
        }
        public void SendMessage(string message, MessageType type = MessageType.CHAT)
        {
            if (this == null) { Server.s.Log(message); return; }
            unchecked { SendMessage((byte)type, Server.DefaultColor + message); }
        }
        public void SendChat(Player p, string message)
        {
            if (this == null) { Server.s.Log(message); return; }
            Player.SendMessage(p, message);
        }
        public void SendMessage(byte type, string message)
        {
            if (this == null) { Server.s.Log(message); return; }
            if (ZoneSpam.AddSeconds(2) > DateTime.Now && message.Contains("This zone belongs to ")) return;

            byte[] buffer = new byte[65];
            unchecked { buffer[0] = (extensions.Contains(Extension.MessageTypes)) ? type : (byte)0; }

            for (int i = 0; i < 10; i++)
            {
                message = message.Replace("%" + i, "&" + i);
                message = message.Replace("&" + i + " &", " &");
            }
            for (char ch = 'a'; ch <= 'f'; ch++)
            {
                message = message.Replace("%" + ch, "&" + ch);
                message = message.Replace("&" + ch + " &", " &");
            }

            if (Server.dollardollardollar)
                message = message.Replace("$name", "$" + name);
            else
                message = message.Replace("$name", name);
            message = message.Replace("$date", DateTime.Now.ToString("yyyy-MM-dd"));
            message = message.Replace("$time", DateTime.Now.ToString("HH:mm:ss"));
            message = message.Replace("$ip", ip);
            message = message.Replace("$color", color);
            message = message.Replace("$rank", group.name);
            message = message.Replace("$level", level.name);
            message = message.Replace("$deaths", overallDeath.ToString());
            message = message.Replace("$money", money.ToString());
            message = message.Replace("$blocks", overallBlocks.ToString());
            message = message.Replace("$first", firstLogin.ToString());
            message = message.Replace("$kicked", totalKicked.ToString());
            message = message.Replace("$server", Server.name);
            message = message.Replace("$motd", Server.motd);

            message = message.Replace("$irc", Server.ircServer + " > " + Server.ircChannel);

            if (Server.parseSmiley && parseSmiley)
            {
                message = message.Replace(":)", "(darksmile)");
                message = message.Replace(":D", "(smile)");
                message = message.Replace("<3", "(heart)");
            }

            byte[] stored = new byte[1];

            stored[0] = (byte)1;
            message = message.Replace("(darksmile)", enc.GetString(stored));
            stored[0] = (byte)2;
            message = message.Replace("(smile)", enc.GetString(stored));
            stored[0] = (byte)3;
            message = message.Replace("(heart)", enc.GetString(stored));
            stored[0] = (byte)4;
            message = message.Replace("(diamond)", enc.GetString(stored));
            stored[0] = (byte)7;
            message = message.Replace("(bullet)", enc.GetString(stored));
            stored[0] = (byte)8;
            message = message.Replace("(hole)", enc.GetString(stored));
            stored[0] = (byte)11;
            message = message.Replace("(male)", enc.GetString(stored));
            stored[0] = (byte)12;
            message = message.Replace("(female)", enc.GetString(stored));
            stored[0] = (byte)15;
            message = message.Replace("(sun)", enc.GetString(stored));
            stored[0] = (byte)16;
            message = message.Replace("(right)", enc.GetString(stored));
            stored[0] = (byte)17;
            message = message.Replace("(left)", enc.GetString(stored));
            stored[0] = (byte)19;
            message = message.Replace("(double)", enc.GetString(stored));
            stored[0] = (byte)22;
            message = message.Replace("(half)", enc.GetString(stored));
            stored[0] = (byte)24;
            message = message.Replace("(uparrow)", enc.GetString(stored));
            stored[0] = (byte)25;
            message = message.Replace("(downarrow)", enc.GetString(stored));
            stored[0] = (byte)26;
            message = message.Replace("(rightarrow)", enc.GetString(stored));
            stored[0] = (byte)30;
            message = message.Replace("(up)", enc.GetString(stored));
            stored[0] = (byte)31;
            message = message.Replace("(down)", enc.GetString(stored));

            int totalTries = 0;
        retryTag: try
            {

                if (OnPlayerSendMessageEvent != null) OnPlayerSendMessageEvent(this, message);
                if (OnSendMessageEvent != null) OnSendMessageEvent(message);
                if (noSendMessage) { Server.s.Debug("SendMessageEvent cancelled (noSendMessage = true)"); return; }

                foreach (string line in Wordwrap(message))
                {
                    string newLine = line;
                    if (newLine.TrimEnd(' ')[newLine.TrimEnd(' ').Length - 1] < '!')
                    {
                        newLine += '\'';
                    }

                    StringFormat(newLine, 64).CopyTo(buffer, 1);
                    //Server.s.Debug("Sending Packet(13): " + newLine);
                    SendRaw(Magic.CHAT_MESSAGE, buffer);
                }
            }
            catch (Exception e)
            {
                message = "&f" + message;
                totalTries++;
                if (totalTries < 10) goto retryTag;
                else Server.ErrorLog(e);
            }
        }
        public void SendMotd()
        {
            byte[] buffer = new byte[130];
            buffer[0] = (byte)8;
            StringFormat(Server.name, 64).CopyTo(buffer, 1);
            StringFormat(Server.motd, 64).CopyTo(buffer, 65);

            if (Block.canPlace(this, Block.blackrock))
                buffer[129] = 100;
            else
                buffer[129] = 0;

            Server.s.Debug("Sending Packet(0): 8 " + Server.name + " " + Server.motd + " " + buffer[129]);
            SendRaw(Magic.IDENTIFICATION, buffer);
            
        }

        public void SendUserMOTD()
        {
            byte[] buffer = new byte[130];
            Random rand = new Random();
            buffer[0] = Server.version;
            if (level.motd == "ignore") { StringFormat(Server.name, 64).CopyTo(buffer, 1); StringFormat(Server.motd, 64).CopyTo(buffer, 65); }
            else StringFormat(level.motd, 128).CopyTo(buffer, 1);

            if (Block.canPlace(this.group.Permission, Block.blackrock))
                buffer[129] = 100;
            else
                buffer[129] = 0;
            Server.s.Debug("Sending Packet(0): " + ((level.motd == "ignore") ? Server.name + " " + Server.motd : level.motd) + " " + buffer[129]);
            SendRaw(Magic.IDENTIFICATION, buffer);
        }

        public void SendMap()
        {
            Server.s.Debug("Sending Packet(2)");
            SendRaw(Magic.LEVEL_INIT);
            byte[] buffer = new byte[level.blocks.Length + 4];
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(level.blocks.Length)).CopyTo(buffer, 0);
            //ushort xx; ushort yy; ushort z;z

            for (int i = 0; i < level.blocks.Length; ++i)
            {
                if (extensions.Contains(Extension.CustomBlocks) && (CustomBlockSupportLevel >= Block.SupportLevel(level.blocks[i])))
                    buffer[4 + i] = Block.Convert(level.blocks[i]);
                else
                    buffer[4 + i] = Block.Convert(Block.Fallback(level.blocks[i]));
            }

            buffer = GZip(buffer);
            int number = (int)Math.Ceiling(((double)buffer.Length) / 1024);
            for (int i = 1; buffer.Length > 0; ++i)
            {
                short length = (short)Math.Min(buffer.Length, 1024);
                byte[] send = new byte[1027];
                HTNO(length).CopyTo(send, 0);
                Buffer.BlockCopy(buffer, 0, send, 2, length);
                byte[] tempbuffer = new byte[buffer.Length - length];
                Buffer.BlockCopy(buffer, length, tempbuffer, 0, buffer.Length - length);
                buffer = tempbuffer;
                send[1026] = (byte)(i * 100 / number);
                Server.s.Debug("Sending Packet(3): " + length + " {chunk data} " + (i * 100 / number).ToString());
                SendRaw(Magic.LEVEL_DATA, send);
                if (ip == "127.0.0.1") { }
                else if (Server.updateTimer.Interval > 1000) Thread.Sleep(100);
                else Thread.Sleep(10);
            }
            SendHackControl();
            buffer = new byte[6];
            HTNO((short)level.width).CopyTo(buffer, 0);
            HTNO((short)level.depth).CopyTo(buffer, 2);
            HTNO((short)level.height).CopyTo(buffer, 4);
            Server.s.Debug("Sending Packet(4): " + level.width + " " + level.depth + " " + level.height);
            SendRaw(Magic.LEVEL_FINALIZE, buffer);
            Loading = false;

            if (extensions.Contains(Extension.EnvWeatherType))
                SendWeather(level.weather);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void SendExtInfo()
        {
            byte[] buffer = new byte[66];

            StringFormat("MCSong Server v" + Server.Version, 64).CopyTo(buffer, 0);
            HTNO((short)Extension.all.Count).CopyTo(buffer, 64);

            Server.s.Debug("Sending Packet(16): MCSong Server " + Extension.all.Count);
            SendRaw(Magic.EXTINFO, buffer);
        }

        public void SendExtEntry()
        {
            foreach (Extension e in Extension.all)
            {
                byte[] buffer = new byte[68];
                StringFormat(e.name, 64).CopyTo(buffer, 0);
                HTNO(e.version).CopyTo(buffer, 64);
                Server.s.Debug("Sending Packet(17): " + e.name + " " + e.version);
                SendRaw(Magic.EXTENTRY, buffer);
            }
        }

        public void SendClickDistance(short distance)
        {
            clickDistance = distance;
            byte[] buffer = new byte[2];
            HTNO(distance).CopyTo(buffer, 0);
            Server.s.Debug("Sending Packet(18): " + distance);
            SendRaw(Magic.CLICK_DISTANCE, buffer);
            Server.s.Log(name + "'s click distance was set to " + distance);
        }

        public void SendCustomBlockSupportLevel()
        {
            byte[] buffer = new byte[1];
            buffer[0] = Server.CustomBlockSupportLevel;
            Server.s.Debug("Sending Packet(19): " + Server.CustomBlockSupportLevel);
            SendRaw(Magic.CUSTOM_BLOCK_SUPPORT_LEVEL, buffer);
        }

        public void SendHackControl()
        {
            byte[] buffer = new byte[7];
            buffer[0] = (level.hacks.Flying) ? (byte)1 : (byte)0;
            buffer[1] = (level.hacks.NoClip) ? (byte)1 : (byte)0;
            buffer[2] = (level.hacks.Speeding) ? (byte)1 : (byte)0;
            buffer[3] = (level.hacks.SpawnControl) ? (byte)1 : (byte)0;
            buffer[4] = (level.hacks.ThirdPerson) ? (byte)1 : (byte)0;
            HTNO(level.hacks.JumpHeight).CopyTo(buffer, 5);
            Server.s.Debug("Sending Packet(32): " + buffer[0] + " " + buffer[1] + " " + buffer[2] + " " + buffer[3] + " " + buffer[4] + " " + level.hacks.JumpHeight);
            SendRaw(Magic.HACK_CONTROL, buffer);
        }

        public void SendWeather(Weather weather)
        {
            byte[] buffer = new byte[1] { (byte)weather };
            Server.s.Debug("Sending Packet(31): " + weather.ToString());
            SendRaw(Magic.ENV_SET_WEATHER_TYPE, buffer);
        }

        public void SendSpawn(byte id, string name, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            pos = new ushort[3] { x, y, z }; // This could be remove and not effect the server :/
            rot = new byte[2] { rotx, roty };
            byte[] buffer = new byte[73]; buffer[0] = id;
            StringFormat(name, 64).CopyTo(buffer, 1);
            HTNO(x).CopyTo(buffer, 65);
            HTNO(y).CopyTo(buffer, 67);
            HTNO(z).CopyTo(buffer, 69);
            buffer[71] = rotx; buffer[72] = roty;
            Server.s.Debug("Sending Packet(7): " + id + " " + name + " " + x + " " + y + " " + z + " " + rotx + " " + roty);
            SendRaw(Magic.SPAWN_PLAYER, buffer);
        }
        public void SendPos(byte id, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            if (x < 0) x = 32;
            if (y < 0) y = 32;
            if (z < 0) z = 32;
            if (x > level.width * 32) x = (ushort)(level.width * 32 - 32);
            if (z > level.height * 32) z = (ushort)(level.height * 32 - 32);
            if (x > 32767) x = 32730;
            if (y > 32767) y = 32730;
            if (z > 32767) z = 32730;

            pos[0] = x; pos[1] = y; pos[2] = z;
            rot[0] = rotx; rot[1] = roty;

            /*
            pos = new ushort[3] { x, y, z };
            rot = new byte[2] { rotx, roty };*/
            byte[] buffer = new byte[9]; buffer[0] = id;
            HTNO(x).CopyTo(buffer, 1);
            HTNO(y).CopyTo(buffer, 3);
            HTNO(z).CopyTo(buffer, 5);
            buffer[7] = rotx; buffer[8] = roty;
            //Server.s.Debug("Sending Packet(8): " + id + " " + x + " " + y + " " + z + " " + rotx + " " + roty);
            SendRaw(Magic.POSITION_ROTATION, buffer);
        }
        //TODO: Figure a way to SendPos without changing rotation
        public void SendDie(byte id) { Server.s.Debug("Sending Packet(0x0C): " + id); SendRaw(Magic.DESPAWN_PLAYER, new byte[1] { id }); }
        public void SendBlockchange(ushort x, ushort y, ushort z, byte type)
        {
            if (!extensions.Contains(Extension.CustomBlocks) || (CustomBlockSupportLevel < Block.SupportLevel(type)))
            {
                Server.s.Debug("Sending fallback block:" + type + " > " + Block.Fallback(type));
                type = Block.Fallback(type);
            }
            if (x < 0 || y < 0 || z < 0) return;
            if (x >= level.width || y >= level.depth || z >= level.height) return;

            byte[] buffer = new byte[7];
            HTNO(x).CopyTo(buffer, 0);
            HTNO(y).CopyTo(buffer, 2);
            HTNO(z).CopyTo(buffer, 4);
            buffer[6] = Block.Convert(type);
            Server.s.Debug("Sending Packet(6): " + x + " " + y + " " + z + " " + Block.Convert(type));
            SendRaw(Magic.BLOCK_CHANGE, buffer);
        }
        void SendKick(string message) { Server.s.Debug("Sending Packet(14): " + message); SendRaw(Magic.DISCONNECT, StringFormat(message, 64)); }
        void SendPing() { /*pingDelay = 0; pingDelayTimer.Start(); Server.s.Debug("Sending Packet(1)");*/ SendRaw(Magic.PING); }
        void UpdatePosition()
        {

            //pingDelayTimer.Stop();

            // Shameless copy from JTE's Server
            byte changed = 0;   //Denotes what has changed (x,y,z, rotation-x, rotation-y)
            // 0 = no change - never happens with this code.
            // 1 = position has changed
            // 2 = rotation has changed
            // 3 = position and rotation have changed
            // 4 = Teleport Required (maybe something to do with spawning)
            // 5 = Teleport Required + position has changed
            // 6 = Teleport Required + rotation has changed
            // 7 = Teleport Required + position and rotation has changed
            //NOTE: Players should NOT be teleporting this often. This is probably causing some problems.
            if (oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2])
                changed |= 1;

            if (oldrot[0] != rot[0] || oldrot[1] != rot[1])
            {
                changed |= 2;
            }
            if (Math.Abs(pos[0] - basepos[0]) > 32 || Math.Abs(pos[1] - basepos[1]) > 32 || Math.Abs(pos[2] - basepos[2]) > 32)
                changed |= 4;

            if ((oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2]) && (basepos[0] != pos[0] || basepos[1] != pos[1] || basepos[2] != pos[2]))
                changed |= 4;

            byte[] buffer = new byte[0]; Magic msg = 0; string content = "";
            if ((changed & 4) != 0)
            {
                msg = Magic.POSITION_ROTATION; //Player teleport - used for spawning or moving too fast
                buffer = new byte[9]; buffer[0] = id;
                HTNO(pos[0]).CopyTo(buffer, 1);
                HTNO(pos[1]).CopyTo(buffer, 3);
                HTNO(pos[2]).CopyTo(buffer, 5);
                buffer[7] = rot[0];

                content = id + " " + pos[0] + " " + pos[1] + " " + pos[2] + " " + rot[0] + " ";

                if (Server.flipHead)
                {
                    if (rot[1] > 64 && rot[1] < 192) { buffer[8] = rot[1]; content += rot[1]; }
                    else { buffer[8] = (byte)(rot[1] - (rot[1] - 128)); content += (rot[1] - (rot[1] - 128)); }
                }
                else { buffer[8] = rot[1]; content += rot[1]; }

                //Realcode
                //buffer[8] = rot[1];
                
            }
            else if (changed == 1)
            {
                try
                {
                    msg = Magic.POSITION_UPDATE; //Position update
                    buffer = new byte[4]; buffer[0] = id;
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[0] - oldpos[0])), 0, buffer, 1, 1);
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[1] - oldpos[1])), 0, buffer, 2, 1);
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[2] - oldpos[2])), 0, buffer, 3, 1);
                    content = id + " " + (pos[0] - oldpos[0]) + " " + (pos[1] - oldpos[1]) + " " + (pos[2] - oldpos[2]);
                }
                catch { }
            }
            else if (changed == 2)
            {
                msg = Magic.ROTATION_UPDATE; //Orientation update
                buffer = new byte[3]; buffer[0] = id;
                buffer[1] = rot[0];

                content = id + " " + rot[0] + " ";

                if (Server.flipHead)
                {
                    if (rot[1] > 64 && rot[1] < 192) { buffer[2] = rot[1]; content += rot[1]; }
                    else { buffer[2] = (byte)(rot[1] - (rot[1] - 128)); content += (rot[1] - (rot[1] - 128)); }
                }
                else { buffer[2] = rot[1]; content += rot[1]; }

                //Realcode
                //buffer[2] = rot[1];
            }
            else if (changed == 3)
            {
                try
                {
                    msg = Magic.POSITION_ROTATION_UPDATE; //Position and orientation update
                    buffer = new byte[6]; buffer[0] = id;
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[0] - oldpos[0])), 0, buffer, 1, 1);
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[1] - oldpos[1])), 0, buffer, 2, 1);
                    Buffer.BlockCopy(System.BitConverter.GetBytes((sbyte)(pos[2] - oldpos[2])), 0, buffer, 3, 1);
                    buffer[4] = rot[0];

                    content = id + " " + (pos[0] - oldpos[0]) + " " + (pos[1] - oldpos[1]) + " " + (pos[2] - oldpos[2]) + " " + rot[0] + " ";

                    if (Server.flipHead)
                    {
                        if (rot[1] > 64 && rot[1] < 192) { buffer[5] = rot[1]; content += rot[1]; }
                        else { buffer[5] = (byte)(rot[1] - (rot[1] - 128)); content += (rot[1] - (rot[1] - 128)); }
                    }
                    else { buffer[5] = rot[1]; content += rot[1]; }

                    //Realcode
                    //buffer[5] = rot[1];
                }
                catch { }
            }

            oldpos = pos; oldrot = rot;
            if (changed != 0)
                try
                {
                    foreach (Player p in players)
                    {
                        if (p != this && p.level == level)
                        {
                            Server.s.Debug("Sending Packet(" + msg + "): " + content);
                            p.SendRaw(msg, buffer);
                        }
                    }
                } catch { }
        }
        #endregion
        #region == GLOBAL MESSAGES ==
        public static void GlobalBlockchange(Level level, ushort x, ushort y, ushort z, byte type)
        {
            players.ForEach(delegate(Player p) { if (p.level == level) { p.SendBlockchange(x, y, z, type); } });
        }
        public static void GlobalChat(Player from, string message) { GlobalChat(from, message, true); }
        public static void GlobalChat(Player from, string message, bool showname)
        {
            if (showname) { message = from.color + from.voicestring + from.color + from.prefix + from.name + ": &f" + message; }
            players.ForEach(delegate(Player p) { if (p.level.worldChat) Player.SendMessage(p, message); });
        }
        public static void GlobalChatLevel(Player from, string message, bool showname)
        {
            if (showname) { message = "<Level>" + from.color + from.voicestring + from.color + from.prefix + from.name + ": &f" + message; }
            players.ForEach(delegate(Player p) { if (p.level == from.level) Player.SendMessage(p, Server.DefaultColor + message); });
        }
        public static void GlobalChatWorld(Player from, string message, bool showname)
        {
            if (showname) { message = "<World>" + from.color + from.voicestring + from.color + from.prefix + from.name + ": &f" + message; }
            players.ForEach(delegate(Player p) { if (p.level.worldChat) Player.SendMessage(p, Server.DefaultColor + message); });
        }
        public static void GlobalMessage(string message)
        {
            message = message.Replace("%", "&");
            players.ForEach(delegate(Player p) { if (p.level.worldChat) Player.SendMessage(p, message); });
        }
        public static void GlobalMessageLevel(Level l, string message)
        {
            players.ForEach(delegate(Player p) { if (p.level == l) Player.SendMessage(p, message); });
        }
        public static void GlobalMessageGC(string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if (Server.gcAgreed.Contains(p.name) || Server.devs.Contains(p.name.ToLower()))
                    {
                        Player.SendMessage(p, message);
                    }
                });
            }
            catch { Server.s.Log("Error occured with Global Chat"); }
        }
        public static void GlobalMessageOps(string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if (p.group.Permission >= Server.opchatperm || Server.devs.Contains(p.name.ToLower()))
                    {
                        Player.SendMessage(p, message);
                    }
                });
            }
            catch { Server.s.Log("Error occured with Op Chat"); }
        }
        public static void GlobalMessageAdmins(string message)
        {
            try
            {
                players.ForEach(delegate(Player p)
                {
                    if (p.group.Permission >= Server.adminchatperm || Server.devs.Contains(p.name.ToLower()))
                    {
                        Player.SendMessage(p, message);
                    }
                });
            }
            catch { Server.s.Log("Error occured with Admin Chat"); }
        }
        public static void GlobalSpawn(Player from, ushort x, ushort y, ushort z, byte rotx, byte roty, bool self, string possession = "")
        {
            players.ForEach(delegate(Player p)
            {
                if (p.Loading && p != from) { return; }
                if (p.level != from.level || (from.hidden && !self)) { return; }
                if (p != from) { p.SendSpawn(from.id, from.color + from.name + possession, x, y, z, rotx, roty); }
                else if (self)
                {
                    if (!p.ignorePermission)
                    {
                        p.pos = new ushort[3] { x, y, z }; p.rot = new byte[2] { rotx, roty };
                        p.oldpos = p.pos; p.basepos = p.pos; p.oldrot = p.rot;
                        unchecked { p.SendSpawn((byte)-1, from.color + from.name + possession, x, y, z, rotx, roty); }
                    }
                }
            });
        }
        public static void GlobalDie(Player from, bool self)
        {
            players.ForEach(delegate(Player p)
            {
                if (p.level != from.level || (from.hidden && !self)) { return; }
                if (p != from) { p.SendDie(from.id); }
                else if (self) { unchecked { p.SendDie((byte)-1); } }
            });
        }

        public bool MarkPossessed(string marker = "")
        {
            if (marker != "")
            {
                Player controller = Player.Find(marker);
                if (controller == null)
                {
                    return false;
                }
                marker = " (" + controller.color + controller.name + color + ")";
            }
            GlobalDie(this, true);
            GlobalSpawn(this, pos[0], pos[1], pos[2], rot[0], rot[1], true, marker);
            return true;
        }

        public static void GlobalUpdate() { try { players.ForEach(delegate (Player p) { if (!p.hidden) { p.UpdatePosition(); } }); } catch { } }
        #endregion
        #region == DISCONNECTING ==
        public void Disconnect() { leftGame(); }
        public void Kick(string kickString)
        {
            if (Player.OnPlayerKickedEvent != null) OnPlayerKickedEvent(this, kickString);
            if (this.OnKickedEvent != null) OnKickedEvent(kickString);
            if (noKick) return;
            leftGame(kickString);
        }

        public void leftGame(string kickString = "", bool skip = false)
        {
            try
            {
                if (disconnected)
                {
                    if (connections.Contains(this)) connections.Remove(this);
                    return;
                }
                //   FlyBuffer.Clear();
                disconnected = true;
                pingTimer.Stop();
                afkTimer.Stop();
                afkCount = 0;
                afkStart = DateTime.Now;

                if (Server.afkset.Contains(name)) Server.afkset.Remove(name);

                if (kickString == "") kickString = "Disconnected.";

                SendKick(kickString);

                if (loggedIn)
                {
                    isFlying = false;
                    aiming = false;

                    if (team != null)
                    {
                        team.RemoveMember(this);
                    }

                    GlobalDie(this, false);
                    if (kickString == "Disconnected." || kickString.IndexOf("Server shutdown") != -1 || kickString == Server.customShutdownMessage)
                    {
                        if (!hidden) { GlobalChat(this, "&c- " + color + prefix + name + Server.DefaultColor + " disconnected.", false); }
                        IRCBot.Say(name + " left the game.");
                        Server.s.Log(name + " disconnected.");
                    }
                    else
                    {
                        totalKicked++;
                        GlobalChat(this, "&c- " + color + prefix + name + Server.DefaultColor + " kicked (" + kickString + ").", false);
                        IRCBot.Say(name + " kicked (" + kickString + ").");
                        Server.s.Log(name + " kicked (" + kickString + ").");
                    }

                    try { save(); }
                    catch (Exception e) { Server.ErrorLog(e); }

                    players.Remove(this);
                    Server.s.PlayerListUpdate();
                    left.Add(this.name.ToLower(), this.ip);
                    //PlayerDB.allOffline.Add(new OfflinePlayer(name));
                    if (Server.AutoLoad && level.unload)
                    {
                        foreach (Player pl in Player.players)
                            if (pl.level == level) return;
                        if (!level.name.Contains("Museum " + Server.DefaultColor))
                        {
                            level.Unload();
                        }
                    }

                    try
                    {
                        if (!Directory.Exists("extra/undo")) Directory.CreateDirectory("extra/undo");
                        if (!Directory.Exists("extra/undoPrevious")) Directory.CreateDirectory("extra/undoPrevious");
                        DirectoryInfo di = new DirectoryInfo("extra/undo");
                        if (di.GetDirectories("*").Length >= Server.totalUndo)
                        {
                            Directory.Delete("extra/undoPrevious", true);
                            Directory.Move("extra/undo", "extra/undoPrevious");
                            Directory.CreateDirectory("extra/undo");
                        }

                        if (!Directory.Exists("extra/undo/" + name)) Directory.CreateDirectory("extra/undo/" + name);
                        di = new DirectoryInfo("extra/undo/" + name);
                        StreamWriter w = new StreamWriter(File.Create("extra/undo/" + name + "/" + di.GetFiles("*.undo").Length + ".undo"));

                        foreach (UndoPos uP in UndoBuffer)
                        {
                            w.Write(uP.mapName + " " +
                                    uP.x + " " + uP.y + " " + uP.z + " " +
                                    uP.timePlaced.ToString().Replace(' ', '&') + " " +
                                    uP.type + " " + uP.newtype + " ");
                        }
                        w.Flush();
                        w.Close();
                    }
                    catch (Exception e) { Server.ErrorLog(e); }

                    UndoBuffer.Clear();
                }
                else
                {
                    connections.Remove(this);
                    Server.s.Log(ip + " disconnected.");
                }
            }
            catch (Exception e) { Server.ErrorLog(e); }
        }


        #endregion
        #region == CHECKING ==
        public static List<Player> GetPlayers() { return new List<Player>(players); }
        public static bool Exists(string name)
        {
            foreach (Player p in players)
            { if (p.name.ToLower() == name.ToLower()) { return true; } } return false;
        }
        public static bool Exists(byte id)
        {
            foreach (Player p in players)
            { if (p.id == id) { return true; } } return false;
        }
        public static Player Find(string name)
        {
            List<Player> tempList = new List<Player>();
            tempList.AddRange(players);
            Player tempPlayer = null; bool returnNull = false;

            foreach (Player p in tempList)
            {
                if (p.name.ToLower() == name.ToLower()) return p;
                if (p.name.ToLower().IndexOf(name.ToLower()) != -1)
                {
                    if (tempPlayer == null) tempPlayer = p;
                    else returnNull = true;
                }
            }

            if (returnNull == true) return null;
            if (tempPlayer != null) return tempPlayer;
            return null;
        }
        public static Group GetGroup(string name)
        {
            return Group.findPlayerGroup(name);
        } 
        public static string GetColor(string name)
        { 
            return GetGroup(name).color; 
        }
        #endregion
        #region == OTHER ==
        static byte FreeId()
        {
            /*
            for (byte i = 0; i < 255; i++)
            {
                foreach (Player p in players)
                {
                    if (p.id == i) { goto Next; }
                } return i;
            Next: continue;
            } unchecked { return (byte)-1; }*/

            for (byte i = 0; i < 255; i++)
            {
                bool used = false;
                foreach (Player p in players)
                    if (p.id == i) used = true;
                if (!used)
                    return i;
            }
            return (byte)1;
        }
        static byte[] StringFormat(string str, int size)
        {
            byte[] bytes = new byte[size];
            bytes = enc.GetBytes(str.PadRight(size).Substring(0, size));
            return bytes;
        }
        static List<string> Wordwrap(string message)
        {
            List<string> lines = new List<string>();
            message = Regex.Replace(message, @"(&[0-9a-f])+(&[0-9a-f])", "$2");
            message = Regex.Replace(message, @"(&[0-9a-f])+$", "");

            int limit = 64; string color = "";

            while (message.Length > 0)
            {
                //if (Regex.IsMatch(message, "&a")) break;

                if (lines.Count > 0)
                {
                    if (message[0].ToString() == "&")
                        message = "> " + message.Trim();
                    else
                        message = "> " + color + message.Trim();
                }

                if (message.IndexOf("&") == message.IndexOf("&", message.IndexOf("&") + 1) - 2)
                    message = message.Remove(message.IndexOf("&"), 2);

                if (message.Length <= limit) { lines.Add(message); break; }
                for (int i = limit - 1; i > limit - 20; --i)
                    if (message[i] == ' ')
                    {
                        lines.Add(message.Substring(0, i));
                        goto Next;
                    }

            retry:
                if (message.Length == 0 || limit == 0) { return lines; }

                try
                {
                    if (message.Substring(limit - 2, 1) == "&" || message.Substring(limit - 1, 1) == "&")
                    {
                        message = message.Remove(limit - 2, 1);
                        limit -= 2;
                        goto retry;
                    }
                    else if (message[limit - 1] < 32 || message[limit - 1] > 127)
                    {
                        message = message.Remove(limit - 1, 1);
                        limit -= 1;
                        //goto retry;
                    }
                }
                catch { return lines; }
                lines.Add(message.Substring(0, limit));

            Next: message = message.Substring(lines[lines.Count - 1].Length);
                if (lines.Count == 1) limit = 60;

                int index = lines[lines.Count - 1].LastIndexOf('&');
                if (index != -1)
                {
                    if (index < lines[lines.Count - 1].Length - 1)
                    {
                        char next = lines[lines.Count - 1][index + 1];
                        if ("0123456789abcdef".IndexOf(next) != -1) { color = "&" + next; }
                        if (index == lines[lines.Count - 1].Length - 1)
                        {
                            lines[lines.Count - 1] = lines[lines.Count - 1].Substring(0, lines[lines.Count - 1].Length - 2);
                        }
                    }
                    else if (message.Length != 0)
                    {
                        char next = message[0];
                        if ("0123456789abcdef".IndexOf(next) != -1)
                        {
                            color = "&" + next;
                        }
                        lines[lines.Count - 1] = lines[lines.Count - 1].Substring(0, lines[lines.Count - 1].Length - 1);
                        message = message.Substring(1);
                    }
                }
            } return lines;
        }
        public static bool ValidName(string name)
        {
            string allowedchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890._";
            foreach (char ch in name) { if (allowedchars.IndexOf(ch) == -1) { return false; } } return true;
        }
        public static byte[] GZip(byte[] bytes)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            GZipStream gs = new GZipStream(ms, CompressionMode.Compress, true);
            gs.Write(bytes, 0, bytes.Length);
            gs.Close();
            gs.Dispose();
            ms.Position = 0;
            bytes = new byte[ms.Length];
            ms.Read(bytes, 0, (int)ms.Length);
            ms.Close();
            ms.Dispose();
            return bytes;
        }
        #endregion
        #region == Host <> Network ==
        public static byte[] HTNO(ushort x)
        {
            byte[] y = BitConverter.GetBytes(x); Array.Reverse(y); return y;
        }
        public static ushort NTHO(byte[] x, int offset)
        {
            byte[] y = new byte[2];
            Buffer.BlockCopy(x, offset, y, 0, 2); Array.Reverse(y);
            return BitConverter.ToUInt16(y, 0);
        }
        public static short NTHOshort(byte[] x, int offset)
        {
            byte[] y = new byte[2];
            Buffer.BlockCopy(x, offset, y, 0, 2); Array.Reverse(y);
            return BitConverter.ToInt16(y, 0);
        }
        public static int NTHOint(byte[] x, int offset)
        {
            byte[] y = new byte[4];
            Buffer.BlockCopy(x, offset, y, 0, 4); Array.Reverse(y);
            return BitConverter.ToInt32(y, 0);
        }
        public static byte[] HTNO(short x)
        {
            byte[] y = BitConverter.GetBytes(x); Array.Reverse(y); return y;
        }
        public static byte[] HTNO(int x)
        {
            byte[] y = BitConverter.GetBytes(x); Array.Reverse(y); return y;
        }
        #endregion

        bool CheckBlockSpam()
        {
            if (spamBlockLog.Count >= spamBlockCount)
            {
                DateTime oldestTime = spamBlockLog.Dequeue();
                double spamTimer = DateTime.Now.Subtract(oldestTime).TotalSeconds;
                if (spamTimer < spamBlockTimer && !ignoreGrief)
                {
                    this.Kick("You were kicked by antigrief system. Slow down.");
                    SendMessage(c.red + name + " was kicked for suspected griefing.");
                    Server.s.Log(name + " was kicked for block spam (" + spamBlockCount + " blocks in " + spamTimer + " seconds)");
                    return true;
                }
            }
            spamBlockLog.Enqueue(DateTime.Now);
            return false;
        }

#region getters
        public ushort[] footLocation
        {
            get
            {
                return getLoc(false);
            }
        }
        public ushort[] headLocation 
        {
            get
            {
                return getLoc(true);
            }
        }

        public ushort[] getLoc(bool head)
        {
            ushort[] myPos = pos;
            myPos[0] /= 32;
            if (head) myPos[1] = (ushort)((myPos[1] + 4) / 32);
            else myPos[1] = (ushort)((myPos[1] + 4) / 32 - 1);
            myPos[2] /= 32;
            return myPos;
        }

        public void setLoc(ushort[] myPos)
        {
            myPos[0] *= 32;
            myPos[1] *= 32;
            myPos[2] *= 32;
            unchecked { SendPos((byte)-1, myPos[0], myPos[1], myPos[2], rot[0], rot[1]); }
        }

#endregion
    }
}