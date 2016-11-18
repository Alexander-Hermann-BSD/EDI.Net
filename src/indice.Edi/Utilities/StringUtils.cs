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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using System.Linq;

namespace indice.Edi.Utilities
{
	/// <summary>
	/// String utils.
	/// </summary>
    static class StringUtils
    {
		/// <summary>
		/// The carriage return line feed.
		/// </summary>
        public const string CarriageReturnLineFeed = "\r\n";
        /// <summary>
        /// The empty.
        /// </summary>
		public const string Empty = "";
        /// <summary>
        /// The carriage return.
        /// </summary>
		public const char CarriageReturn = '\r';
        /// <summary>
        /// The line feed.
        /// </summary>
		public const char LineFeed = '\n';
        /// <summary>
        /// The tab.
        /// </summary>
		public const char Tab = '\t';

		/// <summary>
		/// Formats the with.
		/// </summary>
		/// <returns>The with.</returns>
		/// <param name="format">Format.</param>
		/// <param name="provider">Provider.</param>
		/// <param name="arg0">Arg0.</param>
        public static string FormatWith(this string format, IFormatProvider provider, object arg0) {
            return format.FormatWith(provider, new[] { arg0 });
        }

		/// <summary>
		/// Formats the with.
		/// </summary>
		/// <returns>The with.</returns>
		/// <param name="format">Format.</param>
		/// <param name="provider">Provider.</param>
		/// <param name="arg0">Arg0.</param>
		/// <param name="arg1">Arg1.</param>
        public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1) {
            return format.FormatWith(provider, new[] { arg0, arg1 });
        }

