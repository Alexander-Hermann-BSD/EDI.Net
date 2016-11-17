using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi
{
	/// <summary>
	/// Edi structure type.
	/// </summary>
    public enum EdiStructureType
    {
		/// <summary>
		/// nothing
		/// </summary>
        None = 0,
		/// <summary>
		/// The interchange.
		/// </summary>
        Interchange = 1,
		/// <summary>
		/// The group.
		/// </summary>
        Group = 2,
		/// <summary>
		/// The message.
		/// </summary>
        Message = 3,
		/// <summary>
		/// The segment group.
		/// </summary>
        SegmentGroup = 4,
		/// <summary>
		/// The segment.
		/// </summary>
        Segment = 5,
		/// <summary>
		/// The element.
		/// </summary>
        Element = 6,
    }
}
