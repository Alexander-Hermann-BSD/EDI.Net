using indice.Edi.Serialization;
using indice.Edi.Utilities;
using System;
using System.Collections.Generic;

namespace indice.Edi.Tests.Models.EdiFact01
{
	/// <summary>
	/// DTMP eriod.
	/// </summary>
    public struct DTMPeriod
    {
		/// <summary>
		/// From.
		/// </summary>
        public readonly DateTime From;
        /// <summary>
        /// To.
        /// </summary>
		public readonly DateTime To;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:indice.Edi.Tests.Models.EdiFact01.DTMPeriod"/> struct.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
        public DTMPeriod(DateTime from, DateTime to) {
            From = from;
            To = to;
        }

		/// <summary>
		/// Parse the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
        public static DTMPeriod Parse(string text) {
            var textFrom  = text?.Substring(0, 12);
            var textTo = text?.Substring(12, 12);
            return new DTMPeriod(
                    textFrom.ParseEdiDate("yyyyMMddHHmm"),
                    textTo.ParseEdiDate("yyyyMMddHHmm")
                );
        }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.DTMPeriod"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.DTMPeriod"/>.</returns>
        public override string ToString() {
            return $"{From:yyyyMMddHHmm}{To:yyyyMMddHHmm}";
        }
        
		/// <summary>
		/// Ops the implicit.
		/// </summary>
		/// <returns>The implicit.</returns>
		/// <param name="value">Value.</param>
        public static implicit operator string(DTMPeriod value) {
            return value.ToString();
        }

        // With a cast operator from string --> MyClass or MyStruct 
        // we can convert any edi component value to our custom implementation.
        /// <summary>
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="value">Value.</param>
		public static explicit operator DTMPeriod(string value) {
            return DTMPeriod.Parse(value);
        }
    }

	/// <summary>
	/// Dtm.
	/// </summary>
    [EdiElement, EdiPath("DTM/0")]
    public class DTM
    {
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
        [EdiValue("9(3)", Path = "DTM/0/0")]
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>The date time.</value>
		[EdiValue("X(12)", Path = "DTM/0/1", Format = "yyyyMMddHHmm")]
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
		[EdiValue("9(3)", Path = "DTM/0/2")]
        public int Code { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.DTM"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.DTM"/>.</returns>
        public override string ToString() {
            return DateTime.ToString();
        }
    }
    
