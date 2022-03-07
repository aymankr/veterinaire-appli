using Mauxnimale_CE2.ui.Components;
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
<<<<<<< HEAD
            Font = new Font("Roboto", width / 16);
=======
            Font = new System.Drawing.Font("Roboto", width / 16);
>>>>>>> efcd1bba50a1b7c49c4750f760b6a0e25f379f4d
            Size = new Size(width, width / 3);
        }
    }
}
