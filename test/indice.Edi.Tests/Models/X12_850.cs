using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Models
{
    /// <summary>
    /// Purchase Order 850
    /// </summary>
    public class PurchaseOrder_850
    {

        #region ISA and IEA
		/// <summary>
		/// Gets or sets the authorization information qualifier.
		/// </summary>
		/// <value>The authorization information qualifier.</value>
        [EdiValue("9(2)", Path = "ISA/0", Description = "ISA01 - Authorization Information Qualifier")]
        public int AuthorizationInformationQualifier { get; set; }

		/// <summary>
		/// Gets or sets the authorization information.
		/// </summary>
		/// <value>The authorization information.</value>
        [EdiValue("X(10)", Path = "ISA/1", Description = "ISA02 - Authorization Information")]
        public string AuthorizationInformation { get; set; }

		/// <summary>
		/// Gets or sets the security information qualifier.
		/// </summary>
		/// <value>The security information qualifier.</value>
        [EdiValue("9(2)", Path = "ISA/2", Description = "ISA03 - Security Information Qualifier")]
        public string Security_Information_Qualifier { get; set; }

		/// <summary>
		/// Gets or sets the security information.
		/// </summary>
		/// <value>The security information.</value>
        [EdiValue("X(10)", Path = "ISA/3", Description = "ISA04 - Security Information")]
        public string Security_Information { get; set; }

		/// <summary>
		/// Gets or sets the identifier qualifier.
		/// </summary>
		/// <value>The identifier qualifier.</value>
        [EdiValue("9(2)", Path = "ISA/4", Description = "ISA05 - Interchange ID Qualifier")]
        public string ID_Qualifier { get; set; }

		/// <summary>
		/// Gets or sets the sender identifier.
		/// </summary>
		/// <value>The sender identifier.</value>
        [EdiValue("X(15)", Path = "ISA/5", Description = "ISA06 - Interchange Sender ID")]
        public string Sender_ID { get; set; }

		/// <summary>
		/// Gets or sets the identifier qualifier2.
		/// </summary>
		/// <value>The identifier qualifier2.</value>
        [EdiValue("9(2)", Path = "ISA/6", Description = "ISA07 - Interchange ID Qualifier")]
        public string ID_Qualifier2 { get; set; }

		/// <summary>
		/// Gets or sets the receiver identifier.
		/// </summary>
		/// <value>The receiver identifier.</value>
        [EdiValue("X(15)", Path = "ISA/7", Description = "ISA08 - Interchange Receiver ID")]
        public string Receiver_ID { get; set; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
        [EdiValue("9(6)", Path = "ISA/8", Format = "yyMMdd", Description = "I09 - Interchange Date")]
        [EdiValue("9(4)", Path = "ISA/9", Format = "HHmm", Description = "I10 - Interchange Time")]
        public DateTime Date { get; set; }

		/// <summary>
		/// Gets or sets the control standards identifier.
		/// </summary>
		/// <value>The control standards identifier.</value>
        [EdiValue("X(1)", Path = "ISA/10", Description = "ISA11 - Interchange Control Standards ID")]
        public string Control_Standards_ID { get; set; }

		/// <summary>
		/// Gets or sets the control version.
		/// </summary>
		/// <value>The control version.</value>
        [EdiValue("9(5)", Path = "ISA/11", Description = "ISA12 - Interchange Control Version Num")]
        public int ControlVersion { get; set; }

		/// <summary>
		/// Gets or sets the control number.
		/// </summary>
		/// <value>The control number.</value>
        [EdiValue("9(9)", Path = "ISA/12", Description = "ISA13 - Interchange Control Number")]
        public int ControlNumber { get; set; }

		/// <summary>
		/// Gets or sets the acknowledgement requested.
		/// </summary>
		/// <value>The acknowledgement requested.</value>
        [EdiValue("9(1)", Path = "ISA/13", Description = "ISA14 - Acknowledgement Requested")]
        public bool? AcknowledgementRequested { get; set; }

		/// <summary>
		/// Gets or sets the usage indicator.
		/// </summary>
		/// <value>The usage indicator.</value>
        [EdiValue("X(1)", Path = "ISA/14", Description = "ISA15 - Usage Indicator")]
        public string Usage_Indicator { get; set; }

		/// <summary>
		/// Gets or sets the component element separator.
		/// </summary>
		/// <value>The component element separator.</value>
        [EdiValue("X(1)", Path = "ISA/15", Description = "ISA16 - Component Element Separator")]
        public char? Component_Element_Separator { get; set; }

		/// <summary>
		/// Gets or sets the groups count.
		/// </summary>
		/// <value>The groups count.</value>
        [EdiValue("9(1)", Path = "IEA/0", Description = "IEA01 - Num of Included Functional Grps")]
        public int GroupsCount { get; set; }

		/// <summary>
		/// Gets or sets the trailer control number.
		/// </summary>
		/// <value>The trailer control number.</value>
        [EdiValue("9(9)", Path = "IEA/1", Description = "IEA02 - Interchange Control Number")]
        public int TrailerControlNumber { get; set; }

        #endregion
        
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
            [EdiValue("X(2)", Path = "GS/0", Description = "GS01 - Functional Identifier Code")]
            public string FunctionalIdentifierCode { get; set; }

			/// <summary>
			/// Gets or sets the application sender code.
			/// </summary>
			/// <value>The application sender code.</value>
            [EdiValue("X(15)", Path = "GS/1", Description = "GS02 - Application Sender's Code")]
            public string ApplicationSenderCode { get; set; }

			/// <summary>
			/// Gets or sets the application receiver code.
			/// </summary>
			/// <value>The application receiver code.</value>
            [EdiValue("X(15)", Path = "GS/2", Description = "GS03 - Application Receiver's Code")]
            public string ApplicationReceiverCode { get; set; }

			/// <summary>
			/// Gets or sets the date.
			/// </summary>
			/// <value>The date.</value>
            [EdiValue("9(8)", Path = "GS/3", Format = "yyyyMMdd", Description = "GS04 - Date")]
            [EdiValue("9(4)", Path = "GS/4", Format = "HHmm", Description = "GS05 - Time")]
            public DateTime Date { get; set; }

			/// <summary>
			/// Gets or sets the group control number.
			/// </summary>
			/// <value>The group control number.</value>
            [EdiValue("9(9)", Path = "GS/5", Format = "HHmm", Description = "GS06 - Group Control Number")]
            public int GroupControlNumber { get; set; }

			/// <summary>
			/// Gets or sets the agency code.
			/// </summary>
			/// <value>The agency code.</value>
            [EdiValue("X(2)", Path = "GS/6", Format = "HHmm", Description = "GS07 Responsible Agency Code")]
            public string AgencyCode { get; set; }

			/// <summary>
			/// Gets or sets the version.
			/// </summary>
			/// <value>The version.</value>
            [EdiValue("X(2)", Path = "GS/7", Format = "HHmm", Description = "GS08 Version / Release / Industry Identifier Code")]
            public string Version { get; set; }

			/// <summary>
			/// Gets or sets the orders.
			/// </summary>
			/// <value>The orders.</value>
            public List<Order> Orders { get; set; }

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
		/// Order.
		/// </summary>
        [EdiMessage]
        public class Order
        {
            #region Header Trailer
			/// <summary>
			/// Gets or sets the transaction set code.
			/// </summary>
			/// <value>The transaction set code.</value>
            [EdiValue("X(3)", Path = "ST/0", Description = "ST01 - Transaction set ID code")]
            public string TransactionSetCode { get; set; }

			/// <summary>
			/// Gets or sets the transaction set control number.
			/// </summary>
			/// <value>The transaction set control number.</value>
            [EdiValue("X(9)", Path = "ST/1", Description = "ST02 - Transaction set control number")]
            public string TransactionSetControlNumber { get; set; }

			/// <summary>
			/// Gets or sets the segments couts.
			/// </summary>
			/// <value>The segments couts.</value>
            [EdiValue(Path = "SE/0", Description = "SE01 - Number of included segments")]
            public int SegmentsCouts { get; set; }

			/// <summary>
			/// Gets or sets the trailer transaction set control number.
			/// </summary>
			/// <value>The trailer transaction set control number.</value>
            [EdiValue("X(9)", Path = "SE/1", Description = "SE02 - Transaction set control number (same as ST02)")]
            public string TrailerTransactionSetControlNumber { get; set; } 
            #endregion

			/// <summary>
			/// Gets or sets the trans set purpose code.
			/// </summary>
			/// <value>The trans set purpose code.</value>
            [EdiValue("X(2)", Path = "BEG/0", Description = "BEG01 - Trans. Set Purpose Code")]
            public string TransSetPurposeCode { get; set; }

			/// <summary>
			/// Gets or sets the purchase order type code.
			/// </summary>
			/// <value>The purchase order type code.</value>
            [EdiValue("X(2)", Path = "BEG/1", Description = "BEG02 - Purchase Order Type Code")]
            public string PurchaseOrderTypeCode { get; set; }

			/// <summary>
			/// Gets or sets the purchase order number.
			/// </summary>
			/// <value>The purchase order number.</value>
            [EdiValue(Path = "BEG/2", Description = "BEG03 - Purchase Order Number")]
            public string PurchaseOrderNumber { get; set; }

			/// <summary>
			/// Gets or sets the purchase order date.
			/// </summary>
			/// <value>The purchase order date.</value>
            [EdiValue("9(8)", Path = "BEG/4", Format = "yyyyMMdd", Description = "BEG05 - Purchase Order Date")]
            public string PurchaseOrderDate { get; set; }

			/// <summary>
			/// Gets or sets the entity identifier code.
			/// </summary>
			/// <value>The entity identifier code.</value>
            [EdiValue(Path = "CUR/0", Description = "CUR01 - Entity Identifier Code")]
            public string EntityIdentifierCode { get; set; }

			/// <summary>
			/// Gets or sets the currency code.
			/// </summary>
			/// <value>The currency code.</value>
            [EdiValue("X(3)", Path = "CUR/1", Description = "CUR02 - Currency Code")]
            public string CurrencyCode { get; set; }

			/// <summary>
			/// Gets or sets the reference identification qualifier.
			/// </summary>
			/// <value>The reference identification qualifier.</value>
            [EdiValue(Path = "REF/0", Description = "REF01 - Reference Identification Qualifier IA – Vendor Number assigned by Carhartt")]
            public string ReferenceIdentificationQualifier { get; set; }

			/// <summary>
			/// Gets or sets the reference identification.
			/// </summary>
			/// <value>The reference identification.</value>
            [EdiValue(Path = "REF/1", Description = "REF02 - Reference Identification")]
            public string ReferenceIdentification { get; set; }

			/// <summary>
			/// Gets or sets the transportation termscode.
			/// </summary>
			/// <value>The transportation termscode.</value>
            [EdiValue(Path = "FOB/4", Description = "FOB05 - Transportation Terms code")]
            public string TransportationTermscode { get; set; }

			/// <summary>
			/// Gets or sets the location qualifier.
			/// </summary>
			/// <value>The location qualifier.</value>
            [EdiValue(Path = "FOB/5", Description = "FOB06 - Code identifying type of location KL – Port of loading")]
            public string LocationQualifier { get; set; }

			/// <summary>
			/// Gets or sets the terms type code.
			/// </summary>
			/// <value>The terms type code.</value>
            [EdiValue("X(2)", Path = "ITD/0", Description = "ITD01 - Terms Type Code")]
            public string TermsTypeCode { get; set; }

			/// <summary>
			/// Gets or sets the terms basis date code.
			/// </summary>
			/// <value>The terms basis date code.</value>
            [EdiValue(Path = "ITD/1", Description = "ITD02 - Terms Basis Date Code")]
            public string TermsBasisDateCode { get; set; }

			/// <summary>
			/// Gets or sets the terms net days.
			/// </summary>
			/// <value>The terms net days.</value>
            [EdiValue(Path = "ITD/6", Description = "ITD07 - Terms Net Days")]
            public string TermsNetDays { get; set; }

			/// <summary>
			/// Gets or sets the transportation method.
			/// </summary>
			/// <value>The transportation method.</value>
            [EdiValue(Path = "TD5/3", Description = "TD504 - Transportation Method/Type Code")]
            public string TransportationMethod { get; set; }

			/// <summary>
			/// Gets or sets the order header message text.
			/// </summary>
			/// <value>The order header message text.</value>
            [EdiValue(Path = "MSG/0", Description = "MSG01 - Message Text")]
            public string OrderHeaderMessageText { get; set; }

			/// <summary>
			/// Gets or sets the addresses.
			/// </summary>
			/// <value>The addresses.</value>
            public List<Address> Addresses { get; set; }

			/// <summary>
			/// Gets or sets the items.
			/// </summary>
			/// <value>The items.</value>
            public List<OrderDetail> Items { get; set; }
            
			/// <summary>
			/// Gets or sets the total transaction amount.
			/// </summary>
			/// <value>The total transaction amount.</value>
            [EdiValue(Path = "AMT/1", Description = "AMT02 - Total amount of the Purchase Order")]
            public string TotalTransactionAmount { get; set; }

        }

		/// <summary>
		/// Order detail.
		/// </summary>
        [EdiSegment, EdiSegmentGroup("PO1", SequenceEnd = "CTT")]
        public class OrderDetail
        {
			/// <summary>
			/// Gets or sets the order line number.
			/// </summary>
			/// <value>The order line number.</value>
            [EdiValue(Path = "PO1/0", Description = "PO101 - Order Line Number")]
            public string OrderLineNumber { get; set; }

			/// <summary>
			/// Gets or sets the quantity ordered.
			/// </summary>
			/// <value>The quantity ordered.</value>
            [EdiValue(Path = "PO1/1", Description = "PO102 - Quantity Ordered")]
            public decimal QuantityOrdered { get; set; }

			/// <summary>
			/// Gets or sets the unit of measurement.
			/// </summary>
			/// <value>The unit of measurement.</value>
            [EdiValue(Path = "PO1/2", Description = "PO103 - Unit Of Measurement")]
            public string UnitOfMeasurement { get; set; }

			/// <summary>
			/// Gets or sets the unit price.
			/// </summary>
			/// <value>The unit price.</value>
            [EdiValue(Path = "PO1/3", Description = "PO104 - Unit Price")]
            public decimal UnitPrice { get; set; }

			/// <summary>
			/// Gets or sets the buyers partno.
			/// </summary>
			/// <value>The buyers partno.</value>
            [EdiValue(Path = "PO1/8", Description = "PO109 - Buyer’s Part #.")]
            public string BuyersPartno { get; set; }

			/// <summary>
			/// Gets or sets the vendors partno.
			/// </summary>
			/// <value>The vendors partno.</value>
            [EdiValue(Path = "PO1/10", Description = "PO111 - Vendor’s Part #.")]
            public string VendorsPartno { get; set; }

			/// <summary>
			/// Gets or sets the product description.
			/// </summary>
			/// <value>The product description.</value>
            [EdiValue(Path = "PID/4", Description = "PID05 - Product Description")]
            public string ProductDescription { get; set; }

			/// <summary>
			/// Gets or sets the measurement value.
			/// </summary>
			/// <value>The measurement value.</value>
            [EdiValue(Path = "MEA/2", Description = "MEA03 - Measurement Value")]
            public decimal MeasurementValue { get; set; }

			/// <summary>
			/// Gets or sets the available from date.
			/// </summary>
			/// <value>The available from date.</value>
            [EdiCondition("018", Path = "DTM/0/0")]
            public DTM AvailableFromDate { get; set; }

			/// <summary>
			/// Gets or sets the arrival date.
			/// </summary>
			/// <value>The arrival date.</value>
            [EdiCondition("067", Path = "DTM/0/0")]
            public DTM ArrivalDate  { get; set; }

			/// <summary>
			/// Gets or sets the TC 201.
			/// </summary>
			/// <value>The TC 201.</value>
            [EdiValue(Path = "TC2/0", Description = "TC201 - Measurement Value")]
            public string TC201 { get; set; }
            /// <summary>
            /// Gets or sets the message.
            /// </summary>
            /// <value>The message.</value>
			public List<MSG> MSG { get; set; }

        }

		/// <summary>
		/// Address.
		/// </summary>
        [EdiSegment, EdiSegmentGroup("N1", SequenceEnd = "PO1")]
        public class Address
        {
			/// <summary>
			/// Gets or sets the address code.
			/// </summary>
			/// <value>The address code.</value>
            [EdiValue(Path = "N1/0", Description = "N101 - Address Code")]
            public string AddressCode { get; set; }

			/// <summary>
			/// Gets or sets the name of the address.
			/// </summary>
			/// <value>The name of the address.</value>
            [EdiValue(Path = "N1/1", Description = "N102 - Address Name")]
            public string AddressName { get; set; }

			/// <summary>
			/// Gets or sets the address information.
			/// </summary>
			/// <value>The address information.</value>
            [EdiValue(Path = "N3/0", Description = "N301 - Address Information")]
            public string AddressInformation { get; set; }

			/// <summary>
			/// Gets or sets the name of the city.
			/// </summary>
			/// <value>The name of the city.</value>
            [EdiValue(Path = "N4/0", Description = "N401 - City Name")]
            public string CityName { get; set; }

			/// <summary>
			/// Gets or sets the country code.
			/// </summary>
			/// <value>The country code.</value>
            [EdiValue(Path = "N4/3", Description = "N404 - Country Code")]
            public string CountryCode { get; set; }

        }

		/// <summary>
		/// Dtm.
		/// </summary>
        [EdiSegment, EdiPath("DTM")]
        public class DTM
        {
			/// <summary>
			/// Gets or sets the date time qualifier.
			/// </summary>
			/// <value>The date time qualifier.</value>
            [EdiValue(Path = "DTM/0", Description = "DTM01 - Date/Time Qualifier")]
            public string DateTimeQualifier { get; set; }

			/// <summary>
			/// Gets or sets the date.
			/// </summary>
			/// <value>The date.</value>
            [EdiValue("9(8)", Path = "DTM/1", Format = "yyyyMMdd", Description = "DTM02 - Date format =CCYYMMDD")]
            public DateTime Date { get; set; }
        }

		/// <summary>
		/// Message.
		/// </summary>
        [EdiElement, EdiPath("MSG/0")]
        public class MSG
        {
			/// <summary>
			/// Gets or sets the message text.
			/// </summary>
			/// <value>The message text.</value>
            [EdiValue(Path = "MSG/0", Description = "MSG01 - Message Text")]
            public string MessageText { get; set; }
        }

        #region Edi Enumerations
        #endregion
    }
}
