using Mauxnimale_CE2.ui;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    public partial class MainWindow : Form
    {
        AInterface interfac; //pas interface parce que sinon ca marche mal
        public MainWindow()
        {
            InitializeComponent();

            //interfac = new InterfaceHome(this);
            //interfac.load();

            Refresh();
        }

        public void switchInterface(AInterface inter)
        {
            interfac = inter;
            interfac.load();
        }
    }
}
