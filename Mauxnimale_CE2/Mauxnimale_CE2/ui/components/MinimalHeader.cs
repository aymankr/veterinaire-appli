using System.Windows.Forms;
using System.Drawing;
using Mauxnimale_CE2.ui.components;

namespace Mauxnimale_CE2.ui
{
    internal class MinimalHeader : SplitContainer
    {
        private MainWindow window;

        public MinimalHeader(MainWindow window)
        {
            this.window = window;

            // Setup container 
            Name = "HeaderContainer";
            Parent = window;
            Size = new Size(this.window.Size.Width / 2, this.window.Size.Height / 6);
            Location = new Point((this.window.Size.Width / 2) - (Width / 3), (this.window.Height / 4) - Height);
            Orientation = Orientation.Vertical;
            SplitterDistance = Width / 4;
            IsSplitterFixed = true;
            SplitterWidth = 1;

            // Setup logo
            UIPicture logo = new UIPicture(new Size(100, 100), Properties.Resources.AppLogo);
            logo.Name = "ApplicationLogo";
            logo.Parent = Panel1;
            logo.Dock = DockStyle.Fill;

            // Setup title
            UITitleLabel title = new UITitleLabel("Mauxnimale", 30);
            title.Name = "ApplicationTitle";
            title.Parent = Panel2;
            title.Location = new Point(title.Location.X, title.Parent.Height / 2 - title.Height / 2);

            // Add title & logo to 
            Panel1.Controls.Add(logo);
            Panel2.Controls.Add(title);
        }

        public void load()
        {
            window.Controls.Add(this);
        }
    }
}
