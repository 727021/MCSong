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
    public partial class EnvColorsDialog : Form
    {
        public Level level;

        //a=sky b=clouds c=fog d=shadows e=sunlight
        Color a = Color.FromArgb(Convert.ToInt32(0x99), Convert.ToInt32(0xCC), Convert.ToInt32(0xFF));
        Color b = Color.White;
        Color c = Color.White;
        Color d = Color.FromArgb(Convert.ToInt32(0x9B), Convert.ToInt32(0x9B), Convert.ToInt32(0x9B));
        Color f = Color.White;

        public EnvColorsDialog(Level l)
        {
            InitializeComponent();
            level = l;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; this.Close();
        }

        private void EnvColorsDialog_Load(object sender, EventArgs e)
        {   
            if (!level.skyColor.Contains<short>(-1))
                a = Color.FromName(String.Format("{0:X}", level.skyColor[0]) + String.Format("{0:X}", level.skyColor[1]) + String.Format("{0:X}", level.skyColor[2]));
            if (!level.cloudColor.Contains<short>(-1))
                b = Color.FromName(String.Format("{0:X}", level.cloudColor[0]) + String.Format("{0:X}", level.cloudColor[1]) + String.Format("{0:X}", level.cloudColor[2]));
            if (!level.fogColor.Contains<short>(-1))
                c = Color.FromName(String.Format("{0:X}", level.fogColor[0]) + String.Format("{0:X}", level.fogColor[1]) + String.Format("{0:X}", level.fogColor[2]));
            if (!level.shadowColor.Contains<short>(-1))
                d = Color.FromName(String.Format("{0:X}", level.shadowColor[0]) + String.Format("{0:X}", level.shadowColor[1]) + String.Format("{0:X}", level.shadowColor[2]));
            if (!level.sunlightColor.Contains<short>(-1))
                f = Color.FromName(String.Format("{0:X}", level.sunlightColor[0]) + String.Format("{0:X}", level.sunlightColor[1]) + String.Format("{0:X}", level.sunlightColor[2]));
            updateColors();
        }
        

        private void button6_Click(object sender, EventArgs e)
        {
            a = Color.FromArgb(Convert.ToInt32(0x99), Convert.ToInt32(0xCC), Convert.ToInt32(0xFF));
            b = Color.White;
            c = Color.White;
            d = Color.FromArgb(Convert.ToInt32(0x9B), Convert.ToInt32(0x9B), Convert.ToInt32(0x9B));
            f = Color.White;
            updateColors();
        }

        private void colorButton(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                b.BackColor = colorDialog1.Color;
                if (b == button1)
                    textBox1.Text = colorDialog1.Color.Name;
                if (b == button2)
                    textBox2.Text = colorDialog1.Color.Name;
                if (b == button3)
                    textBox3.Text = colorDialog1.Color.Name;
                if (b == button4)
                    textBox4.Text = colorDialog1.Color.Name;
                if (b == button5)
                    textBox5.Text = colorDialog1.Color.Name;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            level.skyColor = new short[3] { a.R, a.G, a.B };
            level.cloudColor = new short[3] { b.R, b.G, b.B };
            level.fogColor = new short[3] { c.R, c.G, c.B };
            level.shadowColor = new short[3] { d.R, d.G, d.B };
            level.sunlightColor = new short[3] { f.R, f.G, f.B };
            this.DialogResult = DialogResult.OK; this.Close();
        }

        private void updateColors()
        {
            button1.BackColor = a;
            textBox1.Text = a.Name;
            button2.BackColor = b;
            textBox2.Text = b.Name;
            button3.BackColor = c;
            textBox3.Text = c.Name;
            button4.BackColor = d;
            textBox4.Text = d.Name;
            button5.BackColor = f;
            textBox5.Text = f.Name;
        }
    }
}
