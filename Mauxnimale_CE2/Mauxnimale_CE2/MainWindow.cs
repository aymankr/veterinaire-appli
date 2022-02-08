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
        InterfaceAbs window; //pas interface parce que sinon ca marche mal
        public MainWindow()
        {

            window = new InterfaceConnection(this);
            window.load();

            InitializeComponent();            
        }

        public void switchInterface(InterfaceAbs interf)
        {
            window = interf;
            window.load();
        }
    }
}
