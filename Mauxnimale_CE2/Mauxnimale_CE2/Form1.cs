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
    public partial class Form1 : Form
    {
        InterfaceAbs interfac; //pas interface parce que sinon ca marche mal
        public Form1()
        {
            InitializeComponent();

            //interfac = new InterfaceConnexion(this);
            //interfac.load();

            Refresh();
        }

        public void switchInterface(InterfaceAbs interf)
        {
            interfac = interf;
            interfac.load();
        }
    }
}
