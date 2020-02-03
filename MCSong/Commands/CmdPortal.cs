using System;
using System.IO;
using System.Collections.Generic;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;
using System.Data;

namespace MCSong
{
    public class CmdPortal : Command
    {
        public override string name { get { return "portal"; } }
        public override string[] aliases { get { return new string[] { "o" }; } }
        public override CommandType type { get { return CommandType.Building; } }
        public override bool consoleUsable { get { return false; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdPortal() { }

        public override void Use(Player p, string message)
        {
            portalPos portalPos;

            portalPos.Multi = false;

            if (message.IndexOf(' ') != -1)
            {
                if (message.Split(' ')[1].ToLower() == "multi")
                {
                    portalPos.Multi = true;
                    message = message.Split(' ')[0];
                }
                else
                {
                    Player.SendMessage(p, "Invalid parameters");
                    return;
                }
            }

            if (message.ToLower() == "blue" || message == "") { portalPos.type = Block.blue_portal; }
            else if (message.ToLower() == "orange") { portalPos.type = Block.orange_portal; }
            else if (message.ToLower() == "air") { portalPos.type = Block.air_portal; }
            else if (message.ToLower() == "water") { portalPos.type = Block.water_portal; }
            else if (message.ToLower() == "lava") { portalPos.type = Block.lava_portal; }
            else if (message.ToLower() == "show") { showPortals(p); return; }
            else { Help(p); return; }

            p.ClearBlockchange();

            portPos port;

            port.x = 0; port.y = 0; port.z = 0; port.portMapName = "";
            portalPos.port = new List<portPos>();

            p.blockchangeObject = portalPos;
            Player.SendMessage(p, "Place a the &aEntry block" + Server.DefaultColor + " for the portal"); p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(EntryChange);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/portal [orange/blue/air/water/lava] [multi] - Activates Portal mode.");
            Player.SendMessage(p, "/portal [type] multi - Place Entry blocks until exit is wanted.");
            Player.SendMessage(p, "/portal show - Shows portals, green = in, red = out.");
        }

        public void EntryChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            portalPos bp = (portalPos)p.blockchangeObject;

            if (bp.Multi && type == Block.red && bp.port.Count > 0) { ExitChange(p, x, y, z, type); return; }

            byte b = p.level.GetTile(x, y, z);
            p.level.Blockchange(p, x, y, z, bp.type);
            p.SendBlockchange(x, y, z, Block.green);
            portPos Port;

            Port.portMapName = p.level.name;
            Port.x = x; Port.y = y; Port.z = z;

            bp.port.Add(Port);

            p.blockchangeObject = bp;

            if (!bp.Multi)
            {
                p.Blockchange += new Player.BlockchangeEventHandler(ExitChange);
                Player.SendMessage(p, "&aEntry block placed");
            }
            else
            {
                p.Blockchange += new Player.BlockchangeEventHandler(EntryChange);
                Player.SendMessage(p, "&aEntry block placed. &cRed block for exit");
            }
        }
        public void ExitChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            portalPos bp = (portalPos)p.blockchangeObject;

            foreach (portPos pos in bp.port)
            {
                SQLiteHelper.SQLResult portalQuery = SQLiteHelper.ExecuteQuery(
                    $@"SELECT entryx, entryy, entryz, exitmap, exitx, exity, exitz " +
                    $@"FROM Portals{pos.portMapName} " +
                    $@"WHERE entryx = {pos.x} AND entryy {pos.y} AND entryz = {pos.z};");

                if (portalQuery.rowsAffected <= 0)
                {
                    SQLiteHelper.ExecuteQuery(
                        $@"INSERT INTO Portals{pos.portMapName} (entryx, entryy, entryz, exitmap, exitx, exity, exitz) " +
                        $@"VALUES ({pos.x}, {pos.y}, {pos.z}, '{p.level.name}', {x}, {y}, {z});");
                }
                else
                {
                    SQLiteHelper.ExecuteQuery(
                        $@"UPDATE Portals{pos.portMapName} " +
                        $@"SET exitmap = '{p.level.name}', exitx = {x}, exity = {y}, exitz = {z} " +
                        $@"WHERE entryx = {pos.x} AND entryy = {pos.y} AND entryz = {pos.z};");
                }

                if (pos.portMapName == p.level.name) p.SendBlockchange(pos.x, pos.y, pos.z, bp.type);
            }

            Player.SendMessage(p, "&3Exit" + Server.DefaultColor + " block placed");

            if (p.staticCommands) { bp.port.Clear(); p.blockchangeObject = bp; p.Blockchange += new Player.BlockchangeEventHandler(EntryChange); }
        }

        public struct portalPos { public List<portPos> port; public byte type; public bool Multi; }
        public struct portPos { public ushort x, y, z; public string portMapName; }

        public void showPortals(Player p)
        {
            p.showPortals = !p.showPortals;

            SQLiteHelper.SQLResult portalQuery = SQLiteHelper.ExecuteQuery($@"SELECT entryx, entryy, entryz, exitmap, exitx, exity, exitz FROM Portals{p.level.name};");

            foreach (var row in portalQuery)
            {
                if (row["exitmap"].Equals(p.level.name))
                    p.SendBlockchange(ushort.Parse(row["exitx"]), ushort.Parse(row["exity"]), ushort.Parse(row["exitz"]), p.showPortals ? Block.orange_portal : Block.air);
                p.SendBlockchange(ushort.Parse(row["entryx"]), ushort.Parse(row["entryy"]), ushort.Parse(row["entryz"]), p.showPortals ? Block.blue_portal : p.level.GetTile(ushort.Parse(row["entryx"]), ushort.Parse(row["entryy"]), ushort.Parse(row["entryz"])));
            }
            Player.SendMessage(p, $"Now {(p.showPortals ? $"showing &a{portalQuery.rowsAffected}{Server.DefaultColor}" : "hiding")} portals.");
        }
    }
}