using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PassGenCSharp.App.Data;
using PassGenCSharp.App.Generator;
using PassGenCSharp.App.Security;

namespace PassGenCSharp
{
    public partial class Home : System.Windows.Forms.Form {
        Data data;

        TextBox passwordInput;
        RichTextBox descriptionInput;
        TextBox emailInput;
        TextBox nicknameInput;
        TextBox platformInput;
        Settings settings;
        TextBox path;

        bool isOnAddingPage;

        public Home() {
            InitializeComponent();
            settings = new Settings();
            settings.Length = 16;
            settings.Type = 1;
            isOnAddingPage = false;
            try {
                if (Properties.Settings.Default.Path == "") {
                    folderBrowserDialog1.ShowDialog();
                    Properties.Settings.Default.Path = folderBrowserDialog1.SelectedPath;
                    Console.WriteLine(folderBrowserDialog1.SelectedPath);
                    data = new Data(folderBrowserDialog1.SelectedPath);
                }
                else {
                    data = new Data(Properties.Settings.Default.Path);
                }
                Properties.Settings.Default.Save();
            }
            catch {
                Properties.Settings.Default.Path = "";
                Properties.Settings.Default.Save();
                MessageBox.Show("No folder selected");
                this.Close();
            }

            
        }

        private void FillPanel(bool edit) {
            var lista = data.GetPlatforms();

            flowLayoutPanel2.Controls.Clear();

            var title = new Label();
            
            if (edit)
                title.Text = "Edit";
            else
                title.Text = "Browse";

            title.Font = new Font("century gothic", 18);
            title.AutoSize = true;

            flowLayoutPanel2.Controls.Add(title);

            foreach (var record in lista) {
                Button button = new Button();
                button.Width = 370;
                button.Text = record;
                button.Font = new Font("century gothic", 15);
                button.FlatAppearance.BorderColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = Color.White;
                button.Height = 40;
                if (edit)
                    button.Click += OpenEdit;
                else
                    button.Click += OpenDetails;
                flowLayoutPanel2.Controls.Add(button);
            }
        }
        private void button1_Click(object sender, EventArgs e) {
            isOnAddingPage = true;
            isOnSettingsPage = false;
            flowLayoutPanel2.Controls.Clear();

            ClearButtons();
            button1.BackColor = Color.LightGray;

            //Platform input
            Label platform = new Label();
            platform.Text = "Platform: ";
            platform.Font = new Font("century gothic", 14);

            platformInput = new TextBox();
            platformInput.Width = 300;
            platformInput.Font = new Font("arial narrow", 14);
            platformInput.Margin = new Padding(10, 5, 10, 2);


            flowLayoutPanel2.Controls.Add(platform);
            flowLayoutPanel2.Controls.Add(platformInput);


            //E-mail input
            Label email = new Label();
            email.Margin = new Padding(0, 15, 0, 0);
            email.Text = "E-mail: ";
            email.Font = new Font("century gothic", 14);

            emailInput = new TextBox();
            emailInput.Width = 300;
            emailInput.Font = new Font("arial narrow", 14);
            emailInput.Margin = new Padding(10, 5, 10, 2);

            flowLayoutPanel2.Controls.Add(email);
            flowLayoutPanel2.Controls.Add(emailInput);


            //Nickname input
            Label nickname = new Label();
            nickname.Margin = new Padding(0, 15, 0, 0);
            nickname.Text = "Username: ";
            nickname.Font = new Font("century gothic", 14);
            nickname.AutoSize = true;

            nicknameInput = new TextBox();
            nicknameInput.Width = 300;
            nicknameInput.Font = new Font("arial narrow", 14);
            nicknameInput.Margin = new Padding(10, 5, 10, 2);

            flowLayoutPanel2.Controls.Add(nickname);
            flowLayoutPanel2.Controls.Add(nicknameInput);


            //Password input
            Label password = new Label();
            password.Margin = new Padding(0, 15, 0, 0);
            password.Text = "Password: ";
            password.Font = new Font("century gothic", 14);

            passwordInput = new TextBox();
            passwordInput.Width = 300;
            passwordInput.Font = new Font("arial narrow", 14);
            passwordInput.Margin = new Padding(10, 5, 10, 2);
            passwordInput.PasswordChar = '*';

            flowLayoutPanel2.Controls.Add(password);
            flowLayoutPanel2.Controls.Add(passwordInput);


            //Password options
            Button showButton = new Button();
            showButton.Text = "SHOW";
            showButton.Width = 87;
            showButton.Margin = new Padding(10, 5, 10, 2);
            showButton.Click += ShowPassword; 
            showButton.FlatStyle = FlatStyle.Flat;
            showButton.FlatAppearance.BorderColor = Color.White;

            Button generationSettingsButton = new Button();
            generationSettingsButton.Text = "SETTINGS";
            generationSettingsButton.Width = 87;
            generationSettingsButton.Margin = new Padding(10, 5, 10, 2);
            generationSettingsButton.Click += Settings_clicked;
            generationSettingsButton.FlatStyle = FlatStyle.Flat;
            generationSettingsButton.FlatAppearance.BorderColor = Color.White;


            Button generateButton = new Button();
            generateButton.Text = "GENERATE";
            generateButton.Width = 87;
            generateButton.Margin = new Padding(10, 5, 10, 2);
            generateButton.Click += GeneratePassword;
            generateButton.FlatStyle = FlatStyle.Flat;
            generateButton.FlatAppearance.BorderColor = Color.White;

            flowLayoutPanel2.Controls.Add(showButton);
            flowLayoutPanel2.Controls.Add(generationSettingsButton);
            flowLayoutPanel2.Controls.Add(generateButton);

            //Description input
            Label description = new Label();
            description.Margin = new Padding(0, 15, 0, 0);
            description.Text = "Description: ";
            description.Font = new Font("century gothic", 14);
            description.AutoSize = true;

            descriptionInput = new RichTextBox();
            descriptionInput.Width = 300;
            descriptionInput.Font = new Font("arial narrow", 14);
            descriptionInput.Margin = new Padding(10, 5, 10, 2);

            flowLayoutPanel2.Controls.Add(description);
            flowLayoutPanel2.Controls.Add(descriptionInput);

            Button saveButton = new Button();
            saveButton.Text = "SAVE";
            saveButton.Width = 300;
            saveButton.Margin = new Padding(10, 5, 10, 2);
            saveButton.Click += SaveButton_Click;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.FlatAppearance.BorderColor = Color.White;

            flowLayoutPanel2.Controls.Add(saveButton);
        }
        
