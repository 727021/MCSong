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
        delegate void StringCallback(string s);
        delegate void PlayerListCallback(List<Player> players);
        delegate void ReportCallback(Report r);
        delegate void VoidDelegate();

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
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
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

        private void Window_Load(object sender, EventArgs e) {
            thisWindow = this;
            MaximizeBox = false;
            this.Text = "<server name here>";
            this.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("MCSong.Lawl.ico"));

            lblCopyright.Text += DateTime.Today.Year.ToString();
            txtGlobalOut.Enabled = txtGlobalIn.Enabled = btnGlobalChat.Enabled = Server.gc;

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
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);

            /*System.Timers.Timer MapTimer = new System.Timers.Timer(10000);
            MapTimer.Elapsed += delegate {
                UpdateMapList("'");
            }; MapTimer.Start();*/

            // Update the list on load, unload, and physchange
            Level.OnLevelLoadEvent += UpdateMapList;
            Level.OnLevelUnloadEvent += UpdateMapList;
            Level.OnLevelPhysChangeEvent += UpdateMapsTab;

            //if (File.Exists(Logger.ErrorLogPath))
                //txtErrors.Lines = File.ReadAllLines(Logger.ErrorLogPath);
            try
            {
                if (File.Exists("extra/Changelog.txt"))
                {
                    File.Delete("extra/Changelog.txt");
                }
                WebClient Web = new WebClient();
                Web.DownloadFile("http://updates.mcsong.x10.mx/changelog.txt", "extra/Changelog.txt");
                Web.Dispose();
            }
            catch { }
            if (File.Exists("extra/Changelog.txt"))
            {
                foreach (string line in File.ReadAllLines(("extra/Changelog.txt")))
                {
                    txtChangelog.AppendText("\r\n\t\t" + line);
                }
            }
            else
            {
                txtChangelog.Text = "\r\nChangelog not found!\r\nDownload it manually from http://updates.mcsong.x10.mx/changelog.txt and save it as \'extra/Changelog.txt\'";
            }
            txtCurrentVersion.Text = Server.Version;
            try { txtLatestVersion.Enabled = true; txtLatestVersion.Text = new WebClient().DownloadString("http://updates.mcsong.x10.mx/curversion.txt"); }
            catch { txtLatestVersion.Enabled = false; }

            Server.devs.ForEach(delegate(string dev) { txtDevList.Text += (Environment.NewLine + dev); });
        }

        void SettingsUpdate()
        {
            if (shuttingDown) return;
            if (txtLog.InvokeRequired)
            {
                VoidDelegate d = new VoidDelegate(SettingsUpdate);
                this.Invoke(d);
            }  else {
                this.Text = Server.name + " - MCSong Version: " + Server.Version;
            }
        }

        void HeartBeatFail() {
            WriteLine("Recent Heartbeat Failed");
        }

        void newError(string message)
        {
            try
            {
                if (txtErrors.InvokeRequired)
                {
                    LogDelegate d = new LogDelegate(newError);
                    this.Invoke(d, new object[] { message });
                }
                else
                {
                    txtErrors.AppendText(Environment.NewLine + message);
                    txtErrors.SelectionStart = txtErrors.Text.Length;
                    txtErrors.ScrollToCaret();
                }
            } catch { }
        }
        void newSystem(string message)
        {
            try
            {
                if (txtSystem.InvokeRequired)
                {
                    LogDelegate d = new LogDelegate(newSystem);
                    this.Invoke(d, new object[] { message });
                }
                else
                {
                    txtSystem.AppendText(Environment.NewLine + message);
                    txtSystem.SelectionStart = txtSystem.Text.Length;
                    txtSystem.ScrollToCaret();
                }
            } catch { }
        }

        delegate void LogDelegate(string message);

        /// <summary>
        /// Does the same as Console.Write() only in the form
        /// </summary>
        /// <param name="s">The string to write</param>
        public void Write(string s) {
            if (shuttingDown) return;
            if (txtLog.InvokeRequired) {
                LogDelegate d = new LogDelegate(Write);
                this.Invoke(d, new object[] { s });
            } else {
                txtLog.AppendText(s);
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
            }
        }
        /// <summary>
        /// Does the same as Console.WriteLine() only in the form
        /// </summary>
        /// <param name="s">The line to write</param>
        public void WriteLine(string s)
        {
            if (shuttingDown) return;
            if (this.InvokeRequired) {
                LogDelegate d = new LogDelegate(WriteLine);
                this.Invoke(d, new object[] { s });
            } else {
                txtLog.AppendText("\r\n" + s);
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
            }
        }
        public void WriteLineOp(string s)
        {
            if (shuttingDown) return;
            if (this.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(WriteLineOp);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                txtOpOut.AppendText((txtOpOut.Text.Length > 0) ? "\r\n" + s : s);
                txtOpOut.SelectionLength = txtOpOut.Text.Length;
                txtOpOut.ScrollToCaret();
            }
        }
        public void WriteLineAdmin(string s)
        {
            if (shuttingDown) return;
            if (this.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(WriteLineAdmin);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                txtAdminOut.AppendText((txtAdminOut.Text.Length > 0) ? "\r\n" + s : s);
                txtAdminOut.SelectionLength = txtAdminOut.Text.Length;
                txtAdminOut.ScrollToCaret();
            }
        }
        public void WriteLineGlobal(string s)
        {
            if (shuttingDown) return;
            if (this.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(WriteLineGlobal);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                txtGlobalOut.AppendText((txtGlobalOut.Text.Length > 0) ? "\r\n" + s : s);
                txtGlobalOut.SelectionLength = txtGlobalOut.Text.Length;
                txtGlobalOut.ScrollToCaret();
            }
        }
        /// <summary>
        /// Updates the list of client names in the window
        /// </summary>
        /// <param name="players">The list of players to add</param>
        public void UpdateClientList(List<Player> players) {
            if (this.InvokeRequired) {
                PlayerListCallback d = new PlayerListCallback(UpdateClientList);
                this.Invoke(d, new object[] { players });
            } else {
                liClients.Items.Clear();
                Player.players.ForEach(delegate(Player p) { liClients.Items.Add(p.name); });
                txtPlayerCount.Clear();
                txtPlayerCount.Text = Player.players.Count.ToString() + "/";
                int guests = 0;
                Player.players.ForEach(delegate(Player p) { if (p.group.Permission == LevelPermission.Guest) { guests++; } });
                txtPlayerCount.AppendText(guests.ToString());
            }
        }

        public void UpdateMapList(string blah) {            
            if (this.InvokeRequired) {
                LogDelegate d = new LogDelegate(UpdateMapList);
                this.Invoke(d, new object[] { blah });
            } else {
                liMaps.Items.Clear();
                foreach (Level level in Server.levels) {
                    liMaps.Items.Add(level.name + " - " + level.physics);
                }
                liUnloaded.Items.Clear();
                foreach (string f in Directory.GetFiles("levels/", "*.lvl", SearchOption.TopDirectoryOnly))
                {
                    FileInfo fi = new FileInfo(f);
                    string n = fi.Name.Replace(fi.Extension, "");
                    if (Level.Find(n) == null)
                        liUnloaded.Items.Add(n);
                }
                UpdateMapsTab();
            }
        }

        /// <summary>
        /// Places the server's URL at the top of the window
        /// </summary>
        /// <param name="s">The URL to display</param>
        public void UpdateUrl(string s)
        {
            if (this.InvokeRequired)
            {
                StringCallback d = new StringCallback(UpdateUrl);
                this.Invoke(d, new object[] { s });
            }
            else
                txtUrl.Text = s;
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
                    Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                    Server.s.LogOp("Console: " + newtext);
                    IRCBot.Say("Console: " + newtext, true);
                 //   WriteLine("(OPs):<CONSOLE> " + txtInput.Text);
                    txtInput.Clear();
                }
                else if (txtInput.Text[0] == ';')
                {
                    newtext = text.Remove(0, 1).Trim();
                    Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                    Server.s.LogAdmin("Console: " + newtext);
                    IRCBot.Say("Console: " + newtext, true);
                    txtInput.Clear();
                }
                else if (txtInput.Text[0] == '\\')
                {
                    if (!Server.gc) return;
                    newtext = text.Remove(0, 1).Trim();
                    Player.GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] Console: &f" + newtext);
                    Server.s.LogGC("[" + Server.gcNick + "] Console: " + newtext);
                    GlobalBot.Say("Console: " + newtext);
                    txtInput.Clear();
                }
                else
                {
                    Player.GlobalMessage("Console [&a" + Server.ZallState + Server.DefaultColor + "]: &f" + txtInput.Text);
                    IRCBot.Say("Console [" + Server.ZallState + "]: " + txtInput.Text);
                    WriteLine("<CONSOLE> " + txtInput.Text);
                    txtInput.Clear();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                string up = cbInput.up();
                txtInput.Text = (up == "") ? txtInput.Text : up;
                txtInput.SelectionLength = txtInput.Text.Length;
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtInput.Text = cbInput.down();
                txtInput.SelectionLength = txtInput.Text.Length;
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
                string up = cbCommands.up();
                txtCommands.Text = (up == "") ? txtCommands.Text : up;
                txtCommands.SelectionLength = txtCommands.Text.Length;
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtCommands.Text = cbCommands.down();
                txtCommands.SelectionLength = txtCommands.Text.Length;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e) { 
            if (notifyIcon1 != null) {
                notifyIcon1.Visible = false;
            }
            MCSong_.Gui.Program.ExitProgram(false); 
        }

        public void newCommand(string p) { 
            if (txtCommandsUsed.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(newCommand);
                this.Invoke(d, new object[] { p });
            }
            else
            {
                txtCommandsUsed.AppendText("\r\n" + p);
                txtCommandsUsed.SelectionStart = txtCommandsUsed.Text.Length;
                txtCommandsUsed.ScrollToCaret();
            }
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
            txtAdminOut.SelectionLength = txtAdminOut.Text.Length;
            txtOpOut.SelectionLength = txtOpOut.Text.Length;
            txtGlobalOut.SelectionLength = txtGlobalOut.Text.Length;
            txtAdminOut.ScrollToCaret();
            txtOpOut.ScrollToCaret();
            txtGlobalOut.ScrollToCaret();
            UpdateMapList("'");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("extra/Changelog.txt"))
                {
                    File.Delete("extra/Changelog.txt");
                }
                WebClient Web = new WebClient();
                Web.DownloadFile("http://updates.mcsong.x10.mx/changelog.txt", "extra/Changelog.txt");
                Web.Dispose();
            }
            catch { }
            if (File.Exists("extra/Changelog.txt"))
            {
                foreach (string line in File.ReadAllLines(("extra/Changelog.txt")))
                {
                    txtChangelog.AppendText("\r\n\t\t" + line);
                }
            }
            else
            {
                txtChangelog.Text = "\r\nChangelog not found!\r\nDownload it manually from http://updates.mcsong.x10.mx/changelog.txt and save it as \'extra/Changelog.txt\'";
            }
            txtCurrentVersion.Text = Server.Version;
            try { txtLatestVersion.Enabled = true; txtLatestVersion.Text = new WebClient().DownloadString("http://updates.mcsong.x10.mx/curversion.txt"); }
            catch { txtLatestVersion.Enabled = false; }
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
                Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                Server.s.LogOp("Console: " + newtext);
                IRCBot.Say("Console: " + newtext, true);
                //   WriteLine("(OPs):<CONSOLE> " + txtInput.Text);
                txtInput.Clear();
            }
            else if (txtInput.Text[0] == ';')
            {
                newtext = text.Remove(0, 1).Trim();
                Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + newtext);
                Server.s.LogAdmin("Console: " + newtext);
                IRCBot.Say("Console: " + newtext, true);
                txtInput.Clear();
            }
            else if (txtInput.Text[0] == '\\')
            {
                if (!Server.gc) return;
                newtext = text.Remove(0, 1).Trim();
                Player.GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] Console: &f" + newtext);
                Server.s.LogGC("[" + Server.gcNick + "] Console: " + newtext);
                GlobalBot.Say("Console: " + newtext);
                txtInput.Clear();
            }
            else
            {
                Player.GlobalMessage("Console [&a" + Server.ZallState + Server.DefaultColor + "]: &f" + txtInput.Text);
                IRCBot.Say("Console [" + Server.ZallState + "]: " + txtInput.Text);
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
            lblInfoVersion.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://mcsong.x10.mx"); }
            catch { MessageBox.Show("Unable to open link: http://mcsong.x10.mx", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://mcsong.x10.mx/forums/"); }
            catch { MessageBox.Show("Unable to open link: http://mcsong.x10.mx/forums/", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://mcsong.x10.mx/servers/"); }
            catch { MessageBox.Show("Unable to open link: http://mcsong.x10.mx/servers/", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://www.classicube.net/"); }
            catch { MessageBox.Show("Unable to open link: http://www.classicube.net/", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://dev.mysql.com/downloads/windows/installer/"); }
            catch { MessageBox.Show("Unable to open link: http://dev.mysql.com/downloads/windows/installer/", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://portforward.com/english/routers/port_forwarding/routerindex.htm"); }
            catch { MessageBox.Show("Unable to open link: http://portforward.com/english/routers/port_forwarding/routerindex.htm", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://mcsong.x10.mx/forums/forumdisplay.php?fid=16"); }
            catch { MessageBox.Show("Unable to open link: http://mcsong.x10.mx/forums/forumdisplay.php?fid=16", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://www.classicube.net/"); }
            catch { MessageBox.Show("Unable to open link: http://www.classicube.net/", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("https://github.com/727021/MCSong"); }
            catch { MessageBox.Show("Unable to open link: https://github.com/727021/MCSong", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://mcsong.x10.mx/remote/"); }
            catch { MessageBox.Show("Unable to open link: http://mcsong.x10.mx/remote/", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
            string text = txtAdminIn.Text.Trim();

            Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
            Server.s.LogAdmin("Console: " + text);
            IRCBot.Say("Console: " + text, true);
            txtAdminIn.Clear();
        }

        private void btnOpChat_Click(object sender, EventArgs e)
        {
            if (txtOpIn.Text == null || txtOpIn.Text.Trim() == "") { return; }
            cbOpChat.addEntry(txtOpIn.Text);
            string text = txtOpIn.Text.Trim();

            Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
            Server.s.LogOp("Console: " + text);
            IRCBot.Say("Console: " + text, true);
            txtOpIn.Clear();
        }

        private void btnGlobalChat_Click(object sender, EventArgs e)
        {
            if (txtGlobalIn.Text == null || txtGlobalIn.Text.Trim() == "") return;
            cbGlobalChat.addEntry(txtGlobalIn.Text);
            string text = txtGlobalIn.Text.Trim();

            Player.GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] Console: &f" + text);
            Server.s.LogGC("[" + Server.gcNick + "] Console: " + text);
            GlobalBot.Say("Console: " + text);
            txtGlobalIn.Clear();
        }

        private void txtAdminIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtAdminIn.Text == null || txtAdminIn.Text.Trim() == "") { return; }
                cbAdminChat.addEntry(txtAdminIn.Text);
                string text = txtAdminIn.Text.Trim();

                Player.GlobalMessageAdmins("To Admins &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
                Server.s.LogAdmin("Console: " + text);
                IRCBot.Say("Console: " + text, true);
                txtAdminIn.Clear();
            }
            else if (e.KeyCode == Keys.Up)
            {
                string up = cbAdminChat.up();
                txtAdminIn.Text = (up == "") ? txtAdminIn.Text : up;
                txtAdminIn.SelectionLength = txtAdminIn.Text.Length;
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtAdminIn.Text = cbAdminChat.down();
                txtAdminIn.SelectionLength = txtAdminIn.Text.Length;
            }
        }

        private void txtOpIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtOpIn.Text == null || txtOpIn.Text.Trim() == "") { return; }
                cbOpChat.addEntry(txtOpIn.Text);
                string text = txtOpIn.Text.Trim();

                Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + "Console [&a" + Server.ZallState + Server.DefaultColor + "]&f- " + text);
                Server.s.LogOp("Console: " + text);
                IRCBot.Say("Console: " + text, true);
                txtOpIn.Clear();
            }
            else if (e.KeyCode == Keys.Up)
            {
                string up = cbOpChat.up();
                txtOpIn.Text = (up == "") ? txtOpIn.Text : up;
                txtOpIn.SelectionLength = txtOpIn.Text.Length;
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtOpIn.Text = cbOpChat.down();
                txtOpIn.SelectionLength = txtOpIn.Text.Length;
            }
        }

        private void txtGlobalIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtGlobalIn.Text == null || txtGlobalIn.Text.Trim() == "") return;
                cbGlobalChat.addEntry(txtGlobalIn.Text);
                string text = txtGlobalIn.Text.Trim();

                Player.GlobalMessageGC(Server.gcColor + "[Global][" + Server.gcNick + "] Console: &f" + text);
                Server.s.LogGC("[" + Server.gcNick + "] Console: " + text);
                GlobalBot.Say("Console: " + text);
                txtGlobalIn.Clear();
            }
            else if (e.KeyCode == Keys.Up)
            {
                string up = cbGlobalChat.up();
                txtGlobalIn.Text = (up == "") ? txtGlobalIn.Text : up;
                txtGlobalIn.SelectionLength = txtGlobalIn.Text.Length;
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtGlobalIn.Text = cbGlobalChat.down();
                txtGlobalIn.SelectionLength = txtGlobalIn.Text.Length;
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
            if (!String .IsNullOrWhiteSpace(liMaps.Text))
            {
                Level l = Level.Find(liMaps.Text.Remove(liMaps.Text.Length - 4));
                if (l == null) goto clearMapPath;

                btnUpdateLevel.Enabled = btnRenameLevel.Enabled = btnDeleteLevel.Enabled = true;

                btnUnloadLevel.Enabled = true;
                btnLoadLevel.Enabled = false;
                cmbLevelPhys.Enabled = true;

                txtLevelPath.Text = new FileInfo("levels/" + l.name + ".lvl").FullName;
                txtLevelMotd.Text = l.motd;
                cmbLevelPhys.SelectedIndex = l.physics;
                txtLevelX.Text = l.width.ToString();
                txtLevelY.Text = l.height.ToString();
                txtLevelZ.Text = l.depth.ToString();


                DrawLevel(l);
                return;
            }
            else if (!String.IsNullOrWhiteSpace(liUnloaded.Text))
            {
                btnUpdateLevel.Enabled = btnRenameLevel.Enabled = btnDeleteLevel.Enabled = false;

                btnUnloadLevel.Enabled = false;
                btnLoadLevel.Enabled = true;
                cmbLevelPhys.Enabled = false;

                txtLevelPath.Text = new FileInfo("levels/" + liUnloaded.Text + ".lvl").FullName;
                goto clearMaps;
            }
            else
            {
                btnLoadLevel.Enabled = btnUnloadLevel.Enabled = cmbLevelPhys.Enabled = false;
                btnUpdateLevel.Enabled = btnRenameLevel.Enabled = btnDeleteLevel.Enabled = false;
            }

        clearMapPath:
            txtLevelPath.Clear();
        clearMaps:
            txtLevelMotd.Clear();
            cmbLevelPhys.SelectedIndex = 0;
            txtLevelX.Clear();
            txtLevelY.Clear();
            txtLevelZ.Clear();
            pbMapViewer.Image = new Bitmap(pbMapViewer.Width, pbMapViewer.Height);
        }

        private void DrawLevel(Level l)
        {
            try
            {
                if (l == null) return;
                Rectangle r = new Rectangle(0, 0, pbMapViewer.Width, pbMapViewer.Height);
                pbMapViewer.Image = new IsoCat(l, IsoCatMode.Normal, 0).Draw(out r, new BackgroundWorker() { WorkerReportsProgress = true });
            }
            catch (Exception e) { Server.ErrorLog(e); pbMapViewer.Image = new Bitmap(pbMapViewer.Width, pbMapViewer.Height); gbMapViewer.Text = "MapViewer - ERROR"; }
        }

        private void btnUnloadLevel_Click(object sender, EventArgs e)
        {
            if (liMaps.SelectedIndex < 0) return;
            Level l = Level.Find(liMaps.SelectedItem.ToString().Remove((liMaps.SelectedItem.ToString().Length - 4)));
            liMaps.SetSelected(0, false);
            if (l == Server.mainLevel) { MessageBox.Show("You cannot unload the main level."); return; }
            l.Unload();
            UpdateMapList("'");
            if (Level.Find(l.name) == null)
                liUnloaded.SetSelected(liUnloaded.Items.IndexOf(l.name), true);
        }

        private void btnLoadLevel_Click(object sender, EventArgs e)
        {
            if (liUnloaded.SelectedIndex < 0) return;
            string l = liUnloaded.Text;
            liUnloaded.SetSelected(0, false);
            Command.all.Find("load").Use(null, l);
            UpdateMapList("'");
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

                txtLevelPath.Text = new FileInfo("levels/" + l.name + ".lvl").FullName;
                // Only change things if the aren't blank
                l.motd = (String.IsNullOrEmpty(txtLevelMotd.Text)) ? l.motd : txtLevelMotd.Text;
                l.setPhysics(cmbLevelPhys.SelectedIndex);

                l.Save(true);
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

                            MySQL.executeQuery("DROP TABLE `Block" + message + "`, `Portals" + message + "`, `Messages" + message + "`, `Zone" + message + "`");

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

                            MySQL.executeQuery("DROP TABLE `Block" + message + "`, `Portals" + message + "`, `Messages" + message + "`, `Zone" + message + "`");

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
            UpdateMapList("'");
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

                        MySQL.executeQuery("RENAME TABLE `Block" + oldName.ToLower() + "` TO `Block" + newName.ToLower() +
                            "`, `Portals" + oldName.ToLower() + "` TO `Portals" + newName.ToLower() +
                            "`, `Messages" + oldName.ToLower() + "` TO Messages" + newName.ToLower() +
                            ", `Zone" + oldName.ToLower() + "` TO `Zone" + newName.ToLower() + "`");

                        Player.GlobalMessage("Renamed " + oldName + " to " + newName);
                    }
                    catch { }
                }
            }
            catch (Exception ex) { Server.ErrorLog(ex); }
            UpdateMapList("'");
            newName = "";
        }
    }
}
