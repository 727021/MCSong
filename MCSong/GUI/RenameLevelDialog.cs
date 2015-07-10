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
    public partial class RenameLevelDialog : Form
    {
        public RenameLevelDialog(Level level)
        {
            InitializeComponent();
            l = level;
        }
        private Level l;

        private void RenameLevelDialog_Load(object sender, EventArgs e)
        {
            textBox1.Text = l.name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                Window.thisWindow.newName = textBox1.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(textBox1.Text))
                {
                    Window.thisWindow.newName = textBox1.Text;
                    this.DialogResult = DialogResult.OK;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
