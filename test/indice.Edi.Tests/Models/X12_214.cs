using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Models
{
	/// <summary>
	/// Transportation 214.
	/// </summary>
    public class Transportation_214
    {
		/// <summary>
		/// Gets or sets the authorization information qualifier.
		/// </summary>
		/// <value>The authorization information qualifier.</value>
        [EdiValue("9(2)", Path = "ISA/0", Description = "I01 - Authorization Information Qualifier")]
        public int AuthorizationInformationQualifier { get; set; }
        /// <summary>
        /// Gets or sets the authorization information.
        /// </summary>
        /// <value>The authorization information.</value>
		[EdiValue("X(10)", Path = "ISA/1", Description = "")]
        public string AuthorizationInformation { get; set; }
        /// <summary>
        /// Gets or sets the security information qualifier.
        /// </summary>
        /// <value>The security information qualifier.</value>
		[EdiValue("9(2)", Path = "ISA/2", Description = "I03 - Security Information Qualifier")]
        public string Security_Information_Qualifier { get; set; }

		/// <summary>
		/// Gets or sets the security information.
		/// </summary>
		/// <value>The security information.</value>
        [EdiValue("X(10)", Path = "ISA/3", Description = "I04 - Security Information")]
        public string Security_Information { get; set; }

		/// <summary>
		/// Gets or sets the identifier qualifier.
		/// </summary>
		/// <value>The identifier qualifier.</value>
        [EdiValue("9(2)", Path = "ISA/4", Description = "I05 - Interchange ID Qualifier")]
        public string ID_Qualifier { get; set; }

		/// <summary>
		/// Gets or sets the sender identifier.
		/// </summary>
		/// <value>The sender identifier.</value>
        [EdiValue("X(15)", Path = "ISA/5", Description = "I06 - Interchange Sender ID")]
        public string Sender_ID { get; set; }

		/// <summary>
		/// Gets or sets the identifier qualifier2.
		/// </summary>
		/// <value>The identifier qualifier2.</value>
        [EdiValue("9(2)", Path = "ISA/6", Description = "I05 - Interchange ID Qualifier")]
        public string ID_Qualifier2 { get; set; }

		/// <summary>
		/// Gets or sets the receiver identifier.
		/// </summary>
		/// <value>The receiver identifier.</value>
        [EdiValue("X(15)", Path = "ISA/7", Description = "I07 - Interchange Receiver ID")]
        public string Receiver_ID { get; set; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
        [EdiValue("9(6)", Path = "ISA/8", Format = "yyMMdd", Description = "I08 - Interchange Date")]
        [EdiValue("9(4)", Path = "ISA/9", Format = "HHmm", Description = "TI09 - Interchange Time")]
        public DateTime Date { get; set; }

		/// <summary>
		/// Gets or sets the control standards identifier.
		/// </summary>
		/// <value>The control standards identifier.</value>
        [EdiValue("X(1)", Path = "ISA/10", Description = "I10 - Interchange Control Standards ID")]
        public string Control_Standards_ID { get; set; }

		/// <summary>
		/// Gets or sets the control version.
		/// </summary>
		/// <value>The control version.</value>
        [EdiValue("9(5)", Path = "ISA/11", Description = "I11 - Interchange Control Version Num")]
        public int ControlVersion { get; set; }

		/// <summary>
		/// Gets or sets the control number.
		/// </summary>
		/// <value>The control number.</value>
        [EdiValue("9(9)", Path = "ISA/12", Description = "I12 - Interchange Control Number")]
        public int ControlNumber { get; set; }

		/// <summary>
		/// Gets or sets the acknowledgement requested.
		/// </summary>
		/// <value>The acknowledgement requested.</value>
        [EdiValue("9(1)", Path = "ISA/13", Description = "I13 - Acknowledgement Requested")]
        public bool? AcknowledgementRequested { get; set; }

		/// <summary>
		/// Gets or sets the usage indicator.
		/// </summary>
		/// <value>The usage indicator.</value>
        [EdiValue("X(1)", Path = "ISA/14", Description = "I14 - Usage Indicator")]
        public string Usage_Indicator { get; set; }

		/// <summary>
		/// Gets or sets the component element separator.
		/// </summary>
		/// <value>The component element separator.</value>
        [EdiValue("X(1)", Path = "ISA/15", Description = "I15 - Component Element Separator")]
        public char? Component_Element_Separator { get; set; }
        /// <summary>
        /// Gets or sets the groups count.
        /// </summary>
        /// <value>The groups count.</value>
		[EdiValue("9(1)", Path = "IEA/0", Description = "I16 - Num of Included Functional Grps")]
        public int GroupsCount { get; set; }

		/// <summary>
		/// Gets or sets the trailer control number.
		/// </summary>
		/// <value>The trailer control number.</value>
        [EdiValue("9(9)", Path = "IEA/1", Description = "I12 - Interchange Control Number")]
        public int TrailerControlNumber { get; set; }

		/// <summary>
		/// Gets or sets the groups.
		/// </summary>
		/// <value>The groups.</value>
        public List<FunctionalGroup> Groups { get; set; }

		/// <summary>
		/// Functional group.
		/// </summary>
        [EdiGroup]
        public class FunctionalGroup
        {
			/// <summary>
			/// Gets or sets the functional identifier code.
			/// </summary>
			/// <value>The functional identifier code.</value>
            [EdiValue("X(2)", Path = "GS/0", Description = "479 - Functional Identifier Code")]
            public string FunctionalIdentifierCode { get; set; }

			/// <summary>
			/// Gets or sets the application sender code.
			/// </summary>
			/// <value>The application sender code.</value>
            [EdiValue("X(15)", Path = "GS/1", Description = "142 - Application Sender's Code")]
            public string ApplicationSenderCode { get; set; }

			/// <summary>
			/// Gets or sets the application receiver code.
			/// </summary>
			/// <value>The application receiver code.</value>
            [EdiValue("X(15)", Path = "GS/2", Description = "124 - Application Receiver's Code")]
            public string ApplicationReceiverCode { get; set; }

			/// <summary>
			/// Gets or sets the date.
			/// </summary>
			/// <value>The date.</value>
            [EdiValue("9(8)", Path = "GS/3", Format = "yyyyMMdd", Description = "373 - Date")]
            [EdiValue("9(4)", Path = "GS/4", Format = "HHmm", Description = "337 - Time")]
            public DateTime Date { get; set; }

			/// <summary>
			/// Gets or sets the group control number.
			/// </summary>
			/// <value>The group control number.</value>
            [EdiValue("9(9)", Path = "GS/5", Format = "HHmm", Description = "28 - Group Control Number")]
            public int GroupControlNumber { get; set; }

			/// <summary>
			/// Gets or sets the agency code.
			/// </summary>
			/// <value>The agency code.</value>
            [EdiValue("X(2)", Path = "GS/6", Format = "HHmm", Description = "455 Responsible Agency Code")]
            public string AgencyCode { get; set; }

			/// <summary>
			/// Gets or sets the version.
			/// </summary>
			/// <value>The version.</value>
            [EdiValue("X(2)", Path = "GS/7", Format = "HHmm", Description = "480 Version / Release / Industry Identifier Code")]
            public string Version { get; set; }

			/// <summary>
			/// Gets or sets the messages.
			/// </summary>
			/// <value>The messages.</value>
            public List<Message> Messages { get; set; }

			/// <summary>
			/// Gets or sets the transactions count.
			/// </summary>
			/// <value>The transactions count.</value>
            [EdiValue("9(1)", Path = "GE/0", Description = "97 Number of Transaction Sets Included")]
            public int TransactionsCount { get; set; }

			/// <summary>
			/// Gets or sets the group trailer control number.
			/// </summary>
			/// <value>The group trailer control number.</value>
            [EdiValue("9(9)", Path = "GE/1", Description = "28 Group Control Number")]
            public int GroupTrailerControlNumber { get; set; }


        }

		/// <summary>
		/// Message.
		/// </summary>
        [EdiMessage]
        public class Message
        {
			/// <summary>
			/// Gets or sets the identifier code.
			/// </summary>
			/// <value>The identifier code.</value>
            [EdiValue("9(3)", Path = "ST/00", Description = "")]
            public int IdentifierCode { get; set; }
            /// <summary>
            /// Gets or sets the control number.
            /// </summary>
            /// <value>The control number.</value>
			[EdiValue("X(9)", Path = "ST/01", Description = "")]
            public string ControlNumber { get; set; }

			/// <summary>
			/// Gets or sets the reference identification.
			/// </summary>
			/// <value>The reference identification.</value>
            [EdiValue("9(30)", Path = "B10/0")]
            public int ReferenceIdentification { get; set; }

			/// <summary>
			/// Gets or sets the places.
			/// </summary>
			/// <value>The places.</value>
            public List<Place> Places { get; set; }
        }

		/// <summary>
		/// Place.
		/// </summary>
        [EdiSegment, EdiSegmentGroup("N1", SequenceEnd = "LX")]
        public class Place
        {
			/// <summary>
			/// Gets or sets the field value1.
			/// </summary>
			/// <value>The field value1.</value>
            [EdiValue("X(9)", Path = "N1/0", Description = "")]
            public string FieldValue1 { get; set; }

			/// <summary>
			/// Gets or sets the field value2.
			/// </summary>
			/// <value>The field value2.</value>
            [EdiValue("X(9)", Path = "N3/0", Description = "")]
            public string FieldValue2 { get; set; }
        }

    }
}
