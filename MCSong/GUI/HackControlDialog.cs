using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCSong.Gui
{
    public partial class HackControlDialog : Form
    {
        public HackControlDialog(Level level)
        {
            InitializeComponent();
            l = level;
        }
        Level l;

        private void HackControl_Load(object sender, EventArgs e)
        {
            chkFlying.Checked = l.hacks.Flying;
            chkNoClip.Checked = l.hacks.NoClip;
            chkSpeeding.Checked = l.hacks.Speeding;
            chkSpawnControl.Checked = l.hacks.SpawnControl;
            chkThirdPerson.Checked = l.hacks.ThirdPerson;
            numJumpHeight.Value = (l.hacks.JumpHeight >= 0) ? (decimal)(Math.Round((decimal)l.hacks.JumpHeight / 16, MidpointRounding.AwayFromZero) / 2) : (decimal)-0.5;
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            l.hacks.Flying = chkFlying.Checked;
            l.hacks.NoClip = chkNoClip.Checked;
            l.hacks.Speeding = chkSpeeding.Checked;
            l.hacks.SpawnControl = chkSpawnControl.Checked;
            l.hacks.ThirdPerson = chkThirdPerson.Checked;
            l.hacks.JumpHeight = (numJumpHeight.Value >= 0) ? (short)(numJumpHeight.Value * 32) : (short)-1;

            this.DialogResult = DialogResult.OK;
        }

        private void btnDefaults_Click(object sender, EventArgs e)
        {
            chkFlying.Checked = chkNoClip.Checked = chkSpeeding.Checked = chkSpawnControl.Checked = chkThirdPerson.Checked = true;
            numJumpHeight.Value = (decimal)-0.5;
        }
    }
}
