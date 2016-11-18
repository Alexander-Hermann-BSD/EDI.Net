#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

namespace indice.Edi.Utilities
{
	/// <summary>
	/// String reference.
	/// </summary>
    internal struct StringReference
    {
		/// <summary>
		/// The chars.
		/// </summary>
        private readonly char[] _chars;
        /// <summary>
        /// The start index.
        /// </summary>
		private readonly int _startIndex;
        /// <summary>
        /// The length.
        /// </summary>
		private readonly int _length;

		/// <summary>
		/// Gets the chars.
		/// </summary>
		/// <value>The chars.</value>
        public char[] Chars {
            get { return _chars; }
        }

		/// <summary>
		/// Gets the start index.
		/// </summary>
		/// <value>The start index.</value>
        public int StartIndex {
            get { return _startIndex; }
        }

		/// <summary>
		/// Gets the length.
		/// </summary>
		/// <value>The length.</value>
        public int Length {
            get { return _length; }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Utilities.StringReference"/> struct.
		/// </summary>
		/// <param name="chars">Chars.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="length">Length.</param>
        public StringReference(char[] chars, int startIndex, int length) {
            _chars = chars;
            _startIndex = startIndex;
            _length = length;
        }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Utilities.StringReference"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Utilities.StringReference"/>.</returns>
        public override string ToString() {
            return new string(_chars, _startIndex, _length);
        }
    }
}
