using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;

namespace MCSong.Gui
{
    public partial class UpdateWindow : Form
    {
        public UpdateWindow()
        {
            InitializeComponent();
        }
        private void UpdateWindow_Load(object sender, EventArgs e)
        {
            Updater.Load("properties/updater.properties");
            chkAutoUpdateRO.Checked = Server.autoupdate;
            chkNotifyRO.Checked = Server.autonotify;
            txtCountdownRO.Text = Server.restartcountdown;

            try
            {
                WebClient client = new WebClient();
                client.DownloadFile("http://updates.mcsong.x10.mx/versions.txt", "text/revs.txt");
                listRevisions.Items.Clear();
                FileInfo file = new FileInfo("text/revs.txt");
                StreamReader stRead = file.OpenText();
                if (File.Exists("text/revs.txt"))
                {
                    while (!stRead.EndOfStream)
                    {
                        listRevisions.Items.Add(stRead.ReadLine());
                    }
                }
                stRead.Close();
                stRead.Dispose();
                file.Delete();
                client.Dispose();
            }
            catch
            {
                MessageBox.Show("Could not load versions", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            try
            {
                txtCurrentVersion.Text = Server.Version;
                txtLatestVersion.Text = new WebClient().DownloadString("http://updates.mcsong.x10.mx/curversion.txt");
                txtCurrentVersion.BackColor = (txtCurrentVersion.Text == txtLatestVersion.Text) ? Color.Green : Color.Red;
                txtCurrentVersion.Update();
            }
            catch { }
        }

        private void UpdateWindow_Unload(object sender, EventArgs e)
        {
            Window.updLoaded = false;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            if (Server.selectedrevision != "")
            {
                MCSong_.Gui.Program.PerformUpdate(true);
                
            }
            else { MCSong_.Gui.Program.PerformUpdate(false); }
      /*      if (!Program.CurrentUpdate)
                Program.UpdateCheck();
            else
            {
                Thread messageThread = new Thread(new ThreadStart(delegate
                {
                    MessageBox.Show("Already checking for updates.");
                })); messageThread.Start();
            } */
        }


        private void listRevisions_SelectedValueChanged(object sender, EventArgs e)
        {
            Server.selectedrevision = listRevisions.SelectedItem.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            new UpdatePropertiesDialog().ShowDialog();
            chkAutoUpdateRO.Checked = Server.autoupdate;
            chkNotifyRO.Checked = Server.autonotify;
            txtCountdownRO.Text = Server.restartcountdown;
        }
    }
}
