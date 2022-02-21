using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui
{
    public partial class UIRoundButton : Button
    {
        public UIRoundButton()
        {
            InitializeComponent();
        }

        public UIRoundButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
