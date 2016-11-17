using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace indice.Edi
{
	/// <summary>
	/// Edi position.
	/// </summary>
    internal struct EdiPosition
    {
		/// <summary>
		/// The container type to use
		/// </summary>
        internal EdiContainerType Type;
		/// <summary>
		/// The position.
		/// </summary>
        internal int Position;
		/// <summary>
		/// The name of the segment.
		/// </summary>
        internal string SegmentName;
		/// <summary>
		/// Index available or not..
		/// </summary>
        internal bool HasIndex;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.EdiPosition"/> struct.
		/// </summary>
		/// <param name="type">A container type to use.</param>
        public EdiPosition(EdiContainerType type) {
            Type = type;
            HasIndex = TypeHasIndex(type);
            Position = -1;
            SegmentName = null;
        }

		/// <summary>
		/// Writes to a given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb">given <see cref="StringBuilder"/></param>
        internal void WriteTo(StringBuilder sb) {
            switch (Type) {
                case EdiContainerType.Segment:
                    if (sb.Length > 0)
                        sb.Append('.');

                    string segmentName = SegmentName;
                    sb.Append(segmentName);
                    break;
                case EdiContainerType.Element:
                case EdiContainerType.Component:
                    sb.Append('[');
                    sb.Append(Position);
                    sb.Append(']');
                    break;
            }
        }

		/// <summary>
		/// Does the given <see cref="EdiContainerType"/> has an index?
		/// </summary>
		/// <returns><c>true</c>, if the given <see cref="EdiContainerType"/> has an index, <c>false</c> otherwise.</returns>
		/// <param name="type">A given <see cref="EdiContainerType"/>.</param>
        internal static bool TypeHasIndex(EdiContainerType type) {
            return (type == EdiContainerType.Segment || type == EdiContainerType.Element || type == EdiContainerType.Component);
        }

		/// <summary>
		/// Builds the path.
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="positions">Positions.</param>
        internal static string BuildPath(IEnumerable<EdiPosition> positions) {
            StringBuilder sb = new StringBuilder();

            foreach (EdiPosition state in positions) {
                state.WriteTo(sb);
            }

            return sb.ToString();
        }

		/// <summary>
		/// Formats the message.
		/// </summary>
		/// <returns>The message.</returns>
		/// <param name="lineInfo">Line info.</param>
		/// <param name="path">Path.</param>
		/// <param name="message">Message.</param>
        internal static string FormatMessage(IEdiLineInfo lineInfo, string path, string message) {
            // don't add a fullstop and space when message ends with a new line
            if (!message.EndsWith(Environment.NewLine, StringComparison.Ordinal)) {
                message = message.Trim();

                if (!message.EndsWith('.'))
                    message += ".";

                message += " ";
            }

            message += "Path '{0}'".FormatWith(CultureInfo.InvariantCulture, path);

            if (lineInfo != null && lineInfo.HasLineInfo())
                message += ", line {0}, position {1}".FormatWith(CultureInfo.InvariantCulture, lineInfo.LineNumber, lineInfo.LinePosition);

            message += ".";

            return message;
        }
    }
}
