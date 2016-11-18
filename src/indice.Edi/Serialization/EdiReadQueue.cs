using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indice.Edi.Utilities;
using System.Globalization;

namespace indice.Edi.Serialization
{

	/// <summary>
	/// Read queue extensions.
	/// </summary>
    internal static class ReadQueueExtensions
    {
		/// <summary>
		/// Containses the path.
		/// </summary>
		/// <returns><c>true</c>, if path was containsed, <c>false</c> otherwise.</returns>
		/// <param name="queue">Queue.</param>
		/// <param name="path">Path.</param>
        public static bool ContainsPath(this Queue<EdiEntry> queue, string path) {
            if (string.IsNullOrWhiteSpace(path) || queue.Count == 0)
                return false;
            return queue.Any(entry => entry.Token.IsPrimitiveToken() && EdiPath.Parse(path).Equals(entry.Path));
        }

		/// <summary>
		/// Reads as string.
		/// </summary>
		/// <returns>The as string.</returns>
		/// <param name="queue">Queue.</param>
		/// <param name="path">Path.</param>
        public static string ReadAsString(this Queue<EdiEntry> queue, string path) {
            if (!ContainsPath(queue, path))
                return null;
            while (queue.Count > 0) {
                var entry = queue.Dequeue();
                if (entry.Token.IsPrimitiveToken() && EdiPath.Parse(path).Equals(entry.Path)) {
                    return entry.Value;
                }
            }
            return null;
        }

		/// <summary>
		/// Reads as int32.
		/// </summary>
		/// <returns>The as int32.</returns>
		/// <param name="queue">Queue.</param>
		/// <param name="path">Path.</param>
		/// <param name="culture">Culture.</param>
        public static int? ReadAsInt32(this Queue<EdiEntry> queue, string path, CultureInfo culture = null) {
            var text = ReadAsString(queue, path);
            if (text != null) {
                text = text.TrimStart('Z'); // Z suppresses leading zeros
            }
            if (string.IsNullOrEmpty(text))
                return null;

            var integer = default(int);
            if (!int.TryParse(text, NumberStyles.Integer, culture ?? CultureInfo.InvariantCulture, out integer)) {
                throw new EdiException("Cannot parse int from string '{0}'. Path {1}".FormatWith(culture, text, path));
            }
            return integer;
        }

		/// <summary>
		/// Reads as decimal.
		/// </summary>
		/// <returns>The as decimal.</returns>
		/// <param name="queue">Queue.</param>
		/// <param name="path">Path.</param>
		/// <param name="picture">Picture.</param>
		/// <param name="decimalMark">Decimal mark.</param>
        public static decimal? ReadAsDecimal(this Queue<EdiEntry> queue, string path, Picture? picture, char? decimalMark) {
            var text = ReadAsString(queue, path);
            if (string.IsNullOrEmpty(text))
                return null;
            
            return text.Parse(picture, decimalMark);
        }
    }

	/// <summary>
	/// Edi entry.
	/// </summary>
    internal struct EdiEntry
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiEntry"/> struct.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="token">Token.</param>
		/// <param name="value">Value.</param>
        public EdiEntry(string path, EdiToken token, string value) {
            Path = path;
            Value = value;
            Token = token;
        }

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <value>The path.</value>
        public string Path { get; }

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
        public string Value { get; }

		/// <summary>
		/// Gets the token.
		/// </summary>
		/// <value>The token.</value>
        public EdiToken Token { get; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Serialization.EdiEntry"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Serialization.EdiEntry"/>.</returns>
        public override string ToString() {
            return $"{Path ?? "-"} {Value}";
        }
    }
}