	/// <summary>
	/// UTCO ffset.
	/// </summary>
    [EdiElement, EdiPath("DTM/0"), EdiCondition("ZZZ", Path = "DTM/0/0")]
    public class UTCOffset
    {
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
        [EdiValue("X(3)", Path = "DTM/0/0")]
        public int? ID { get; set; }
        /// <summary>
        /// Gets or sets the hours.
        /// </summary>
        /// <value>The hours.</value>
		[EdiValue("9(1)", Path = "DTM/0/1")]
        public int Hours { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
		[EdiValue("9(3)", Path = "DTM/0/2")]
        public int Code { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.UTCOffset"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.UTCOffset"/>.</returns>
        public override string ToString() {
            return Hours.ToString();
        }
    }

	/// <summary>
	/// Period.
	/// </summary>
    [EdiElement, EdiPath("DTM/0"), EdiCondition("324", Path = "DTM/0/0")]
    public class Period
    {
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
        [EdiValue("9(3)", Path = "DTM/0/0")]
        public int ID { get; set; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
        [EdiValue("9(24)", Path = "DTM/0/1")]
        public DTMPeriod Date { get; set; }

		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>The code.</value>
        [EdiValue("9(3)", Path = "DTM/0/2")]
        public int Code { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.Period"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.Period"/>.</returns>
        public override string ToString() {
            return $"{Date.From} | {Date.To}";
        }
    }

	/// <summary>
	/// Item number.
	/// </summary>
    [EdiElement, EdiPath("LIN/2")]
    public class ItemNumber
    {
		/// <summary>
		/// Gets or sets the number.
		/// </summary>
		/// <value>The number.</value>
        [EdiValue("X(1)", Path = "LIN/2/0")]
        public string Number { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
        [EdiValue("9(3)", Path = "LIN/2/1")]
        public string Type { get; set; }

		/// <summary>
		/// Gets or sets the code list qualifier.
		/// </summary>
		/// <value>The code list qualifier.</value>
        [EdiValue("9(3)", Path = "LIN/2/2")]
        public string CodeListQualifier { get; set; }

		/// <summary>
		/// Gets or sets the code list responsible agency.
		/// </summary>
		/// <value>The code list responsible agency.</value>
        [EdiValue("9(3)", Path = "LIN/2/3")]
        public string CodeListResponsibleAgency { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.ItemNumber"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.EdiFact01.ItemNumber"/>.</returns>
        public override string ToString() {
            return $"{Number} {Type} {CodeListQualifier}";
        }
    }

	/// <summary>
	/// Nad.
	/// </summary>
    [EdiSegment, EdiPath("NAD")]
    public class NAD
    {
		/// <summary>
		/// Gets or sets the party qualifier.
		/// </summary>
		/// <value>The party qualifier.</value>
        [EdiValue("X(3)", Path = "NAD/0/0")]
        public string PartyQualifier { get; set; }

		/// <summary>
		/// Gets or sets the party identifier.
		/// </summary>
		/// <value>The party identifier.</value>
        [EdiValue("X(35)", Path = "NAD/1/0")]
        public string PartyId { get; set; }

		/// <summary>
		/// Gets or sets the responsible agency.
		/// </summary>
		/// <value>The responsible agency.</value>
        [EdiValue("X(3)", Path = "NAD/1/2")]
        public string ResponsibleAgency { get; set; }
    }

	/// <summary>
	/// Interchange.
	/// </summary>
    public class Interchange
    {
		/// <summary>
		/// Gets or sets the syntax identifier.
		/// </summary>
		/// <value>The syntax identifier.</value>
        [EdiValue("X(4)", Mandatory = true, Path = "UNB/0")]
        public string SyntaxIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the syntax version.
		/// </summary>
		/// <value>The syntax version.</value>
        [EdiValue("9(1)", Path = "UNB/0/1", Mandatory = true)]
        public int SyntaxVersion { get; set; }

		/// <summary>
		/// Gets or sets the sender identifier.
		/// </summary>
		/// <value>The sender identifier.</value>
        [EdiValue("X(35)", Path = "UNB/1/0", Mandatory = true)]
        public string SenderId { get; set; }
        /// <summary>
        /// Gets or sets the partner IDC ode qualifier.
        /// </summary>
        /// <value>The partner IDC ode qualifier.</value>
		[EdiValue("X(4)", Path = "UNB/1/1", Mandatory = true)]
        public string PartnerIDCodeQualifier { get; set; }
        /// <summary>
        /// Gets or sets the reverse routing address.
        /// </summary>
        /// <value>The reverse routing address.</value>
		[EdiValue("X(14)", Path = "UNB/1/2", Mandatory = false)]
        public string ReverseRoutingAddress { get; set; }

		/// <summary>
		/// Gets or sets the recipient identifier.
		/// </summary>
		/// <value>The recipient identifier.</value>
        [EdiValue("X(35)", Path = "UNB/2/0", Mandatory = true)]
        public string RecipientId { get; set; }

		/// <summary>
		/// Gets or sets the parter IDC ode.
		/// </summary>
		/// <value>The parter IDC ode.</value>
        [EdiValue("X(4)", Path = "UNB/2/1", Mandatory = true)]
        public string ParterIDCode { get; set; }
        /// <summary>
        /// Gets or sets the routing address.
        /// </summary>
        /// <value>The routing address.</value>
		[EdiValue("X(14)", Path = "UNB/2/2", Mandatory = false)]
        public string RoutingAddress { get; set; }

		/// <summary>
		/// Gets or sets the date of preparation.
		/// </summary>
		/// <value>The date of preparation.</value>
        [EdiValue("9(6)", Path = "UNB/3/0", Format = "ddMMyy", Description = "Date of Preparation")]
        [EdiValue("9(4)", Path = "UNB/3/1", Format = "HHmm", Description = "Time or Prep")]
        public DateTime DateOfPreparation { get; set; }

		/// <summary>
		/// Gets or sets the control reference.
		/// </summary>
		/// <value>The control reference.</value>
        [EdiValue("X(14)", Path = "UNB/4", Mandatory = true)]
        public string ControlRef { get; set; }

		/// <summary>
		/// Gets or sets the ack request.
		/// </summary>
		/// <value>The ack request.</value>
        [EdiValue("9(1)", Path = "UNB/8", Mandatory = false)]
        public int AckRequest { get; set; }

		/// <summary>
		/// Gets or sets the quote message.
		/// </summary>
		/// <value>The quote message.</value>
        public Quote QuoteMessage { get; set; }

		/// <summary>
		/// Gets or sets the trailer control count.
		/// </summary>
		/// <value>The trailer control count.</value>
        [EdiValue("X(1)", Path = "UNZ/0")]
        public int TrailerControlCount { get; set; }

		/// <summary>
		/// Gets or sets the trailer control reference.
		/// </summary>
		/// <value>The trailer control reference.</value>
        [EdiValue("X(14)", Path = "UNZ/1")]
        public string TrailerControlReference { get; set; }
    }

	/// <summary>
	/// Quote.
	/// </summary>
    [EdiMessage]
    public class Quote
    {
		/// <summary>
		/// Gets or sets the message reference.
		/// </summary>
		/// <value>The message reference.</value>
        [EdiValue("X(14)", Path = "UNH/0/0")]
        public string MessageRef { get; set; }

		/// <summary>
		/// Gets or sets the type of the message.
		/// </summary>
		/// <value>The type of the message.</value>
        [EdiValue("X(6)", Path = "UNH/1/0")]
        public string MessageType { get; set; }

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
        [EdiValue("X(3)", Path = "UNH/1/1")]
        public string Version { get; set; }

		/// <summary>
		/// Gets or sets the release number.
		/// </summary>
		/// <value>The release number.</value>
        [EdiValue("X(3)", Path = "UNH/1/2")]
        public string ReleaseNumber { get; set; }

		/// <summary>
		/// Gets or sets the controlling agency.
		/// </summary>
		/// <value>The controlling agency.</value>
        [EdiValue("X(2)", Path = "UNH/1/3")]
        public string ControllingAgency { get; set; }

		/// <summary>
		/// Gets or sets the association assigned code.
		/// </summary>
		/// <value>The association assigned code.</value>
        [EdiValue("X(6)", Path = "UNH/1/4")]
        public string AssociationAssignedCode { get; set; }

		/// <summary>
		/// Gets or sets the common access reference.
		/// </summary>
		/// <value>The common access reference.</value>
        [EdiValue("X(35)", Path = "UNH/2/0")]
        public string CommonAccessRef { get; set; }

		/// <summary>
		/// Gets or sets the name of the message.
		/// </summary>
		/// <value>The name of the message.</value>
        [EdiValue("X(3)", Path = "BGM/0/0")]
        public string MessageName { get; set; }

		/// <summary>
		/// Gets or sets the document number.
		/// </summary>
		/// <value>The document number.</value>
        [EdiValue("X(35)", Path = "BGM/1/0")]
        public string DocumentNumber { get; set; }

		/// <summary>
		/// Gets or sets the message function.
		/// </summary>
		/// <value>The message function.</value>
        [EdiValue("X(3)", Path = "BGM/2/0", Mandatory = false)]
        public string MessageFunction { get; set; }

		/// <summary>
		/// Gets or sets the type of the response.
		/// </summary>
		/// <value>The type of the response.</value>
        [EdiValue("X(3)", Path = "BGM/3/0")]
        public string ResponseType { get; set; }

		/// <summary>
		/// Gets or sets the message date.
		/// </summary>
		/// <value>The message date.</value>
        [EdiCondition("137", Path = "DTM/0/0")]
        public DTM MessageDate { get; set; }

		/// <summary>
		/// Gets or sets the processing start date.
		/// </summary>
		/// <value>The processing start date.</value>
        [EdiCondition("163", Path = "DTM/0/0")]
        public DTM ProcessingStartDate { get; set; }

		/// <summary>
		/// Gets or sets the processing end date.
		/// </summary>
		/// <value>The processing end date.</value>
        [EdiCondition("164", Path = "DTM/0/0")]
        public DTM ProcessingEndDate { get; set; }

		/// <summary>
		/// Gets or sets the UTCO ffset.
		/// </summary>
		/// <value>The UTCO ffset.</value>
        public UTCOffset UTCOffset { get; set; }

		/// <summary>
		/// Gets or sets the currency qualifier.
		/// </summary>
		/// <value>The currency qualifier.</value>
        [EdiValue("X(3)", Path = "CUX/0/0")]
        public string CurrencyQualifier { get; set; }

		/// <summary>
		/// Gets or sets the ISOC urrency.
		/// </summary>
		/// <value>The ISOC urrency.</value>
        [EdiValue("X(3)", Path = "CUX/0/1")]
        public string ISOCurrency { get; set; }

		/// <summary>
		/// Gets or sets the nad.
		/// </summary>
		/// <value>The nad.</value>
        public List<NAD> NAD { get; set; }

		/// <summary>
		/// Gets or sets the location qualifier.
		/// </summary>
		/// <value>The location qualifier.</value>
        [EdiValue("X(3)", Path = "LOC/0/0")]
        public string LocationQualifier { get; set; }

		/// <summary>
		/// Gets or sets the location identifier.
		/// </summary>
		/// <value>The location identifier.</value>
        [EdiValue("X(3)", Path = "LOC/1/0")]
        public string LocationId { get; set; }

		/// <summary>
		/// Gets or sets the location responsible agency.
		/// </summary>
		/// <value>The location responsible agency.</value>
        [EdiValue("X(3)", Path = "LOC/1/2")]
        public string LocationResponsibleAgency { get; set; }

		/// <summary>
		/// Gets or sets the lines.
		/// </summary>
		/// <value>The lines.</value>
        public List<LineItem> Lines { get; set; }

		/// <summary>
		/// Gets or sets the uns.
		/// </summary>
		/// <value>The uns.</value>
        [EdiValue("X(1)", Path = "UNS/0/0")]
        public char? UNS { get; set; }

		/// <summary>
		/// Gets or sets the trailer message segments count.
		/// </summary>
		/// <value>The trailer message segments count.</value>
        [EdiValue("X(1)", Path = "UNT/0")]
        public int TrailerMessageSegmentsCount { get; set; }

		/// <summary>
		/// Gets or sets the trailer message reference.
		/// </summary>
		/// <value>The trailer message reference.</value>
        [EdiValue("X(14)", Path = "UNT/1")]
        public string TrailerMessageReference { get; set; }
    }

	/// <summary>
	/// Line item.
	/// </summary>
    [EdiSegment, EdiSegmentGroup("LIN", SequenceEnd = "UNS")]
    public class LineItem
    {
		/// <summary>
		/// Gets or sets the line number.
		/// </summary>
		/// <value>The line number.</value>
        [EdiValue("X(1)", Path = "LIN/0/0")]
        public int LineNumber { get; set; }

		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>The code.</value>
        [EdiValue("9(3)", Path = "LIN/1/0")]
        public string Code { get; set; }

		/// <summary>
		/// Gets or sets the number identification.
		/// </summary>
		/// <value>The number identification.</value>
        public ItemNumber NumberIdentification { get; set; }

		/// <summary>
		/// Gets or sets the period.
		/// </summary>
		/// <value>The period.</value>
        public Period Period { get; set; }

		/// <summary>
		/// Gets or sets the prices.
		/// </summary>
		/// <value>The prices.</value>
        public List<PriceDetails> Prices { get; set; }
    }

	/// <summary>
	/// Price details.
	/// </summary>
    [EdiSegment, EdiSegmentGroup("PRI")]
    public class PriceDetails
    {
		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value>The price.</value>
        public Price Price { get; set; }

		/// <summary>
		/// Gets or sets the range.
		/// </summary>
		/// <value>The range.</value>
        public Range Range { get; set; }

    }

	/// <summary>
	/// Price.
	/// </summary>
    [EdiElement, EdiPath("PRI/0")]
    public class Price
    {
		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>The code.</value>
        [EdiValue("X(3)", Path = "PRI/0/0")]
        public string Code { get; set; }

		/// <summary>
		/// Gets or sets the amount.
		/// </summary>
		/// <value>The amount.</value>
        [EdiValue("X(15)", Path = "PRI/0/1")]
        public decimal? Amount { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
        [EdiValue("X(3)", Path = "PRI/0/2")]
        public string Type { get; set; }
    }

	/// <summary>
	/// Range.
	/// </summary>
    [EdiSegment, EdiPath("RNG")]
    public class Range
    {
		/// <summary>
		/// Gets or sets the measurement unit code.
		/// </summary>
		/// <value>The measurement unit code.</value>
        [EdiValue("X(3)", Path = "RNG/0/0")]
        public string MeasurementUnitCode { get; set; }

		/// <summary>
		/// Gets or sets the minimum.
		/// </summary>
		/// <value>The minimum.</value>
        [EdiValue("X(18)", Path = "RNG/1/0")]
        public decimal? Minimum { get; set; }

		/// <summary>
		/// Gets or sets the maximum.
		/// </summary>
		/// <value>The maximum.</value>
        [EdiValue("X(18)", Path = "RNG/1/1")]
        public decimal? Maximum { get; set; }
    }
}
