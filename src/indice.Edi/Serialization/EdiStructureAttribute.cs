using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// Base class for all structure difining attributes 
    /// </summary>
    public abstract class EdiStructureAttribute : EdiAttribute
    {
		/// <summary>
		/// The mandatory.
		/// </summary>
        private bool _Mandatory;
		/// <summary>
		/// The description.
		/// </summary>
        private string _Description;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:indice.Edi.Serialization.EdiStructureAttribute"/> is mandatory.
		/// </summary>
		/// <value><c>true</c> if mandatory; otherwise, <c>false</c>.</value>
        public bool Mandatory {
            get { return _Mandatory; }
            set { _Mandatory = value; }
        }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}