        private void button2_Click(object sender, EventArgs e) {
            isOnAddingPage = false;
            isOnSettingsPage = false;
            FillPanel(true);
            ClearButtons();
            button2.BackColor = Color.LightGray;
        }

        private void button3_Click(object sender, EventArgs e) {
            isOnAddingPage = false;
            isOnSettingsPage = false;
            FillPanel(false);
            ClearButtons();
            button3.BackColor = Color.LightGray;
        }

        private void button4_Click(object sender, EventArgs e) {
            isOnAddingPage = true;
            isOnSettingsPage = false;
            flowLayoutPanel2.Controls.Clear();
            
            ClearButtons();
            button4.BackColor = Color.LightGray;

            Label title = new Label();
            title.Text = "Generator";
            title.Font = new Font("century gothic", 18);
            title.AutoSize = true;
            title.Margin = new Padding(3, 3, 3, 25);

            Button showButton = new Button();
            showButton.Text = "SHOW";
            showButton.Width = 86;
            showButton.Margin = new Padding(10, 5, 10, 2);
            showButton.Click += ShowPassword;
            showButton.FlatStyle = FlatStyle.Flat;
            showButton.FlatAppearance.BorderColor = Color.White;


            Button generationSettingsButton = new Button();
            generationSettingsButton.Text = "SETTINGS";
            generationSettingsButton.Width = 87;
            generationSettingsButton.Margin = new Padding(10, 5, 10, 2);
            generationSettingsButton.Click += Settings_clicked;
            generationSettingsButton.FlatStyle = FlatStyle.Flat;
            generationSettingsButton.FlatAppearance.BorderColor = Color.White;


            Button generateButton = new Button();
            generateButton.Text = "GENERATE";
            generateButton.Width = 86;
            generateButton.Margin = new Padding(10, 5, 10, 2);
            generateButton.Click += GeneratePassword;
            generateButton.FlatStyle = FlatStyle.Flat;
            generateButton.FlatAppearance.BorderColor = Color.White;


            passwordInput = new TextBox();
            passwordInput.Width = 300;
            passwordInput.Font = new Font("arial", 14);
            passwordInput.Margin = new Padding(10, 5, 10, 2);
            passwordInput.PasswordChar = '*';


            flowLayoutPanel2.Controls.Add(title);

            flowLayoutPanel2.Controls.Add(passwordInput);

            flowLayoutPanel2.Controls.Add(showButton);
            flowLayoutPanel2.Controls.Add(generationSettingsButton);
            flowLayoutPanel2.Controls.Add(generateButton);
        }

        private void Main_Activated(object sender, EventArgs e) {
            if (!isOnAddingPage && !isOnSettingsPage) {
                FillPanel(false);
                ClearButtons();
                button3.BackColor = Color.LightGray;
            }
        }

        private void Settings_clicked(object sender, EventArgs e) {
            new SettingsForm(settings).Show();
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            Model model = new Model(0, platformInput.Text, emailInput.Text,
                passwordInput.Text, descriptionInput.Text, nicknameInput.Text);
            Console.WriteLine(model.Description);
            data.AddRecord(model);

            MessageBox.Show("Zapisano hasło");
        }

        private void GeneratePassword(object sender, EventArgs e) {
            string password;
            switch (settings.Type) {
                case 0: password = Generator.GeneratePasswordAlfabetical(settings.Length);
                    break;
                case 1: password = Generator.GeneratePasswordAlfanumerical(settings.Length);
                    break;
                case 2: password = Generator.GeneratePasswordPrintableChars(settings.Length);
                    break;

                default: password = Generator.GeneratePasswordAlfabetical(settings.Length);
                    break;
            }
           

            passwordInput.Text = password;
        }

