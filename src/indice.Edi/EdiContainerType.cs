using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi
{
	/// <summary>
	/// Edi container type.
	/// </summary>
    internal enum EdiContainerType
    {
		/// <summary>
		/// nothing.
		/// </summary>
        None = 0,
		/// <summary>
		/// a segment.
		/// </summary>
        Segment = 1,
		/// <summary>
		/// an element.
		/// </summary>
        Element = 2,
		/// <summary>
		/// a component.
		/// </summary>
        Component = 3, 
    }
}
