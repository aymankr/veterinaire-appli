using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui.components
{
    public class SellProduct : SplitContainer
    {
        public PRODUIT Product;
        public int QuantityToSell
        {
            get { return (int)_quantityChooser.Value; }
        }

        private Label _title;
        private NumericUpDown _quantityChooser;

        public SellProduct(PRODUIT product, Size size, Point location)
        {
            Product = product;
            Size = size;
            Location = location;
            SplitterDistance = Width / 4 * 3;

            generateLabel();
            generateQuantityChooser();
        }

        #region Génération des composants

        private void generateLabel()
        {
            _title = new Label();
            _title.Size = Panel1.Size;
            _title.Text = Product.ToString();

            Panel1.Controls.Add(_title);
        }

        private void generateQuantityChooser()
        {
            _quantityChooser = new NumericUpDown();
            _quantityChooser.Size = Panel2.Size;
            _quantityChooser.Minimum = 1;
            _quantityChooser.Maximum = Product.QUANTITEENSTOCK;

            Panel2.Controls.Add(_quantityChooser);
        }

        #endregion
    }
}