        private void ShowPassword(object sender, EventArgs e) {
            if (passwordInput.PasswordChar == '*')
                passwordInput.PasswordChar = (char) 0;
            else
                passwordInput.PasswordChar = '*';

        }

        private void OpenEdit(object sender, EventArgs e) {
            var text = (sender as Button).Text;
            var platform = data.GetDetails(text);

            foreach (var record in platform) {
                var edit = new Edit(record, data);
                edit.Show();
            }
        }

        private void OpenDetails(object sender, EventArgs e) {
            var text = (sender as Button).Text;
            var platform = data.GetDetails(text);

            foreach (var record in platform) {
                var details = new Details(record);
                details.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            Application.Exit();
            this.Close();


        }

        private void button6_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        int x;
        int y;
        private bool firstClick = false;
        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                firstClick = true;
                move_timer.Enabled = true;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                move_timer.Enabled = false;
            }
        }

        private void move_timer_Tick(object sender, EventArgs e) {
            if (firstClick) {
                firstClick = false;
                x = Cursor.Position.X;
                y = Cursor.Position.Y;
            }
            int deltax = x - Cursor.Position.X;
            int deltay = y - Cursor.Position.Y;

            this.Location = new Point(this.Location.X - deltax, this.Location.Y - deltay);

            x = Cursor.Position.X;
            y = Cursor.Position.Y;
        }

    
        bool isHidden = false;
        private void button7_Click_2(object sender, EventArgs e) {
            if (!isHidden) {
                ButtonVisible(false);
                for (int i = 0; i < 145; i++) {
                    button7.Width -= i % 2;
                    flowLayoutPanel1.Width = flowLayoutPanel1.Width - 1;
                }

                for (int i = 0; i < 180; i++) {
                    flowLayoutPanel2.Location = 
                        new Point(flowLayoutPanel2.Location.X - (i % 2), flowLayoutPanel2.Location.Y);
                }
                isHidden = true;
            }
            else {
                for (int i = 0; i < 145; i++) {
                    button7.Width += i % 2;
                    flowLayoutPanel1.Width = flowLayoutPanel1.Width + 1;    
                }
                for (int i = 0; i < 180; i++) {
                    flowLayoutPanel2.Location =
                        new Point(flowLayoutPanel2.Location.X + (i % 2), flowLayoutPanel2.Location.Y);
                }
                ButtonVisible(true);
                isHidden = false;
            }
        }
        
        private void ClearButtons() {
            button1.BackColor = Color.FromArgb(36, 199, 147);
            button2.BackColor = Color.FromArgb(36, 199, 147);
            button3.BackColor = Color.FromArgb(36, 199, 147);
            button4.BackColor = Color.FromArgb(36, 199, 147);
            button8.BackColor = Color.FromArgb(36, 199, 147);
            button9.BackColor = Color.FromArgb(36, 199, 147);
        }

        private void ButtonVisible(bool visible) {
            button1.Visible = visible;
            button2.Visible = visible;
            button3.Visible = visible;
            button4.Visible = visible;
            button8.Visible = visible;
            button9.Visible = visible;
        }

        bool isOnSettingsPage = false;
        private void button8_Click(object sender, EventArgs e) {
            ClearButtons();
            button8.BackColor = Color.LightGray;
            isOnSettingsPage = true;
            flowLayoutPanel2.Controls.Clear();

            Label title = new Label();
            title.Text = "Options";
            title.Width = 350;
            title.Font = new Font("century gothic", 18);
            title.Height = 50;

            path = new TextBox();
            path.Text = Properties.Settings.Default.Path;
            path.ReadOnly = true;
            path.Width = 250;
            path.Font = new Font("consolas", 12);
          
            Button changePath = new Button();
            changePath.Text = "Change";
            changePath.Height = 26;
            changePath.Click += this.ChangePath;
            changePath.FlatStyle = FlatStyle.Flat;

            flowLayoutPanel2.Controls.Add(title);
            flowLayoutPanel2.Controls.Add(path);
            flowLayoutPanel2.Controls.Add(changePath);

        }

        private void ChangePath(object sender, EventArgs e) {
            folderBrowserDialog1.ShowDialog();
            try {
                Properties.Settings.Default.Path = folderBrowserDialog1.SelectedPath;
                Console.WriteLine(folderBrowserDialog1.SelectedPath);
                data = new Data(folderBrowserDialog1.SelectedPath);
                
            
                Properties.Settings.Default.Save();
            }
            catch {
                Properties.Settings.Default.Path = "";
                Properties.Settings.Default.Save();
                MessageBox.Show("No folder selected");
                this.Close();
            }

            path.Text = Properties.Settings.Default.Path;
        }

        private void button9_Click(object sender, EventArgs e) {
            ClearButtons();
            button9.BackColor = Color.LightGray;
            flowLayoutPanel2.Controls.Clear();
        }
    }
}
