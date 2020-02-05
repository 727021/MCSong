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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using MCSong;
using System.Diagnostics;

namespace MCSong.Gui
{
    public partial class Window : Form
    {
        Regex regex = new Regex(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\." +
                                "([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");
        // for cross thread use
        //delegate void StringCallback(string s);
        //delegate void PlayerListCallback(List<Player> players);
        //delegate void ReportCallback(Report r);
        //delegate void VoidDelegate();

        public static event EventHandler Minimize;
        public NotifyIcon notifyIcon1 = new NotifyIcon();
        //  public static bool Minimized = false;
        
        internal static Server s;

        bool shuttingDown = false;

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle |= CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public Window() {
            InitializeComponent();
        }

        private void Window_Minimize(object sender, EventArgs e)
        {
      /*     if (!Minimized)
            {
                Minimized = true;
                ntf.Text = "MCZall";
                ntf.Icon = this.Icon;
                ntf.Click += delegate
                {
                    try
                    {
                        Minimized = false;
                        this.ShowInTaskbar = true;
                        this.Show();
                        WindowState = FormWindowState.Normal;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                };
                ntf.Visible = true;
                this.ShowInTaskbar = false;
            } */
        }

        public static Window thisWindow;

        private void Window_Load(object sender, EventArgs e)
        {
            thisWindow = this;
            MaximizeBox = false;
            this.Text = "<server name here>";
            this.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("MCSong.Lawl.ico"));

            CheckForIllegalCrossThreadCalls = false;

            lblCopyright.Text += DateTime.Today.Year.ToString();
            txtGlobalOut.Enabled = txtGlobalIn.Enabled = btnGlobalChat.Enabled = Server.gc;
            lblUrl.Text = "";

            this.Show();
            this.BringToFront();
            WindowState = FormWindowState.Normal;

            s = new Server();
            s.OnLog += WriteLine;
            s.OnOp += WriteLineOp;
            s.OnAdmin += WriteLineAdmin;
            s.OnGlobal += WriteLineGlobal;
            s.OnCommand += newCommand;
            s.OnError += newError;
            s.OnSystem += newSystem;

            foreach (TabPage tP in tabControl1.TabPages)
                tabControl1.SelectTab(tP);
            tabControl1.SelectTab(tabControl1.TabPages[0]);

            s.HeartBeatFail += HeartBeatFail;
            s.OnURLChange += UpdateUrl;
            s.OnPlayerListChange += UpdateClientList;
            s.OnSettingsUpdate += SettingsUpdate;
            s.Start();
            notifyIcon1.Text = ("MCSong Server: " + Server.name);

            this.notifyIcon1.ContextMenuStrip = this.iconContext;
            this.notifyIcon1.Icon = this.Icon;
            this.notifyIcon1.Visible = true;
            notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);

            /*System.Timers.Timer MapTimer = new System.Timers.Timer(10000);
            MapTimer.Elapsed += delegate {
                UpdateMapList("'");
            }; MapTimer.Start();*/

            // Update the list on load, unload, and physchange
            Level.OnLevelLoadEvent += UpdateMapList;
            Level.OnLevelUnloadEvent += UpdateMapList;
            Level.OnLevelPhysChangeEvent += UpdateMapsTab;

            foreach (Group g in Group.GroupList)
            {
                cmbPerBuild.Items.Add(g.name);
                cmbPerVisit.Items.Add(g.name);
            }

            //if (File.Exists(Logger.ErrorLogPath))
            //txtErrors.Lines = File.ReadAllLines(Logger.ErrorLogPath);

            new Thread((ThreadStart)(() =>
            {
                thisWindow.Invoke(new Action(delegate
                {
                    txtChangelog.Text = "\r\nLoading...";
                }));
                try
                {
                    if (File.Exists("extra/Changelog.txt"))
                    {
                        File.Delete("extra/Changelog.txt");
                    }
                    WebClient Web = new WebClient();
                    Web.DownloadFile("https://raw.githubusercontent.com/727021/MCSong/master/Text/Changelog.txt", "extra/Changelog.txt");
                    Web.Dispose();
                }
                catch { }
                string latest = "";
                try { latest = new WebClient().DownloadString("http://updates.mcsong.x10.mx/curversion.txt"); }
                catch { }

                thisWindow.Invoke(new Action(delegate
                {
                    try
                    {
                        txtChangelog.Clear();
                        foreach (string line in File.ReadAllLines(("extra/Changelog.txt")))
                        {
                            txtChangelog.AppendText("\r\n  " + line);
                        }
                    }
                    catch
                    {
                        txtChangelog.Text = "\r\nChangelog not found!\r\nDownload it manually from https://raw.githubusercontent.com/727021/MCSong/master/Text/Changelog.txt and save it as \'extra/Changelog.txt\'";
                    }
                    txtCurrentVersion.Text = Server.Version;
                    txtLatestVersion.Text = latest;
                    txtLatestVersion.Enabled = (latest != "");
                    if (txtLatestVersion.Enabled)
                        txtCurrentVersion.ForeColor = (txtCurrentVersion.Text == txtLatestVersion.Text) ? Color.Green : Color.Red;
                }));
                
            })).Start();


