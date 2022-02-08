using System.Windows.Forms;
using System.Drawing;

namespace Mauxnimale_CE2
{
    internal class Title
    {
        private Form1 form;

        public Title(Form1 form)
        {
            this.form = form;
        }

        public void load()
        {
            form.Controls.Add(SetupTitle());
            form.Controls.Add(SetupLogo());
        }

        private Label SetupTitle()
        {
            Label title = new Label();
            title.Text = "Mauxnimale";
            title.Font = new System.Drawing.Font("Poppins", 30);
            title.Size = new System.Drawing.Size(form.Width, 60);
            title.ForeColor = System.Drawing.Color.CadetBlue;
            title.Location = new System.Drawing.Point(form.Width / 5, 25);
       
            return title;
        }

        private Control SetupLogo()
        {
            Control logo = new Control();
            logo.BackgroundImage = Image.FromFile("resources/images/logo.png");
            logo.Location = new Point();

            return logo;
        }
    }
}
