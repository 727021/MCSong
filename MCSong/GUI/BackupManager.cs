using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MCSong.Gui
{
    public partial class BackupManager : Form
    {
        public BackupManager()
        {
            InitializeComponent();
        }

        private void BackupManager_Load(object sender, EventArgs e)
        {
            UpdateMapList();
            UpdateInfo();
        }

        private void UpdateMapList()
        {
            liMaps.Items.Clear();
            foreach (FileInfo f in new DirectoryInfo("levels").GetFiles("*.lvl", SearchOption.TopDirectoryOnly))
            {
                liMaps.Items.Add(f.Name.Replace(".lvl", ""));
            }
            UpdateBackupList();
            UpdateInfo();
        }
        private void UpdateBackupList()
        {
            if (String.IsNullOrWhiteSpace(liMaps.Text))
            {
                liBackups.Items.Clear();
                return;
            }
            string l = liMaps.Text;
            liBackups.Items.Clear();
            try
            {
                foreach (DirectoryInfo di in new DirectoryInfo(@Server.backupLocation + "/" + l).GetDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    liBackups.Items.Add(di.Name);
                }
            }
            catch (DirectoryNotFoundException) {  }
        }
        private void UpdateInfo()
        {
            if (!String.IsNullOrWhiteSpace(liMaps.Text))// Is a level selected?
            {
                btnCreate.Enabled = true;
                if (!String.IsNullOrWhiteSpace(liBackups.Text))// Is a backup selected?
                {
                    btnRestore.Enabled = btnDelete.Enabled = true;
                    lblTime.Text = "Backup Time:";
                    txtTime.Text = new FileInfo(@Server.backupLocation + "/" + liMaps.Text + "/" + liBackups.Text + "/" + liMaps.Text + ".lvl").LastWriteTime.ToString();
                    return;
                }
                btnRestore.Enabled = btnDelete.Enabled = false;
                lblTime.Text = "Last Backup:";
                try
                {
                    DirectoryInfo di = new DirectoryInfo(@Server.backupLocation + "/" + liMaps.Text + "/");
                    txtTime.Text = (di.GetDirectories().Length == 0) ? "Never" : di.GetDirectories().OrderByDescending(d => d.LastWriteTime).First().LastWriteTime.ToString();
                }
                catch (DirectoryNotFoundException) { txtTime.Text = "Never"; }
                return;
            }
            btnCreate.Enabled = btnRestore.Enabled = btnDelete.Enabled = false;
            lblTime.Text = "Last Backup:";
            txtTime.Clear();
        }

        private void liMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBackupList();
            UpdateInfo();
        }

        private void liBackups_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string pre = liMaps.Text;
            new CreateBackupDialog(liMaps.Text).ShowDialog();
            UpdateMapList();
            try { liMaps.SetSelected(liMaps.Items.IndexOf(pre), true); }
            catch { }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Restore backup " + liBackups.Text + " for level " + liMaps.Text + "?", "Restore backup?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    File.Copy(@Server.backupLocation + "/" + liMaps.Text + "/" + liBackups.Text + "/" + liMaps.Text + ".lvl", "levels/" + liMaps.Text + ".lvl", true);
                    Level temp = Level.Load(liMaps.Text);
                    temp.physThread.Start();
                    if (temp != null)
                    {
                        Player.players.ForEach(delegate(Player p)
                        {
                            if (p.level.name == liMaps.Text)
                            {
                                p.level.spawnx = temp.spawnx;
                                p.level.spawny = temp.spawny;
                                p.level.spawnz = temp.spawnz;

                                p.level.height = temp.height;
                                p.level.width = temp.width;
                                p.level.depth = temp.depth;

                                p.level.blocks = temp.blocks;
                                p.level.setPhysics(0);
                                p.level.ClearPhysics();
                            }
                        });
                    }
                    Player.GlobalMessage("Restored backup " + liBackups.Text + " for level " + liMaps.Text + ".");
                    MessageBox.Show("Restored backup " + liBackups.Text + " for level " + liMaps.Text + ".");
                }
                catch { MessageBox.Show("Restore fail"); Server.s.Log("Restore fail"); }
            }
            string pre = liMaps.Text;
            string prev = liBackups.Text;
            UpdateMapList();
            try { liMaps.SetSelected(liMaps.Items.IndexOf(pre), true); }
            catch { }
            try { liBackups.SetSelected(liBackups.Items.IndexOf(prev), true); }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this backup? This cannot be undone!", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(@Server.backupLocation + "/" + liMaps.Text + "/" + liBackups.Text);
                    di.Delete(true);
                    MessageBox.Show("Backup deleted.");
                }
                catch (Exception ex) { MessageBox.Show("Error while deleting backup."); Server.ErrorLog(ex); }
            }
            UpdateBackupList();
        }

        private void BackupManager_Resize(object sender, EventArgs e)
        {
            int offset = this.Size.Height - this.MinimumSize.Height;
            liMaps.Height = 147 + offset;
            liBackups.Height = 147 + offset;
        }
    }
}
