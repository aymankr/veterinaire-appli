using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2
{
    public abstract class AInterface
    {
        protected MainWindow window;
        protected SALARIE user;

        public AInterface(MainWindow window, SALARIE user)
        {
            this.user = user;
            this.window = window;
        }

        public abstract void load();
        public void updateSize()
        {
            window.Controls.Clear();
            load();
        }
    }
}
