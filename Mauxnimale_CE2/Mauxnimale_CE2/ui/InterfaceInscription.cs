using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.controllers.utils;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceInscription : AInterface
    {
        private MainWindow window;

        private MinimalHeader header;
        private Footer footer;

        private UITextBox loginTextBox;

        public InterfaceInscription(MainWindow window, SALARIE user)
        {
            this.window = window;
            header = new MinimalHeader(window);
            footer = new Footer(window);
            loginTextBox = generateLoginTextBox();
        }

        public override void load()
        {
            header.load();
            footer.load();
            window.Controls.Add(generateSubmitButton());
            window.Controls.Add(loginTextBox);
        }

        #region Window components

        private UIButton generateSubmitButton()
        {
            UIButton button = new UIButton(UIColor.ORANGE, "Inscrire", 400);
            button.Name = "Submit inscription button";
            button.Parent = window;
            button.Location = new Point(window.Width / 2 - button.Width / 2, window.Height - window.Height / 4 - button.Height);
            button.Click += submitInscription;
            return button;
        }

        private UITextBox generateLoginTextBox()
        {
            UITextBox loginTextBox = new UITextBox();
            loginTextBox.Name = "Login text box";
            loginTextBox.Parent = window;
            loginTextBox.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            loginTextBox.Location = new Point(window.Width / 2 - loginTextBox.Width / 2, window.Height - window.Height / 2 - loginTextBox.Height);
            loginTextBox.Font = new Font("Poppins", window.Height * 3 / 100);
            loginTextBox.Text = "New user login";
            loginTextBox.ForeColor = Color.Gray;
            loginTextBox.LostFocus += loginFocusLeave;
            loginTextBox.GotFocus += loginFocusEnter;
            return loginTextBox;
        }

        #endregion

        #region Event management

        private void loginFocusEnter(object sender, EventArgs e)
        {
            if (loginTextBox.Text == "New user login")
            {
                loginTextBox.Text = "";
                loginTextBox.ForeColor = Color.Black;
            }
        }

        private void loginFocusLeave(object sender, EventArgs e)
        {
            if (loginTextBox.Text.Length == 0)
            {
                loginTextBox.Text = "New user login";
                loginTextBox.ForeColor = Color.Gray;
            }
        }

        private void submitInscription(object sender, EventArgs e)
        {
            // Verify that the login is valid
            if (loginTextBox.Text.Length == 0 || loginTextBox.Text == "New user login")
            {
                string errorMessage = "Please, enter a user login";
                MessageBox.Show(window, errorMessage, "Invalid input", MessageBoxButtons.OK);
                return;
            }
            if (!InputVerification.noSpecialCharacters(loginTextBox.Text))
            {
                string errorMessage = "Login: " + loginTextBox.Text + " is not valid.\nPlease, avoid special characters.";
                MessageBox.Show(window, errorMessage, "Invalid input", MessageBoxButtons.OK);
                return;
            }

            // Try to register
            string tempPassword = RegistrationController.registerNewUser(loginTextBox.Text);

            // Verify that the user with the login doest not already exists
            if (tempPassword == null)
            {
                string errorMessage = "User with login: " + loginTextBox.Text + " already exists.";
                MessageBox.Show(window, errorMessage, "User already exists", MessageBoxButtons.OK);
            }
            else
            {
                // If all good, display the temporary password
                string message = "User with login: " + loginTextBox.Text + " successfully registered.\nHis temporary password is: " + tempPassword;
                MessageBox.Show(window, message, "User registered", MessageBoxButtons.OK);
            }

        }

        #endregion

        public override void updateSize()
        {
            //throw new System.NotImplementedException();
        }
    }
}
