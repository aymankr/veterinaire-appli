using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui.components.componentsTools
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
            TextAlign = ContentAlignment.MiddleCenter;
            Font = new Font("Poppins", width / 16);
            AutoSize = true;
            Margin = new Padding(0);
        }
    }
}
