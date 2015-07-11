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
using System.Windows.Forms;

namespace MCSong.Gui
{
    public partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message msg)
        {
            const int WM_SIZE = 0x0005;
            const int SIZE_MINIMIZED = 1;

            if ((msg.Msg == WM_SIZE) && ((int)msg.WParam == SIZE_MINIMIZED) && (Window.Minimize != null))
            {
                this.Window_Minimize(this, EventArgs.Empty);
            }

            base.WndProc(ref msg);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnPlay = new System.Windows.Forms.Button();
            this.chkMaintenance = new System.Windows.Forms.CheckBox();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnProperties = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gBCommands = new System.Windows.Forms.GroupBox();
            this.btnCommand = new System.Windows.Forms.Button();
            this.txtCommandsUsed = new System.Windows.Forms.TextBox();
            this.txtCommands = new System.Windows.Forms.TextBox();
            this.gBChat = new System.Windows.Forms.GroupBox();
            this.btnChat = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtAdminOut = new System.Windows.Forms.TextBox();
            this.btnAdminChat = new System.Windows.Forms.Button();
            this.txtAdminIn = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.txtOpOut = new System.Windows.Forms.TextBox();
            this.btnOpChat = new System.Windows.Forms.Button();
            this.txtOpIn = new System.Windows.Forms.TextBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.txtGlobalOut = new System.Windows.Forms.TextBox();
            this.btnGlobalChat = new System.Windows.Forms.Button();
            this.txtGlobalIn = new System.Windows.Forms.TextBox();
            this.tpPlayers = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPlayerCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.liClients = new System.Windows.Forms.ListBox();
            this.tpLevels = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbMapViewer = new System.Windows.Forms.GroupBox();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.pbMapViewer = new System.Windows.Forms.PictureBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbPerVisit = new System.Windows.Forms.ComboBox();
            this.cmbPerBuild = new System.Windows.Forms.ComboBox();
            this.btnRenameLevel = new System.Windows.Forms.Button();
            this.btnBackupManager = new System.Windows.Forms.Button();
            this.btnDeleteLevel = new System.Windows.Forms.Button();
            this.cmbLevelPhys = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLevelY = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLevelZ = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLevelX = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLevelMotd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnUpdateLevel = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.liUnloaded = new System.Windows.Forms.ListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.liMaps = new System.Windows.Forms.ListBox();
            this.txtLevelPath = new System.Windows.Forms.TextBox();
            this.btnUnloadLevel = new System.Windows.Forms.Button();
            this.btnLoadLevel = new System.Windows.Forms.Button();
            this.tpChangelog = new System.Windows.Forms.TabPage();
            this.txtLatestVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurrentVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChangelog = new System.Windows.Forms.Button();
            this.txtChangelog = new System.Windows.Forms.TextBox();
            this.tpLogs = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSystem = new System.Windows.Forms.TextBox();
            this.gbErrors = new System.Windows.Forms.GroupBox();
            this.txtErrors = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblUrl = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.linkLabel10 = new System.Windows.Forms.LinkLabel();
            this.linkLabel9 = new System.Windows.Forms.LinkLabel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.txtDevList = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.txtThreads = new System.Windows.Forms.TextBox();
            this.txtMemory = new System.Windows.Forms.TextBox();
            this.txtCpu = new System.Windows.Forms.TextBox();
            this.txtUptime = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tmrRestart = new System.Windows.Forms.Timer(this.components);
            this.iconContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownServer = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLevelHacks = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblStartingCounters = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gBCommands.SuspendLayout();
            this.gBChat.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.tpPlayers.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tpLevels.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbMapViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapViewer)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tpChangelog.SuspendLayout();
            this.tpLogs.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbErrors.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.iconContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tpPlayers);
            this.tabControl1.Controls.Add(this.tpLevels);
            this.tabControl1.Controls.Add(this.tpChangelog);
            this.tabControl1.Controls.Add(this.tpLogs);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.tabControl1.Location = new System.Drawing.Point(1, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(581, 514);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.btnPlay);
            this.tabPage1.Controls.Add(this.chkMaintenance);
            this.tabPage1.Controls.Add(this.lblCopyright);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox9);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.gBCommands);
            this.tabPage1.Controls.Add(this.gBChat);
            this.tabPage1.Controls.Add(this.txtUrl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(573, 488);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(392, 7);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(40, 23);
            this.btnPlay.TabIndex = 39;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // chkMaintenance
            // 
            this.chkMaintenance.AutoSize = true;
            this.chkMaintenance.Location = new System.Drawing.Point(449, 11);
            this.chkMaintenance.Name = "chkMaintenance";
            this.chkMaintenance.Size = new System.Drawing.Size(118, 17);
            this.chkMaintenance.TabIndex = 38;
            this.chkMaintenance.Text = "Maintenance Mode";
            this.chkMaintenance.UseVisualStyleBackColor = true;
            this.chkMaintenance.Click += new System.EventHandler(this.chkMaintenance_Click);
            // 
            // lblCopyright
            // 
            this.lblCopyright.Location = new System.Drawing.Point(517, 464);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(46, 13);
            this.lblCopyright.TabIndex = 36;
            this.lblCopyright.Text = "© ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtHost);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(6, 448);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 37);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            // 
            // txtHost
            // 
            this.txtHost.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHost.Location = new System.Drawing.Point(85, 13);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(109, 21);
            this.txtHost.TabIndex = 28;
            this.txtHost.Text = "Alive";
            this.txtHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHost.TextChanged += new System.EventHandler(this.txtHost_TextChanged);
            this.txtHost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommands_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Console State:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button2);
            this.groupBox9.Controls.Add(this.button4);
            this.groupBox9.Controls.Add(this.button5);
            this.groupBox9.Controls.Add(this.button6);
            this.groupBox9.Location = new System.Drawing.Point(209, 448);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(302, 37);
            this.groupBox9.TabIndex = 36;
            this.groupBox9.TabStop = false;
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(153, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 23);
            this.button2.TabIndex = 37;
            this.button2.Text = "Restart";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // button4
            // 
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(229, 11);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(70, 23);
            this.button4.TabIndex = 35;
            this.button4.Text = "Close";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(77, 11);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(70, 23);
            this.button5.TabIndex = 34;
            this.button5.Text = "Properties";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnProperties_Click_1);
            // 
            // button6
            // 
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(3, 11);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(70, 23);
            this.button6.TabIndex = 36;
            this.button6.Text = "Updater";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRestart);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnProperties);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(209, 448);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 37);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            // 
            // btnRestart
            // 
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.Location = new System.Drawing.Point(153, 11);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(70, 23);
            this.btnRestart.TabIndex = 37;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(229, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 23);
            this.btnClose.TabIndex = 35;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // btnProperties
            // 
            this.btnProperties.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProperties.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProperties.Location = new System.Drawing.Point(77, 11);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(70, 23);
            this.btnProperties.TabIndex = 34;
            this.btnProperties.Text = "Properties";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click_1);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "Updater";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // gBCommands
            // 
            this.gBCommands.Controls.Add(this.btnCommand);
            this.gBCommands.Controls.Add(this.txtCommandsUsed);
            this.gBCommands.Controls.Add(this.txtCommands);
            this.gBCommands.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBCommands.Location = new System.Drawing.Point(392, 34);
            this.gBCommands.Name = "gBCommands";
            this.gBCommands.Size = new System.Drawing.Size(175, 408);
            this.gBCommands.TabIndex = 34;
            this.gBCommands.TabStop = false;
            this.gBCommands.Text = "Commands";
            // 
            // btnCommand
            // 
            this.btnCommand.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommand.Location = new System.Drawing.Point(148, 384);
            this.btnCommand.Margin = new System.Windows.Forms.Padding(0);
            this.btnCommand.Name = "btnCommand";
            this.btnCommand.Size = new System.Drawing.Size(21, 21);
            this.btnCommand.TabIndex = 36;
            this.btnCommand.Text = "→";
            this.btnCommand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCommand.UseVisualStyleBackColor = true;
            this.btnCommand.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // txtCommandsUsed
            // 
            this.txtCommandsUsed.BackColor = System.Drawing.Color.White;
            this.txtCommandsUsed.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCommandsUsed.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommandsUsed.Location = new System.Drawing.Point(8, 19);
            this.txtCommandsUsed.Multiline = true;
            this.txtCommandsUsed.Name = "txtCommandsUsed";
            this.txtCommandsUsed.ReadOnly = true;
            this.txtCommandsUsed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommandsUsed.Size = new System.Drawing.Size(161, 359);
            this.txtCommandsUsed.TabIndex = 0;
            // 
            // txtCommands
            // 
            this.txtCommands.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommands.Location = new System.Drawing.Point(8, 384);
            this.txtCommands.Name = "txtCommands";
            this.txtCommands.Size = new System.Drawing.Size(137, 21);
            this.txtCommands.TabIndex = 28;
            this.txtCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommands_KeyDown);
            // 
            // gBChat
            // 
            this.gBChat.Controls.Add(this.btnChat);
            this.gBChat.Controls.Add(this.txtLog);
            this.gBChat.Controls.Add(this.txtInput);
            this.gBChat.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBChat.Location = new System.Drawing.Point(6, 34);
            this.gBChat.Name = "gBChat";
            this.gBChat.Size = new System.Drawing.Size(380, 408);
            this.gBChat.TabIndex = 32;
            this.gBChat.TabStop = false;
            this.gBChat.Text = "Chat";
            // 
            // btnChat
            // 
            this.btnChat.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChat.Location = new System.Drawing.Point(353, 384);
            this.btnChat.Margin = new System.Windows.Forms.Padding(0);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(21, 21);
            this.btnChat.TabIndex = 35;
            this.btnChat.Text = "→";
            this.btnChat.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnChat.UseVisualStyleBackColor = true;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLog.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(6, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(368, 359);
            this.txtLog.TabIndex = 1;
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(6, 384);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(344, 21);
            this.txtInput.TabIndex = 27;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // txtUrl
            // 
            this.txtUrl.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtUrl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(6, 9);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(380, 21);
            this.txtUrl.TabIndex = 25;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(573, 488);
            this.tabPage2.TabIndex = 6;
            this.tabPage2.Text = "Chat";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox14, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox15, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.groupBox16, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(573, 488);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtAdminOut);
            this.groupBox14.Controls.Add(this.btnAdminChat);
            this.groupBox14.Controls.Add(this.txtAdminIn);
            this.groupBox14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox14.Location = new System.Drawing.Point(3, 3);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(567, 156);
            this.groupBox14.TabIndex = 0;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Admin Chat";
            // 
            // txtAdminOut
            // 
            this.txtAdminOut.Location = new System.Drawing.Point(6, 20);
            this.txtAdminOut.Multiline = true;
            this.txtAdminOut.Name = "txtAdminOut";
            this.txtAdminOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAdminOut.Size = new System.Drawing.Size(556, 103);
            this.txtAdminOut.TabIndex = 38;
            // 
            // btnAdminChat
            // 
            this.btnAdminChat.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminChat.Location = new System.Drawing.Point(541, 129);
            this.btnAdminChat.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdminChat.Name = "btnAdminChat";
            this.btnAdminChat.Size = new System.Drawing.Size(21, 21);
            this.btnAdminChat.TabIndex = 37;
            this.btnAdminChat.Text = "→";
            this.btnAdminChat.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdminChat.UseVisualStyleBackColor = true;
            this.btnAdminChat.Click += new System.EventHandler(this.btnAdminChat_Click);
            // 
            // txtAdminIn
            // 
            this.txtAdminIn.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdminIn.Location = new System.Drawing.Point(6, 129);
            this.txtAdminIn.Name = "txtAdminIn";
            this.txtAdminIn.Size = new System.Drawing.Size(532, 21);
            this.txtAdminIn.TabIndex = 36;
            this.txtAdminIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAdminIn_KeyDown);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.txtOpOut);
            this.groupBox15.Controls.Add(this.btnOpChat);
            this.groupBox15.Controls.Add(this.txtOpIn);
            this.groupBox15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox15.Location = new System.Drawing.Point(3, 165);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(567, 156);
            this.groupBox15.TabIndex = 1;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Op Chat";
            // 
            // txtOpOut
            // 
            this.txtOpOut.Location = new System.Drawing.Point(6, 20);
            this.txtOpOut.Multiline = true;
            this.txtOpOut.Name = "txtOpOut";
            this.txtOpOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOpOut.Size = new System.Drawing.Size(556, 103);
            this.txtOpOut.TabIndex = 41;
            // 
            // btnOpChat
            // 
            this.btnOpChat.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpChat.Location = new System.Drawing.Point(541, 129);
            this.btnOpChat.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpChat.Name = "btnOpChat";
            this.btnOpChat.Size = new System.Drawing.Size(21, 21);
            this.btnOpChat.TabIndex = 40;
            this.btnOpChat.Text = "→";
            this.btnOpChat.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpChat.UseVisualStyleBackColor = true;
            this.btnOpChat.Click += new System.EventHandler(this.btnOpChat_Click);
            // 
            // txtOpIn
            // 
            this.txtOpIn.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpIn.Location = new System.Drawing.Point(6, 129);
            this.txtOpIn.Name = "txtOpIn";
            this.txtOpIn.Size = new System.Drawing.Size(532, 21);
            this.txtOpIn.TabIndex = 39;
            this.txtOpIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpIn_KeyDown);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.txtGlobalOut);
            this.groupBox16.Controls.Add(this.btnGlobalChat);
            this.groupBox16.Controls.Add(this.txtGlobalIn);
            this.groupBox16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox16.Location = new System.Drawing.Point(3, 327);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(567, 158);
            this.groupBox16.TabIndex = 2;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Global Chat";
            // 
            // txtGlobalOut
            // 
            this.txtGlobalOut.Location = new System.Drawing.Point(6, 20);
            this.txtGlobalOut.Multiline = true;
            this.txtGlobalOut.Name = "txtGlobalOut";
            this.txtGlobalOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGlobalOut.Size = new System.Drawing.Size(556, 103);
            this.txtGlobalOut.TabIndex = 44;
            // 
            // btnGlobalChat
            // 
            this.btnGlobalChat.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGlobalChat.Location = new System.Drawing.Point(541, 129);
            this.btnGlobalChat.Margin = new System.Windows.Forms.Padding(0);
            this.btnGlobalChat.Name = "btnGlobalChat";
            this.btnGlobalChat.Size = new System.Drawing.Size(21, 21);
            this.btnGlobalChat.TabIndex = 43;
            this.btnGlobalChat.Text = "→";
            this.btnGlobalChat.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGlobalChat.UseVisualStyleBackColor = true;
            this.btnGlobalChat.Click += new System.EventHandler(this.btnGlobalChat_Click);
            // 
            // txtGlobalIn
            // 
            this.txtGlobalIn.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGlobalIn.Location = new System.Drawing.Point(6, 129);
            this.txtGlobalIn.Name = "txtGlobalIn";
            this.txtGlobalIn.Size = new System.Drawing.Size(532, 21);
            this.txtGlobalIn.TabIndex = 42;
            this.txtGlobalIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGlobalIn_KeyDown);
            // 
            // tpPlayers
            // 
            this.tpPlayers.BackColor = System.Drawing.SystemColors.Control;
            this.tpPlayers.Controls.Add(this.panel1);
            this.tpPlayers.Location = new System.Drawing.Point(4, 22);
            this.tpPlayers.Name = "tpPlayers";
            this.tpPlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlayers.Size = new System.Drawing.Size(573, 488);
            this.tpPlayers.TabIndex = 4;
            this.tpPlayers.Text = "Players";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPlayerCount);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.liClients);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 482);
            this.panel1.TabIndex = 35;
            // 
            // txtPlayerCount
            // 
            this.txtPlayerCount.Location = new System.Drawing.Point(71, 3);
            this.txtPlayerCount.Name = "txtPlayerCount";
            this.txtPlayerCount.ReadOnly = true;
            this.txtPlayerCount.Size = new System.Drawing.Size(53, 21);
            this.txtPlayerCount.TabIndex = 37;
            this.txtPlayerCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Total/Guests:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(130, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(434, 472);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // groupBox4
            // 
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(428, 230);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 239);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(428, 230);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            // 
            // liClients
            // 
            this.liClients.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liClients.FormattingEnabled = true;
            this.liClients.Location = new System.Drawing.Point(4, 29);
            this.liClients.Name = "liClients";
            this.liClients.ScrollAlwaysVisible = true;
            this.liClients.Size = new System.Drawing.Size(120, 446);
            this.liClients.TabIndex = 34;
            this.liClients.SelectedIndexChanged += new System.EventHandler(this.liClients_SelectedIndexChanged);
            // 
            // tpLevels
            // 
            this.tpLevels.BackColor = System.Drawing.SystemColors.Control;
            this.tpLevels.Controls.Add(this.panel2);
            this.tpLevels.Location = new System.Drawing.Point(4, 22);
            this.tpLevels.Name = "tpLevels";
            this.tpLevels.Padding = new System.Windows.Forms.Padding(3);
            this.tpLevels.Size = new System.Drawing.Size(573, 488);
            this.tpLevels.TabIndex = 5;
            this.tpLevels.Text = "Levels";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gbMapViewer);
            this.panel2.Controls.Add(this.groupBox8);
            this.panel2.Controls.Add(this.groupBox7);
            this.panel2.Controls.Add(this.groupBox6);
            this.panel2.Controls.Add(this.txtLevelPath);
            this.panel2.Controls.Add(this.btnUnloadLevel);
            this.panel2.Controls.Add(this.btnLoadLevel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(567, 482);
            this.panel2.TabIndex = 37;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // gbMapViewer
            // 
            this.gbMapViewer.Controls.Add(this.btnSaveImage);
            this.gbMapViewer.Controls.Add(this.pbMapViewer);
            this.gbMapViewer.Location = new System.Drawing.Point(147, 297);
            this.gbMapViewer.Name = "gbMapViewer";
            this.gbMapViewer.Size = new System.Drawing.Size(274, 182);
            this.gbMapViewer.TabIndex = 44;
            this.gbMapViewer.TabStop = false;
            this.gbMapViewer.Text = "Map Viewer";
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(229, 20);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(39, 23);
            this.btnSaveImage.TabIndex = 1;
            this.btnSaveImage.Text = "Save";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // pbMapViewer
            // 
            this.pbMapViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMapViewer.Location = new System.Drawing.Point(3, 17);
            this.pbMapViewer.Name = "pbMapViewer";
            this.pbMapViewer.Size = new System.Drawing.Size(268, 162);
            this.pbMapViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMapViewer.TabIndex = 0;
            this.pbMapViewer.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnLevelHacks);
            this.groupBox8.Controls.Add(this.label15);
            this.groupBox8.Controls.Add(this.label14);
            this.groupBox8.Controls.Add(this.cmbPerVisit);
            this.groupBox8.Controls.Add(this.cmbPerBuild);
            this.groupBox8.Controls.Add(this.btnRenameLevel);
            this.groupBox8.Controls.Add(this.btnBackupManager);
            this.groupBox8.Controls.Add(this.btnDeleteLevel);
            this.groupBox8.Controls.Add(this.cmbLevelPhys);
            this.groupBox8.Controls.Add(this.label13);
            this.groupBox8.Controls.Add(this.txtLevelY);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.txtLevelZ);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.txtLevelX);
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Controls.Add(this.txtLevelMotd);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.btnUpdateLevel);
            this.groupBox8.Location = new System.Drawing.Point(146, 32);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(275, 259);
            this.groupBox8.TabIndex = 43;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Level Settings";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 71);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 19;
            this.label15.Text = "Visit:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(140, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Build:";
            // 
            // cmbPerVisit
            // 
            this.cmbPerVisit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPerVisit.FormattingEnabled = true;
            this.cmbPerVisit.Location = new System.Drawing.Point(43, 68);
            this.cmbPerVisit.Name = "cmbPerVisit";
            this.cmbPerVisit.Size = new System.Drawing.Size(91, 21);
            this.cmbPerVisit.TabIndex = 17;
            // 
            // cmbPerBuild
            // 
            this.cmbPerBuild.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPerBuild.FormattingEnabled = true;
            this.cmbPerBuild.Location = new System.Drawing.Point(178, 68);
            this.cmbPerBuild.Name = "cmbPerBuild";
            this.cmbPerBuild.Size = new System.Drawing.Size(91, 21);
            this.cmbPerBuild.TabIndex = 16;
            // 
            // btnRenameLevel
            // 
            this.btnRenameLevel.Location = new System.Drawing.Point(7, 230);
            this.btnRenameLevel.Name = "btnRenameLevel";
            this.btnRenameLevel.Size = new System.Drawing.Size(128, 23);
            this.btnRenameLevel.TabIndex = 15;
            this.btnRenameLevel.Text = "Rename Level";
            this.btnRenameLevel.UseVisualStyleBackColor = true;
            this.btnRenameLevel.Click += new System.EventHandler(this.btnRenameLevel_Click);
            // 
            // btnBackupManager
            // 
            this.btnBackupManager.Location = new System.Drawing.Point(7, 201);
            this.btnBackupManager.Name = "btnBackupManager";
            this.btnBackupManager.Size = new System.Drawing.Size(93, 23);
            this.btnBackupManager.TabIndex = 13;
            this.btnBackupManager.Text = "Backup Manager";
            this.btnBackupManager.UseVisualStyleBackColor = true;
            this.btnBackupManager.Click += new System.EventHandler(this.btnBackupManager_Click);
            // 
            // btnDeleteLevel
            // 
            this.btnDeleteLevel.Location = new System.Drawing.Point(144, 230);
            this.btnDeleteLevel.Name = "btnDeleteLevel";
            this.btnDeleteLevel.Size = new System.Drawing.Size(125, 23);
            this.btnDeleteLevel.TabIndex = 12;
            this.btnDeleteLevel.Text = "Delete Level";
            this.btnDeleteLevel.UseVisualStyleBackColor = true;
            this.btnDeleteLevel.Click += new System.EventHandler(this.btnDeleteLevel_Click);
            // 
            // cmbLevelPhys
            // 
            this.cmbLevelPhys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevelPhys.FormattingEnabled = true;
            this.cmbLevelPhys.Items.AddRange(new object[] {
            "0 - OFF",
            "1 - Normal",
            "2 - Advanced",
            "3 - Hardcore",
            "4 - Instant",
            "5 - Doors-Only"});
            this.cmbLevelPhys.Location = new System.Drawing.Point(178, 41);
            this.cmbLevelPhys.Name = "cmbLevelPhys";
            this.cmbLevelPhys.Size = new System.Drawing.Size(91, 21);
            this.cmbLevelPhys.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(134, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Physics:";
            // 
            // txtLevelY
            // 
            this.txtLevelY.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLevelY.Location = new System.Drawing.Point(61, 41);
            this.txtLevelY.Name = "txtLevelY";
            this.txtLevelY.ReadOnly = true;
            this.txtLevelY.Size = new System.Drawing.Size(28, 21);
            this.txtLevelY.TabIndex = 9;
            this.txtLevelY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(50, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Y:";
            // 
            // txtLevelZ
            // 
            this.txtLevelZ.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLevelZ.Location = new System.Drawing.Point(107, 41);
            this.txtLevelZ.Name = "txtLevelZ";
            this.txtLevelZ.ReadOnly = true;
            this.txtLevelZ.Size = new System.Drawing.Size(28, 21);
            this.txtLevelZ.TabIndex = 7;
            this.txtLevelZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(96, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Z:";
            // 
            // txtLevelX
            // 
            this.txtLevelX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLevelX.Location = new System.Drawing.Point(16, 41);
            this.txtLevelX.Name = "txtLevelX";
            this.txtLevelX.ReadOnly = true;
            this.txtLevelX.Size = new System.Drawing.Size(28, 21);
            this.txtLevelX.TabIndex = 5;
            this.txtLevelX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "X:";
            // 
            // txtLevelMotd
            // 
            this.txtLevelMotd.Location = new System.Drawing.Point(50, 14);
            this.txtLevelMotd.Name = "txtLevelMotd";
            this.txtLevelMotd.Size = new System.Drawing.Size(219, 21);
            this.txtLevelMotd.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "MOTD:";
            // 
            // btnUpdateLevel
            // 
            this.btnUpdateLevel.Location = new System.Drawing.Point(181, 201);
            this.btnUpdateLevel.Name = "btnUpdateLevel";
            this.btnUpdateLevel.Size = new System.Drawing.Size(88, 23);
            this.btnUpdateLevel.TabIndex = 0;
            this.btnUpdateLevel.Text = "Update Settings";
            this.btnUpdateLevel.UseVisualStyleBackColor = true;
            this.btnUpdateLevel.Click += new System.EventHandler(this.btnUpdateLevel_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.liUnloaded);
            this.groupBox7.Location = new System.Drawing.Point(3, 32);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(137, 447);
            this.groupBox7.TabIndex = 42;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Unloaded";
            // 
            // liUnloaded
            // 
            this.liUnloaded.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liUnloaded.FormattingEnabled = true;
            this.liUnloaded.Location = new System.Drawing.Point(6, 20);
            this.liUnloaded.Name = "liUnloaded";
            this.liUnloaded.ScrollAlwaysVisible = true;
            this.liUnloaded.Size = new System.Drawing.Size(125, 420);
            this.liUnloaded.TabIndex = 37;
            this.liUnloaded.SelectedIndexChanged += new System.EventHandler(this.liUnloaded_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.liMaps);
            this.groupBox6.Location = new System.Drawing.Point(427, 32);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(137, 447);
            this.groupBox6.TabIndex = 41;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Loaded";
            // 
            // liMaps
            // 
            this.liMaps.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liMaps.FormattingEnabled = true;
            this.liMaps.Location = new System.Drawing.Point(6, 20);
            this.liMaps.Name = "liMaps";
            this.liMaps.ScrollAlwaysVisible = true;
            this.liMaps.Size = new System.Drawing.Size(125, 420);
            this.liMaps.TabIndex = 36;
            this.liMaps.SelectedIndexChanged += new System.EventHandler(this.liMaps_SelectedIndexChanged);
            // 
            // txtLevelPath
            // 
            this.txtLevelPath.Location = new System.Drawing.Point(146, 5);
            this.txtLevelPath.Name = "txtLevelPath";
            this.txtLevelPath.ReadOnly = true;
            this.txtLevelPath.Size = new System.Drawing.Size(275, 21);
            this.txtLevelPath.TabIndex = 40;
            this.txtLevelPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnUnloadLevel
            // 
            this.btnUnloadLevel.Location = new System.Drawing.Point(433, 3);
            this.btnUnloadLevel.Name = "btnUnloadLevel";
            this.btnUnloadLevel.Size = new System.Drawing.Size(125, 23);
            this.btnUnloadLevel.TabIndex = 39;
            this.btnUnloadLevel.Text = "← Unload";
            this.btnUnloadLevel.UseVisualStyleBackColor = true;
            this.btnUnloadLevel.Click += new System.EventHandler(this.btnUnloadLevel_Click);
            // 
            // btnLoadLevel
            // 
            this.btnLoadLevel.Location = new System.Drawing.Point(9, 3);
            this.btnLoadLevel.Name = "btnLoadLevel";
            this.btnLoadLevel.Size = new System.Drawing.Size(125, 23);
            this.btnLoadLevel.TabIndex = 38;
            this.btnLoadLevel.Text = "Load →";
            this.btnLoadLevel.UseVisualStyleBackColor = true;
            this.btnLoadLevel.Click += new System.EventHandler(this.btnLoadLevel_Click);
            // 
            // tpChangelog
            // 
            this.tpChangelog.BackColor = System.Drawing.Color.Transparent;
            this.tpChangelog.Controls.Add(this.txtLatestVersion);
            this.tpChangelog.Controls.Add(this.label2);
            this.tpChangelog.Controls.Add(this.txtCurrentVersion);
            this.tpChangelog.Controls.Add(this.label3);
            this.tpChangelog.Controls.Add(this.btnChangelog);
            this.tpChangelog.Controls.Add(this.txtChangelog);
            this.tpChangelog.Location = new System.Drawing.Point(4, 22);
            this.tpChangelog.Name = "tpChangelog";
            this.tpChangelog.Padding = new System.Windows.Forms.Padding(3);
            this.tpChangelog.Size = new System.Drawing.Size(573, 488);
            this.tpChangelog.TabIndex = 1;
            this.tpChangelog.Text = "Changelog";
            // 
            // txtLatestVersion
            // 
            this.txtLatestVersion.BackColor = System.Drawing.SystemColors.Window;
            this.txtLatestVersion.ForeColor = System.Drawing.Color.Green;
            this.txtLatestVersion.Location = new System.Drawing.Point(287, 8);
            this.txtLatestVersion.Name = "txtLatestVersion";
            this.txtLatestVersion.ReadOnly = true;
            this.txtLatestVersion.Size = new System.Drawing.Size(94, 21);
            this.txtLatestVersion.TabIndex = 6;
            this.txtLatestVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Latest Version:";
            // 
            // txtCurrentVersion
            // 
            this.txtCurrentVersion.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrentVersion.Location = new System.Drawing.Point(96, 8);
            this.txtCurrentVersion.Name = "txtCurrentVersion";
            this.txtCurrentVersion.ReadOnly = true;
            this.txtCurrentVersion.Size = new System.Drawing.Size(94, 21);
            this.txtCurrentVersion.TabIndex = 4;
            this.txtCurrentVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Current Version:";
            // 
            // btnChangelog
            // 
            this.btnChangelog.Location = new System.Drawing.Point(492, 6);
            this.btnChangelog.Name = "btnChangelog";
            this.btnChangelog.Size = new System.Drawing.Size(75, 23);
            this.btnChangelog.TabIndex = 1;
            this.btnChangelog.Text = "Reload";
            this.btnChangelog.UseVisualStyleBackColor = true;
            this.btnChangelog.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtChangelog
            // 
            this.txtChangelog.BackColor = System.Drawing.Color.White;
            this.txtChangelog.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtChangelog.Location = new System.Drawing.Point(7, 35);
            this.txtChangelog.Multiline = true;
            this.txtChangelog.Name = "txtChangelog";
            this.txtChangelog.ReadOnly = true;
            this.txtChangelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChangelog.Size = new System.Drawing.Size(557, 442);
            this.txtChangelog.TabIndex = 0;
            // 
            // tpLogs
            // 
            this.tpLogs.BackColor = System.Drawing.Color.Transparent;
            this.tpLogs.Controls.Add(this.tableLayoutPanel2);
            this.tpLogs.Location = new System.Drawing.Point(4, 22);
            this.tpLogs.Name = "tpLogs";
            this.tpLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tpLogs.Size = new System.Drawing.Size(573, 488);
            this.tpLogs.TabIndex = 3;
            this.tpLogs.Text = "Logs";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gbErrors, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(567, 482);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSystem);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(561, 235);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System";
            // 
            // txtSystem
            // 
            this.txtSystem.BackColor = System.Drawing.Color.White;
            this.txtSystem.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSystem.Location = new System.Drawing.Point(3, 17);
            this.txtSystem.Multiline = true;
            this.txtSystem.Name = "txtSystem";
            this.txtSystem.ReadOnly = true;
            this.txtSystem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSystem.Size = new System.Drawing.Size(555, 215);
            this.txtSystem.TabIndex = 1;
            // 
            // gbErrors
            // 
            this.gbErrors.Controls.Add(this.txtErrors);
            this.gbErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbErrors.Location = new System.Drawing.Point(3, 244);
            this.gbErrors.Name = "gbErrors";
            this.gbErrors.Size = new System.Drawing.Size(561, 235);
            this.gbErrors.TabIndex = 4;
            this.gbErrors.TabStop = false;
            this.gbErrors.Text = "Errors";
            // 
            // txtErrors
            // 
            this.txtErrors.BackColor = System.Drawing.Color.White;
            this.txtErrors.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrors.Location = new System.Drawing.Point(3, 17);
            this.txtErrors.Multiline = true;
            this.txtErrors.Name = "txtErrors";
            this.txtErrors.ReadOnly = true;
            this.txtErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrors.Size = new System.Drawing.Size(555, 215);
            this.txtErrors.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.lblUrl);
            this.tabPage3.Controls.Add(this.groupBox13);
            this.tabPage3.Controls.Add(this.groupBox12);
            this.tabPage3.Controls.Add(this.groupBox11);
            this.tabPage3.Controls.Add(this.groupBox10);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(573, 488);
            this.tabPage3.TabIndex = 7;
            this.tabPage3.Text = "Info";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblUrl.Location = new System.Drawing.Point(7, 445);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(31, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "(URL)";
            this.lblUrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label20);
            this.groupBox13.Controls.Add(this.linkLabel9);
            this.groupBox13.Controls.Add(this.linkLabel5);
            this.groupBox13.Controls.Add(this.linkLabel4);
            this.groupBox13.Controls.Add(this.linkLabel10);
            this.groupBox13.Controls.Add(this.label8);
            this.groupBox13.Controls.Add(this.linkLabel6);
            this.groupBox13.Controls.Add(this.label7);
            this.groupBox13.Controls.Add(this.label6);
            this.groupBox13.Controls.Add(this.linkLabel1);
            this.groupBox13.Controls.Add(this.label5);
            this.groupBox13.Location = new System.Drawing.Point(7, 452);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(556, 33);
            this.groupBox13.TabIndex = 3;
            this.groupBox13.TabStop = false;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AccessibleDescription = "http://www.classicube.net/";
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(419, 14);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(59, 13);
            this.linkLabel4.TabIndex = 0;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "ClassiCube";
            this.linkLabel4.Click += new System.EventHandler(this.linkLabels_Click);
            this.linkLabel4.MouseEnter += new System.EventHandler(this.linkLabels_MouseEnter);
            this.linkLabel4.MouseLeave += new System.EventHandler(this.linkLabels_MouseLeave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(401, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "|";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(295, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "|";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "|";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AccessibleDescription = "http://mcsong.x10.mx";
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(6, 14);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(86, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "MCSong Website";
            this.linkLabel1.Click += new System.EventHandler(this.linkLabels_Click);
            this.linkLabel1.MouseEnter += new System.EventHandler(this.linkLabels_MouseEnter);
            this.linkLabel1.MouseLeave += new System.EventHandler(this.linkLabels_MouseLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(98, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "|";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.textBox1);
            this.groupBox12.Location = new System.Drawing.Point(368, 197);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(195, 249);
            this.groupBox12.TabIndex = 2;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "About MCSong";
            // 
            // linkLabel10
            // 
            this.linkLabel10.AccessibleDescription = "http://mcsong.x10.mx/remote/";
            this.linkLabel10.AutoSize = true;
            this.linkLabel10.Location = new System.Drawing.Point(116, 14);
            this.linkLabel10.Name = "linkLabel10";
            this.linkLabel10.Size = new System.Drawing.Size(84, 13);
            this.linkLabel10.TabIndex = 5;
            this.linkLabel10.TabStop = true;
            this.linkLabel10.Text = "Remote Console";
            this.linkLabel10.Click += new System.EventHandler(this.linkLabels_Click);
            this.linkLabel10.MouseEnter += new System.EventHandler(this.linkLabels_MouseEnter);
            this.linkLabel10.MouseLeave += new System.EventHandler(this.linkLabels_MouseLeave);
            // 
            // linkLabel9
            // 
            this.linkLabel9.AccessibleDescription = "https://github.com/727021/MCSong";
            this.linkLabel9.AutoSize = true;
            this.linkLabel9.Location = new System.Drawing.Point(224, 14);
            this.linkLabel9.Name = "linkLabel9";
            this.linkLabel9.Size = new System.Drawing.Size(65, 13);
            this.linkLabel9.TabIndex = 4;
            this.linkLabel9.TabStop = true;
            this.linkLabel9.Text = "Source Code";
            this.linkLabel9.Click += new System.EventHandler(this.linkLabels_Click);
            this.linkLabel9.MouseEnter += new System.EventHandler(this.linkLabels_MouseEnter);
            this.linkLabel9.MouseLeave += new System.EventHandler(this.linkLabels_MouseLeave);
            // 
            // linkLabel6
            // 
            this.linkLabel6.AccessibleDescription = "http://portforward.com/english/routers/port_forwarding/routerindex.htm";
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Location = new System.Drawing.Point(313, 14);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(82, 13);
            this.linkLabel6.TabIndex = 1;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "Port Forwarding";
            this.linkLabel6.Click += new System.EventHandler(this.linkLabels_Click);
            this.linkLabel6.MouseEnter += new System.EventHandler(this.linkLabels_MouseEnter);
            this.linkLabel6.MouseLeave += new System.EventHandler(this.linkLabels_MouseLeave);
            // 
            // linkLabel5
            // 
            this.linkLabel5.AccessibleDescription = "http://dev.mysql.com/downloads/windows/installer/";
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(502, 14);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(38, 13);
            this.linkLabel5.TabIndex = 0;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "MySQL";
            this.linkLabel5.Click += new System.EventHandler(this.linkLabels_Click);
            this.linkLabel5.MouseEnter += new System.EventHandler(this.linkLabels_MouseEnter);
            this.linkLabel5.MouseLeave += new System.EventHandler(this.linkLabels_MouseLeave);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.txtDevList);
            this.groupBox11.Location = new System.Drawing.Point(368, 3);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(195, 191);
            this.groupBox11.TabIndex = 1;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "MCSong Development Team";
            // 
            // txtDevList
            // 
            this.txtDevList.BackColor = System.Drawing.SystemColors.Window;
            this.txtDevList.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDevList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDevList.Location = new System.Drawing.Point(3, 17);
            this.txtDevList.Multiline = true;
            this.txtDevList.Name = "txtDevList";
            this.txtDevList.ReadOnly = true;
            this.txtDevList.Size = new System.Drawing.Size(189, 171);
            this.txtDevList.TabIndex = 0;
            this.txtDevList.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDevList.GotFocus += new System.EventHandler(this.txtDevList_Focus);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.lblStartingCounters);
            this.groupBox10.Controls.Add(this.button3);
            this.groupBox10.Controls.Add(this.groupBox17);
            this.groupBox10.Controls.Add(this.txtThreads);
            this.groupBox10.Controls.Add(this.txtMemory);
            this.groupBox10.Controls.Add(this.txtCpu);
            this.groupBox10.Controls.Add(this.txtUptime);
            this.groupBox10.Controls.Add(this.label19);
            this.groupBox10.Controls.Add(this.label18);
            this.groupBox10.Controls.Add(this.label17);
            this.groupBox10.Controls.Add(this.label16);
            this.groupBox10.Location = new System.Drawing.Point(7, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(355, 443);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Server Statistics";
            // 
            // groupBox17
            // 
            this.groupBox17.Location = new System.Drawing.Point(6, 160);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(346, 10);
            this.groupBox17.TabIndex = 8;
            this.groupBox17.TabStop = false;
            // 
            // txtThreads
            // 
            this.txtThreads.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtThreads.Location = new System.Drawing.Point(285, 127);
            this.txtThreads.Name = "txtThreads";
            this.txtThreads.ReadOnly = true;
            this.txtThreads.Size = new System.Drawing.Size(56, 27);
            this.txtThreads.TabIndex = 7;
            // 
            // txtMemory
            // 
            this.txtMemory.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtMemory.Location = new System.Drawing.Point(123, 127);
            this.txtMemory.Name = "txtMemory";
            this.txtMemory.ReadOnly = true;
            this.txtMemory.Size = new System.Drawing.Size(85, 27);
            this.txtMemory.TabIndex = 6;
            // 
            // txtCpu
            // 
            this.txtCpu.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtCpu.Location = new System.Drawing.Point(10, 94);
            this.txtCpu.Name = "txtCpu";
            this.txtCpu.ReadOnly = true;
            this.txtCpu.Size = new System.Drawing.Size(331, 27);
            this.txtCpu.TabIndex = 5;
            // 
            // txtUptime
            // 
            this.txtUptime.Font = new System.Drawing.Font("Calibri", 12F);
            this.txtUptime.Location = new System.Drawing.Point(10, 42);
            this.txtUptime.Name = "txtUptime";
            this.txtUptime.ReadOnly = true;
            this.txtUptime.Size = new System.Drawing.Size(331, 27);
            this.txtUptime.TabIndex = 4;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Calibri", 12F);
            this.label19.Location = new System.Drawing.Point(214, 130);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 19);
            this.label19.TabIndex = 3;
            this.label19.Text = "Threads:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Calibri", 12F);
            this.label18.Location = new System.Drawing.Point(6, 72);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(191, 19);
            this.label18.TabIndex = 2;
            this.label18.Text = "CPU Usage (MCSong/Total):";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 12F);
            this.label17.Location = new System.Drawing.Point(6, 130);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(111, 19);
            this.label17.TabIndex = 1;
            this.label17.Text = "Memory Usage:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 12F);
            this.label16.Location = new System.Drawing.Point(6, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 19);
            this.label16.TabIndex = 0;
            this.label16.Text = "Uptime:";
            // 
            // tmrRestart
            // 
            this.tmrRestart.Enabled = true;
            this.tmrRestart.Interval = 1000;
            // 
            // iconContext
            // 
            this.iconContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConsole,
            this.shutdownServer});
            this.iconContext.Name = "iconContext";
            this.iconContext.Size = new System.Drawing.Size(164, 48);
            // 
            // openConsole
            // 
            this.openConsole.Name = "openConsole";
            this.openConsole.Size = new System.Drawing.Size(163, 22);
            this.openConsole.Text = "Open Console";
            this.openConsole.Click += new System.EventHandler(this.openConsole_Click);
            // 
            // shutdownServer
            // 
            this.shutdownServer.Name = "shutdownServer";
            this.shutdownServer.Size = new System.Drawing.Size(163, 22);
            this.shutdownServer.Text = "Shutdown Server";
            this.shutdownServer.Click += new System.EventHandler(this.shutdownServer_Click);
            // 
            // btnLevelHacks
            // 
            this.btnLevelHacks.Location = new System.Drawing.Point(104, 201);
            this.btnLevelHacks.Name = "btnLevelHacks";
            this.btnLevelHacks.Size = new System.Drawing.Size(75, 23);
            this.btnLevelHacks.TabIndex = 20;
            this.btnLevelHacks.Text = "Hack Control";
            this.btnLevelHacks.UseVisualStyleBackColor = true;
            this.btnLevelHacks.Click += new System.EventHandler(this.btnLevelHacks_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(204, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "START PROCESS COUNTERS";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblStartingCounters
            // 
            this.lblStartingCounters.AutoSize = true;
            this.lblStartingCounters.Location = new System.Drawing.Point(190, 18);
            this.lblStartingCounters.Name = "lblStartingCounters";
            this.lblStartingCounters.Size = new System.Drawing.Size(151, 13);
            this.lblStartingCounters.TabIndex = 10;
            this.lblStartingCounters.Text = "STARTING PROCESS COUNTERS...";
            this.lblStartingCounters.Visible = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(484, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(12, 13);
            this.label20.TabIndex = 8;
            this.label20.Text = "|";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 17);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(189, 229);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 523);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            this.Load += new System.EventHandler(this.Window_Load);
            this.Resize += new System.EventHandler(this.Window_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gBCommands.ResumeLayout(false);
            this.gBCommands.PerformLayout();
            this.gBChat.ResumeLayout(false);
            this.gBChat.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.tpPlayers.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tpLevels.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbMapViewer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMapViewer)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tpChangelog.ResumeLayout(false);
            this.tpChangelog.PerformLayout();
            this.tpLogs.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbErrors.ResumeLayout(false);
            this.gbErrors.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.iconContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button button1;
        private GroupBox gBCommands;
        private TextBox txtCommandsUsed;
        private GroupBox gBChat;
        internal TextBox txtLog;
        private TextBox txtCommands;
        private TextBox txtInput;
        private TextBox txtUrl;
        private TabPage tpChangelog;
        private TextBox txtChangelog;
        private Timer tmrRestart;
        private Button btnProperties;
        private Button btnClose;
        private ContextMenuStrip iconContext;
        private ToolStripMenuItem openConsole;
        private ToolStripMenuItem shutdownServer;
        private TabPage tpLogs;
        private TextBox txtHost;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox groupBox1;
        private TextBox txtSystem;
        private GroupBox gbErrors;
        private TextBox txtErrors;
        private Button btnChangelog;
        private TextBox txtCurrentVersion;
        private Label label3;
        private Button btnChat;
        private TabPage tpPlayers;
        private ListBox liClients;
        private Button btnCommand;
        private GroupBox groupBox2;
        private Button btnRestart;
        private Label label1;
        internal CheckBox chkMaintenance;
        private Label lblCopyright;
        private GroupBox groupBox3;
        private Panel panel1;
        private TabPage tpLevels;
        private Panel panel2;
        private ListBox liMaps;
        private TextBox txtPlayerCount;
        private Label label4;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnLoadLevel;
        private ListBox liUnloaded;
        private TextBox txtLevelPath;
        private Button btnUnloadLevel;
        private GroupBox groupBox8;
        private GroupBox groupBox7;
        private GroupBox groupBox6;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox9;
        private Button button2;
        private Button button4;
        private Button button5;
        private Button button6;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Button btnPlay;
        private GroupBox groupBox13;
        private LinkLabel linkLabel4;
        private Label label8;
        private Label label7;
        private Label label6;
        private LinkLabel linkLabel1;
        private Label label5;
        private GroupBox groupBox12;
        private LinkLabel linkLabel9;
        private LinkLabel linkLabel6;
        private LinkLabel linkLabel5;
        private GroupBox groupBox11;
        private GroupBox groupBox10;
        private TextBox txtDevList;
        private LinkLabel linkLabel10;
        private TableLayoutPanel tableLayoutPanel3;
        private GroupBox groupBox14;
        private TextBox txtAdminOut;
        private Button btnAdminChat;
        private TextBox txtAdminIn;
        private GroupBox groupBox15;
        private TextBox txtOpOut;
        private Button btnOpChat;
        private TextBox txtOpIn;
        private GroupBox groupBox16;
        private TextBox txtGlobalOut;
        private Button btnGlobalChat;
        private TextBox txtGlobalIn;
        private TextBox txtLatestVersion;
        private Label label2;
        private Button btnUpdateLevel;
        private TextBox txtLevelMotd;
        private Label label9;
        private ComboBox cmbLevelPhys;
        private Label label13;
        private TextBox txtLevelY;
        private Label label12;
        private TextBox txtLevelZ;
        private Label label11;
        private TextBox txtLevelX;
        private Label label10;
        private GroupBox gbMapViewer;
        private PictureBox pbMapViewer;
        private Button btnBackupManager;
        private Button btnDeleteLevel;
        private Button btnRenameLevel;
        private Label lblUrl;
        private Label label14;
        private ComboBox cmbPerVisit;
        private ComboBox cmbPerBuild;
        private Label label15;
        private Button btnSaveImage;
        private GroupBox groupBox17;
        private TextBox txtThreads;
        private TextBox txtMemory;
        private TextBox txtCpu;
        private TextBox txtUptime;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label16;
        private Button btnLevelHacks;
        private Button button3;
        private Label lblStartingCounters;
        private Label label20;
        private TextBox textBox1;
    }
}