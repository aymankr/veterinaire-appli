using System.Drawing;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    class Footer
    {
<<<<<<< HEAD:Mauxnimale_CE2/Mauxnimale_CE2/Footer.cs
        PaintEventArgs e;
        MainWindow form;
=======
        MainWindow window;
>>>>>>> 0caef9d232032591dd3b35aa5b3b0ac3b9c1a264:Mauxnimale_CE2/Mauxnimale_CE2/ui/components/Footer.cs

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
            admin.Location = new Point(100, window.Height - 100);
            admin.BackColor = Color.Transparent;
            admin.Font = new Font("Roboto", 15, FontStyle.Bold);
            admin.ForeColor = Color.White;

            mentions = new Label();
            mentions.AutoSize = true;
            mentions.Text = "Mentions légales";
            mentions.Location = new Point(window.Width - 150 - mentions.Width, window.Height - 100);
            mentions.BackColor = Color.Transparent;
            mentions.Font = new Font("Roboto", 15, FontStyle.Bold);
            mentions.ForeColor = Color.White;
            

            window.Controls.Add(admin);
            window.Controls.Add(mentions);
        }

        public void draw_Rectangles(object sender, PaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(Color.FromArgb(144,222,223));
            up = new Rectangle();
            up.Height = 181;
            up.Width = window.Width;
            up.X = 0;
            up.Y = window.Height - 181;

            e.Graphics.FillRectangle(b, up);

            b = new SolidBrush(Color.FromArgb(33, 188, 190));
            down = new Rectangle();
            down.Height = 123;
            down.Width = window.Width;
            down.X = 0;
            down.Y = window.Height - 123;

            e.Graphics.FillRectangle(b, down);
        }

    }
}
