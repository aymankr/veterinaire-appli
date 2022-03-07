using System.Windows.Forms;
using System.Drawing;

namespace Mauxnimale_CE2.ui.Components
{
    /// <summary>
    /// Text box that fit content with Poppins font and blue color by default.
    /// </summary>
    internal class UITitleLabel : Label
    {
        /// <summary>
        /// Instanciate an UITitleLabel at 0;0, with the given text and font size.
        /// The color of the text is blue.
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <param name="fontSize">The size of the font</param>
        public UITitleLabel(string text, int fontSize)
        {
            Text = text;
            Font = new Font("Poppins", fontSize);
            ForeColor = UIColor.LIGHTBLUE;

            Location = new Point();
            Size = TextRenderer.MeasureText(text, Font);
        }

        /// <summary>
        /// Instanciate an UITitleLabel at location 0;0, with the given text, font size and color.
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <param name="fontSize">The size of the font</param>
        /// <param name="color">The color of the text</param>
        public UITitleLabel(string text, int fontSize, Color color)
        {
            Text = text;
            Font = new Font("Poppins", fontSize);
            ForeColor = color;

            Location = new Point();
            Size = TextRenderer.MeasureText(text, Font);
        }

        /// <summary>
        /// Instanciate an UITitleLabel at the given location, with the given text and font size.
        /// The color of the text is blue.
        /// </summary>
        /// <param name="location">The location of the text box</param>
        /// <param name="text">The text to display</param>
        /// <param name="fontSize">The size of the font</param>
        public UITitleLabel(Point location, string text, int fontSize)
        {
            Text = text;
            Font = new Font("Poppins", fontSize);
            ForeColor = Color.FromArgb(144, 222, 223);

            Location = location;
            Size = TextRenderer.MeasureText(text, Font);
        }

        /// <summary>
        /// Instanciate an UITitleLabel at the given location, with the given text, font size and color.
        /// </summary>
        /// <param name="location">The location of the text box</param>
        /// <param name="text">The text to display</param>
        /// <param name="fontSize">The size of the font</param>
        /// <param name="color">The color of the text</param>
        public UITitleLabel(Point location, string text, int fontSize, Color color)
        {
            Text = text;
            Font = new Font("Poppins", fontSize);
            ForeColor = color;

            Location = location;
            Size = TextRenderer.MeasureText(text, Font);
        }
    }
}
