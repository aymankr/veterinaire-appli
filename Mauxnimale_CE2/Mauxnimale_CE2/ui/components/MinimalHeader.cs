using System.Windows.Forms;
using System.Drawing;
using Mauxnimale_CE2.ui.Components;

namespace Mauxnimale_CE2.UI
{
    internal class MinimalHeader : SplitContainer
    {
        public MinimalHeader(Size windowSize)
        {
            // Setup split 
            Name = "Title";
            Size = new Size(windowSize.Width / 2, windowSize.Height / 4);
            Orientation = Orientation.Vertical;
            SplitterDistance = Width / 4;

            // Setup logo
            UIPicture logo = new UIPicture(new Size(100, 100), Properties.Resources.AppLogo);
            logo.Name = "ApplicationLogo";
            logo.Parent = Panel1;

            // Setup title
            UITitleLabel title = new UITitleLabel("Mauxnimale", 30);
            title.Name = "ApplicationTitle";
            title.Parent = Panel2;

            // Add title & logo to 
            Panel1.Controls.Add(logo);
            Panel2.Controls.Add(title);
        }
    }
}
