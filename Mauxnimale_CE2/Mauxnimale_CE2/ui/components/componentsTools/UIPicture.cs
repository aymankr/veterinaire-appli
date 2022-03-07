using System.Windows.Forms;
using System.Drawing;

namespace Mauxnimale_CE2.ui.Components
{
    /// <summary>
    /// Picture box displaying an image in stretch mode.
    /// </summary>
    internal class UIPicture : PictureBox
    {
        /// <summary>
        /// Instanciate an UIPicture with the given size displaying the given image.
        /// </summary>
        /// <param name="size">The size of the picture</param>
        /// <param name="image">The image to display</param>
        public UIPicture(Size size, Image image)
        {
            Location = new Point();
            Size = size;

            Image = Properties.Resources.AppLogo;
            SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /// <summary>
        /// Instanciate an UIPicture at the given location, with the given size displaying the given image.
        /// </summary>
        /// <param name="location">The location of the picture</param>
        /// <param name="size">The size of the picture</param>
        /// <param name="image">The image to display</param>
        public UIPicture(Point location, Size size, Image image)
        {
            Location = location;
            Size = size;

            Image = Properties.Resources.AppLogo;
            SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
