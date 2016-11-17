using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi
{
	/// <summary>
	/// Edi path format. Implements <see cref="IFormatProvider"/> and <see cref="ICustomFormatter"/>
	/// </summary>
    public class EdiPathFormat : IFormatProvider, ICustomFormatter
    {
		/// <summary>
		/// All array format.
		/// </summary>
        private const string ALL_ARRAY_FORMAT = "{0}[{1}][{2}]";
        /// <summary>
        /// All URI format.
        /// </summary>
		private const string ALL_URI_FORMAT = "{0}/{1}/{2}";
        /// <summary>
        /// The segment format.
        /// </summary>
		private const string SEGMENT_FORMAT = "{0}";
        /// <summary>
        /// The element URI format.
        /// </summary>
		private const string ELEMENT_URI_FORMAT = "{0}/{1}";
        /// <summary>
        /// The element array format.
        /// </summary>
		private const string ELEMENT_ARRAY_FORMAT = "{0}[{1}]";
        /// <summary>
        /// The available format strings.
        /// </summary>
		private static readonly string[] availableFormatStrings = { "S", "E", "C", "s", "e", "c" };

		#region implementation from IFormatProvider
		/// <summary>
		/// Returns an object that provides formatting services for the specified type.
		/// </summary>
		/// <returns>An instance of the object specified by formatType, if the IFormatProvider implementation can supply that type of object; otherwise, null.</returns>
		/// <param name="formatType">An object that specifies the type of format object to return.</param>
        public object GetFormat(Type formatType) {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }
		#endregion
		#region implementation from ICustomFormatter
		/// <summary>
		/// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
		/// </summary>
		/// <param name="fmt">A format string containing formatting specifications.</param>
		/// <param name="arg">An object to format.</param>
		/// <param name="formatProvider">An object that supplies format information about the current instance.</param>
		/// <returns>The string representation of the value of arg, formatted as specified by <i>format</i> and <i>formatProvider</i>.</returns>
		/// <exception cref="FormatException">If the format given in fmt is an invalid format</exception>
        public string Format(string fmt, object arg, IFormatProvider formatProvider) {


            var culture = formatProvider as CultureInfo ?? CultureInfo.CurrentCulture;

            // Provide default formatting if arg is not an Int64. 
            if (arg.GetType() != typeof(EdiPath))
                try {
				return HandleOtherFormats(fmt, arg, culture);
                } catch (FormatException e) {
                    throw new FormatException(string.Format("The format of '{0}' is invalid.", fmt), e);
                }

            // Provide default formatting for unsupported format strings. 
            if (!(availableFormatStrings.Any(s => s == fmt) || string.IsNullOrWhiteSpace(fmt)))
                try {
				return HandleOtherFormats(fmt, arg, culture);
                } catch (FormatException e) {
                    throw new FormatException(string.Format("The format of '{0}' is invalid.", fmt), e);
                }

            var path = (EdiPath)arg;

            var mask = "";
            switch (fmt) {
                case "S":
                case "s":
                    mask = SEGMENT_FORMAT;
                    break;
                case "E":
                    mask = ELEMENT_ARRAY_FORMAT;
                    break;
                case "e":
                    mask = ELEMENT_URI_FORMAT;
                    break;
                case "c":
                    mask = ALL_URI_FORMAT;
                    break;
                case "C":
                default:
                    mask = ALL_ARRAY_FORMAT;
                    break;
            }
            return string.Format(mask, path.Segment, path.ElementIndex, path.ComponentIndex);
        }
		#endregion

		/// <summary>
		/// Handles the other formats.
		/// </summary>
		/// <remarks>Needed for Method Format</remarks>
		/// <returns>The string representation of the value of arg, formatted as specified by <i>format</i> and <i>formatProvider</i>.</returns>
		/// <param name="format">A format string containing formatting specifications.</param>
		/// <param name="arg">An object to format.</param>
		/// <param name="culture">A culture to use</param>
		private string HandleOtherFormats(string format, object arg, CultureInfo culture) {
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, culture);
            else if (arg != null)
                return arg.ToString();
            else
                return string.Empty;
        }
    }

}
