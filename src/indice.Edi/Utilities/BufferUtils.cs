using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Utilities
{
	/// <summary>
	/// Buffer utils.
	/// </summary>
    internal static class BufferUtils
    {
		/// <summary>
		/// Rents the buffer.
		/// </summary>
		/// <returns>The buffer.</returns>
		/// <param name="bufferPool">Buffer pool.</param>
		/// <param name="minSize">Minimum size.</param>
        public static char[] RentBuffer(IArrayPool<char> bufferPool, int minSize) {
            if (bufferPool == null) {
                return new char[minSize];
            }

            char[] buffer = bufferPool.Rent(minSize);
            return buffer;
        }

		/// <summary>
		/// Returns the buffer.
		/// </summary>
		/// <param name="bufferPool">Buffer pool.</param>
		/// <param name="buffer">Buffer.</param>
        public static void ReturnBuffer(IArrayPool<char> bufferPool, char[] buffer) {
            if (bufferPool == null) {
                return;
            }

            bufferPool.Return(buffer);
        }

		/// <summary>
		/// Ensures the size of the buffer.
		/// </summary>
		/// <returns>The buffer size.</returns>
		/// <param name="bufferPool">Buffer pool.</param>
		/// <param name="size">Size.</param>
		/// <param name="buffer">Buffer.</param>
        public static char[] EnsureBufferSize(IArrayPool<char> bufferPool, int size, char[] buffer) {
            if (bufferPool == null) {
                return new char[size];
            }

            if (buffer != null) {
                bufferPool.Return(buffer);
            }

            return bufferPool.Rent(size);
        }
    }

}