        /// <summary>
        /// Formats the with.
        /// </summary>
        /// <returns>The with.</returns>
        /// <param name="format">Format.</param>
        /// <param name="provider">Provider.</param>
        /// <param name="arg0">Arg0.</param>
        /// <param name="arg1">Arg1.</param>
        /// <param name="arg2">Arg2.</param>
		public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2) {
            return format.FormatWith(provider, new[] { arg0, arg1, arg2 });
        }

		/// <summary>
		/// Formats the with.
		/// </summary>
		/// <returns>The with.</returns>
		/// <param name="format">Format.</param>
		/// <param name="provider">Provider.</param>
		/// <param name="arg0">Arg0.</param>
		/// <param name="arg1">Arg1.</param>
		/// <param name="arg2">Arg2.</param>
		/// <param name="arg3">Arg3.</param>
        public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2, object arg3) {
            return format.FormatWith(provider, new[] { arg0, arg1, arg2, arg3 });
        }

        /// <summary>
        /// Formats the with.
        /// </summary>
        /// <returns>The with.</returns>
        /// <param name="format">Format.</param>
        /// <param name="provider">Provider.</param>
        /// <param name="args">Arguments.</param>
		private static string FormatWith(this string format, IFormatProvider provider, params object[] args) {
            // leave this a private to force code to use an explicit overload
            // avoids stack memory being reserved for the object array
            if (string.IsNullOrEmpty(format)) {
                throw new ArgumentNullException(nameof(format));
            }
            return string.Format(provider, format, args);
        }

        /// <summary>
        /// Determines whether the string is all white space. Empty string will return false.
        /// </summary>
        /// <param name="s">The string to test whether it is all white space.</param>
        /// <returns>
        /// 	<c>true</c> if the string is all white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWhiteSpace(string s) {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            if (s.Length == 0)
                return false;

            for (int i = 0; i < s.Length; i++) {
                if (!char.IsWhiteSpace(s[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Nulls an empty string.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns>Null if the string was null, otherwise the string unchanged.</returns>
        public static string NullEmptyString(string s) {
            return (string.IsNullOrEmpty(s)) ? null : s;
        }

        /// <summary>
        /// Creates the string writer.
        /// </summary>
        /// <returns>The string writer.</returns>
        /// <param name="capacity">Capacity.</param>
		public static StringWriter CreateStringWriter(int capacity) {
            StringBuilder sb = new StringBuilder(capacity);
            StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);

            return sw;
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <returns>The length.</returns>
        /// <param name="value">Value.</param>
		public static int? GetLength(string value) {
            if (value == null)
                return null;
            else
                return value.Length;
        }

        /// <summary>
        /// Tos the char as unicode.
        /// </summary>
        /// <param name="c">C.</param>
        /// <param name="buffer">Buffer.</param>
		public static void ToCharAsUnicode(char c, char[] buffer) {
            buffer[0] = '\\';
            buffer[1] = 'u';
            buffer[2] = MathUtils.IntToHex((c >> 12) & '\x000f');
            buffer[3] = MathUtils.IntToHex((c >> 8) & '\x000f');
            buffer[4] = MathUtils.IntToHex((c >> 4) & '\x000f');
            buffer[5] = MathUtils.IntToHex(c & '\x000f');
        }

        /// <summary>
        /// Forgivings the case sensitive find.
        /// </summary>
        /// <returns>The case sensitive find.</returns>
        /// <param name="source">Source.</param>
        /// <param name="valueSelector">Value selector.</param>
        /// <param name="testValue">Test value.</param>
        /// <typeparam name="TSource">The 1st type parameter.</typeparam>
		public static TSource ForgivingCaseSensitiveFind<TSource>(this IEnumerable<TSource> source, Func<TSource, string> valueSelector, string testValue) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (valueSelector == null)
                throw new ArgumentNullException(nameof(valueSelector));

            var caseInsensitiveResults = source.Where(s => string.Equals(valueSelector(s), testValue, StringComparison.OrdinalIgnoreCase));
            if (caseInsensitiveResults.Count() <= 1) {
                return caseInsensitiveResults.SingleOrDefault();
            } else {
                // multiple results returned. now filter using case sensitivity
                var caseSensitiveResults = source.Where(s => string.Equals(valueSelector(s), testValue, StringComparison.Ordinal));
                return caseSensitiveResults.SingleOrDefault();
            }
        }

		/// <summary>
		/// Tos the camel case.
		/// </summary>
		/// <returns>The camel case.</returns>
		/// <param name="s">S.</param>
        public static string ToCamelCase(string s) {
            if (string.IsNullOrEmpty(s))
                return s;

            if (!char.IsUpper(s[0]))
                return s;

            char[] chars = s.ToCharArray();

            for (int i = 0; i < chars.Length; i++) {
                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                    break;

#if !(DOTNET || PORTABLE)
                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
#else
                chars[i] = char.ToLowerInvariant(chars[i]);
#endif
            }

            return new string(chars);
        }

        /// <summary>
        /// Ises the high surrogate.
        /// </summary>
        /// <returns><c>true</c>, if high surrogate was ised, <c>false</c> otherwise.</returns>
        /// <param name="c">C.</param>
		public static bool IsHighSurrogate(char c) {
#if !(PORTABLE40 || PORTABLE)
            return char.IsHighSurrogate(c);
#else
            return (c >= 55296 && c <= 56319);
#endif
        }

        /// <summary>
        /// Ises the low surrogate.
        /// </summary>
        /// <returns><c>true</c>, if low surrogate was ised, <c>false</c> otherwise.</returns>
        /// <param name="c">C.</param>
		public static bool IsLowSurrogate(char c) {
#if !(PORTABLE40 || PORTABLE)
            return char.IsLowSurrogate(c);
#else
            return (c >= 56320 && c <= 57343);
#endif
        }

        /// <summary>
        /// Startses the with.
        /// </summary>
        /// <returns><c>true</c>, if with was startsed, <c>false</c> otherwise.</returns>
        /// <param name="source">Source.</param>
        /// <param name="value">Value.</param>
		public static bool StartsWith(this string source, char value) {
            return (source.Length > 0 && source[0] == value);
        }

        /// <summary>
        /// Endses the with.
        /// </summary>
        /// <returns><c>true</c>, if with was endsed, <c>false</c> otherwise.</returns>
        /// <param name="source">Source.</param>
        /// <param name="value">Value.</param>
		public static bool EndsWith(this string source, char value) {
            return (source.Length > 0 && source[source.Length - 1] == value);
        }
    }
}
