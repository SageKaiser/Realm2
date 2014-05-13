using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Realm2
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method to capitalize the first letter of a string
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>The source string wiht the first letter capitalized.</returns>
        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
        /// <summary>
        /// Extension method for writing color to a RichTextBox.
        /// </summary>
        /// <param name="box">the RichTextBox to be written to.</param>
        /// <param name="text">the text to be written to the RichTextBox.</param>
        /// <param name="color">the color of the text, defined by the Color structure.</param>
        public static void AppendText(this RichTextBox box, string text, string color)
        {
            //initialize a class to convert strings to brush colors
            BrushConverter bc = new BrushConverter();
            //initialize a class for writing to a richtextbox
            TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
            tr.Text = "\r\n" + text;
            //try to write the color given
            try { tr.ApplyPropertyValue(TextElement.ForegroundProperty, bc.ConvertFromString(color)); }
            //if it fails, write it in black
            catch (FormatException) { tr.ApplyPropertyValue(TextElement.ForegroundProperty, bc.ConvertFromString("Black")); }
        }
        /// <summary>
        /// Extension method for writing color to a RichTextBox.
        /// </summary>
        /// <param name="box">the RichTextBox to be written to.</param>
        /// <param name="text">the text to be written to the RichTextBox.</param>
        /// <param name="color">the color of the text, defined by the Color structure.</param>
        /// <param name="sameLine">True if the text is to be written to the same line as the last text.</param>
        public static void AppendText(this RichTextBox box, string text, string color, bool sameLine)
        {
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
            if (!sameLine)
                tr.Text = "\r\n" + text;
            else
                tr.Text = text;
            try { tr.ApplyPropertyValue(TextElement.ForegroundProperty, bc.ConvertFromString(color)); }
            catch (FormatException) { tr.ApplyPropertyValue(TextElement.ForegroundProperty, bc.ConvertFromString("Black")); }
        }
    }
}
