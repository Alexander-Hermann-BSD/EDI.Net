using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
	/// <summary>
	/// Edi structure.
	/// </summary>
    internal class EdiStructure {
		/// <summary>
		/// The type store.
		/// </summary>
        private static readonly ThreadSafeStore<Type, EdiTypeDescriptor> typeStore = new ThreadSafeStore<Type, EdiTypeDescriptor>(GetTypeDescriptor);
        /// <summary>
        /// The container.
        /// </summary>
		private readonly EdiStructureType _Container;
        /// <summary>
        /// The index.
        /// </summary>
		private readonly int _Index;
        /// <summary>
        /// The instance.
        /// </summary>
		private readonly object _Instance;
        /// <summary>
        /// The descriptor.
        /// </summary>
		private readonly EdiTypeDescriptor _Descriptor;
        /// <summary>
        /// The cached reads.
        /// </summary>
		private readonly Queue<EdiEntry> _CachedReads;
        /// <summary>
        /// The is closed.
        /// </summary>
		private bool _isClosed;

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>The container.</value>
        public EdiStructureType Container {
            get { return _Container; }
        }

		/// <summary>
		/// Gets the descriptor.
		/// </summary>
		/// <value>The descriptor.</value>
        public EdiTypeDescriptor Descriptor {
            get { return _Descriptor; }
        }

		/// <summary>
		/// Gets the index.
		/// </summary>
		/// <value>The index.</value>
        public int Index {
            get { return _Index; }
        }

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
        public object Instance {
            get { return _Instance; }
        }

		/// <summary>
		/// Gets the cached reads.
		/// </summary>
		/// <value>The cached reads.</value>
        public Queue<EdiEntry> CachedReads {
            get { return _CachedReads; }
        }

        /// <summary>
        /// This <see cref="EdiStructure"/> is closed and must be removed from stack
        /// </summary>
        public bool IsClosed {
            get { return _isClosed; }
        }

        /// <summary>
        /// This checkes to see if this is a custom sequence of segments.
        /// </summary>
        public bool IsGroup {
            get { return Descriptor.IsSegmentGroup; }
        }

        /// <summary>
        /// The sequence start path.
        /// </summary>
        public EdiPath GroupStart {
            get {
                return Descriptor.SegmentGroupInfo.StartInternal;
            }
        }

        /// <summary>
        /// The sequence end path.
        /// </summary>
        public EdiPath? SequenceEnd {
            get {
                return Descriptor.SegmentGroupInfo.SequenceEndInternal;
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiStructure"/> class.
		/// </summary>
		/// <param name="container">Container.</param>
		/// <param name="instance">Instance.</param>
        public EdiStructure(EdiStructureType container, object instance)
            : this(container, instance, 0, new Queue<EdiEntry>()) {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiStructure"/> class.
		/// </summary>
		/// <param name="container">Container.</param>
		/// <param name="instance">Instance.</param>
		/// <param name="index">Index.</param>
		/// <param name="cache">Cache.</param>
        public EdiStructure(EdiStructureType container, object instance, int index, Queue<EdiEntry> cache) {
            ValidationUtils.ArgumentNotNull(instance, "instance");
            _Container = container;
            _Instance = instance;
            _Index = index;
            _Descriptor = typeStore.Get(instance.GetType());
            _CachedReads = cache;
        }

		/// <summary>
		/// Gets the type descriptor.
		/// </summary>
		/// <returns>The type descriptor.</returns>
		/// <param name="type">Type.</param>
        private static EdiTypeDescriptor GetTypeDescriptor(Type type) => new EdiTypeDescriptor(type);

		/// <summary>
		/// Gets the matching properties.
		/// </summary>
		/// <returns>The matching properties.</returns>
		/// <param name="sructureType">Sructure type.</param>
        public EdiPropertyDescriptor[] GetMatchingProperties(EdiStructureType sructureType) =>
            Descriptor.Properties.Where(p => p.Attributes.OfType(sructureType).Any()).ToArray();

		/// <summary>
		/// Gets the matching properties.
		/// </summary>
		/// <returns>The matching properties.</returns>
		/// <param name="segmentName">Segment name.</param>
        public EdiPropertyDescriptor[] GetMatchingProperties(string segmentName) =>
            Descriptor.Properties.Where(p => p.PathInfo?.PathInternal.Segment == segmentName).ToArray();

		/// <summary>
		/// Gets the ordered properties.
		/// </summary>
		/// <returns>The ordered properties.</returns>
		/// <param name="grammar">Grammar.</param>
        public IEnumerable<EdiPropertyDescriptor> GetOrderedProperties(IEdiGrammar grammar) =>
            GetOrderedProperties(new EdiPathComparer(grammar));

		/// <summary>
		/// Gets the ordered properties.
		/// </summary>
		/// <returns>The ordered properties.</returns>
		/// <param name="comparer">Comparer.</param>
        public IEnumerable<EdiPropertyDescriptor> GetOrderedProperties(IComparer<EdiPath> comparer) =>
            Descriptor.Properties.OrderBy(p => p.PathInfo?.PathInternal ?? default(EdiPath), comparer);
        
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Serialization.EdiStructure"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Serialization.EdiStructure"/>.</returns>
        public override string ToString() {
            var text = new System.Text.StringBuilder();
            text.Append($"{Container}");
            switch (Container) {
                case EdiStructureType.Element:
                    text.Append($" {Descriptor.Attributes.OfType<EdiPathAttribute>().FirstOrDefault()?.PathInternal.ToString("e")}"); // element
                    break;
                case EdiStructureType.SegmentGroup:
                    text.Append($" {GroupStart.ToString("s")}"); // only segment
                    break;
                default:
                    text.Append($" {Descriptor.Attributes.OfType<EdiPathAttribute>().FirstOrDefault()?.PathInternal.ToString("s")}"); // the rest
                    break;
            }
            if (Index > 0)
                text.Append($"[{Index + 1}]");
            return text.ToString();
        }

        /// <summary>
        /// Marks this <see cref="EdiStructure"/> ready for removal from the stack. 
        /// Useful on <seealso cref="EdiStructureType.SegmentGroup"/> where there is a close condition.
        /// </summary>
        public void Close() {
            if (_isClosed)
                throw new EdiException("Cannot close an already closed Structure");
            _isClosed = true;
        }
    }
}
