﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MCSong
{
    public class PlayerDB
    {
        public static List<OfflinePlayer> allOffline = new List<OfflinePlayer>();

        public static void LoadAll()
        {
            allOffline.Clear();
            foreach (FileInfo f in new DirectoryInfo("").GetFiles("*.txt", SearchOption.TopDirectoryOnly))
            {
                string p = f.Name.Replace(".txt", "");
                if (Player.Find(p) == null)
                {
                    allOffline.Add(new OfflinePlayer(p));
                }
            }
        }

        public static bool nameHasIp(string name, string ip)
        {
            foreach (OfflinePlayer p in allOffline)
            {
                if (p.ip == ip && p.name.ToLower() == name.ToLower())
                    return true;
            }
            return false;
        }

        public static bool Load(Player p)
        {
            foreach (OfflinePlayer o in allOffline)
                if (o.name.ToLower() == p.name.ToLower())
                    allOffline.Remove(o);
            string path = "db/players/" + p.name + ".txt";
            if (File.Exists(path))
            {
                foreach (string line in File.ReadAllLines(path))
                {
                    if (!String.IsNullOrEmpty(line) && !line.StartsWith("#"))
                    {
                        string key = line.Split('=')[0].Trim();
                        string val = line.Split('=')[1].Trim();
                        try
                        {
                            switch (key.ToLower())
                            {
                                case "title":
                                    p.title = val;
                                    break;
                                case "tcolor":
                                    p.titlecolor = val;
                                    break;
                                case "color":
                                    p.color = val;
                                    break;
                                case "money":
                                    p.money = Int32.Parse(val);
                                    break;
                                case "flogin":
                                    p.firstLogin = DateTime.Parse(val);
                                    break;
                                case "llogin":
                                    p.lastLogin = DateTime.Parse(val);
                                    break;
                                case "logins":
                                    p.totalLogins = Int32.Parse(val) + 1;
                                    break;
                                case "kicked":
                                    p.totalKicked = Int32.Parse(val);
                                    break;
                                case "deaths":
                                    p.overallDeath = Int32.Parse(val);
                                    break;
                                case "blocks":
                                    p.overallBlocks = Int64.Parse(val);
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            Server.s.Log("An error occured at PlayerDB(" + p.name + ") key: " + key);
                            Server.ErrorLog(e);
                        }
                        p.timeLogged = DateTime.Now;
                    }
                }
                return true;
            }
            else
            {
                p.title = "";
                p.titlecolor = "";
                p.color = p.group.color;
                p.money = 0;
                p.firstLogin = DateTime.Now;
                p.lastLogin = DateTime.Now;
                p.totalLogins = 1;
                p.totalKicked = 0;
                p.overallDeath = 0;
                p.overallBlocks = 0;
                p.timeLogged = DateTime.Now;
                Save(p);
                return false;
            }
        }

        public static void Save(Player p)
        {
            string path = "db/players/" + p.name + ".txt";
            StreamWriter sw = new StreamWriter(File.Create(path));
            sw.WriteLine("# Generated by the MCSong Flatfile System");
            sw.WriteLine("# PlayerDB for " + p.name);
            sw.WriteLine();
            sw.WriteLine("ip = " + p.ip);
            sw.WriteLine("title = " + p.title);
            sw.WriteLine("tcolor = " + p.titlecolor);
            sw.WriteLine("color = " + p.color);
            sw.WriteLine("money = " + p.money);
            sw.WriteLine("flogin = " + p.firstLogin);
            sw.WriteLine("llogin = " + p.lastLogin);
            sw.WriteLine("logins = " + p.totalLogins);
            sw.WriteLine("kicked = " + p.totalKicked);
            sw.WriteLine("deaths = " + p.overallDeath);
            sw.WriteLine("blocks = " + p.overallBlocks);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
}
