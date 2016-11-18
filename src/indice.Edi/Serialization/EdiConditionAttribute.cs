using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// In case multiple MessageTypes or Segment types with the same name. <see cref="EdiConditionAttribute"/> is used 
    /// to discriminate the classes based on a component value
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class EdiConditionAttribute : EdiPathAttribute
    {
		/// <summary>
		/// The match value.
		/// </summary>
        private readonly string _MatchValue;

		/// <summary>
		/// Gets the match value.
		/// </summary>
		/// <value>The match value.</value>
        public string MatchValue {
            get {
                return _MatchValue;
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiConditionAttribute"/> class.
		/// </summary>
		/// <param name="matchValue">Match value.</param>
        public EdiConditionAttribute(string matchValue)
            : base(new EdiPath()) {
            _MatchValue = matchValue;
        }
    }
}
