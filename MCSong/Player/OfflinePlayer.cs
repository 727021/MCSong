using System;
using System.IO;

namespace MCSong
{
    public class OfflinePlayer
    {
        public static OfflinePlayer Find(string name)
        {
            foreach (OfflinePlayer p in PlayerDB.allOffline)
            {
                if (p.name.ToLower() == name.ToLower())
                    return p;
            }
            return null;
        }

        public string ip, name, title, tcolor, color;
        public int money, logins, kicks, deaths;
        public long blocks;
        public DateTime flogin, llogin;
        public bool seen;

        public OfflinePlayer(string name)
        {
            this.name = name;

            string path = "db/players/" + name.ToLower().Trim() + ".txt";
            if (File.Exists(path))
            {
                this.seen = true;
                foreach (string line in File.ReadAllLines(path))
                {
                    if (!String.IsNullOrEmpty(line) && !line.StartsWith("#") && line.IndexOf(':') != -1)
                    {
                        string key = line.Split('=')[0].Trim();
                        string val = line.Split('=')[1].Trim();
                        try
                        {
                            switch (key.ToLower())
                            {
                                case "ip":
                                    this.ip = val;
                                    break;
                                case "title":
                                    this.title = val;
                                    break;
                                case "tcolor":
                                    this.tcolor = val;
                                    break;
                                case "color":
                                    this.color = val;
                                    break;
                                case "money":
                                    this.money = Int32.Parse(val);
                                    break;
                                case "logins":
                                    this.logins = Int32.Parse(val);
                                    break;
                                case "kicked":
                                    this.kicks = Int32.Parse(val);
                                    break;
                                case "deaths":
                                    this.deaths = Int32.Parse(val);
                                    break;
                                case "flogin":
                                    this.flogin = DateTime.Parse(val);
                                    break;
                                case "llogin":
                                    this.llogin = DateTime.Parse(val);
                                    break;
                                case "blocks":
                                    this.blocks = Int64.Parse(val);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            this.ip = "";
                            this.title = "";
                            this.tcolor = "";
                            this.color = Group.findPlayerGroup(name).color;
                            this.money = 0;
                            this.logins = 0;
                            this.kicks = 0;
                            this.deaths = 0;
                            this.blocks = 0;
                            this.flogin = new DateTime();
                            this.llogin = new DateTime();
                        }
                    }
                }
            }
            else
            {
                this.seen = false;
            }
        }
    }
}
