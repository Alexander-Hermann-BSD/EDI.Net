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
	/// Edi property descriptor.
	/// </summary>
    internal class EdiPropertyDescriptor
    {
		/// <summary>
		/// The info.
		/// </summary>
        private readonly PropertyInfo _Info;
        /// <summary>
        /// The attributes.
        /// </summary>
		private readonly List<EdiAttribute> _Attributes;
        /// <summary>
        /// The path info.
        /// </summary>
		private readonly EdiPathAttribute _PathInfo;
        /// <summary>
        /// The condition info.
        /// </summary>
		private readonly EdiConditionAttribute _ConditionInfo;
        /// <summary>
        /// The value info.
        /// </summary>
		private readonly EdiValueAttribute _ValueInfo;
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
		/// Gets the info.
		/// </summary>
		/// <value>The info.</value>
        public PropertyInfo Info {
            get { return _Info; }
        }

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <value>The path.</value>
        public string Path {
            get {
                return _PathInfo?.Path;
            }
        }

		/// <summary>
		/// Gets the segment.
		/// </summary>
		/// <value>The segment.</value>
        public string Segment {
            get {
                return _PathInfo?.Segment;
            }
        }

		/// <summary>
		/// Gets the condition info.
		/// </summary>
		/// <value>The condition info.</value>
        public EdiConditionAttribute ConditionInfo {
            get {
                return _ConditionInfo;
            }
        }

		/// <summary>
		/// Gets the path info.
		/// </summary>
		/// <value>The path info.</value>
        public EdiPathAttribute PathInfo {
            get {
                return _PathInfo;
            }
        }

		/// <summary>
		/// Gets the value info.
		/// </summary>
		/// <value>The value info.</value>
        public EdiValueAttribute ValueInfo {
            get {
                return _ValueInfo;
            }
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
		/// Gets a value indicating whether this <see cref="T:indice.Edi.Serialization.EdiPropertyDescriptor"/> marks segment group.
		/// </summary>
		/// <value><c>true</c> if marks segment group; otherwise, <c>false</c>.</value>
        public bool MarksSegmentGroup {
            get {
                return _SegmentGroupInfo != null;
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiPropertyDescriptor"/> class.
		/// </summary>
		/// <param name="info">Info.</param>
        public EdiPropertyDescriptor(PropertyInfo info) 
            : this(info, null) {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiPropertyDescriptor"/> class.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="attributes">Attributes.</param>
        public EdiPropertyDescriptor(PropertyInfo info, IEnumerable<EdiAttribute> attributes) {
            _Info = info;
            if (attributes == null) {
                attributes = info.GetCustomAttributes<EdiAttribute>()
                                 .Concat(info.PropertyType.GetTypeInfo().GetCustomAttributes<EdiAttribute>());
                if (info.PropertyType.IsCollectionType()) {
                    var itemType = default(Type);
                    if (info.PropertyType.HasElementType) {
                        itemType = info.PropertyType.GetElementType();
                    } else {
                        itemType = Info.PropertyType.GetGenericArguments().First();
                    }
                    attributes = attributes.Concat(itemType.GetTypeInfo().GetCustomAttributes<EdiAttribute>());
                }
            }
            _Attributes = attributes.ToList();
            _PathInfo = Attributes.OfType<EdiPathAttribute>().FirstOrDefault();
            _ConditionInfo = Attributes.OfType<EdiConditionAttribute>().FirstOrDefault();
            _ValueInfo = Attributes.OfType<EdiValueAttribute>().FirstOrDefault();
            _SegmentGroupInfo = Attributes.OfType<EdiSegmentGroupAttribute>().FirstOrDefault();
            if (_ValueInfo != null && _ValueInfo.Path != null && _PathInfo == null) {
                _PathInfo = new EdiPathAttribute(_ValueInfo.Path);
            }
            if (_SegmentGroupInfo != null && _SegmentGroupInfo.StartInternal.Segment != null && _PathInfo == null) {
                _PathInfo = new EdiPathAttribute(_SegmentGroupInfo.StartInternal.Segment);
            }
        }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Serialization.EdiPropertyDescriptor"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Serialization.EdiPropertyDescriptor"/>.</returns>
        public override string ToString() {
            if (ValueInfo != null) {
                return $"Value @ {Path}";
            }
            if (Attributes.Count > 0) {
                return $"{Attributes.InferStructure()} @ {Path}";
            }
            return base.ToString();
        }
    }
}
