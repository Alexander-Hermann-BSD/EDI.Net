using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi
{
	/// <summary>
	/// Edi grammar. Implements <see cref="IEdiGrammar"/>,
	/// </summary>
    public class EdiGrammar : IEdiGrammar
    {
		/// <summary>
		/// The segment name delimiter.
		/// </summary>
        protected char _SegmentNameDelimiter;
        /// <summary>
        /// The component data element separator.
        /// </summary>
		protected char _ComponentDataElementSeparator;
        /// <summary>
        /// The data element separator.
        /// </summary>
		protected char _DataElementSeparator;
        /// <summary>
        /// The decimal mark.
        /// </summary>
		protected char? _DecimalMark;
        /// <summary>
        /// The release character.
        /// </summary>
		protected char? _ReleaseCharacter;
        /// <summary>
        /// The reserved.
        /// </summary>
		protected char[] _Reserved;
        /// <summary>
        /// The segment terminator.
        /// </summary>
		protected char _SegmentTerminator;

		/// <summary>
		/// The separators.
		/// </summary>
        char[] _separators;
        
		/// <summary>
		/// The service string advice tag.
		/// </summary>
        protected string _ServiceStringAdviceTag;
        /// <summary>
        /// The interchange header tag.
        /// </summary>
		protected string _InterchangeHeaderTag;
        /// <summary>
        /// The functional group header tag.
        /// </summary>
		protected string _FunctionalGroupHeaderTag;
        /// <summary>
        /// The message header tag.
        /// </summary>
		protected string _MessageHeaderTag;
        /// <summary>
        /// The message trailer tag.
        /// </summary>
		protected string _MessageTrailerTag;
        /// <summary>
        /// The functional group trailer tag.
        /// </summary>
		protected string _FunctionalGroupTrailerTag;
        /// <summary>
        /// The interchange trailer tag.
        /// </summary>
		protected string _InterchangeTrailerTag;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.EdiGrammar"/> class.
		/// </summary>
        public EdiGrammar() {
            _ComponentDataElementSeparator = ':';
            _SegmentNameDelimiter = _DataElementSeparator = '+';
            _DecimalMark = '.';
            _ReleaseCharacter = '?';
            _Reserved = new[] { ' ' };
            _SegmentTerminator = '\'';
            
            _ServiceStringAdviceTag = "UNA";
            _InterchangeHeaderTag = "UNB";
            _FunctionalGroupHeaderTag = "UNG";
            _MessageHeaderTag = "UNH";
            _MessageTrailerTag = "UNT";
            _FunctionalGroupTrailerTag = "UNE";
            _InterchangeTrailerTag = "UNZ";
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.EdiGrammar"/> class.
		/// </summary>
		/// <param name="grammar">an existing grammar object.</param>
        public EdiGrammar(IEdiGrammar grammar) {
            _ComponentDataElementSeparator = grammar.ComponentDataElementSeparator;
            _DataElementSeparator = grammar.DataElementSeparator;
            _DecimalMark = grammar.DecimalMark;
            _ReleaseCharacter = grammar.ReleaseCharacter;
            _Reserved = grammar.Reserved.Clone() as char[];
            _SegmentTerminator = grammar.SegmentTerminator;

            _ServiceStringAdviceTag = grammar.ServiceStringAdviceTag;
            _InterchangeHeaderTag = grammar.InterchangeHeaderTag;
            _FunctionalGroupHeaderTag = grammar.FunctionalGroupHeaderTag;
            _MessageHeaderTag = grammar.MessageHeaderTag;
            _MessageTrailerTag = grammar.MessageTrailerTag;
            _FunctionalGroupTrailerTag = grammar.FunctionalGroupTrailerTag;
            _InterchangeTrailerTag = grammar.InterchangeTrailerTag;
        }

        /// <summary>
        /// Gets the separators.
        /// </summary>
        /// <value>The separators.</value>
		public char[] Separators {
            get {
                if (_separators == null) {
                    _separators = new[] {
                        _SegmentNameDelimiter,
                        _ComponentDataElementSeparator,
                        _DataElementSeparator,
                        _SegmentTerminator
                    }.Distinct().ToArray();
                }
                return _separators;
            }
        }

		#region iplementation of IEdiGrammar
        
        /// <summary>
        /// Segment name delimiter is the character used to seperate between a segment name and its elements. Default value <value>'+'</value> same as <see cref="DataElementSeparator"/>
        /// </summary>
        public char SegmentNameDelimiter {
            get { return _SegmentNameDelimiter; }
        }

        /// <summary>
        /// Component data element separator is the "second level" separator of data elements within a message segment. Default value  <value>':'</value>
        /// </summary>
        /// <value>The character used to separate between components</value>
        public char ComponentDataElementSeparator {
            get { return _ComponentDataElementSeparator; }
        }

        /// <summary>
        /// Data element separator is the "first level" separator of data elements within a message segment. Default value <value>'+'</value>
        /// </summary>
        /// <value>An array of possible characters</value>
        public char DataElementSeparator {
            get { return _DataElementSeparator; }
        }

        /// <summary>
        /// Used in EDI-Fact Only. Otherwize null
        /// </summary>
        public char? DecimalMark {
            get { return _DecimalMark; }
        }
        /// <summary>
        /// <para>The release character (analogous to the \ in regular expressions)</para>
        /// is used as a prefix to remove special meaning from the separator, segment termination, 
        /// and release characters when they are used as plain text. Default value is <value>'?'</value>
        /// </summary>
        public char? ReleaseCharacter {
            get { return _ReleaseCharacter; }
        }

        /// <summary>
        /// <para>
        /// These characters are reserved for future use. 
        /// </para>
        /// eg. <see cref="SegmentTerminator" /> or <seealso cref="DataElementSeparator" /> can not be any in this list.
        /// </summary>
        /// <value>An array of possible characters</value>
        public char[] Reserved {
            get { return _Reserved; }
        }

        /// <summary>
        /// Segment terminator indicates the end of a message segment.
        /// </summary>
        public char SegmentTerminator {
            get { return _SegmentTerminator; }
        }

        /// <summary>
        /// Only available in EDI Fact. Otherwize null
        /// </summary>
        public string ServiceStringAdviceTag { get { return _ServiceStringAdviceTag; } }

        /// <summary>
        /// Only available in EDI Fact. Otherwize null
        /// </summary>
        public string InterchangeHeaderTag { get { return _InterchangeHeaderTag; } }

        /// <summary>
        /// Only available in EDI Fact. Otherwize null
        /// </summary>
        public string FunctionalGroupHeaderTag { get { return _FunctionalGroupHeaderTag; } }

        /// <summary>
        /// Only available in EDI Fact. Otherwize null
        /// </summary>
        public string MessageHeaderTag { get { return _MessageHeaderTag; } }

        /// <summary>
        /// Only available in EDI Fact. Otherwize null
        /// </summary>
        public string MessageTrailerTag { get { return _MessageTrailerTag; } }

        /// <summary>
        /// Only available in EDI Fact. Otherwize null
        /// </summary>
        public string FunctionalGroupTrailerTag { get { return _FunctionalGroupTrailerTag; } }

        /// <summary>
        /// Only available in EDI Fact. Otherwize null
        /// </summary>
        public string InterchangeTrailerTag { get { return _InterchangeTrailerTag; } }

        /// <summary>
        /// Checks to see if a character is any of the known special characters.
        /// </summary>
        /// <param name="character"></param>
        /// <returns>True if the character is special. Otherwize false.</returns>
        public bool IsSpecial(char character) {
            return Separators.Contains(character);
        }

        /// <summary>
        /// Populates the Edi grammar delimiters using a eg UNA:+.? '
        /// </summary>
        /// <param name="_chars"></param>
        public void SetAdvice(char[] _chars) {
            _ComponentDataElementSeparator = _chars[0];
            _SegmentNameDelimiter = _DataElementSeparator = _chars[1];
            _DecimalMark = _chars[2];
            _ReleaseCharacter = _chars[3];
            _Reserved = new[] { _chars[4] };
            _SegmentTerminator = _chars[5];

            ///TODO: must figure this out to work both for EDIFact and X12. 
            /// The above is only used by the former. http://stackoverflow.com/a/20112217/61577
        }

        /// <summary>
        /// Populates the Edi grammar delimiters using a eg UNA:+.? '
        /// </summary>
        /// <param name="segmentNameDelimiter">populates <see cref="SegmentNameDelimiter"/></param>
        /// <param name="dataElementSeparator">populates <see cref="DataElementSeparator"/></param>
        /// <param name="componentDataElementSeparator">populates <see cref="ComponentDataElementSeparator"/></param>
        /// <param name="segmentTerminator">populates <see cref="SegmentTerminator"/></param>
        /// <param name="releaseCharacter">populates <see cref="ReleaseCharacter"/></param>
        /// <param name="reserved">populates <see cref="Reserved"/></param>
        /// <param name="decimalMark">populates <see cref="DecimalMark"/> character</param>
        public void SetAdvice(char segmentNameDelimiter,
                              char dataElementSeparator,
                              char componentDataElementSeparator,
                              char segmentTerminator,
                              char? releaseCharacter,
                              char? reserved,
                              char? decimalMark) {
            _ComponentDataElementSeparator = componentDataElementSeparator;
            _DataElementSeparator = dataElementSeparator;
            _SegmentNameDelimiter = segmentNameDelimiter;
            _ReleaseCharacter = releaseCharacter;
            _Reserved = reserved.HasValue ? new[] { reserved.Value } : new char[0];
            _DecimalMark = decimalMark;
            _SegmentTerminator = segmentTerminator;
        }

		#endregion

		/// <summary>
		/// Static method to generate a new EdiFact
		/// </summary>
		/// <returns>The EdiFact.</returns>
        public static IEdiGrammar NewEdiFact() {
            return new EdiGrammar();
        }

		/// <summary>
		/// Static method to generate a new tradacoms.
		/// </summary>
		/// <returns>The tradacoms.</returns>
        public static IEdiGrammar NewTradacoms() {
            return new EdiGrammar() {
                _SegmentNameDelimiter = '=',
                _ComponentDataElementSeparator = ':',
                _DataElementSeparator = '+',
                _DecimalMark = null,
                _ReleaseCharacter = '?',
                _Reserved = new[] { ' ' },
                _SegmentTerminator = '\'',
                _ServiceStringAdviceTag = null,
                _InterchangeHeaderTag = "STX",
                _FunctionalGroupHeaderTag = "BAT",
                _MessageHeaderTag = "MHD",
                _MessageTrailerTag = "MTR",
                _FunctionalGroupTrailerTag = "EOB",
                _InterchangeTrailerTag = "END",
            };
        }
        
		/// <summary>
		/// Static function to generate a new X12.
		/// </summary>
		/// <returns>The new X12.</returns>
        public static IEdiGrammar NewX12() {
            return new EdiGrammar() {
                _SegmentNameDelimiter = '*',
                _ComponentDataElementSeparator = '>',
                _DataElementSeparator = '*',
                _DecimalMark = '.',
                _ReleaseCharacter = null,
                _Reserved = new char[0],
                _SegmentTerminator = '~',
                _ServiceStringAdviceTag = null,
                _InterchangeHeaderTag = "ISA",
                _FunctionalGroupHeaderTag = "GS",
                _MessageHeaderTag = "ST",
                _MessageTrailerTag = "SE",
                _FunctionalGroupTrailerTag = "GE",
                _InterchangeTrailerTag = "IEA",
            };
        }

		/// <summary>
		/// Clone this instance.
		/// </summary>
        public EdiGrammar Clone() {
            return new EdiGrammar(this);
        }
    }
}
