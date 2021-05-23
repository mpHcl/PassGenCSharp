using PassGenCSharp.App.Generator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGenCSharp {
    public partial class SettingsForm : Form {
        Settings settings;
        public SettingsForm() {
            InitializeComponent();
        }

        public SettingsForm(Settings settings) {
            InitializeComponent();
            this.settings = settings;
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            label3.Text = "" + trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e) {
            settings.Length = trackBar1.Value;
            settings.Type = comboBox1.SelectedIndex;
            this.Close();
        }
    }
}
