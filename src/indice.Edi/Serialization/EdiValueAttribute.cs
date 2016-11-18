using System;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// Use <see cref="EdiValueAttribute"/> for any value inside a segment. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EdiValueAttribute : EdiAttribute
    {
		/// <summary>
		/// The picture.
		/// </summary>
        private Picture _picture;
		/// <summary>
		/// The mandatory.
		/// </summary>
        private bool _Mandatory;
		/// <summary>
		/// The description.
		/// </summary>
        private string _Description;
		/// <summary>
		/// The format.
		/// </summary>
        private string _Format;
		/// <summary>
		/// The path.
		/// </summary>
        private string _Path;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:indice.Edi.Serialization.EdiValueAttribute"/> is mandatory.
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

		/// <summary>
		/// Gets or sets the format.
		/// </summary>
		/// <value>The format.</value>
        public string Format {
            get { return _Format; }
            set { _Format = value; }
        }

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>The path.</value>
        public string Path {
            get { return _Path; }
            set { _Path = value; }
        }

		/// <summary>
		/// Gets the picture.
		/// </summary>
		/// <value>The picture.</value>
        public Picture Picture {
            get { return _picture; }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiValueAttribute"/> class.
		/// </summary>
        public EdiValueAttribute()
           : this(default(Picture)) {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiValueAttribute"/> class.
		/// </summary>
		/// <param name="picture">Picture.</param>
        public EdiValueAttribute(string picture)
            : this((Picture)picture) {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Serialization.EdiValueAttribute"/> class.
		/// </summary>
		/// <param name="picture">Picture.</param>
        public EdiValueAttribute(Picture picture) {
            _picture = picture;
        }
        
    }
}
