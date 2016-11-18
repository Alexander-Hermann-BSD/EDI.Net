using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// <see cref="EdiPathAttribute"/> is used to specify the path. Path is similar to a relative uri. 
    /// ie DTM/0/1 or DTM/0 or even simply DTM
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class EdiPathAttribute : EdiAttribute
    {
		/// <summary>
		/// The path.
		/// </summary>
        private EdiPath _Path;

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>The path.</value>
        public string Path {
            get { return _Path; }
            set { _Path = (EdiPath)value; }
        }

		/// <summary>
		/// Gets the path internal.
		/// </summary>
		/// <value>The path internal.</value>
        internal EdiPath PathInternal {
            get { return _Path; }
        }

		/// <summary>
		/// Gets the segment.
		/// </summary>
		/// <value>The segment.</value>
        public string Segment {
            get {
                return _Path.Segment;
            }
        }

		/// <summary>
		/// Gets the index of the element.
		/// </summary>
		/// <value>The index of the element.</value>
        public int ElementIndex {
            get {
                return _Path.ElementIndex;
            }
        }

		/// <summary>
		/// Gets the index of the component.
		/// </summary>
		/// <value>The index of the component.</value>
        public int ComponentIndex {
            get {
                return _Path.ComponentIndex;
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiPathAttribute"/> class.
		/// </summary>
		/// <param name="path">Path.</param>
        public EdiPathAttribute(string path) 
            : this((EdiPath)path){

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiPathAttribute"/> class.
		/// </summary>
		/// <param name="segment">Segment.</param>
		/// <param name="elementIndex">Element index.</param>
		/// <param name="componentIndex">Component index.</param>
        public EdiPathAttribute(string segment, int elementIndex, int componentIndex) 
            : this(new EdiPath(segment, elementIndex, componentIndex)) {

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiPathAttribute"/> class.
		/// </summary>
		/// <param name="path">Path.</param>
        public EdiPathAttribute(EdiPath path) {
            _Path = path;
        }
        
    }
}
