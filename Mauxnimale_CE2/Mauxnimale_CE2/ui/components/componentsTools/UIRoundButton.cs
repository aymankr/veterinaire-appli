using Mauxnimale_CE2.ui.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui
{
    public partial class UIRoundButton : Button
    {
        String character;

        public UIRoundButton(int size, String c)
        {
            ForeColor = Color.White;
            character = c;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Size = new Size(size, size);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.DrawEllipse(new Pen(UIColor.DARKBLUE), 0, 0, Size.Width, Size.Height);
            g.FillEllipse(new SolidBrush(UIColor.DARKBLUE), 0, 0, Size.Width, Size.Height);
            g.DrawString(this.character, new System.Drawing.Font("Roboto", Size.Width/2), new SolidBrush(Color.White), Size.Width/7, Size.Height/7);

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, Size.Width, Size.Height);
            this.Region = new Region(path);
            
            
        }
    }
}