            Server.devs.ForEach(delegate(string dev) { txtDevList.Text += (Environment.NewLine + dev); });
        }

        void SettingsUpdate()
        {
            if (shuttingDown) return;
            Invoke(new Action(delegate
            {
                this.Text = Server.name + " - MCSong Version: " + Server.Version;
            }));
        }

        void HeartBeatFail() {
            WriteLine("Recent Heartbeat Failed");
        }

        void newError(string message)
        {
            try
            {
                Invoke(new Action(delegate
                {
                    txtErrors.AppendText(Environment.NewLine + message);
                    txtErrors.Select(txtErrors.MaxLength, 0);
                }));
            }
            catch { }
        }
        void newSystem(string message)
        {
            try
            {
                Invoke(new Action(delegate
                {
                    txtSystem.AppendText(Environment.NewLine + message);
                    txtSystem.Select(txtSystem.Text.Length, 0);
                }));
            }
            catch { }
        }
        
        /// <summary>
        /// Does the same as Console.Write() only in the form
        /// </summary>
        /// <param name="s">The string to write</param>
        public void Write(string s) {
            if (shuttingDown) return;
            Invoke(new Action(delegate
            {
                txtLog.AppendText(s);
                txtLog.Select(txtLog.Text.Length, 0);
            }));
        }
        /// <summary>
        /// Does the same as Console.WriteLine() only in the form
        /// </summary>
        /// <param name="s">The line to write</param>
        public void WriteLine(string s)
        {
            if (shuttingDown) return;
            Invoke(new Action(delegate
            {
                txtLog.AppendText(s + "\r\n");
                txtLog.Select(txtLog.Text.Length, 0);
            }));
        }
        public void WriteLineOp(string s)
        {
            if (shuttingDown) return;
            Invoke(new Action(delegate
            {
                txtOpOut.AppendText(s + "\r\n");
                txtOpOut.Select(txtOpOut.Text.Length, 0);
            }));
        }
        public void WriteLineAdmin(string s)
        {
            if (shuttingDown) return;
            Invoke(new Action(delegate
            {
                txtAdminOut.AppendText(s + "\r\n");
                txtAdminOut.Select(txtAdminOut.Text.Length, 0);
            }));
        }
        public void WriteLineGlobal(string s)
        {
            if (shuttingDown) return;
            Invoke(new Action(delegate
            {
                txtGlobalOut.AppendText(s + "\r\n");
                txtGlobalOut.Select(txtGlobalOut.Text.Length, 0);
            }));
        }
        /// <summary>
        /// Updates the list of client names in the window
        /// </summary>
        /// <param name="players">The list of players to add</param>
        public void UpdateClientList() {
            Invoke(new Action(delegate
            {
                liClients.Items.Clear();
                Player.players.ForEach(delegate (Player p) { liClients.Items.Add(p.name); });
                txtPlayerCount.Clear();
                txtPlayerCount.Text = Player.players.Count.ToString() + "/";
                int guests = 0;
                Player.players.ForEach(delegate (Player p) { if (p.group.Permission == LevelPermission.Guest) { guests++; } });
                txtPlayerCount.AppendText(guests.ToString());
            }));
        }

        public void UpdateMapList(string mapname = "") {            
            Invoke(new Action(delegate
            {
                liMaps.Items.Clear();
                foreach (Level level in Server.levels)
                {
                    liMaps.Items.Add(level.name + " - " + level.physics);
                }
                liUnloaded.Items.Clear();
                if (Directory.Exists("levels"))
                    foreach (string f in Directory.GetFiles("levels/", "*.lvl", SearchOption.TopDirectoryOnly))
                    {
                        FileInfo fi = new FileInfo(f);
                        string n = fi.Name.Replace(fi.Extension, "");
                        if (Level.Find(n) == null)
                            liUnloaded.Items.Add(n);
                    }
                UpdateMapsTab();
            }));
        }

        /// <summary>
        /// Places the server's URL at the top of the window
        /// </summary>
        /// <param name="s">The URL to display</param>
        public void UpdateUrl(string s)
        {
            Invoke(new Action(delegate
            {
                txtUrl.Text = s;
            }));
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e) {
            if (notifyIcon1 != null) {
                notifyIcon1.Visible = false;
            }
            MCSong_.Gui.Program.ExitProgram(false);
        }

        private ChatBuffer cbInput = new ChatBuffer();

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtInput.Text == null || txtInput.Text.Trim() == "") { return; }
                cbInput.addEntry(txtInput.Text);
                string text = txtInput.Text.Trim();
                string newtext = text;
                if (txtInput.Text[0] == '#')
                {
                    newtext = text.Remove(0, 1).Trim();
                    cbOpChat.addEntry(newtext);
                    Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                    Server.s.LogOp("Console: " + newtext);
                    //IRCBot.Say("Console: " + newtext, true);
                 //   WriteLine("(OPs):<CONSOLE> " + txtInput.Text);
                    txtInput.Clear();
                }
                else if (txtInput.Text[0] == ';')
                {
                    newtext = text.Remove(0, 1).Trim();
                    cbAdminChat.addEntry(newtext);
                    Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                    Server.s.LogAdmin("Console: " + newtext);
                    //IRCBot.Say("Console: " + newtext, true);
                    txtInput.Clear();
                }
                else if (txtInput.Text[0] == '\\')
                {
                    if (!Server.gc) return;
                    newtext = text.Remove(0, 1).Trim();
                    cbGlobalChat.addEntry(newtext);
                    Player.GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] Console: &f" + newtext);
                    Server.s.LogGC("[" + Server.gcNick + "] Console: " + newtext);
                    //GlobalBot.Say("Console: " + newtext);
                    txtInput.Clear();
                }
                else
                {
                    Player.GlobalMessage("Console [&a" + Server.ZallState + Server.DefaultColor + "]: &f" + txtInput.Text);
                    //IRCBot.Say("Console [" + Server.ZallState + "]: " + txtInput.Text);
                    WriteLine("<CONSOLE> " + txtInput.Text);
                    txtInput.Clear();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                string up = cbInput.up();
                txtInput.Text = (up == "") ? txtInput.Text : up;
                txtInput.Select(txtInput.Text.Length, 0);
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtInput.Text = cbInput.down();
                txtInput.Select(txtInput.Text.Length, 0);
            }
        }

        private ChatBuffer cbCommands = new ChatBuffer();

        private void txtCommands_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                string sentCmd = "", sentMsg = "";

                if (txtCommands.Text == null || txtCommands.Text.Trim() == "")
                {
                    newCommand("CONSOLE: Whitespace commands are not allowed.");
                    txtCommands.Clear();
                    return;
                }

                cbCommands.addEntry(txtCommands.Text);

                if (txtCommands.Text[0] == '/')
                    if (txtCommands.Text.Length > 1)
                        txtCommands.Text = txtCommands.Text.Substring(1);

                if (txtCommands.Text.IndexOf(' ') != -1) {
                    sentCmd = txtCommands.Text.Split(' ')[0];
                    sentMsg = txtCommands.Text.Substring(txtCommands.Text.IndexOf(' ') + 1);
                } else if (txtCommands.Text != "") {
                    sentCmd = txtCommands.Text;
                } else {
                    return;
                }

                if (Command.all.Find(sentCmd) == null)
                {
                    newCommand("CONSOLE: Command not found.");
                    txtCommands.Clear();
                    return;
                }

                if (Command.all.Find(sentCmd).consoleUsable)
                {
                    try
                    {
                        Command.all.Find(sentCmd).Use(null, sentMsg);
                        newCommand("CONSOLE: USED /" + sentCmd + " " + sentMsg);
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                        newCommand("CONSOLE: Failed command.");
                    }
                }
                else
                {
                    newCommand("CONSOLE: Cannot use /" + sentCmd);
                }

                txtCommands.Clear();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                string up = cbCommands.up();
                txtCommands.Text = (up == "") ? txtCommands.Text : up;
                txtCommands.Select(txtCommands.Text.Length, 0);
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtCommands.Text = cbCommands.down();
                txtCommands.Select(txtCommands.Text.Length, 0);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e) { 
            if (notifyIcon1 != null) {
                notifyIcon1.Visible = false;
            }
            MCSong_.Gui.Program.ExitProgram(false); 
        }

        public void newCommand(string p) {
            Invoke(new Action(delegate
            {
                txtCommandsUsed.AppendText(p + "\r\n");
                txtCommandsUsed.Select(txtCommandsUsed.Text.Length, 0);
            }));
        }

        void ChangeCheck(string newCheck)
        {
            Server.ZallState = newCheck;
        }

        private void txtHost_TextChanged(object sender, EventArgs e)
        {
            if (txtHost.Text != "")
            {
                ChangeCheck(txtHost.Text);
            }
        }

        private void btnProperties_Click_1(object sender, EventArgs e)
        {
            if (!prevLoaded)
            {
                PropertyForm = new PropertyWindow();
                prevLoaded = true;
            }
            PropertyForm.Show();
            PropertyForm.BringToFront();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e) {
            if (!MCSong_.Gui.Program.CurrentUpdate)
                MCSong_.Gui.Program.UpdateCheck();
            else {
                Thread messageThread = new Thread(new ThreadStart(delegate {
                    MessageBox.Show("Already checking for updates.");
                })); messageThread.Start();
            }
        }

        public static bool prevLoaded = false;
        public static bool updLoaded = false;
        Form PropertyForm;
        Form UpdateForm;

        private void gBChat_Enter(object sender, EventArgs e)
        {

        }

        private void Window_Resize(object sender, EventArgs e) {
            this.Hide();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e) {
            this.Show();
            this.BringToFront();
            WindowState = FormWindowState.Normal;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!updLoaded)
            {
                UpdateForm = new UpdateWindow();
                updLoaded = true;
            }
            UpdateForm.Show();
            UpdateForm.BringToFront();
        }

        private void tmrRestart_Tick(object sender, EventArgs e)
        {
            if (Server.autorestart)
            {
                if (DateTime.Now.TimeOfDay.CompareTo(Server.restarttime.TimeOfDay) > 0 && (DateTime.Now.TimeOfDay.CompareTo(Server.restarttime.AddSeconds(1).TimeOfDay)) < 0) {
                    Player.GlobalMessage("The time is now " + DateTime.Now.TimeOfDay);
                    Player.GlobalMessage("The server will now begin auto restart procedures.");
                    Server.s.Log("The time is now " + DateTime.Now.TimeOfDay);
                    Server.s.Log("The server will now begin auto restart procedures.");

                    if (notifyIcon1 != null) {
                        notifyIcon1.Icon = null;
                        notifyIcon1.Visible = false;
                    }
                    MCSong_.Gui.Program.ExitProgram(true);
                }
            }
        }

        private void openConsole_Click(object sender, EventArgs e)
        {
            // Yes, it's a hacky fix.  Don't ask :v
            this.Show();
            this.BringToFront();
            WindowState = FormWindowState.Normal;
            this.Show();
            this.BringToFront();
            WindowState = FormWindowState.Normal;
        }

        private void shutdownServer_Click(object sender, EventArgs e)
        {
            if (notifyIcon1 != null)
            {
                notifyIcon1.Visible = false;
            }
            MCSong_.Gui.Program.ExitProgram(false); 
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            foreach (TabPage tP in tabControl1.TabPages)
            {
                foreach (Control ctrl in tP.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        TextBox txtBox = (TextBox)ctrl;
                        txtBox.Update();
                    }
                }
            }
            txtAdminOut.Select(txtAdminOut.Text.Length, 0);
            txtOpOut.Select(txtOpOut.Text.Length, 0);
            txtGlobalOut.Select(txtGlobalOut.Text.Length, 0);
            txtLog.Select(txtLog.Text.Length, 0);
            UpdateMapList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Thread((ThreadStart)(() =>
            {
                thisWindow.Invoke(new Action(delegate
                {
                    txtChangelog.Text = "\r\nLoading...";
                }));
                try
                {
                    if (File.Exists("extra/Changelog.txt"))
                    {
                        File.Delete("extra/Changelog.txt");
                    }
                    WebClient Web = new WebClient();
                    Web.DownloadFile("https://raw.githubusercontent.com/727021/MCSong/master/Text/Changelog.txt", "extra/Changelog.txt");
                    Web.Dispose();
                }
                catch { }
                string latest = "";
                try { latest = new WebClient().DownloadString("http://updates.mcsong.x10.mx/curversion.txt"); }
                catch { }

                thisWindow.Invoke(new Action(delegate
                {
                    try
                    {
                        txtChangelog.Clear();
                        foreach (string line in File.ReadAllLines(("extra/Changelog.txt")))
                        {
                            txtChangelog.AppendText("\r\n  " + line);
                        }
                    }
                    catch
                    {
                        txtChangelog.Text = "\r\nChangelog not found!\r\nDownload it manually from https://raw.githubusercontent.com/727021/MCSong/master/Text/Changelog.txt and save it as \'extra/Changelog.txt\'";
                    }
                    txtCurrentVersion.Text = Server.Version;
                    txtLatestVersion.Text = latest;
                    txtLatestVersion.Enabled = (latest != "");
                    if (txtLatestVersion.Enabled)
                        txtCurrentVersion.ForeColor = (txtCurrentVersion.Text == txtLatestVersion.Text) ? Color.Green : Color.Red;
                }));

            })).Start();
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            if (txtInput.Text == null || txtInput.Text.Trim() == "") { return; }
            cbInput.addEntry(txtInput.Text);
            string text = txtInput.Text.Trim();
            string newtext = text;
            if (txtInput.Text[0] == '#')
            {
                newtext = text.Remove(0, 1).Trim();
                cbOpChat.addEntry(newtext);
                Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                Server.s.LogOp("Console: " + newtext);
                //IRCBot.Say("Console: " + newtext, true);
                //   WriteLine("(OPs):<CONSOLE> " + txtInput.Text);
                txtInput.Clear();
            }
            else if (txtInput.Text[0] == ';')
            {
                newtext = text.Remove(0, 1).Trim();
                cbAdminChat.addEntry(newtext);
                Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                Server.s.LogAdmin("Console: " + newtext);
                //IRCBot.Say("Console: " + newtext, true);
                txtInput.Clear();
            }
            else if (txtInput.Text[0] == '\\')
            {
                if (!Server.gc) return;
                newtext = text.Remove(0, 1).Trim();
                cbGlobalChat.addEntry(newtext);
                Player.GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] Console: &f" + newtext);
                Server.s.LogGC("[" + Server.gcNick + "] Console: " + newtext);
                //GlobalBot.Say("Console: " + newtext);
                txtInput.Clear();
            }
            else
            {
                Player.GlobalMessage("Console [&a" + Server.ZallState + Server.DefaultColor + "]: &f" + txtInput.Text);
                //IRCBot.Say("Console [" + Server.ZallState + "]: " + txtInput.Text);
                WriteLine("<CONSOLE> " + txtInput.Text);
                txtInput.Clear();
            }
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            string sentCmd = "", sentMsg = "";

            if (txtCommands.Text == null || txtCommands.Text.Trim() == "")
            {
                newCommand("CONSOLE: Whitespace commands are not allowed.");
                txtCommands.Clear();
                return;
            }
            cbCommands.addEntry(txtCommands.Text);
            if (txtCommands.Text[0] == '/')
                if (txtCommands.Text.Length > 1)
                    txtCommands.Text = txtCommands.Text.Substring(1);

            if (txtCommands.Text.IndexOf(' ') != -1)
            {
                sentCmd = txtCommands.Text.Split(' ')[0];
                sentMsg = txtCommands.Text.Substring(txtCommands.Text.IndexOf(' ') + 1);
            }
            else if (txtCommands.Text != "")
            {
                sentCmd = txtCommands.Text;
            }
            else
            {
                return;
            }

            try
            {
                Command.all.Find(sentCmd).Use(null, sentMsg);
                newCommand("CONSOLE: USED /" + sentCmd + " " + sentMsg);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                newCommand("CONSOLE: Failed command.");
            }

            txtCommands.Clear();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Player.GlobalMessage("A restart has been initiated by the console.");
            Player.GlobalMessage("The server will now begin auto restart procedures.");
            Server.s.Log("A restart has been initiated by the console.");
            Server.s.Log("The server will now begin auto restart procedures.");

            if (notifyIcon1 != null)
            {
                notifyIcon1.Icon = null;
                notifyIcon1.Visible = false;
            }
            MCSong_.Gui.Program.ExitProgram(true);
        }

        private void chkMaintenance_Click(object sender, EventArgs e)
        {
            if (chkMaintenance.Checked)
            {
                Server.maintenanceMode = true;
                chkMaintenance.ForeColor = Color.Red;
                chkMaintenance.Font = new Font(chkMaintenance.Font, FontStyle.Bold);
                thisWindow.Text += " [MAINTENANCE]";
                Player.GlobalMessage(c.purple + "MAINTENANCE MODE " + Server.DefaultColor + "has been turned " + c.green + "ON");
                Server.s.Log("MAINTENANCE MODE has been turned ON");
                if (Server.maintKick)
                {
                    Player.GlobalMessage("Kicking all players ranked below " + Level.PermissionToName(Server.maintPerm));
                    Server.s.Log("Kicking all players ranked below" + Level.PermissionToName(Server.maintPerm));
                    foreach (Player p in Player.players)
                    {
                        if (p.group.Permission < Server.maintPerm)
                        {
                            p.Kick("Kicked for server maintenance!");
                        }
                    }
                }
            }
            else if (!chkMaintenance.Checked)
            {
                Server.maintenanceMode = false;
                chkMaintenance.ForeColor = Color.Black;
                chkMaintenance.Font = new Font(chkMaintenance.Font, FontStyle.Regular);
                thisWindow.Text = thisWindow.Text.Replace(" [MAINTENANCE]", "");
                Player.GlobalMessage(c.purple + "MAINTENANCE MODE " + Server.DefaultColor + "has been turned " + c.red + "OFF");
                Server.s.Log("MAINTENANCE MODE has been turned OFF");
            }
        }

        public static void updateMaintenance()
        {
            if (!thisWindow.InvokeRequired)
            {
                thisWindow.chkMaintenance.Checked = Server.maintenanceMode;
                if (Server.maintenanceMode)
                {
                    thisWindow.chkMaintenance.Font = new Font(thisWindow.chkMaintenance.Font, FontStyle.Bold);
                    thisWindow.chkMaintenance.ForeColor = Color.Red;
                    thisWindow.Text += " [MAINTENANCE]";
                }
                else
                {
                    thisWindow.chkMaintenance.Font = new Font(thisWindow.chkMaintenance.Font, FontStyle.Regular);
                    thisWindow.chkMaintenance.ForeColor = Color.Black;
                    thisWindow.Text = thisWindow.Text.Replace("[MAINTENANCE]", "");
                }
            }
            else
            {
                thisWindow.Invoke(new Action(updateMaintenance));
            }
        }

        public static void clearChatBuffer()
        {
            if (!thisWindow.InvokeRequired)
            {
                thisWindow.cbInput.clear();
                thisWindow.cbAdminChat.clear();
                thisWindow.cbOpChat.clear();
                thisWindow.cbGlobalChat.clear();
                thisWindow.cbCommands.clear();
            }
            else
            {
                thisWindow.Invoke(new Action(clearChatBuffer));
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Enabled = false;
            if (txtUrl.Text.StartsWith("http"))
            {
                try
                {
                    Process.Start(txtUrl.Text);
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            }
            else
            {
                MessageBox.Show("The URL found was invalid and could not be started.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            btnPlay.Enabled = true;
        }

        private void txtDevList_Focus(object sender, EventArgs e)
        {
            groupBox11.Focus();
        }
        
        private void liClients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        ChatBuffer cbAdminChat = new ChatBuffer();
        ChatBuffer cbOpChat = new ChatBuffer();
        ChatBuffer cbGlobalChat = new ChatBuffer();

        private void btnAdminChat_Click(object sender, EventArgs e)
        {
            if (txtAdminIn.Text == null || txtAdminIn.Text.Trim() == "") { return; }
            cbAdminChat.addEntry(txtAdminIn.Text);
            cbInput.addEntry(";" + txtAdminIn.Text);
            string text = txtAdminIn.Text.Trim();

            Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
            Server.s.LogAdmin("Console: " + text);
            //IRCBot.Say("Console: " + text, true);
            txtAdminIn.Clear();
        }

        private void btnOpChat_Click(object sender, EventArgs e)
        {
            if (txtOpIn.Text == null || txtOpIn.Text.Trim() == "") { return; }
            cbOpChat.addEntry(txtOpIn.Text);
            cbInput.addEntry("#" + txtOpIn.Text);
            string text = txtOpIn.Text.Trim();

            Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
            Server.s.LogOp("Console: " + text);
            //IRCBot.Say("Console: " + text, true);
            txtOpIn.Clear();
        }

        private void btnGlobalChat_Click(object sender, EventArgs e)
        {
            if (txtGlobalIn.Text == null || txtGlobalIn.Text.Trim() == "") return;
            cbGlobalChat.addEntry(txtGlobalIn.Text);
            cbInput.addEntry("\\" + txtGlobalIn.Text);
            string text = txtGlobalIn.Text.Trim();

            Player.GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] Console: &f" + text);
            Server.s.LogGC("[" + Server.gcNick + "] Console: " + text);
            //GlobalBot.Say("Console: " + text);
            txtGlobalIn.Clear();
        }

        private void txtAdminIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtAdminIn.Text == null || txtAdminIn.Text.Trim() == "") { return; }
                cbAdminChat.addEntry(txtAdminIn.Text);
                cbInput.addEntry(";" + txtAdminIn.Text);
                string text = txtAdminIn.Text.Trim();

                Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
                Server.s.LogAdmin("Console: " + text);
                //IRCBot.Say("Console: " + text, true);
                txtAdminIn.Clear();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                string up = cbAdminChat.up();
                txtAdminIn.Text = (up == "") ? txtAdminIn.Text : up;
                txtAdminIn.Select(txtAdminIn.Text.Length, 0);
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtAdminIn.Text = cbAdminChat.down();
                txtAdminIn.Select(txtAdminIn.Text.Length, 0);
            }
        }

        private void txtOpIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtOpIn.Text == null || txtOpIn.Text.Trim() == "") { return; }
                cbOpChat.addEntry(txtOpIn.Text);
                cbInput.addEntry("#" + txtOpIn.Text);
                string text = txtOpIn.Text.Trim();

                Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
                Server.s.LogOp("Console: " + text);
                //IRCBot.Say("Console: " + text, true);
                txtOpIn.Clear();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                string up = cbOpChat.up();
                txtOpIn.Text = (up == "") ? txtOpIn.Text : up;
                txtOpIn.Select(txtOpIn.Text.Length, 0);
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtOpIn.Text = cbOpChat.down();
                txtOpIn.Select(txtOpIn.Text.Length, 0);
            }
        }

        private void txtGlobalIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtGlobalIn.Text == null || txtGlobalIn.Text.Trim() == "") return;
                cbGlobalChat.addEntry(txtGlobalIn.Text);
                cbInput.addEntry("\\" + txtGlobalIn.Text);
                string text = txtGlobalIn.Text.Trim();

                Player.GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] Console: &f" + text);
                Server.s.LogGC("[" + Server.gcNick + "] Console: " + text);
                //GlobalBot.Say("Console: " + text);
                txtGlobalIn.Clear();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                string up = cbGlobalChat.up();
                txtGlobalIn.Text = (up == "") ? txtGlobalIn.Text : up;
                txtGlobalIn.Select(txtGlobalIn.Text.Length, 0);
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtGlobalIn.Text = cbGlobalChat.down();
                txtGlobalIn.Select(txtGlobalIn.Text.Length, 0);
            }
        }

        public void CheckGlobal()
        {
            txtGlobalOut.Enabled = txtGlobalIn.Enabled = btnGlobalChat.Enabled = Server.gc;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpdateMapsTab()
        {
            if (!this.InvokeRequired)
            {
                if (!String.IsNullOrWhiteSpace(liMaps.Text))
                {
                    Level l = Level.Find(liMaps.Text.Remove(liMaps.Text.Length - 4));

                    btnLevelHacks.Enabled = true;
                    btnEnvColors.Enabled = true;
                    txtLevelMotd.ReadOnly = false;
                    
                    btnLoadLevel.Enabled = false;
                    cmbLevelPhys.Enabled = cmbPerVisit.Enabled = cmbPerBuild.Enabled = true;

                    chkGrassGrowing.Enabled = chkKillerBlocks.Enabled = chkSurvivalDeath.Enabled = true;
                    chkFiniteMode.Enabled = chkEdgeWater.Enabled = chkAnimalAi.Enabled = true;
                    chkLevelChat.Enabled = chkAutoUnload.Enabled = chkAutoLoad.Enabled = true;

                    btnUnloadLevel.Enabled = btnRenameLevel.Enabled = btnDeleteLevel.Enabled = chkAutoLoad.Enabled = chkAutoUnload.Enabled = (l != Server.mainLevel);
                    
                    txtLevelMotd.Text = l.motd;
                    cmbLevelPhys.SelectedIndex = l.physics;
                    txtLevelX.Text = l.width.ToString();
                    txtLevelY.Text = l.depth.ToString();
                    txtLevelZ.Text = l.height.ToString();
                    try {cmbPerVisit.SelectedIndex = cmbPerVisit.Items.IndexOf(Level.PermissionToName(l.permissionvisit)); }
                    catch { cmbPerVisit.SelectedIndex = 0; }
                    try { cmbPerBuild.SelectedIndex = cmbPerBuild.Items.IndexOf(Level.PermissionToName(l.permissionbuild)); }
                    catch { cmbPerBuild.SelectedIndex = 0; }

                    chkGrassGrowing.Checked = l.GrassGrow;
                    chkKillerBlocks.Checked = l.Killer;
                    chkSurvivalDeath.Checked = l.Death;
                    chkFiniteMode.Checked = l.finite;
                    chkEdgeWater.Checked = l.edgeWater;
                    chkAnimalAi.Checked = l.ai;
                    chkLevelChat.Checked = !l.worldChat;
                    chkAutoUnload.Checked = (l == Server.mainLevel) ? false : l.unload;
                    chkAutoLoad.Checked = (l == Server.mainLevel) ? ((File.Exists("extra/autoload.txt") ? (new List<string>(File.ReadAllLines("extra/autoload.txt")).Contains(l.name) || new List<string>(File.ReadAllLines("extra/autoload.txt")).Contains(l.name.ToLower())) : false)) : false;

                    btnUpdateLevel.Enabled = false;

                    DrawLevel(l);
                    btnSaveImage.Enabled = true;
                    return;
                }
                else if (!String.IsNullOrWhiteSpace(liUnloaded.Text))
                {
                    btnUpdateLevel.Enabled = btnRenameLevel.Enabled = btnDeleteLevel.Enabled = btnLevelHacks.Enabled = btnEnvColors.Enabled = false;
                    txtLevelMotd.ReadOnly = true;
                    btnUnloadLevel.Enabled = false;
                    btnLoadLevel.Enabled = true;
                    
                    goto clearMaps;
                }
                else
                {
                    cmbLevelPhys.Enabled = cmbPerVisit.Enabled = cmbPerBuild.Enabled = false;
                    btnLoadLevel.Enabled = btnUnloadLevel.Enabled = false;
                    txtLevelMotd.ReadOnly = true;
                    btnUpdateLevel.Enabled = btnRenameLevel.Enabled = btnDeleteLevel.Enabled = btnLevelHacks.Enabled = btnEnvColors.Enabled = false;
                }
                
            clearMaps:
                txtLevelMotd.Clear();
                cmbLevelPhys.SelectedIndex = 0;
                cmbPerBuild.SelectedIndex = cmbPerVisit.SelectedIndex = 0;
                txtLevelX.Clear();
                txtLevelY.Clear();
                txtLevelZ.Clear();
                btnSaveImage.Enabled = false;
                pbMapViewer.Image = new Bitmap(pbMapViewer.Width, pbMapViewer.Height);
                cmbLevelPhys.Enabled = cmbPerVisit.Enabled = cmbPerBuild.Enabled = false;
                chkGrassGrowing.Checked = chkKillerBlocks.Checked = chkSurvivalDeath.Checked = false;
                chkFiniteMode.Checked = chkEdgeWater.Checked = chkAnimalAi.Checked = false;
                chkLevelChat.Checked = chkAutoUnload.Checked = chkAutoLoad.Checked = false;
                chkGrassGrowing.Enabled = chkKillerBlocks.Enabled = chkSurvivalDeath.Enabled = false;
                chkFiniteMode.Enabled = chkEdgeWater.Enabled = chkAnimalAi.Enabled = false;
                chkLevelChat.Enabled = chkAutoUnload.Enabled = chkAutoLoad.Enabled = false;
            }
            else
            {
                this.Invoke(new Action(UpdateMapsTab));
            }
        }

        private void DrawLevel(Level l)
        {
            try
            {
                if (l == null) return;
                Rectangle r = new Rectangle(0, 0, pbMapViewer.Width, pbMapViewer.Height);
                //pbMapViewer.Image = new IsoCat(l, IsoCatMode.Normal, 0).Draw(out r, new BackgroundWorker() { WorkerReportsProgress = true });
            }
            catch (Exception e) { Server.ErrorLog(e); pbMapViewer.Image = new Bitmap(pbMapViewer.Width, pbMapViewer.Height); gbMapViewer.Text = "MapViewer - ERROR"; }
        }

        private void btnUnloadLevel_Click(object sender, EventArgs e)
        {
            if (liMaps.SelectedIndex < 0) return;
            Level l = Level.Find(liMaps.SelectedItem.ToString().Remove((liMaps.SelectedItem.ToString().Length - 4)));
            if (l == Server.mainLevel) { MessageBox.Show("You cannot unload the main level."); return; }
            liMaps.SetSelected(0, false);
            l.Unload();
            UpdateMapList();
            if (l == null && liUnloaded.Items.Contains(l.name))
                liUnloaded.SetSelected(liUnloaded.Items.IndexOf(l.name), true);
        }

        private void btnLoadLevel_Click(object sender, EventArgs e)
        {
            if (liUnloaded.SelectedIndex < 0) return;
            string l = liUnloaded.Text;
            liUnloaded.SetSelected(0, false);
            Command.all.Find("load").Use(null, l);
            UpdateMapList();
            liMaps.SetSelected(liMaps.Items.IndexOf(Level.Find(l).name + " - " + Level.Find(l).physics.ToString()), true);
        }

        private void liMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (liMaps.SelectedIndex > -1)
                if (liUnloaded.SelectedIndex > -1) liUnloaded.SetSelected(0, false);
                UpdateMapsTab();
        }

        private void liUnloaded_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (liUnloaded.SelectedIndex > -1)
                    if (liMaps.SelectedIndex > -1) liMaps.SetSelected(0, false);
                    UpdateMapsTab();
        }

        private void btnUpdateLevel_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(liMaps.Text))
            {
                Level l = Level.Find(liMaps.Text.Remove(liMaps.Text.Length - 4));
                if (l == null) MessageBox.Show("Could not find level with name \"" + liMaps.Text.Remove(liMaps.Text.Length - 4) + "\"", "Level not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Only change things if the aren't blank
                l.motd = (String.IsNullOrEmpty(txtLevelMotd.Text)) ? l.motd : txtLevelMotd.Text;

                l.permissionbuild = Level.PermissionFromName(cmbPerBuild.Items[cmbPerBuild.SelectedIndex].ToString());
                l.permissionvisit = Level.PermissionFromName(cmbPerVisit.Items[cmbPerVisit.SelectedIndex].ToString());

                l.GrassGrow = chkGrassGrowing.Checked;
                l.Killer = chkKillerBlocks.Checked;
                l.Death = chkSurvivalDeath.Checked;
                l.finite = chkFiniteMode.Checked;
                l.edgeWater = chkEdgeWater.Checked;
                l.ai = chkAnimalAi.Checked;
                l.worldChat = !chkLevelChat.Checked;
                l.unload = chkAutoUnload.Checked;
                {
                    List<string> oldlines = new List<string>();
                    using (StreamReader r = new StreamReader("text/autoload.txt"))
                    {
                        bool done = false;
                        string line;
                        while ((line = r.ReadLine()) != null)
                        {
                            if (line.ToLower().Contains(l.name.ToLower()))
                            {
                                if (chkAutoLoad.Checked == false)
                                {
                                    line = "";
                                }
                                done = true;
                            }
                            oldlines.Add(line);
                        }
                        if (chkAutoLoad.Checked == true && done == false)
                        {
                            oldlines.Add(l.name + "=" + l.physics);
                        }
                    }
                    File.Delete("text/autoload.txt");
                    using (StreamWriter SW = new StreamWriter("text/autoload.txt"))
                        foreach (string line in oldlines)
                            if (line.Trim() != "")
                                SW.WriteLine(line);
                }
                l.Save(true, true);
                l.setPhysics(cmbLevelPhys.SelectedIndex);// Physics has to be set last because it causes a maps tab update
                btnUpdateLevel.Enabled = false;
                return;
            }
            else
                MessageBox.Show("You have to select a loaded level before you can change it.", "Select a level", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnDeleteLevel_Click(object sender, EventArgs e)
        {
            if (Level.Find(liMaps.Text.Remove(liMaps.Text.Length - 4)) == Server.mainLevel) { MessageBox.Show("Cannot delete the main level."); return; }
            DialogResult dr = MessageBox.Show("Keep backups of " + liMaps.Text.Remove(liMaps.Text.Length - 4) + "?", "Keep backups?", MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Yes)
            {
                if (MessageBox.Show("Are you sure you want to delete the level " + liMaps.Text.Remove(liMaps.Text.Length - 4) + "?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Delete only the level
                    string message = liMaps.Text.Remove(liMaps.Text.Length - 4);
                    Level l = Level.Find(message);
                    if (l != null) l.Unload();

                    try
                    {
                        if (!Directory.Exists("levels/deleted")) Directory.CreateDirectory("levels/deleted");

                        if (File.Exists("levels/" + message + ".lvl"))
                        {
                            if (File.Exists("levels/deleted/" + message + ".lvl"))
                            {
                                int currentNum = 0;
                                while (File.Exists("levels/deleted/" + message + currentNum + ".lvl")) currentNum++;

                                File.Move("levels/" + message + ".lvl", "levels/deleted/" + message + currentNum + ".lvl");
                            }
                            else
                            {
                                File.Move("levels/" + message + ".lvl", "levels/deleted/" + message + ".lvl");
                            }

                            try { File.Delete("levels/level properties/" + message + ".properties"); }
                            catch { }
                            try { File.Delete("levels/level properties/" + message); }
                            catch { }

                            SQLiteHelper.ExecuteQuery($@"DROP TABLE IF EXISTS Blocks{message};");
                            SQLiteHelper.ExecuteQuery($@"DROP TABLE IF EXISTS Portals{message};");
                            SQLiteHelper.ExecuteQuery($@"DROP TABLE IF EXISTS Messages{message};");
                            SQLiteHelper.ExecuteQuery($@"DROP TABLE IF EXISTS Zones{message};");

                            Player.GlobalMessage("Level " + message + " was deleted.");
                            MessageBox.Show("Level " + message + " was deleted.");
                        }
                        else
                        {
                            MessageBox.Show("Could not find level file: " + message + ".lvl");
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error while deleting"); Server.ErrorLog(ex); }
                }
            }
            else if (dr == DialogResult.No)
            {
                if (MessageBox.Show("Are you sure you want to delete the level " + liMaps.Text.Remove(liMaps.Text.Length - 4) + " AND all its backups?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Delete the level and all backups
                    string message = liMaps.Text.Remove(liMaps.Text.Length - 4);
                    Level l = Level.Find(message);
                    if (l != null) l.Unload();

                    try
                    {
                        if (!Directory.Exists("levels/deleted")) Directory.CreateDirectory("levels/deleted");

                        if (File.Exists("levels/" + message + ".lvl"))
                        {
                            if (File.Exists("levels/deleted/" + message + ".lvl"))
                            {
                                int currentNum = 0;
                                while (File.Exists("levels/deleted/" + message + currentNum + ".lvl")) currentNum++;

                                File.Move("levels/" + message + ".lvl", "levels/deleted/" + message + currentNum + ".lvl");
                            }
                            else
                            {
                                File.Move("levels/" + message + ".lvl", "levels/deleted/" + message + ".lvl");
                            }

                            try { File.Delete("levels/level properties/" + message + ".properties"); }
                            catch { }
                            try { File.Delete("levels/level properties/" + message); }
                            catch { }
                            try { Directory.Delete(Server.backupLocation + "/" + message); }
                            catch { }

                            SQLiteHelper.ExecuteQuery($@"DROP TABLE IF EXISTS Blocks{message};");
                            SQLiteHelper.ExecuteQuery($@"DROP TABLE IF EXISTS Portals{message};");
                            SQLiteHelper.ExecuteQuery($@"DROP TABLE IF EXISTS Messages{message};");
                            SQLiteHelper.ExecuteQuery($@"DROP TABLE IF EXISTS Zones{message};");

                            Player.GlobalMessage("Level " + message + " was deleted.");
                            MessageBox.Show("Level " + message + " AND all its backups were deleted.");
                        }
                        else
                        {
                            MessageBox.Show("Could not find level file: " + message + ".lvl");
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error while deleting"); Server.ErrorLog(ex); }
                }
            }
            UpdateMapList();
        }

        private void btnBackupManager_Click(object sender, EventArgs e)
        {
            BackupManager bm = new BackupManager();
            bm.ShowDialog();
        }

        public string newName = "";
        private void btnRenameLevel_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(liMaps.Text)) return;
            Level l = Level.Find(liMaps.Text.Remove(liMaps.Text.Length - 4));
            if (l == Server.mainLevel) { MessageBox.Show("Cannot rename the main level."); return; }
            try
            {
                if (new RenameLevelDialog(l).ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists("levels/" + newName)) { MessageBox.Show("Level already exists."); return; }
                    string oldName = l.name;
                    l.Unload();

                    try
                    {
                        File.Move("levels/" + oldName + ".lvl", "levels/" + newName + ".lvl");

                        try
                        {
                            File.Move("levels/level properties/" + oldName + ".properties", "levels/level properties/" + newName + ".properties");
                        }
                        catch { }
                        try
                        {
                            File.Move("levels/level properties/" + oldName, "levels/level properties/" + newName + ".properties");
                        }
                        catch { }

                        SQLiteHelper.ExecuteQuery($@"ALTER TABLE Blocks{oldName} RENAME TO Blocks{newName};");
                        SQLiteHelper.ExecuteQuery($@"ALTER TABLE Portals{oldName} RENAME TO Portals{newName};");
                        SQLiteHelper.ExecuteQuery($@"ALTER TABLE Messages{oldName} RENAME TO Messages{newName};");
                        SQLiteHelper.ExecuteQuery($@"ALTER TABLE Zones{oldName} RENAME TO Zones{newName};");

                        Player.GlobalMessage("Renamed " + oldName + " to " + newName);
                    }
                    catch { }
                }
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
            UpdateMapList();
            newName = "";
        }

        private void linkLabels_MouseEnter(object sender, EventArgs e)
        {
            lblUrl.Text = ((LinkLabel)sender).AccessibleDescription;
            int pad = groupBox13.Width - lblUrl.Width;
            lblUrl.Padding = new Padding(pad, 0, 0, 0);
        }

        private void linkLabels_MouseLeave(object sender, EventArgs e)
        {
            lblUrl.Padding = new Padding(0);
            lblUrl.ResetText();
        }

        private void linkLabels_Click(object sender, EventArgs e)
        {
            try { Process.Start(((LinkLabel)sender).AccessibleDescription); }
            catch { MessageBox.Show("Unable to open link: " + ((LinkLabel)sender).AccessibleDescription, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("extra/images/" + liMaps.Text.Remove(liMaps.Text.Length - 4)))
                Directory.CreateDirectory("extra/images/" + liMaps.Text.Remove(liMaps.Text.Length - 4));
            DirectoryInfo di = new DirectoryInfo("extra/images/" + liMaps.Text.Remove(liMaps.Text.Length - 4));
            string path = "extra/images/" + liMaps.Text.Remove(liMaps.Text.Length - 4) + "/" + di.GetFiles().Length + ".png";
            pbMapViewer.Image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            MessageBox.Show("Map saved to " + path);
        }

        private void UpdateStats(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(delegate
            {
                if (Server.PCCounter == null || Server.ProcessCounter == null)
                    Server.s.Log("Starting performance counters...");
                if (Server.PCCounter == null)
                {
                    new Thread((ThreadStart)(() =>
                    {
                        Server.PCCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                        Server.PCCounter.BeginInit();
                        Server.PCCounter.NextValue();
                    })).Start();
                }
                if (Server.ProcessCounter == null)
                {
                    new Thread((ThreadStart)(() =>
                    {
                        Server.ProcessCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
                        Server.ProcessCounter.BeginInit();
                        Server.ProcessCounter.NextValue();
                    })).Start();
                }

                TimeSpan tp = Process.GetCurrentProcess().TotalProcessorTime;
                TimeSpan up = (DateTime.Now - Process.GetCurrentProcess().StartTime);

                string cpu = Math.Round(Server.ProcessCounter.NextValue()).ToString();
                string cputotal = Math.Round(Server.PCCounter.NextValue()).ToString();
                txtCpu.Text = cpu + "%  /  " + cputotal + "%";
                txtMemory.Text = Math.Round((double)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576).ToString() + " MB";
                txtThreads.Text = Process.GetCurrentProcess().Threads.Count.ToString();
                txtUptime.Text = up.Days + " Days, " + up.Hours + " Hours, " + up.Minutes + " Minutes, " + up.Seconds + " Seconds";
                lblStartingCounters.Visible = false;
            }));
        }

        private void btnLevelHacks_Click(object sender, EventArgs e)
        {
            Level l = Level.Find(liMaps.Text.Remove(liMaps.Text.Length - 4));
            new HackControlDialog(l).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button3.Enabled = false;
            lblStartingCounters.Visible = true;
            System.Timers.Timer statsTimer = new System.Timers.Timer(1000);
            statsTimer.Elapsed += UpdateStats;
            statsTimer.Start();
        }

        private void btnNewLevel_Click(object sender, EventArgs e)
        {
            if (new NewLevelDialog().ShowDialog() == DialogResult.OK)
                UpdateMapList();
        }

        private void LevelSettings_Changed(object sender, EventArgs e)
        {
            btnUpdateLevel.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Level l = Level.Find(liMaps.Text.Remove(liMaps.Text.Length - 4));
            new EnvColorsDialog(l).ShowDialog();
        }
    }
}
