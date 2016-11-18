using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Tests
{
	/// <summary>
	/// Helpers.
	/// </summary>
    public static class Helpers
    {
		/// <summary>
		/// The assembly.
		/// </summary>
        private static readonly Assembly _assembly = typeof(EdiTextReaderTests).GetTypeInfo().Assembly;
        /// <summary>
        /// Gets the resource stream.
        /// </summary>
        /// <returns>The resource stream.</returns>
        /// <param name="fileName">File name.</param>
		public static Stream GetResourceStream(string fileName) {
            var qualifiedResources = _assembly.GetManifestResourceNames().OrderBy(x => x).ToArray();
            Stream stream = _assembly.GetManifestResourceStream("indice.Edi.Tests.Samples." + fileName);
            return stream;
        }

		/// <summary>
		/// Streams from string.
		/// </summary>
		/// <returns>The from string.</returns>
		/// <param name="value">Value.</param>
        public static MemoryStream StreamFromString(string value) {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
