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
            interfac = new InterfaceConnection(this, null);
=======
            interfac = new InterfaceAppointmentManagment(this, null);
>>>>>>> 45636f7 (modification des ordonnance fonctionnelle)
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
