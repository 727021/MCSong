using System;
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
            for (int i = 1; i <= di.Length; i++)
            {
                if (i.ToString() != di[i - 1].Name)
                {
                    txtName.Text = i.ToString();
                    txtName.Select(txtName.Text.Length, 1);
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Backup name cannot be empty!"); return; }
            Level l = Level.Find(name);
            if (l != null)
            {
                int num = BackupLevel(txtName.Text, true);
            }
            else
            {
                int num = BackupLevel(txtName.Text);
            }
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

            }
            return -1;
        }
    }
}
