using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// Contains the extenstion methods for the RichTextBox
    /// </summary>
    public static class RichTextBoxExtensions
    {
        /// <summary>
        /// This appends text of a certain color in a RichTextBox
        /// </summary>
        /// <param name="box">The RichTextBox </param>
        /// <param name="text">Text being added</param>
        /// <param name="color">The color of the text</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string text, System.Drawing.Color color)
        {
            /* box.SelectionStart = box.TextLength;
            box.SelectionLength = 0; */ //testing!

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        /// <summary>
        /// Appends bold text to the RichTextBox
        /// </summary>
        /// <param name="box">The RichTextBox</param>
        /// <param name="text">Text being added</param>
        public static void AppendBoldText(this System.Windows.Forms.RichTextBox box, string text)
        {
            box.SelectionFont = new Font(box.Font, FontStyle.Bold);
            box.AppendText(text);
            box.SelectionFont = new Font(box.Font, FontStyle.Regular);
        }

        /// <summary>
        /// Appends bold and colored text to the RichTextBox
        /// </summary>
        /// <param name="box">The RichTextBox</param>
        /// <param name="text">Text being added</param>
        /// <param name="color">Color of the text being added.</param>
        public static void AppendColorBoldText(this System.Windows.Forms.RichTextBox box, string text, System.Drawing.Color color)
        {
            box.SelectionFont = new Font(box.Font, FontStyle.Bold);
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionFont = new Font(box.Font, FontStyle.Regular);
            box.SelectionColor = box.ForeColor;
        }
    }
}
