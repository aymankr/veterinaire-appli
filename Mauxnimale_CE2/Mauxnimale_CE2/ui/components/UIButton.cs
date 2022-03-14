using System.Drawing;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui.components
{
    public partial class UIButton : Button
    {
        public UIButton(Color color, string text, int width)
        {
            BackColor = color;
            Text = text;
            ForeColor = Color.White;
            FlatStyle = FlatStyle.Flat;
            TabStop = false;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Roboto", width / 16);
            Size = new Size(width, width / 3);
        }
    }
}
