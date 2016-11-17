using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi
{
    /// <summary>
    /// Compares two <see cref="EdiPath"/>s based on their logical structure.
    /// 
    /// <list type="bullet">
    /// <listheader>The resulting order would be:</listheader>
    /// <item><description>ServiceStringAdvice</description></item>
    /// <item><description>InterchangeHeader</description></item>
    /// <item><description>FunctionalGroupHeader</description></item>
    /// <item><description>MessageHeader</description></item>
    /// <item><description>CustomSegments</description></item>
    /// <item><description>MessageTrailer</description></item>
    /// <item><description>FunctionalGroupTrailer</description></item>
    /// <item><description>InterchangeTrailer</description></item>
    /// </list>
    /// </summary>
    public class EdiPathComparer : IComparer<EdiPath>
    {
		/// <summary>
		/// The segment order.
		/// </summary>
        private readonly List<string> segmentOrder;
		/// <summary>
		/// The index of the custom segment.
		/// </summary>
        private readonly int customSegmentIndex;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.EdiPathComparer"/> class.
		/// </summary>
		/// <param name="grammar">Grammar.</param>
		/// <exception cref="ArgumentNullException">If grammar is null</exception>
        public EdiPathComparer(IEdiGrammar grammar) {
            if (null == grammar)
                throw new ArgumentNullException(nameof(grammar));
            segmentOrder = new List<string> {
                grammar.InterchangeHeaderTag,
                grammar.FunctionalGroupHeaderTag,
                grammar.MessageHeaderTag,
                null, // custom segments go here.
                grammar.MessageTrailerTag,
                grammar.FunctionalGroupTrailerTag,
                grammar.InterchangeTrailerTag
            };
            if (!string.IsNullOrWhiteSpace(grammar.ServiceStringAdviceTag)) {
                segmentOrder.Insert(0, grammar.ServiceStringAdviceTag);
                customSegmentIndex = 4;
            } else {
                customSegmentIndex = 3;
            }
        }
        
		#region implementation of IComparer<EdiPath>
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
		/// <returns>A signed integer that indicates the relative values of x and y.</returns>
		public int Compare(EdiPath x, EdiPath y) {
            if (x.Segment != y.Segment) { 
                var i = Rank(x);
                var j = Rank(y);
                return i.CompareTo(j);
            }
            return x.CompareTo(y);
        }
		#endregion

		/// <summary>
		/// Rank the specified path.
		/// </summary>
		/// <remarks>Needed for the implementation of IComparer</remarks>
		/// <param name="path">Path to rank.</param>
		/// <returns>The rank of the given path to the current <see cref="IEdiGrammar"/>.</returns>
        public int Rank(EdiPath path) {
            var i = segmentOrder.IndexOf(path.Segment);
            i = i > -1 ? i : customSegmentIndex;
            return i;
        }
    }
}
