﻿using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace indice.Edi
{
	/// <summary>
	/// Abstract Edi reader. Implements <see cref="IDisposable"/>.
	/// </summary>
    public abstract class EdiReader : IDisposable
    {
        #region Reader State
        /// <summary>
        /// Specifies the state of the reader.
        /// </summary>
        protected internal enum State
        {
            /// <summary>
            /// The Read method has not been called.
            /// </summary>
            Start,

            /// <summary>
            /// The end of the file has been reached successfully.
            /// </summary>
            Complete,

            /// <summary>
            /// The Close method has been called.
            /// </summary>
            Closed,

            /// <summary>
            /// Reader is at the start of a Segment.
            /// </summary>
            SegmentStart,

            /// <summary>
            /// Reader is inside a Segment.
            /// </summary>
            Segment,

            /// <summary>
            /// Reader is at the name of a Segment.
            /// </summary>
            SegmentName,

            /// <summary>
            /// Reader is at the start of an Element.
            /// </summary>
            ElementStart,

            /// <summary>
            /// Reader is inside an Element.
            /// </summary>
            Element,

            /// <summary>
            /// Reader is at the start of a Component.
            /// </summary>
            ComponentStart,

            /// <summary>
            /// Reader is inside a Component.
            /// </summary>
            Component,

            /// <summary>
            /// Reader is in a post value state.
            /// </summary>
            PostValue,

            /// <summary>
            /// An error occurred that prevents the read operation from continuing.
            /// </summary>
            Error,

            /// <summary>
            /// The end of the file has been reached successfully.
            /// </summary>
            Finished
        }
        #endregion

		/// <summary>
		/// current Token data
		/// </summary>
        EdiToken _tokenType;
		/// <summary>
		/// current state
		/// </summary>
        internal State _currentState;
		/// <summary>
		/// The culture to use
		/// </summary>
        private CultureInfo _culture;
		/// <summary>
		/// The grammar.
		/// </summary>
        private readonly IEdiGrammar _grammar;
		/// <summary>
		/// The value.
		/// </summary>
        private object _value;
		/// <summary>
		/// The current position.
		/// </summary>
        private EdiPosition _currentPosition;
		/// <summary>
		/// The stack.
		/// </summary>
        private readonly List<EdiPosition> _stack;
		/// <summary>
		/// The max depth.
		/// </summary>
        private int? _maxDepth;
		/// <summary>
		/// Is an exceeded max depth available?
		/// </summary>
        private bool _hasExceededMaxDepth;
		/// <summary>
		/// The path.
		/// </summary>
        private string _Path;

        /// <summary>
        /// Gets the current reader state.
        /// </summary>
        /// <value>The current reader state.</value>
        protected State CurrentState {
            get { return _currentState; }
        }

        /// <summary>
        /// Gets the <see cref="IEdiGrammar"/> rules for use in the reader.
        /// </summary>
        /// <value>The current reader state.</value>
        public IEdiGrammar Grammar {
            get { return _grammar; }
        }

        /// <summary>
        /// Gets or sets the culture used when reading EDI. Defaults to <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        public CultureInfo Culture {
            get { return _culture ?? CultureInfo.InvariantCulture; }
            set { _culture = value; }
        }

        /// <summary>
        /// Gets or sets the maximum depth allowed when reading EDI. Reading past this depth will throw a <see cref="EdiReaderException"/>.
        /// </summary>
        public int? MaxDepth {
            get { return _maxDepth; }
            set {
                if (value <= 0)
                    throw new ArgumentException("Value must be positive.", nameof(value));

                _maxDepth = value;
            }
        }

        /// <summary>
        /// Gets the type of the current EDI token. 
        /// </summary>
        public virtual EdiToken TokenType {
            get { return _tokenType; }
        }

        /// <summary>
        /// Gets the text value of the current EDI token.
        /// </summary>
        public virtual object Value {
            get { return _value; }
        }

        /// <summary>
        /// Gets The Common Language Runtime (CLR) type for the current EDI token.
        /// </summary>
        public virtual Type ValueType {
            get { return (_value != null) ? _value.GetType() : null; }
        }

        /// <summary>
        /// Gets the depth of the current token in the EDI document.
        /// </summary>
        /// <value>The depth of the current token in the EDI document.</value>
        public virtual int Depth {
            get {
                int depth = _stack.Count;
                if (TokenType.IsStartToken() || _currentPosition.Type == EdiContainerType.None)
                    return depth;
                else
                    return depth + 1;
            }
        }

        /// <summary>
        /// Gets the path of the current EDI token. 
        /// </summary>
        public string Path {
            get {
                return _Path;
            }
        }

        #region EDI Logical Structure
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:indice.Edi.EdiReader"/> is service string advice.
		/// </summary>
		/// <value><c>true</c> if is service string advice; otherwise, <c>false</c>.</value>
        protected bool IsServiceStringAdvice {
            get {
                return Grammar.ServiceStringAdviceTag != null && 
                       TokenType == EdiToken.SegmentName &&
                       _currentPosition.SegmentName == Grammar.ServiceStringAdviceTag;
            }
        }
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:indice.Edi.EdiReader"/> is start interchange.
		/// </summary>
		/// <value><c>true</c> if is start interchange; otherwise, <c>false</c>.</value>
        public bool IsStartInterchange {
            get {
                return TokenType == EdiToken.SegmentName && 
                       _currentPosition.SegmentName == Grammar.InterchangeHeaderTag;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:indice.Edi.EdiReader"/> is end interchange.
        /// </summary>
        /// <value><c>true</c> if is end interchange; otherwise, <c>false</c>.</value>
        public bool IsEndInterchange {
            get {
                return TokenType == EdiToken.SegmentName &&
                       _currentPosition.SegmentName == Grammar.InterchangeTrailerTag;
            }
        }
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:indice.Edi.EdiReader"/> is start group.
		/// </summary>
		/// <value><c>true</c> if is start group; otherwise, <c>false</c>.</value>
        public bool IsStartGroup {
            get {
                return TokenType == EdiToken.SegmentName &&
                       _currentPosition.SegmentName == Grammar.FunctionalGroupHeaderTag;
            }
        }
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:indice.Edi.EdiReader"/> is end group.
		/// </summary>
		/// <value><c>true</c> if is end group; otherwise, <c>false</c>.</value>
        public bool IsEndGroup {
            get {
                return TokenType == EdiToken.SegmentName &&
                       _currentPosition.SegmentName == Grammar.FunctionalGroupTrailerTag;
            }
        }
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:indice.Edi.EdiReader"/> is start message.
		/// </summary>
		/// <value><c>true</c> if is start message; otherwise, <c>false</c>.</value>
        public bool IsStartMessage {
            get {
                return TokenType == EdiToken.SegmentName &&
                       _currentPosition.SegmentName == Grammar.MessageHeaderTag;
            }
        }
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:indice.Edi.EdiReader"/> is end message.
		/// </summary>
		/// <value><c>true</c> if is end message; otherwise, <c>false</c>.</value>
        public bool IsEndMessage {
            get {
                return TokenType == EdiToken.SegmentName &&
                       _currentPosition.SegmentName == Grammar.MessageTrailerTag;
            }
        }
		/// <summary>
		/// Checks the inside segment.
		/// </summary>
		/// <returns><c>true</c>, if inside segment was checked, <c>false</c> otherwise.</returns>
		/// <param name="segmentName">Segment name.</param>
        public virtual bool CheckInsideSegment(string segmentName) {
            if (string.IsNullOrEmpty(segmentName)) {
                throw new ArgumentNullException(nameof(segmentName));
            }
            if (segmentName.Length != 3) {
                throw new ArgumentOutOfRangeException(nameof(segmentName), segmentName, "Unexpected value '{1}' for parameter {0}");
            }
            return Path.StartsWith(segmentName.ToUpperInvariant(), StringComparison.Ordinal);
        }
        #endregion 

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiReader"/> class.
        /// </summary>
        protected EdiReader(IEdiGrammar grammar) {
            if (null == grammar)
                throw new ArgumentNullException(nameof(grammar));
            _currentState = State.Start;
            _grammar = grammar;
            _stack = new List<EdiPosition>(4);
            _maxDepth = 5; // SEGMENT/ELEMENT/COMPONENT/VALUE
        }
		/// <summary>
		/// Gets the current path.
		/// </summary>
		/// <returns>The current path.</returns>
        private string GetCurrentPath() {
            if (_currentPosition.Type == EdiContainerType.None)
                return string.Empty;

            bool startingSegment = _currentState != State.SegmentStart;

            IEnumerable<EdiPosition> positions = (!startingSegment)
                ? _stack
                : _stack.Concat(new[] { _currentPosition });

            return EdiPosition.BuildPath(positions);
        }
		/// <summary>
		/// Push the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
        private void Push(EdiContainerType value) {
            if (_currentPosition.Type == EdiContainerType.None) {
                _currentPosition = new EdiPosition(value);
            } else {
                _stack.Add(_currentPosition);
                _currentPosition = new EdiPosition(value);

                // this is a little hacky because Depth increases when first property/value is written but only testing here is faster/simpler
                if (_maxDepth != null && Depth + 1 > _maxDepth && !_hasExceededMaxDepth) {
                    _hasExceededMaxDepth = true;
                    throw EdiReaderException.Create(this, "The reader's MaxDepth of {0} has been exceeded.".FormatWith(CultureInfo.InvariantCulture, _maxDepth));
                }
            }
        }
		/// <summary>
		/// Pop this instance.
		/// </summary>
        private EdiContainerType Pop() {
            EdiPosition oldPosition;
            if (_stack.Count > 0) {
                oldPosition = _currentPosition;
                _currentPosition = _stack[_stack.Count - 1];
                _stack.RemoveAt(_stack.Count - 1);
            } else {
                oldPosition = _currentPosition;
                _currentPosition = new EdiPosition();
            }
            if (_maxDepth != null && Depth <= _maxDepth)
                _hasExceededMaxDepth = false;

            return oldPosition.Type;
        }

        /// <summary>
        /// Skips the children of the current token.
        /// </summary>
        public void Skip() {
            if (TokenType == EdiToken.SegmentName)
                Read();

            if (TokenType.IsStartToken()) {
                int depth = Depth;

                while (Read() && (depth < Depth)) {
                }
            }
        }
		/// <summary>
		/// Peek this instance.
		/// </summary>
        private EdiContainerType Peek() {
            return _currentPosition.Type;
        }

        /// <summary>
        /// Reads the next EDI token from the stream.
        /// </summary>
        /// <returns>true if the next token was read successfully; false if there are no more tokens to read.</returns>
        public abstract bool Read();

        /// <summary>
        /// Reads the next EDI token from the stream as a <see cref="Nullable{Int32}"/>.
        /// </summary>
        /// <returns>A <see cref="Nullable{Int32}"/>. This method will return <c>null</c> at the end of an array.</returns>
        public abstract int? ReadAsInt32();

        /// <summary>
        /// Reads the next EDI token from the stream as a <see cref="String"/>.
        /// </summary>
        /// <returns>A <see cref="String"/>. This method will return <c>null</c> at the end of an array.</returns>
        public abstract string ReadAsString();

        /// <summary>
        /// Reads the next EDI token from the stream as a <see cref="Nullable{Decimal}"/>.
        /// </summary>
        /// <param name="picture">The <see cref="Nullable{Picture}"/> is the format information needed to parse this into a float</param>
        /// <returns>A <see cref="Nullable{Decimal}"/>. This method will return <c>null</c> at the end of an array.</returns>
        public abstract decimal? ReadAsDecimal(Picture? picture);

        /// <summary>
        /// Reads the next EDI token from the stream as a <see cref="Nullable{DateTime}"/>.
        /// </summary>
        /// <returns>A <see cref="String"/>. This method will return <c>null</c> at the end of an array.</returns>
        public abstract DateTime? ReadAsDateTime();

        #region Read Internal Methods
		/// <summary>
		/// Reads the internal.
		/// </summary>
		/// <returns><c>true</c>, if internal was  read, <c>false</c> otherwise.</returns>
        internal virtual bool ReadInternal() {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Reads as decimal internal.
        /// </summary>
        /// <returns>The as decimal internal.</returns>
        /// <param name="picture">Picture.</param>
        internal decimal? ReadAsDecimalInternal(Picture? picture) {
            EdiToken t;
            if (!ReadInternal()) {
                SetToken(EdiToken.None);
                return null;
            }

            t = TokenType;
            if (t == EdiToken.Null)
                return null;
            if (t == EdiToken.String) {
                string s = (string)Value;
                if (s != null) {
                    s = s.TrimStart('Z'); // Z suppresses leading zeros
                }
                if (string.IsNullOrEmpty(s)) {
                    SetToken(EdiToken.Null);
                    return null;
                }
                decimal d;
                if (s.TryParse(picture, Grammar.DecimalMark, out d)) {
                    SetToken(EdiToken.Float, d, false);
                    return d;
                }
                throw EdiReaderException.Create(this, "Could not convert string to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, Value));
            }
            throw EdiReaderException.Create(this, "Error reading decimal. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, t));
        }
		/// <summary>
		/// Reads as int32 internal.
		/// </summary>
		/// <returns>The as int32 internal.</returns>
        internal int? ReadAsInt32Internal() {
            EdiToken t;
            if (!ReadInternal()) {
                SetToken(EdiToken.None);
                return null;
            } else {
                t = TokenType;
            }
            if (t == EdiToken.Null)
                return null;
            int i;
            if (t == EdiToken.String) {
                string s = (string)Value;
                if (s != null) {
                    s = s.TrimStart('Z'); // Z suppresses leading zeros
                }
                if (string.IsNullOrEmpty(s)) {
                    SetToken(EdiToken.Null);
                    return null;
                }
                if (int.TryParse(s, NumberStyles.Integer, Culture, out i)) {
                    SetToken(EdiToken.Integer, i, false);
                    return i;
                }
                throw EdiReaderException.Create(this, "Could not convert string to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, Value));
            }
            throw EdiReaderException.Create(this, "Error reading integer. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, TokenType));
        }
		/// <summary>
		/// Reads as string internal.
		/// </summary>
		/// <returns>The as string internal.</returns>
        internal string ReadAsStringInternal() {
            EdiToken t;
            if (!ReadInternal()) {
                SetToken(EdiToken.None);
                return null;
            } else {
                t = TokenType;
            }
            if (t == EdiToken.String)
                return (string)Value;

            if (t == EdiToken.Null)
                return null;

            if (t.IsPrimitiveToken()) {
                if (Value != null) {
                    string s;
                    if (Value is IFormattable)
                        s = ((IFormattable)Value).ToString(null, Culture);
                    else
                        s = Value.ToString();

                    SetToken(EdiToken.String, s, false);
                    return s;
                }
            }
            throw EdiReaderException.Create(this, "Error reading string. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, t));
        }
		/// <summary>
		/// Reads as date time internal.
		/// </summary>
		/// <returns>The as date time internal.</returns>
        internal DateTime? ReadAsDateTimeInternal() {
            if (!ReadInternal()) {
                SetToken(EdiToken.None);
                return null;
            }
            if (TokenType == EdiToken.Date)
                return (DateTime)Value;

            if (TokenType == EdiToken.Null)
                return null;

            if (TokenType == EdiToken.String) {
                string s = (string)Value;
                if (string.IsNullOrEmpty(s)) {
                    SetToken(EdiToken.Null);
                    return null;
                }
                DateTime dt;
                
                if (DateTime.TryParse(s, Culture, DateTimeStyles.RoundtripKind, out dt)) {
                    SetToken(EdiToken.Date, dt, false);
                    return dt;
                }
                throw EdiReaderException.Create(this, "Could not convert string to DateTime: {0}.".FormatWith(CultureInfo.InvariantCulture, Value));
            }
            
            throw EdiReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, TokenType));
        } 
        #endregion
		/// <summary>
		/// Gets the position.
		/// </summary>
		/// <returns>The position.</returns>
		/// <param name="depth">Depth.</param>
        internal EdiPosition GetPosition(int depth) {
            if (depth < _stack.Count)
                return _stack[depth];

            return _currentPosition;
        }

        #region Set Token
        /// <summary>
        /// Sets the current token.
        /// </summary>
        /// <param name="newToken">The new token.</param>
        protected void SetToken(EdiToken newToken) {
            SetToken(newToken, null, true);
        }

        /// <summary>
        /// Sets the current token and value.
        /// </summary>
        /// <param name="newToken">The new token.</param>
        /// <param name="value">The value.</param>
        protected void SetToken(EdiToken newToken, object value) {
            SetToken(newToken, value, true);
        }
		/// <summary>
		/// Sets the token.
		/// </summary>
		/// <param name="newToken">New token.</param>
		/// <param name="value">Value.</param>
		/// <param name="updateIndex">If set to <c>true</c> update index.</param>
        internal void SetToken(EdiToken newToken, object value, bool updateIndex) {
            _tokenType = newToken;
            _value = value;

            switch (newToken) {
                case EdiToken.SegmentStart:
                    while (Peek() > EdiContainerType.Segment) {
                        Pop();
                    }
                    if (Peek() == EdiContainerType.None) {
                        Push(EdiContainerType.Segment);
                    }
                    if (updateIndex) {
                        IncrementPosition();
                    }
                    _currentState = State.SegmentStart;
                    break;
                case EdiToken.ElementStart:
                    while (Peek() > EdiContainerType.Element) {
                        Pop();
                    }
                    if (Peek() == EdiContainerType.Segment) {
                        Push(EdiContainerType.Element);
                    }
                    if (updateIndex) {
                        IncrementPosition();
                    }
                    _currentState = State.ElementStart;
                    break;
                case EdiToken.ComponentStart:
                    while (Peek() > EdiContainerType.Component) {
                        Pop();
                    }
                    if (Peek() == EdiContainerType.Element) {
                        Push(EdiContainerType.Component);
                    }
                    if (updateIndex) {
                        IncrementPosition();
                    }
                    _currentState = State.ComponentStart;

                    break;
                case EdiToken.SegmentName:
                    _currentState = State.SegmentName;
                    _currentPosition.SegmentName = (string)value;
                    break;
                case EdiToken.String:
                case EdiToken.Integer:
                case EdiToken.Float:
                case EdiToken.Boolean:
                case EdiToken.Date:
                case EdiToken.Null:
                    SetPostValueState();
                    break;
            }
            if (newToken.IsStartToken()) { 
                _Path = GetCurrentPath();
            }
        } 
        #endregion
		/// <summary>
		/// Sets the state of the post value.
		/// </summary>
        internal void SetPostValueState() {
            if (Peek() != EdiContainerType.None)
                _currentState = State.PostValue;
            else
                SetFinished();
        }
		/// <summary>
		/// Increments the position.
		/// </summary>
        private void IncrementPosition() {
            if (_currentPosition.HasIndex) { 
                _currentPosition.Position++;
            }
        }

        /// <summary>
        /// Sets the finished.
        /// </summary>
        private void SetFinished() {
            _currentState = State.Finished;
        }

        /// <summary>
        /// Changes the <see cref="State"/> to Closed. 
        /// </summary>
        public virtual void Close() {
            _currentState = State.Closed;
            _tokenType = EdiToken.None;
            _value = null;
        }

        #region IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose() {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (_currentState != State.Closed && disposing)
                Close();
        }


        #endregion

    }
}
