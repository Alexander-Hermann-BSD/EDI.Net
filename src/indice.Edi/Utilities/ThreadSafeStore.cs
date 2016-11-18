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
using System.Threading;

namespace indice.Edi.Utilities
{
	/// <summary>
	/// Thread safe store.
	/// </summary>
    internal class ThreadSafeStore<TKey, TValue>
    {
		/// <summary>
		/// The lock.
		/// </summary>
        private readonly object _lock = new object();
        /// <summary>
        /// The store.
        /// </summary>
		private Dictionary<TKey, TValue> _store;
        /// <summary>
        /// The creator.
        /// </summary>
		private readonly Func<TKey, TValue> _creator;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Utilities.ThreadSafeStore`2"/> class.
		/// </summary>
		/// <param name="creator">Creator.</param>
        public ThreadSafeStore(Func<TKey, TValue> creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            _creator = creator;
            _store = new Dictionary<TKey, TValue>();
        }

		/// <summary>
		/// Get the specified key.
		/// </summary>
		/// <param name="key">Key.</param>
        public TValue Get(TKey key)
        {
            TValue value;
            if (!_store.TryGetValue(key, out value))
                return AddValue(key);

            return value;
        }

		/// <summary>
		/// Adds the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="key">Key.</param>
        private TValue AddValue(TKey key)
        {
            TValue value = _creator(key);

            lock (_lock)
            {
                if (_store == null)
                {
                    _store = new Dictionary<TKey, TValue>();
                    _store[key] = value;
                }
                else
                {
                    // double check locking
                    TValue checkValue;
                    if (_store.TryGetValue(key, out checkValue))
                        return checkValue;

                    Dictionary<TKey, TValue> newStore = new Dictionary<TKey, TValue>(_store);
                    newStore[key] = value;

#if !(DOTNET || PORTABLE)
                    Thread.MemoryBarrier();
#endif
                    _store = newStore;
                }

                return value;
            }
        }
    }
}