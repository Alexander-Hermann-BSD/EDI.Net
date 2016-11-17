using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace indice.Edi
{
	/// <summary>
	/// Edi path.
	/// </summary>
    public struct EdiPath : IComparable<EdiPath>, IEquatable<EdiPath>
    {
		/// <summary>
		/// The parse pattern.
		/// </summary>
        private const string PARSE_PATTERN = @"^([A-Z]{1}[A-Z0-9]{1,3})?([\[/]{1}(\d+?)\]?)?([\[/]{1}(\d+?)\]?)?$"; // supports both "STX/2/1 and STX[2][1]"
        /// <summary>
        /// The segment.
        /// </summary>
		private readonly string _Segment;
        /// <summary>
        /// The index of the element.
        /// </summary>
		private readonly int _ElementIndex;
        /// <summary>
        /// The index of the component.
        /// </summary>
		private readonly int _ComponentIndex;

		/// <summary>
		/// Gets the segment.
		/// </summary>
		/// <value>The segment.</value>
        public string Segment {
            get { return _Segment; }
        }

		/// <summary>
		/// Gets the index of the element.
		/// </summary>
		/// <value>The index of the element.</value>
        public int ElementIndex {
            get { return _ElementIndex; }
        }

		/// <summary>
		/// Gets the index of the component.
		/// </summary>
		/// <value>The index of the component.</value>
        public int ComponentIndex {
            get { return _ComponentIndex; }
        }
        
		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.EdiPath"/> struct.
		/// </summary>
		/// <param name="segment">Segment.</param>
        public EdiPath(string segment) : this (segment, 0, 0) {
            
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.EdiPath"/> struct.
		/// </summary>
		/// <param name="segment">Segment.</param>
		/// <param name="elementIndex">Element index.</param>
        public EdiPath(string segment, int elementIndex) : this(segment, elementIndex, 0) {
            
        }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.EdiPath"/> struct.
		/// </summary>
		/// <param name="segment">Segment.</param>
		/// <param name="elementIndex">Element index.</param>
		/// <param name="componentIndex">Component index.</param>
        public EdiPath(string segment, int elementIndex, int componentIndex) {
            _Segment = segment ?? string.Empty;
            _ElementIndex = elementIndex;
            _ComponentIndex = componentIndex;
        }
        
		/// <summary>
		/// Serves as a hash function for a <see cref="T:indice.Edi.EdiPath"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
        public override int GetHashCode() {
            return _Segment.GetHashCode() ^ _ElementIndex.GetHashCode() ^ _ComponentIndex.GetHashCode();
        }

		#region implementation of IEquatable<EdiPath>
		/// <summary>
		/// Determines whether the specified <see cref="indice.Edi.EdiPath"/> is equal to the current <see cref="T:indice.Edi.EdiPath"/>.
		/// </summary>
		/// <param name="other">The <see cref="indice.Edi.EdiPath"/> to compare with the current <see cref="T:indice.Edi.EdiPath"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="indice.Edi.EdiPath"/> is equal to the current
		/// <see cref="T:indice.Edi.EdiPath"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(EdiPath other) {
            return other.Segment == Segment && other.ElementIndex == ElementIndex && other.ComponentIndex == ComponentIndex;
        }
		#endregion

		/// <summary>
		/// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:indice.Edi.EdiPath"/>.
		/// </summary>
		/// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:indice.Edi.EdiPath"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="T:indice.Edi.EdiPath"/>;
		/// otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj) {
            if (obj != null && (obj is EdiPath || obj is string)) {
                var other = default(EdiPath);
                if (obj is EdiPath)
                    other = (EdiPath)obj;
                else
                    other = (EdiPath)(string)obj;
                return Equals(other);
            }
            return base.Equals(obj);
        }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.EdiPath"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.EdiPath"/>.</returns>
        public override string ToString() {
            return ToString("C", null); 
        }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.EdiPath"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.EdiPath"/>.</returns>
		/// <param name="format">Format.</param>
        public string ToString(string format) {
            return ToString(format, null);
        }
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.EdiPath"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.EdiPath"/>.</returns>
		/// <param name="formatProvider">Format provider.</param>
        public string ToString(IFormatProvider formatProvider) {
            return ToString("C", formatProvider);
        }
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.EdiPath"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.EdiPath"/>.</returns>
		/// <param name="format">Format.</param>
		/// <param name="formatProvider">Format provider.</param>
        public string ToString(string format, IFormatProvider formatProvider) {
            var formatter = new EdiPathFormat();
            return formatter.Format(format, this, formatProvider);
        }

		/// <summary>
		/// Parse the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
        public static EdiPath Parse(string text) {
            var match = Regex.Match(text, PARSE_PATTERN);
            if (match != null) {
                var segment = match.Groups[1].Value;
                int element;
                int component;
                int.TryParse(match.Groups[3].Value, out element);
                int.TryParse(match.Groups[5].Value, out component);
                return new EdiPath(segment, element, component);
            } else {
                return new EdiPath();
            }
        }

		#region implementation of IComparable<EdiPath>

		/// <summary>
		/// Compares to a given object of the type <see cref="EdiPath"/>.
		/// </summary>
		/// <returns>Indication of the relative position.</returns>
		/// <param name="other">an other <see cref="EdiPath"/> object.</param>
        public int CompareTo(EdiPath other) {
            var result = string.Compare(Segment, other.Segment, StringComparison.OrdinalIgnoreCase);
            if (result == 0) result = ElementIndex.CompareTo(other.ElementIndex);
            if (result == 0) result = ComponentIndex.CompareTo(other.ComponentIndex);
            return result;
        }

		#endregion
        
		/// <summary>
		/// Ops the implicit.
		/// </summary>
		/// <returns>The given value as <see cref="string"/>.</returns>
		/// <param name="value">Value.</param>
        public static implicit operator string (EdiPath value) {
            return value.ToString();
        }

		/// <summary>
		/// Ops the explicit.
		/// </summary>
		/// <returns>The given value as <see cref="EdiPath"/>.</returns>
		/// <param name="value">Value.</param>
        public static explicit operator EdiPath(string value) {
            return EdiPath.Parse(value);
        }
    }
}
