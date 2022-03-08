using Mauxnimale_CE2.ui;
using System;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    public partial class MainWindow : Form
    {
        AInterface interfac; //pas interface parce que sinon ca marche mal
        public MainWindow()
        {
            InitializeComponent();
            Resize += new EventHandler(windowResize);

<<<<<<< HEAD
            interfac = new InterfaceGestionConsultation(this);
=======
            interfac = new InterfaceConnection(this);
>>>>>>> 46eb9eb9e295cc281f8d1f047a88438ce01d060a
            interfac.load();

            Refresh();
        }

        public void switchInterface(AInterface inter)
        {
            interfac = inter;
            interfac.load();
        }

        public void windowResize(object sender, EventArgs e)
        {
            interfac.updateSize();
            Refresh();
        }
    }
}
