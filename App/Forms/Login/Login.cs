using PassGenCSharp.App.Security;
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

namespace PassGenCSharp {
    public partial class Login : Form {
        TextBox passwordInput;
        TextBox confirmPasswordInput;


        string passwordHash;

        public Login() {
            this.Text = "Login";
            this.Height = 100;
            this.Width = 200; 
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            if (!File.Exists("password.txt")) {

                DeleteExistingKey();
                
                File.Create("password.txt").Close();
                
                passwordInput = new TextBox();              
                confirmPasswordInput  = new TextBox();
                passwordInput.PasswordChar = '*';
                confirmPasswordInput.PasswordChar = '*';

                Button createPasswordButton = new Button();
                createPasswordButton.Text = "Create";
                createPasswordButton.Click += CreatePasswordButton_Click;
                
                flowLayoutPanel.Controls.Add(passwordInput);
                flowLayoutPanel.Controls.Add(confirmPasswordInput);
                flowLayoutPanel.Controls.Add(createPasswordButton);
                
                this.Controls.Add(flowLayoutPanel);
            }
            else {
                passwordHash = GetPasswordHash();
                
                passwordInput = new TextBox();
                passwordInput.PasswordChar = '*';

                Button loginButton = new Button();
                loginButton.Click += LoginButton_Click;
                loginButton.Text = "Login";

                flowLayoutPanel.Controls.Add(passwordInput);
                flowLayoutPanel.Controls.Add(loginButton);
                
                this.Controls.Add(flowLayoutPanel);
               
            }
            
        }

        private void DeleteExistingKey() {
            if (File.Exists("key.txt"))
                File.Delete("key.txt");
            if (File.Exists("IV.txt"))
                File.Delete("IV.txt");
        }

        private string GetPasswordHash() {
            return File.ReadAllText("password.txt");
        }

        private void LoginButton_Click(object sender, EventArgs e) {
            if (StringSHA512.GetStringHash(passwordInput.Text) == passwordHash) {
                this.Visible = false;
                new Home().Show();
            }
            else {
                MessageBox.Show("Password is not correct, try again!");
            }
        }

        private void CreatePasswordButton_Click(object sender, EventArgs e) {
            if (passwordInput.Text == confirmPasswordInput.Text) {
                File.WriteAllText("password.txt", StringSHA512.GetStringHash(passwordInput.Text));
                new Home().Show();
                this.Visible = false;
            }
            else {
                MessageBox.Show("Passwords are not the same, try again!");
            }
        }
    }
}
