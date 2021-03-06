﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCSong.Gui
{
    public partial class CreateBackupDialog : Form
    {
        public CreateBackupDialog(string level)
        {
            InitializeComponent();
            name = level;
        }

        private string name;

        private void CreateBackupDialog_Load(object sender, EventArgs e)
        {
            DirectoryInfo[] di = new DirectoryInfo(@Server.backupLocation + "/" + name).GetDirectories("*");
            di.OrderBy(d => d.Name);
            string s = "";
            int x = 1;
            for (int i = 0; i < di.Length; i++)
            {
                x = i + 2;
                if ((i + 1).ToString() != di[i].Name)
                {
                    s = (i + 1).ToString();
                    break;
                }
            }
            txtName.Text = (s == "") ? x.ToString() : s;
            txtName.Select(txtName.Text.Length, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Backup name cannot be empty!"); return; }
            retry:
            Level l = Level.Find(name);
            int num = -1;
            if (l != null)
            {
                num = BackupLevel(txtName.Text, true);
            }
            else
            {
                num = BackupLevel(txtName.Text);
            }
            if (num == -1)
            {
                if (MessageBox.Show("Failed to create backup " + txtName.Text + " for level " + name + ".", "", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                {
                    goto retry;
                }
                this.DialogResult = DialogResult.Cancel;
                return;
            }
            MessageBox.Show("Created backup " + txtName.Text + " for level " + name + ".");
            this.DialogResult = DialogResult.OK;
        }

        private int BackupLevel(string backupName, bool loaded = false)
        {
            if (loaded)
            {
                Level l = Level.Find(name);
                l.Save(true);
                return l.Backup(true, backupName);
            }
            else
            {
                int backupNumber = 1; string backupPath = @Server.backupLocation;
                if (Directory.Exists(backupPath + "/" + name))
                {
                    backupNumber = Directory.GetDirectories(backupPath + "/" + name).Length + 1;
                }
                else
                {
                    Directory.CreateDirectory(backupPath + "/" + name);
                }

                string path = backupPath + "/" + name + "/" + backupName;

                Directory.CreateDirectory(path);

                string BackPath = path + "/" + name + ".lvl";
                string current = "levels/" + name + ".lvl";
                try
                {
                    File.Copy(current, BackPath, true);
                    return backupNumber;
                }
                catch (Exception e)
                {
                    Server.ErrorLog(e);
                    Server.s.Log("FAILED TO INCREMENTAL BACKUP :" + name);
                    return -1;
                }
            }
        }
    }
}
