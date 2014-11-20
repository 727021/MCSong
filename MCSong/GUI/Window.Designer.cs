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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnProperties = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gBCommands = new System.Windows.Forms.GroupBox();
            this.btnCommand = new System.Windows.Forms.Button();
            this.txtCommandsUsed = new System.Windows.Forms.TextBox();
            this.txtCommands = new System.Windows.Forms.TextBox();
            this.gBChat = new System.Windows.Forms.GroupBox();
            this.btnChat = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.tpLogs = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSystem = new System.Windows.Forms.TextBox();
            this.gbErrors = new System.Windows.Forms.GroupBox();
            this.txtErrors = new System.Windows.Forms.TextBox();
            this.btnChangelog = new System.Windows.Forms.TabPage();
            this.txtCurrentVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtChangelog = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.liMaps = new System.Windows.Forms.ListBox();
            this.mapsStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.physicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finiteModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animalAIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeWaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.growingGrassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.survivalDeathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killerBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rPChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liClients = new System.Windows.Forms.ListBox();
            this.playerStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.whoisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrRestart = new System.Windows.Forms.Timer(this.components);
            this.iconContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownServer = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkMaintenance = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gBCommands.SuspendLayout();
            this.gBChat.SuspendLayout();
            this.tpLogs.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbErrors.SuspendLayout();
            this.btnChangelog.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.mapsStrip.SuspendLayout();
            this.playerStrip.SuspendLayout();
            this.iconContext.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tpLogs);
            this.tabControl1.Controls.Add(this.btnChangelog);
            this.tabControl1.Controls.Add(this.tabPage2);
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
            this.tabPage1.Controls.Add(this.chkMaintenance);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.groupBox3);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Console State:";
            // 
            // gBCommands
            // 
            this.gBCommands.Controls.Add(this.btnCommand);
            this.gBCommands.Controls.Add(this.txtCommandsUsed);
            this.gBCommands.Controls.Add(this.txtCommands);
            this.gBCommands.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBCommands.Location = new System.Drawing.Point(416, 34);
            this.gBCommands.Name = "gBCommands";
            this.gBCommands.Size = new System.Drawing.Size(151, 408);
            this.gBCommands.TabIndex = 34;
            this.gBCommands.TabStop = false;
            this.gBCommands.Text = "Commands";
            // 
            // btnCommand
            // 
            this.btnCommand.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommand.Location = new System.Drawing.Point(124, 384);
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
            this.txtCommandsUsed.Size = new System.Drawing.Size(137, 359);
            this.txtCommandsUsed.TabIndex = 0;
            // 
            // txtCommands
            // 
            this.txtCommands.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommands.Location = new System.Drawing.Point(8, 384);
            this.txtCommands.Name = "txtCommands";
            this.txtCommands.Size = new System.Drawing.Size(113, 21);
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
            this.gBChat.Size = new System.Drawing.Size(404, 408);
            this.gBChat.TabIndex = 32;
            this.gBChat.TabStop = false;
            this.gBChat.Text = "Chat";
            // 
            // btnChat
            // 
            this.btnChat.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChat.Location = new System.Drawing.Point(376, 384);
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
            this.txtLog.Size = new System.Drawing.Size(391, 359);
            this.txtLog.TabIndex = 1;
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(6, 384);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(365, 21);
            this.txtInput.TabIndex = 27;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
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
            // txtUrl
            // 
            this.txtUrl.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtUrl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(7, 7);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(403, 21);
            this.txtUrl.TabIndex = 25;
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
            // btnChangelog
            // 
            this.btnChangelog.BackColor = System.Drawing.Color.Transparent;
            this.btnChangelog.Controls.Add(this.txtCurrentVersion);
            this.btnChangelog.Controls.Add(this.label3);
            this.btnChangelog.Controls.Add(this.button2);
            this.btnChangelog.Controls.Add(this.txtChangelog);
            this.btnChangelog.Location = new System.Drawing.Point(4, 22);
            this.btnChangelog.Name = "btnChangelog";
            this.btnChangelog.Padding = new System.Windows.Forms.Padding(3);
            this.btnChangelog.Size = new System.Drawing.Size(573, 488);
            this.btnChangelog.TabIndex = 1;
            this.btnChangelog.Text = "Changelog";
            // 
            // txtCurrentVersion
            // 
            this.txtCurrentVersion.Location = new System.Drawing.Point(96, 8);
            this.txtCurrentVersion.Name = "txtCurrentVersion";
            this.txtCurrentVersion.Size = new System.Drawing.Size(94, 21);
            this.txtCurrentVersion.TabIndex = 4;
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(492, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Reload";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.liMaps);
            this.tabPage2.Controls.Add(this.liClients);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(573, 488);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // liMaps
            // 
            this.liMaps.ContextMenuStrip = this.mapsStrip;
            this.liMaps.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liMaps.FormattingEnabled = true;
            this.liMaps.Location = new System.Drawing.Point(226, 254);
            this.liMaps.Name = "liMaps";
            this.liMaps.ScrollAlwaysVisible = true;
            this.liMaps.Size = new System.Drawing.Size(120, 186);
            this.liMaps.TabIndex = 35;
            // 
            // mapsStrip
            // 
            this.mapsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.physicsToolStripMenuItem,
            this.unloadToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.mapsStrip.Name = "mapsStrip";
            this.mapsStrip.Size = new System.Drawing.Size(117, 92);
            // 
            // physicsToolStripMenuItem
            // 
            this.physicsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.physicsToolStripMenuItem.Name = "physicsToolStripMenuItem";
            this.physicsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.physicsToolStripMenuItem.Text = "Physics";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem2.Text = "0";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem3.Text = "1";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem4.Text = "2";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem5.Text = "3";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem6.Text = "4";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // unloadToolStripMenuItem
            // 
            this.unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
            this.unloadToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.unloadToolStripMenuItem.Text = "Unload";
            this.unloadToolStripMenuItem.Click += new System.EventHandler(this.unloadToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finiteModeToolStripMenuItem,
            this.animalAIToolStripMenuItem,
            this.edgeWaterToolStripMenuItem,
            this.growingGrassToolStripMenuItem,
            this.survivalDeathToolStripMenuItem,
            this.killerBlocksToolStripMenuItem,
            this.rPChatToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // finiteModeToolStripMenuItem
            // 
            this.finiteModeToolStripMenuItem.Name = "finiteModeToolStripMenuItem";
            this.finiteModeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.finiteModeToolStripMenuItem.Text = "Finite Mode";
            this.finiteModeToolStripMenuItem.Click += new System.EventHandler(this.finiteModeToolStripMenuItem_Click);
            // 
            // animalAIToolStripMenuItem
            // 
            this.animalAIToolStripMenuItem.Name = "animalAIToolStripMenuItem";
            this.animalAIToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.animalAIToolStripMenuItem.Text = "Animal AI";
            this.animalAIToolStripMenuItem.Click += new System.EventHandler(this.animalAIToolStripMenuItem_Click);
            // 
            // edgeWaterToolStripMenuItem
            // 
            this.edgeWaterToolStripMenuItem.Name = "edgeWaterToolStripMenuItem";
            this.edgeWaterToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.edgeWaterToolStripMenuItem.Text = "Edge Water";
            this.edgeWaterToolStripMenuItem.Click += new System.EventHandler(this.edgeWaterToolStripMenuItem_Click);
            // 
            // growingGrassToolStripMenuItem
            // 
            this.growingGrassToolStripMenuItem.Name = "growingGrassToolStripMenuItem";
            this.growingGrassToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.growingGrassToolStripMenuItem.Text = "Grass Growing";
            this.growingGrassToolStripMenuItem.Click += new System.EventHandler(this.growingGrassToolStripMenuItem_Click);
            // 
            // survivalDeathToolStripMenuItem
            // 
            this.survivalDeathToolStripMenuItem.Name = "survivalDeathToolStripMenuItem";
            this.survivalDeathToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.survivalDeathToolStripMenuItem.Text = "Survival Death";
            this.survivalDeathToolStripMenuItem.Click += new System.EventHandler(this.survivalDeathToolStripMenuItem_Click);
            // 
            // killerBlocksToolStripMenuItem
            // 
            this.killerBlocksToolStripMenuItem.Name = "killerBlocksToolStripMenuItem";
            this.killerBlocksToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.killerBlocksToolStripMenuItem.Text = "Killer Blocks";
            this.killerBlocksToolStripMenuItem.Click += new System.EventHandler(this.killerBlocksToolStripMenuItem_Click);
            // 
            // rPChatToolStripMenuItem
            // 
            this.rPChatToolStripMenuItem.Name = "rPChatToolStripMenuItem";
            this.rPChatToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.rPChatToolStripMenuItem.Text = "RP Chat";
            this.rPChatToolStripMenuItem.Click += new System.EventHandler(this.rPChatToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // liClients
            // 
            this.liClients.ContextMenuStrip = this.playerStrip;
            this.liClients.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liClients.FormattingEnabled = true;
            this.liClients.Location = new System.Drawing.Point(226, 49);
            this.liClients.Name = "liClients";
            this.liClients.ScrollAlwaysVisible = true;
            this.liClients.Size = new System.Drawing.Size(120, 199);
            this.liClients.TabIndex = 34;
            // 
            // playerStrip
            // 
            this.playerStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whoisToolStripMenuItem,
            this.kickToolStripMenuItem,
            this.banToolStripMenuItem,
            this.voiceToolStripMenuItem});
            this.playerStrip.Name = "playerStrip";
            this.playerStrip.Size = new System.Drawing.Size(106, 92);
            // 
            // whoisToolStripMenuItem
            // 
            this.whoisToolStripMenuItem.Name = "whoisToolStripMenuItem";
            this.whoisToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.whoisToolStripMenuItem.Text = "whois";
            this.whoisToolStripMenuItem.Click += new System.EventHandler(this.whoisToolStripMenuItem_Click);
            // 
            // kickToolStripMenuItem
            // 
            this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            this.kickToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.kickToolStripMenuItem.Text = "kick";
            this.kickToolStripMenuItem.Click += new System.EventHandler(this.kickToolStripMenuItem_Click);
            // 
            // banToolStripMenuItem
            // 
            this.banToolStripMenuItem.Name = "banToolStripMenuItem";
            this.banToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.banToolStripMenuItem.Text = "ban";
            this.banToolStripMenuItem.Click += new System.EventHandler(this.banToolStripMenuItem_Click);
            // 
            // voiceToolStripMenuItem
            // 
            this.voiceToolStripMenuItem.Name = "voiceToolStripMenuItem";
            this.voiceToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.voiceToolStripMenuItem.Text = "voice";
            this.voiceToolStripMenuItem.Click += new System.EventHandler(this.voiceToolStripMenuItem_Click);
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
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(517, 464);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "© 2014";
            // 
            // chkMaintenance
            // 
            this.chkMaintenance.AutoSize = true;
            this.chkMaintenance.Location = new System.Drawing.Point(438, 11);
            this.chkMaintenance.Name = "chkMaintenance";
            this.chkMaintenance.Size = new System.Drawing.Size(118, 17);
            this.chkMaintenance.TabIndex = 38;
            this.chkMaintenance.Text = "Maintenance Mode";
            this.chkMaintenance.UseVisualStyleBackColor = true;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 523);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            this.Load += new System.EventHandler(this.Window_Load);
            this.Resize += new System.EventHandler(this.Window_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.gBCommands.ResumeLayout(false);
            this.gBCommands.PerformLayout();
            this.gBChat.ResumeLayout(false);
            this.gBChat.PerformLayout();
            this.tpLogs.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbErrors.ResumeLayout(false);
            this.gbErrors.PerformLayout();
            this.btnChangelog.ResumeLayout(false);
            this.btnChangelog.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.mapsStrip.ResumeLayout(false);
            this.playerStrip.ResumeLayout(false);
            this.iconContext.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button button1;
        private GroupBox gBCommands;
        private TextBox txtCommandsUsed;
        private GroupBox gBChat;
        private TextBox txtLog;
        private TextBox txtCommands;
        private TextBox txtInput;
        private TextBox txtUrl;
        private TabPage btnChangelog;
        private TextBox txtChangelog;
        private Timer tmrRestart;
        private Button btnProperties;
        private Button btnClose;
        private ContextMenuStrip iconContext;
        private ToolStripMenuItem openConsole;
        private ToolStripMenuItem shutdownServer;
        private TabPage tpLogs;
        private TextBox txtHost;
        private ContextMenuStrip playerStrip;
        private ToolStripMenuItem whoisToolStripMenuItem;
        private ToolStripMenuItem kickToolStripMenuItem;
        private ToolStripMenuItem banToolStripMenuItem;
        private ToolStripMenuItem voiceToolStripMenuItem;
        private ContextMenuStrip mapsStrip;
        private ToolStripMenuItem physicsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem unloadToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem finiteModeToolStripMenuItem;
        private ToolStripMenuItem animalAIToolStripMenuItem;
        private ToolStripMenuItem edgeWaterToolStripMenuItem;
        private ToolStripMenuItem growingGrassToolStripMenuItem;
        private ToolStripMenuItem survivalDeathToolStripMenuItem;
        private ToolStripMenuItem killerBlocksToolStripMenuItem;
        private ToolStripMenuItem rPChatToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox groupBox1;
        private TextBox txtSystem;
        private GroupBox gbErrors;
        private TextBox txtErrors;
        private Button button2;
        private TextBox txtCurrentVersion;
        private Label label3;
        private Button btnChat;
        private TabPage tabPage2;
        private ListBox liMaps;
        private ListBox liClients;
        private Button btnCommand;
        private GroupBox groupBox2;
        private Button btnRestart;
        private Label label1;
        private CheckBox chkMaintenance;
        private Label label2;
        private GroupBox groupBox3;
    }
}