using Mauxnimale_CE2.ui;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    class Footer
    {
        MainWindow window;

        Label admin;
        Label mentions;
        Rectangle up;
        Rectangle down;

        public Footer(MainWindow form)
        {
            this.window = form;
        }

        public void load()
        {
            generate_Labels();

            window.Paint += new PaintEventHandler(draw_Rectangles);
        }

        public void generate_Labels()
        {
            admin = new Label();
            admin.AutoSize = true;  
            admin.Text = "Contact admin";
            admin.Location = new Point(window.Width / 8, window.Height * 9 / 10);
            admin.BackColor = Color.Transparent;
            admin.Font = new Font("Roboto", 15, FontStyle.Bold);
            admin.ForeColor = Color.White;

            mentions = new Label();
            mentions.AutoSize = true;
            mentions.Text = "Mentions légales";
            mentions.Location = new Point(window.Width * 7 / 8 - mentions.Width, window.Height * 9 / 10);
            mentions.BackColor = Color.Transparent;
            mentions.Font = new Font("Roboto", 15, FontStyle.Bold);
            mentions.ForeColor = Color.White;
            
            admin.Click += new EventHandler(adminClick);
            mentions.Click += new EventHandler(legalClick);

            window.Controls.Add(admin);
            window.Controls.Add(mentions);
        }

        public void adminClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAdmin(window));
        }

        public void legalClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceLegalMentions(window));
        }

        public void draw_Rectangles(object sender, PaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(Color.FromArgb(144,222,223));
            up = new Rectangle();
            up.Height = window.Height / 8;
            up.Width = window.Width;
            up.X = 0;
            up.Y = window.Height - up.Height;

            e.Graphics.FillRectangle(b, up);

            b = new SolidBrush(Color.FromArgb(33, 188, 190));
            down = new Rectangle();
            down.Height = window.Height / 10;
            down.Width = window.Width;
            down.X = 0;
            down.Y = window.Height - down.Height;

            e.Graphics.FillRectangle(b, down);
        }

    }
}
