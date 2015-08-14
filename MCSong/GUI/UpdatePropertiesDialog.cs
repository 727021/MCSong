using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCSong
{
    public partial class UpdatePropertiesDialog : Form
    {
        public UpdatePropertiesDialog()
        {
            InitializeComponent();
        }

        private void UpdatePropertiesDialog_Load(object sender, EventArgs e)
        {
            Updater.Load("properties/updater.properties");
            chkAutoupdate.Checked = Server.autoupdate;
            checkBox2.Checked = Server.autonotify;
            txtCountdown.Text = Server.restartcountdown;
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try { UInt32.Parse(txtCountdown.Text.Trim()); }
            catch { goto fail; }
            Server.autoupdate = chkAutoupdate.Checked;
            Server.autonotify = checkBox2.Checked;
            Server.restartcountdown = txtCountdown.Text.Trim();
            Updater.Save("properties/updater.properties");
            this.DialogResult = DialogResult.OK;
            return;
            fail:
            MessageBox.Show("Countdown must be a positive number!");
        }
    }
}
