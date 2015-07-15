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
    public partial class NewLevelDialog : Form
    {
        public NewLevelDialog()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string errors = "";
            if (String.IsNullOrWhiteSpace(txtName.Text))
                errors += "The name cannot be blank.\r\n";
            if (errors == "")
               if (File.Exists("levels/" + txtName.Text + ".lvl"))
                errors += "A level with this name already exists.\r\n";
            if (cmbX.SelectedIndex < 0)
                errors += "A width must be selected.\r\n";
            if (cmbZ.SelectedIndex < 0)
                errors += "A height must be selected.\r\n";
            if (cmbY.SelectedIndex < 0)
                errors += "A depth must be selected.\r\n";
            if (cmbType.SelectedIndex < 0)
                errors += "A type must be selected.";
            if (errors != "")
                MessageBox.Show(errors);
            else
            {
                Level l = new Level(txtName.Text, Convert.ToUInt16(cmbX.Items[cmbX.SelectedIndex]), Convert.ToUInt16(cmbY.Items[cmbY.SelectedIndex]), Convert.ToUInt16(cmbZ.Items[cmbZ.SelectedIndex]),cmbType.Items[cmbType.SelectedIndex].ToString());
                l.Save(true, true);
                Server.addLevel(Level.Load(l.name));
                MessageBox.Show(String.Format("Created {0} type level named {1}, with dimensions {2}, {3}, {4}.", new object[] { cmbType.Items[cmbType.SelectedIndex].ToString(), l.name, l.width, l.depth, l.height }));
                this.DialogResult = DialogResult.OK;
            }
        }

        private void NewLevelDialog_Load(object sender, EventArgs e)
        {
            txtName.Text = "NewLevel";
            cmbX.SelectedIndex = cmbZ.SelectedIndex = 3;
            cmbY.SelectedIndex = 2;
            cmbType.SelectedIndex = 0;
            txtName.SelectAll();
        }
    }
}
