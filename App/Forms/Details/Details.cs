using PassGenCSharp.App.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGenCSharp
{
    public partial class Details : Form
    {
        Model model;
        public Details() {
            InitializeComponent();
        }

        public Details(Model model) {
            InitializeComponent();
            this.model = model;
            this.Text = model.Platform;
        }
        private void Details_Load(object sender, EventArgs e) {
            textBox1.Text = model.Platform;
            textBox2.Text = model.Email;
            textBox3.Text = model.Nickname;
            textBox4.Text = model.Password;
            richTextBox1.Text = model.Description;
        }

        private void button5_Click(object sender, EventArgs e) {
            this.Close();
        }

        int x;
        int y;
        bool firstClick = false;
        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                firstClick = true;
                timer1.Enabled = true;
            } 
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (firstClick) {
                x = Cursor.Position.X;
                y = Cursor.Position.Y;
                firstClick = false;            
            }

            int deltax = x - Cursor.Position.X;
            int deltay = y - Cursor.Position.Y;

            this.Location = new Point(this.Location.X - deltax, this.Location.Y - deltay);

            x = Cursor.Position.X;
            y = Cursor.Position.Y;
        }
    }
}
