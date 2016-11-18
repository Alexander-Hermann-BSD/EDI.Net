using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
	/// <summary>
	/// Edi type descriptor.
	/// </summary>
    class EdiTypeDescriptor
    {
		/// <summary>
		/// The attributes.
		/// </summary>
        private readonly List<EdiAttribute> _Attributes;
        /// <summary>
        /// The properties.
        /// </summary>
		private readonly List<EdiPropertyDescriptor> _Properties;
        /// <summary>
        /// The type of the clr.
        /// </summary>
		private readonly Type _ClrType;
        /// <summary>
        /// The segment group info.
        /// </summary>
		private readonly EdiSegmentGroupAttribute _SegmentGroupInfo;

		/// <summary>
		/// Gets the attributes.
		/// </summary>
		/// <value>The attributes.</value>
        public List<EdiAttribute> Attributes {
            get { return _Attributes; }
        }

		/// <summary>
		/// Gets the properties.
		/// </summary>
		/// <value>The properties.</value>
        public List<EdiPropertyDescriptor> Properties {
            get { return _Properties; }
        }

		/// <summary>
		/// Gets the type of the clr.
		/// </summary>
		/// <value>The type of the clr.</value>
        public Type ClrType {
            get { return _ClrType; }
        }

		/// <summary>
		/// Gets the segment group info.
		/// </summary>
		/// <value>The segment group info.</value>
        public EdiSegmentGroupAttribute SegmentGroupInfo {
            get {
                return _SegmentGroupInfo;
            }
        }

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:indice.Edi.Serialization.EdiTypeDescriptor"/> is segment group.
		/// </summary>
		/// <value><c>true</c> if is segment group; otherwise, <c>false</c>.</value>
        public bool IsSegmentGroup {
            get {
                return SegmentGroupInfo != null;
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiTypeDescriptor"/> class.
		/// </summary>
		/// <param name="clrType">Clr type.</param>
        public EdiTypeDescriptor(Type clrType) {
            _ClrType = clrType;
            _Properties = new List<EdiPropertyDescriptor>();
            var props = ClrType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(pi => new EdiPropertyDescriptor(pi)).Where(pi => pi.Attributes.Any());
            // support for multiple value attributes on the same property. Bit hacky.
            foreach (var p in props) {
                var valueAttributes = p.Attributes.OfType<EdiValueAttribute>().ToArray();
                if (valueAttributes.Length > 1) {
                    foreach (var vAttr in valueAttributes) {
                        _Properties.Add(new EdiPropertyDescriptor(p.Info, p.Attributes.Except(new[] { vAttr })));
                    }
                } else {
                    _Properties.Add(p);
                }
            }
            
            _Attributes = new List<EdiAttribute>();
            Attributes.AddRange(ClrType.GetTypeInfo().GetCustomAttributes<EdiAttribute>());
            _SegmentGroupInfo = Attributes.OfType<EdiSegmentGroupAttribute>().SingleOrDefault();
        }
    }
}
