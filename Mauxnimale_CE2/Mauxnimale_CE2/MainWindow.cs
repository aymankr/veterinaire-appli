using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    public partial class MainWindow : Form
    {
        InterfaceAbs interfac; //pas interface parce que sinon ca marche mal
        public MainWindow()
        {
            InitializeComponent();

            MinimalHeader test = new MinimalHeader(this);
            test.load();

            Refresh();
        }

        public void switchInterface(InterfaceAbs interf)
        {
            interfac = interf;
            interfac.load();
        }
    }
}
