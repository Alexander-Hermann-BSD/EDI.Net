using indice.Edi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indice.Edi.Tests.Models
{
	/// <summary>
	/// Interchange.
	/// </summary>
    public class Interchange
    {
		/// <summary>
		/// Gets or sets the sender code.
		/// </summary>
		/// <value>The sender code.</value>
        [EdiValue("X(14)", Path = "STX/1/0")]
        public string SenderCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the sender.
		/// </summary>
		/// <value>The name of the sender.</value>
        [EdiValue("X(35)", Path = "STX/1/1")]
        public string SenderName { get; set; }

		/// <summary>
		/// Gets or sets the transmission stamp.
		/// </summary>
		/// <value>The transmission stamp.</value>
        [EdiValue("9(6)", Path = "STX/3/0", Format = "yyMMdd", Description = "TRDT - Date")]
        [EdiValue("9(6)", Path = "STX/3/1", Format = "HHmmss", Description = "TRDT - Time")]
        public DateTime TransmissionStamp { get; set; }

		/// <summary>
		/// Gets or sets the head.
		/// </summary>
		/// <value>The head.</value>
        public InterchangeHeader Head { get; set; }

		/// <summary>
		/// Gets or sets the vat.
		/// </summary>
		/// <value>The vat.</value>
        public InterchangeVatSummary Vat { get; set; }

		/// <summary>
		/// Gets or sets the summary.
		/// </summary>
		/// <value>The summary.</value>
        public InterchangeTrailer Summary { get; set; }

		/// <summary>
		/// Gets or sets the invoices.
		/// </summary>
		/// <value>The invoices.</value>
        public List<UtilityBill> Invoices { get; set; }
    }

	/// <summary>
	/// Interchange header.
	/// </summary>
    [EdiMessage, EdiCondition("UTLHDR", Path = "MHD/1")]
    public class InterchangeHeader
    {
		/// <summary>
		/// Gets or sets the transaction code.
		/// </summary>
		/// <value>The transaction code.</value>
        [EdiValue("9(4)"), EdiPath("TYP")]
        public string TransactionCode { get; set; }

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
        [EdiValue("9(1)", Path = "MHD/1/1")]
        public int Version { get; set; }

		/// <summary>
		/// Gets or sets the name of the client.
		/// </summary>
		/// <value>The name of the client.</value>
        [EdiValue("X(40)", Path = "CDT/1")]
        public string ClientName { get; set; }
    }

	/// <summary>
	/// Interchange trailer.
	/// </summary>
    [EdiMessage, EdiCondition("UTLTLR", Path = "MHD/1")]
    public class InterchangeTrailer
    {
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
        [EdiValue("9(1)", Path = "MHD/1/1")]
        public int Version { get; set; }
    }

	/// <summary>
	/// Interchange vat summary.
	/// </summary>
    [EdiMessage, EdiCondition("UVATLR", Path = "MHD/1")]
    public class InterchangeVatSummary
    {
		/// <summary>
		/// Gets or sets the transaction code.
		/// </summary>
		/// <value>The transaction code.</value>
        [EdiValue("9(4)"), EdiPath("TYP")]
        public string TransactionCode { get; set; }

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
        [EdiValue("9(1)", Path = "MHD/1/1")]
        public int Version { get; set; }
    }


	/// <summary>
	/// Utility bill.
	/// </summary>
    [EdiMessage, EdiCondition("UTLBIL", Path = "MHD/1")]
    public class UtilityBill
    {
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
        [EdiValue("9(1)", Path = "MHD/1/1")]
        public int Version { get; set; }

		/// <summary>
		/// Gets or sets the invoice number.
		/// </summary>
		/// <value>The invoice number.</value>
        [EdiValue("X(17)", Path = "BCD/2/0", Description = "INVN - Date")]
        public string InvoiceNumber { get; set; }

		/// <summary>
		/// Gets or sets the meter.
		/// </summary>
		/// <value>The meter.</value>
        public MetetAdminNumber Meter { get; set; }
        /// <summary>
        /// Gets or sets the supply contract.
        /// </summary>
        /// <value>The supply contract.</value>
		public ContractData SupplyContract { get; set; }

		/// <summary>
		/// Gets or sets the bill type code.
		/// </summary>
		/// <value>The bill type code.</value>
        [EdiValue("X(3)", Path = "BCD/5/0", Description = "BTCD - Date")]
        public BillTypeCode BillTypeCode { get; set; }

		/// <summary>
		/// Gets or sets the issue date.
		/// </summary>
		/// <value>The issue date.</value>
        [EdiValue("9(6)", Path = "BCD/1/0", Format = "yyMMdd", Description = "TXDT - Date")]
        public DateTime IssueDate { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>The start date.</value>
        [EdiValue("9(6)", Path = "BCD/7/0", Format = "yyMMdd", Description = "SUMO - Date")]
        public DateTime StartDate { get; set; }

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>The end date.</value>
        [EdiValue("9(6)", Path = "BCD/7/1", Format = "yyMMdd", Description = "SUMO - Date")]
        public DateTime EndDate { get; set; }

		/// <summary>
		/// Gets or sets the totals.
		/// </summary>
		/// <value>The totals.</value>
        public UtilityBillTrailer Totals { get; set; }
        /// <summary>
        /// Gets or sets the vat.
        /// </summary>
        /// <value>The vat.</value>
		public UtilityBillValueAddedTax Vat { get; set; }
        /// <summary>
        /// Gets or sets the charges.
        /// </summary>
        /// <value>The charges.</value>
		public List<ConsumptionChargeCharge> Charges { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.UtilityBill"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.UtilityBill"/>.</returns>
        public override string ToString() {
            return string.Format("{0} TD:{1:d} F:{2:d} T:{3:d} Type:{4}", InvoiceNumber, IssueDate, StartDate, EndDate, BillTypeCode);
        }
    }

	/// <summary>
	/// Consumption charge charge.
	/// </summary>
    [EdiSegment, EdiPath("CCD")]
    public class ConsumptionChargeCharge
    {
        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>The sequence number.</value>
        [EdiValue("9(10)", Path = "CCD/0")]
        public int SequenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the charge indicator.
		/// </summary>
		/// <value>The charge indicator.</value>
        [EdiValue("X(3)", Path = "CCD/1")]
        public ChargeIndicator? ChargeIndicator { get; set; }

		/// <summary>
		/// Gets or sets the article number.
		/// </summary>
		/// <value>The article number.</value>
        [EdiValue("9(13)", Path = "CCD/1/1")]
        public int? ArticleNumber { get; set; }

		/// <summary>
		/// Gets or sets the supplier code.
		/// </summary>
		/// <value>The supplier code.</value>
        [EdiValue("X(3)", Path = "CCD/1/2")]
        public string SupplierCode { get; set; }

		/// <summary>
		/// Gets or sets the tariff code.
		/// </summary>
		/// <value>The tariff code.</value>
        [EdiValue("X(6)", Path = "CCD/2/0", Description = "TCOD")]
        public string TariffCode { get; set; }

		/// <summary>
		/// Gets or sets the tariff description.
		/// </summary>
		/// <value>The tariff description.</value>
        [EdiValue("X(40)", Path = "CCD/2/1", Description = "TCOD")]
        public string TariffDescription { get; set; }

		/// <summary>
		/// Gets or sets the tariff code modifier1.
		/// </summary>
		/// <value>The tariff code modifier1.</value>
        [EdiValue("X(6)", Path = "CCD/3/0", Description = "TMOD")]
        public string TariffCodeModifier1 { get; set; }

		/// <summary>
		/// Gets or sets the tariff code modifier2.
		/// </summary>
		/// <value>The tariff code modifier2.</value>
        [EdiValue("X(6)", Path = "CCD/3/1", Description = "TMOD")]
        public string TariffCodeModifier2 { get; set; }

		/// <summary>
		/// Gets or sets the tariff code modifier3.
		/// </summary>
		/// <value>The tariff code modifier3.</value>
        [EdiValue("X(6)", Path = "CCD/3/2", Description = "TMOD")]
        public string TariffCodeModifier3 { get; set; }

		/// <summary>
		/// Gets or sets the tariff code modifier4.
		/// </summary>
		/// <value>The tariff code modifier4.</value>
        [EdiValue("X(6)", Path = "CCD/3/3", Description = "TMOD")]
        public string TariffCodeModifier4 { get; set; }

		/// <summary>
		/// Gets or sets the meter number.
		/// </summary>
		/// <value>The meter number.</value>
        [EdiValue("X(35)", Path = "CCD/4", Description = "MTNR")]
        public string MeterNumber { get; set; }

		/// <summary>
		/// Gets or sets the meter location.
		/// </summary>
		/// <value>The meter location.</value>
        [EdiValue("X(40)", Path = "CCD/5", Description = "MLOC")]
        public string MeterLocation { get; set; }

		/// <summary>
		/// Gets or sets the present read date.
		/// </summary>
		/// <value>The present read date.</value>
        [EdiValue("9(6)", Path = "CCD/6", Format = "yyMMdd", Description = "PRDT")]
        public DateTime? PresentReadDate { get; set; }

		/// <summary>
		/// Gets or sets the previous read date.
		/// </summary>
		/// <value>The previous read date.</value>
        [EdiValue("9(6)", Path = "CCD/7", Format = "yyMMdd", Description = "PVDT")]
        public DateTime? PreviousReadDate { get; set; }

		/// <summary>
		/// Gets or sets the reading period.
		/// </summary>
		/// <value>The reading period.</value>
        [EdiValue("9(3)", Path = "CCD/8", Description = "NDRP")]
        public int? ReadingPeriod { get; set; }

		/// <summary>
		/// Gets or sets the present reading.
		/// </summary>
		/// <value>The present reading.</value>
        [EdiValue("9(15)", Path = "CCD/9/0", Description = "PRRD")]
        public decimal PresentReading { get; set; }

		/// <summary>
		/// Gets or sets the type of the present reading.
		/// </summary>
		/// <value>The type of the present reading.</value>
        [EdiValue("X(4)", Path = "CCD/9/1", Description = "PRRD")]
        public ReadingDataType? PresentReadingType { get; set; }

		/// <summary>
		/// Gets or sets the previous reading.
		/// </summary>
		/// <value>The previous reading.</value>
        [EdiValue("9(15)", Path = "CCD/9/2", Description = "PRRD")]
        public decimal PreviousReading { get; set; }

		/// <summary>
		/// Gets or sets the type of the previous reading.
		/// </summary>
		/// <value>The type of the previous reading.</value>
        [EdiValue("X(4)", Path = "CCD/9/3", Description = "PRRD")]
        public ReadingDataType? PreviousReadingType { get; set; }

		/// <summary>
		/// Gets or sets the units consumed billing.
		/// </summary>
		/// <value>The units consumed billing.</value>
        [EdiValue("9(10)V9(3)", Path = "CCD/10/0", Description = "CONS")]
        public decimal? UnitsConsumedBilling { get; set; }

		/// <summary>
		/// Gets or sets the unit of measure billing.
		/// </summary>
		/// <value>The unit of measure billing.</value>
        [EdiValue("X(6)", Path = "CCD/10/1", Description = "CONS")]
        public string UnitOfMeasureBilling { get; set; }
        
		/// <summary>
		/// The units negative billing.
		/// </summary>
        private string _UnitsNegativeBilling;
        [EdiValue("X(4)", Path = "CCD/10/2", Description = "CONS")]
        public string UnitsNegativeBilling {
            get { return _UnitsNegativeBilling; }
            set {
                _UnitsNegativeBilling = value;
                if (_UnitsNegativeBilling == "R") {
                    UnitsConsumedBilling = UnitsConsumedBilling * -1;
                }
            }
        }

		/// <summary>
		/// Gets or sets the units consumed base.
		/// </summary>
		/// <value>The units consumed base.</value>
        [EdiValue("9(10)V9(3)", Path = "CCD/11/0", Description = "CONB")]
        public decimal? UnitsConsumedBase { get; set; }

		/// <summary>
		/// Gets or sets the unit of measure base.
		/// </summary>
		/// <value>The unit of measure base.</value>
        [EdiValue("X(6)", Path = "CCD/11/1", Description = "CONB")]
        public string UnitOfMeasureBase { get; set; }
        
		/// <summary>
		/// The units negative base.
		/// </summary>
        private string _UnitsNegativeBase;
        /// <summary>
        /// Gets or sets the units negative base.
        /// </summary>
        /// <value>The units negative base.</value>
		[EdiValue("X(4)", Path = "CCD/11/2", Description = "CONB")]
        public string UnitsNegativeBase {
            get { return _UnitsNegativeBase; }
            set {
                _UnitsNegativeBase = value;
                if (_UnitsNegativeBase == "R") {
                    UnitsConsumedBase = UnitsConsumedBase * -1;
                }
            }
        }

		/// <summary>
		/// Gets or sets the adjustment factor code.
		/// </summary>
		/// <value>The adjustment factor code.</value>
        [EdiValue("X(3)", Path = "CCD/12/0", Description = "ADJF")]
        public string AdjustmentFactorCode { get; set; }

		/// <summary>
		/// Gets or sets the adjustment factor value.
		/// </summary>
		/// <value>The adjustment factor value.</value>
        [EdiValue("9(10)V9(5)", Path = "CCD/12/1", Description = "ADJF")]
        public decimal AdjustmentFactorValue { get; set; }

		/// <summary>
		/// Gets or sets the adjustment factor negative indicator.
		/// </summary>
		/// <value>The adjustment factor negative indicator.</value>
        [EdiValue("X(4)", Path = "CCD/12/2", Description = "ADJF")]
        public string AdjustmentFactorNegativeIndicator { get; set; }

		/// <summary>
		/// Gets or sets the units consumed adjusted.
		/// </summary>
		/// <value>The units consumed adjusted.</value>
        [EdiValue("9(10)V9(3)", Path = "CCD/13/0", Description = "CONA")]
        public decimal UnitsConsumedAdjusted { get; set; }

		/// <summary>
		/// Gets or sets the unit of measure adjusted.
		/// </summary>
		/// <value>The unit of measure adjusted.</value>
        [EdiValue("X(6)", Path = "CCD/13/1", Description = "CONA")]
        public string UnitOfMeasureAdjusted { get; set; }
        
		/// <summary>
		/// Gets or sets the negative indicator adjusted.
		/// </summary>
		/// <value>The negative indicator adjusted.</value>
        [EdiValue("X(4)", Path = "CCD/13/2", Description = "CONA")]
        public string NegativeIndicatorAdjusted { get; set; }

		/// <summary>
		/// Gets or sets the base price unit.
		/// </summary>
		/// <value>The base price unit.</value>
        [EdiValue("9(10)V9(5)", Path = "CCD/14", Description = "BPRI")]
        public decimal? BasePriceUnit { get; set; }

		/// <summary>
		/// Gets or sets the units billed.
		/// </summary>
		/// <value>The units billed.</value>
        [EdiValue("9(10)V9(3)", Path = "CCD/15/0", Description = "NUCT")]
        public decimal UnitsBilled { get; set; }

		/// <summary>
		/// Gets or sets the unit of measure billed.
		/// </summary>
		/// <value>The unit of measure billed.</value>
        [EdiValue("X(6)", Path = "CCD/15/1", Description = "NUCT")]
        public string UnitOfMeasureBilled { get; set; }
       
		/// <summary>
		/// Gets or sets the negative indicator billed.
		/// </summary>
		/// <value>The negative indicator billed.</value>
        [EdiValue("X(4)", Path = "CCD/15/2", Description = "NUCT")]
        public string NegativeIndicatorBilled { get; set; }

		/// <summary>
		/// Gets or sets the charge start date.
		/// </summary>
		/// <value>The charge start date.</value>
        [EdiValue("9(6)", Path = "CCD/16", Format = "yyMMdd", Description = "CSDT")]
        public DateTime ChargeStartDate { get; set; }

		/// <summary>
		/// Gets or sets the charge end date.
		/// </summary>
		/// <value>The charge end date.</value>
        [EdiValue("9(6)", Path = "CCD/17", Format = "yyMMdd", Description = "CEDT")]
        public DateTime ChargeEndDate { get; set; }

		/// <summary>
		/// Gets or sets the price per unit.
		/// </summary>
		/// <value>The price per unit.</value>
        [EdiValue("9(10)V9(5)", Path = "CCD/18", Description = "CPPU")]
        public decimal? PricePerUnit { get; set; }

		/// <summary>
		/// Gets or sets the total type of the charge for charge.
		/// </summary>
		/// <value>The total type of the charge for charge.</value>
        [EdiValue("9(10)V9(2)", Path = "CCD/18/0", Description = "CTOT")]
        public decimal TotalChargeForChargeType { get; set; }

		/// <summary>
		/// Gets or sets the total charge credit indicator.
		/// </summary>
		/// <value>The total charge credit indicator.</value>
        [EdiValue("X(4)", Path = "CCD/18/1", Description = "CTOT")]
        public string TotalChargeCreditIndicator { get; set; }

		/// <summary>
		/// Gets or sets the vat type of supply.
		/// </summary>
		/// <value>The vat type of supply.</value>
        [EdiValue("X(1)", Path = "CCD/19", Description = "TSUP")]
        public string VatTypeOfSupply { get; set; }

		/// <summary>
		/// Gets or sets the vat rate category code.
		/// </summary>
		/// <value>The vat rate category code.</value>
        [EdiValue("X(1)", Path = "CCD/20", Description = "VATC")]
        public VatRateCategoryCode? VatRateCategoryCode { get; set; }

		/// <summary>
		/// Gets or sets the vat rate percentage.
		/// </summary>
		/// <value>The vat rate percentage.</value>
        [EdiValue("9(3)V9(3)", Path = "CCD/21", Description = "VATP")]
        public string VatRatePercentage { get; set; }

		/// <summary>
		/// Gets or sets the meter sub address code.
		/// </summary>
		/// <value>The meter sub address code.</value>
        [EdiValue("X(17)", Path = "CCD/22/0", Description = "MSAD")]
        public string MeterSubAddressCode { get; set; }

		/// <summary>
		/// Gets or sets the meter sub address line.
		/// </summary>
		/// <value>The meter sub address line.</value>
        [EdiValue("X(40)", Path = "CCD/22/1", Description = "MSAD")]
        public string MeterSubAddressLine { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.ConsumptionChargeCharge"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.ConsumptionChargeCharge"/>.</returns>
        public override string ToString() {
            return string.Format("SeqNum:{0} Type:{1}", SequenceNumber, ChargeIndicator);
        }
    }

	/// <summary>
	/// Utility bill trailer.
	/// </summary>
    [EdiSegment, EdiPath("BTL")]
    public class UtilityBillTrailer
    {
        //[EdiValue("9(10)", Path = "BTL/0")]
        //public decimal TotalPaymentDetails { get; set; }

		/// <summary>
		/// Gets or sets the total charge before vat.
		/// </summary>
		/// <value>The total charge before vat.</value>
        [EdiValue("9(10)", Path = "BTL/1")]
        public decimal TotalChargeBeforeVat { get; set; }

		/// <summary>
		/// Gets or sets the bill total vat ammout payable.
		/// </summary>
		/// <value>The bill total vat ammout payable.</value>
        [EdiValue("9(10)", Path = "BTL/2")]
        public decimal BillTotalVatAmmoutPayable { get; set; }

        //[EdiValue("9(10)", Path = "BTL/3")]
        //public decimal BalanceBroughtForward { get; set; }

		/// <summary>
		/// Gets or sets the total bill amount payable.
		/// </summary>
		/// <value>The total bill amount payable.</value>
        [EdiValue("9(10)", Path = "BTL/4")]
        public decimal TotalBillAmountPayable { get; set; }
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.UtilityBillTrailer"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.UtilityBillTrailer"/>.</returns>
		public override string ToString() {
            return string.Format("Net:{0} Vat:{1} Gross:{2}", TotalChargeBeforeVat, BillTotalVatAmmoutPayable, TotalBillAmountPayable);
        }
    }

	/// <summary>
	/// Metet admin number.
	/// </summary>
    [EdiSegment, EdiPath("MAN")]
    public class MetetAdminNumber
    {
		/// <summary>
		/// The distributors identifier.
		/// </summary>
        static Dictionary<string, string> _distributorsIdentifier = new Dictionary<string, string>();
        /// <summary>
        /// Initializes a new instance of the <see cref="T:indice.Edi.Tests.Models.MetetAdminNumber"/> class.
        /// </summary>
		public MetetAdminNumber() {
            if (_distributorsIdentifier == null || _distributorsIdentifier.Count() == 0) {
                _distributorsIdentifier.Add("10", "Eastern Electricity");
                _distributorsIdentifier.Add("11", "East Midlands Electricity");
                _distributorsIdentifier.Add("12", "London Electricity");
                _distributorsIdentifier.Add("13", "Manweb");
                _distributorsIdentifier.Add("14", "Midlands Electricity");
                _distributorsIdentifier.Add("15", "Northern Electricity");
                _distributorsIdentifier.Add("16", "NORWEB");
                _distributorsIdentifier.Add("17", "Scottish Hydro Electric");
                _distributorsIdentifier.Add("18", "Scottish Power");
                _distributorsIdentifier.Add("19", "SEEBOARD");
                _distributorsIdentifier.Add("20", "Southern Electric");
                _distributorsIdentifier.Add("21", "SWALEC");
                _distributorsIdentifier.Add("22", "South Western Electricity");
                _distributorsIdentifier.Add("23", "Yorkshire Electricity");
                _distributorsIdentifier.Add("TR", "TRANSCO");
            }
        }
        /// <summary>
        /// Gets or sets the first level sequence number.
        /// </summary>
        /// <value>The first level sequence number.</value>
		[EdiValue("9(10)", Path = "MAN/0", Description = "SEQA")]
        public int FirstLevelSequenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the second level sequence number.
		/// </summary>
		/// <value>The second level sequence number.</value>
        [EdiValue("9(10)", Path = "MAN/1", Description = "SEQB")]
        public int SecondLevelSequenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the distributor identifier.
		/// </summary>
		/// <value>The distributor identifier.</value>
        [EdiValue("X(2)", Path = "MAN/2/0", Description = "MADN")]
        public string DistributorIdentifier { get; set; }
        /// <summary>
        /// Gets the name of the distributor.
        /// </summary>
        /// <value>The name of the distributor.</value>
		public string DistributorName {
            get {
                if (_distributorsIdentifier.ContainsKey(DistributorIdentifier))
                    return _distributorsIdentifier[DistributorIdentifier];
                else
                    throw new ArgumentOutOfRangeException(DistributorIdentifier, "Unregistered distributor identifier");

            }
        }

		/// <summary>
		/// Gets or sets the unique reference number.
		/// </summary>
		/// <value>The unique reference number.</value>
        [EdiValue("X(10)", Path = "MAN/2/1", Description = "MADN")]
        public string UniqueReferenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the check digit.
		/// </summary>
		/// <value>The check digit.</value>
        [EdiValue("9(1)", Path = "MAN/2/2", Description = "MADN")]
        public int? CheckDigit { get; set; }

		/// <summary>
		/// Gets or sets the type of the profile.
		/// </summary>
		/// <value>The type of the profile.</value>
        [EdiValue("9(2)", Path = "MAN/2/3", Description = "MADN")]
        public int? ProfileType { get; set; }

		/// <summary>
		/// Gets or sets the meter time switch details.
		/// </summary>
		/// <value>The meter time switch details.</value>
        [EdiValue("9(3)", Path = "MAN/2/4", Description = "MADN")]
        public int? MeterTimeSwitchDetails { get; set; }

		/// <summary>
		/// Gets or sets the line loss factor.
		/// </summary>
		/// <value>The line loss factor.</value>
        [EdiValue("9(3)", Path = "MAN/2/5", Description = "MADN")]
        public int? LineLossFactor { get; set; }

		/// <summary>
		/// Gets or sets the meter serial number.
		/// </summary>
		/// <value>The meter serial number.</value>
        [EdiValue("X(35)", Path = "MAN/3", Description = "MTNR")]
        public string MeterSerialNumber { get; set; }

		/// <summary>
		/// Gets or sets the number of digits.
		/// </summary>
		/// <value>The number of digits.</value>
        [EdiValue("9(1)", Path = "MAN/4", Description = "NDIG")]
        public int? NumberOfDigits { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.MetetAdminNumber"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.MetetAdminNumber"/>.</returns>
        public override string ToString() {
            return string.Format("SN:{0} PC:{1} LLFC:{2} UniqueId:{3} Distributor:{4}", MeterSerialNumber, ProfileType, LineLossFactor, UniqueReferenceNumber, DistributorIdentifier);
        }

    }

	/// <summary>
	/// Utility bill value added tax.
	/// </summary>
    [EdiSegment, EdiPath("VAT")]
    public class UtilityBillValueAddedTax
    {
		/// <summary>
		/// Gets or sets the first level sequence number.
		/// </summary>
		/// <value>The first level sequence number.</value>
        [EdiValue("9(10)", Path = "VAT/0", Description = "SEQA")]
        public int FirstLevelSequenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the number of days.
		/// </summary>
		/// <value>The number of days.</value>
        [EdiValue("9(3)", Path = "VAT/1", Description = "NDVT")]
        public int? NumberOfDays { get; set; }

		/// <summary>
		/// Gets or sets the percentage qualifying for.
		/// </summary>
		/// <value>The percentage qualifying for.</value>
        [EdiValue("9(3)V9(3)", Path = "VAT/2", Description = "PNDP")]
        public decimal? PercentageQualifyingFor { get; set; }

		/// <summary>
		/// Gets or sets the vat rate category code.
		/// </summary>
		/// <value>The vat rate category code.</value>
        [EdiValue("X(1)", Path = "VAT/3", Description = "VATC")]
        public VatRateCategoryCode VatRateCategoryCode { get; set; }

		/// <summary>
		/// Gets or sets the vat rate percentage.
		/// </summary>
		/// <value>The vat rate percentage.</value>
        [EdiValue("9(3)", Path = "VAT/4", Description = "VATP")]
        public decimal VatRatePercentage { get; set; }

		/// <summary>
		/// Gets or sets the total charge before vat.
		/// </summary>
		/// <value>The total charge before vat.</value>
        [EdiValue("9(10)", Path = "VAT/5/0", Description = "UVLA")]
        public decimal TotalChargeBeforeVat { get; set; }

		/// <summary>
		/// Gets or sets the vat ammount payable.
		/// </summary>
		/// <value>The vat ammount payable.</value>
        [EdiValue("9(10)", Path = "VAT/6/0", Description = "UVTT")]
        public decimal VatAmmountPayable { get; set; }

		/// <summary>
		/// Gets or sets the total charge including vat.
		/// </summary>
		/// <value>The total charge including vat.</value>
        [EdiValue("9(10)V9(2)", Path = "VAT/7/0", Description = "UCSI")]
        public decimal TotalChargeIncludingVat { get; set; }

		/// <summary>
		/// Gets or sets the number of item lines.
		/// </summary>
		/// <value>The number of item lines.</value>
        [EdiValue("9(4)", Path = "VAT/8", Description = "NRIL")]
        public int? NumberOfItemLines { get; set; }

		/// <summary>
		/// Gets or sets the reason for lower zero vat rate.
		/// </summary>
		/// <value>The reason for lower zero vat rate.</value>
        [EdiValue("X(3)", Path = "VAT/9", Description = "RFLV")]
        public ReasonForLowerVatRateType ReasonForLowerZeroVatRate { get; set; }//null-able

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.UtilityBillValueAddedTax"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.UtilityBillValueAddedTax"/>.</returns>
        public override string ToString() {
            return string.Format("Net:{0} Vat:{1} Gross:{2}", TotalChargeBeforeVat, VatAmmountPayable, TotalChargeIncludingVat);
        }
    }

	/// <summary>
	/// Contract data.
	/// </summary>
    [EdiSegment, EdiPath("CDA")]
    public class ContractData
    {
		/// <summary>
		/// Gets or sets the current price schedule reference.
		/// </summary>
		/// <value>The current price schedule reference.</value>
        [EdiValue("X(17)", Path = "CDA/0", Description = "CPSC")]
        public string CurrentPriceScheduleReference { get; set; }

		/// <summary>
		/// Gets or sets the customer order number.
		/// </summary>
		/// <value>The customer order number.</value>
        [EdiValue("X(17)", Path = "CDA/1/0", Description = "ORNO")]
        public string CustomerOrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the supplier order number.
		/// </summary>
		/// <value>The supplier order number.</value>
        [EdiValue("X(17)", Path = "CDA/1/1", Description = "ORNO")]
        public string SupplierOrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the date ordered placed by customer.
		/// </summary>
		/// <value>The date ordered placed by customer.</value>
        [EdiValue("9(6)", Path = "CDA/1/2", Format = "yyMMdd", Description = "ORNO")]
        public DateTime? DateOrderedPlacedByCustomer { get; set; }

		/// <summary>
		/// Gets or sets the date ordered received by supplier.
		/// </summary>
		/// <value>The date ordered received by supplier.</value>
        [EdiValue("9(6)", Path = "CDA/1/3", Format = "yyMMdd", Description = "ORNO")]
        public DateTime? DateOrderedReceivedBySupplier { get; set; }

		/// <summary>
		/// Gets or sets the installation date.
		/// </summary>
		/// <value>The installation date.</value>
        [EdiValue("9(6)", Path = "CDA/2", Format = "yyMMdd", Description = "INSD")]
        public DateTime? InstallationDate { get; set; }

		/// <summary>
		/// Gets or sets the rental period.
		/// </summary>
		/// <value>The rental period.</value>
        [EdiValue("X(3)", Path = "CDA/3", Description = "REPE")]
        public string RentalPeriod { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.ContractData"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:indice.Edi.Tests.Models.ContractData"/>.</returns>
        public override string ToString() {
            return string.Format("CON:{0} SON:{1}", CustomerOrderNumber, SupplierOrderNumber);
        }
    }


    #region Edi Enumerations
	/// <summary>
	/// Charge indicator.
	/// </summary>
    public enum ChargeIndicator
    {
		/// <summary>
		/// The consumption only.
		/// </summary>
        ConsumptionOnly = 1,
        /// <summary>
        /// The combined consumption and charge.
        /// </summary>
		CombinedConsumptionAndCharge = 2,
        /// <summary>
        /// The charge only consumption based.
        /// </summary>
		ChargeOnly_ConsumptionBased = 3,
        /// <summary>
        /// The charge only fixed.
        /// </summary>
		ChargeOnly_Fixed = 4
    }
	/// <summary>
	/// Measure indicator.
	/// </summary>
    public enum MeasureIndicator
    {
        //KVA = Kilovolt - ampere
        //KWH = Kilowatt - hour
        //KW = Kilowatt
        //M3 = Cubic Metre (Gas only)
        //CUFT = Cubic Feet (Gas only)
        //1		=	1 Consumer Unit
    }
	/// <summary>
	/// Vat rate category code.
	/// </summary>
    public enum VatRateCategoryCode
    {
        /// <summary>
        /// Edi Value Description : Low Rate
        /// </summary>
        L = 1,

        /// <summary>
        /// Edi Value Description : Standard Rate
        /// </summary>
        S,

        /// <summary>
        /// Edi Value Description : Exemption From VAT
        /// </summary>
        X,

        /// <summary>
        /// Edi Value Description : Services Outside The Scope of VAT
        /// </summary>
        O,

        /// <summary>
        /// Edi Value Description : Zero Rate
        /// </summary>
        Z,

        /// <summary>
        /// Edi Value Description : Standard Rate In Withdrawn Bill
        /// </summary>
        B,

        /// <summary>
        /// Edi Value Description : Lower Rate In Withdrawn Bill
        /// </summary>
        M,

        /// <summary>
        /// Edi Value Description : Zero Rate In Withdrawn Bill
        /// </summary>
        Q,

        /// <summary>
        /// Edi Value Description : Exemption From Vat In Withdrawn Bill
        /// </summary>
        U
    }
	/// <summary>
	/// Reason for lower vat rate type.
	/// </summary>
    public enum ReasonForLowerVatRateType
    {
        /// <summary>
        /// Edi Value : L
        /// Edi Description : Low Consumption
        /// </summary>
        L = 1,

        /// <summary>
        /// Edi Value : D
        /// Edi Description : Domestic Usage
        /// </summary>
        D,

        /// <summary>
        /// Edi Value : C
        /// Edi Description : Combined
        /// </summary>
        C,

    }
	/// <summary>
	/// Report period.
	/// </summary>
    public enum ReportPeriod
    {
		/// <summary>
		/// The monthly.
		/// </summary>
        Monthly = 'M',
        /// <summary>
        /// The quarterly.
        /// </summary>
		Quarterly = 'Q'
    }
	/// <summary>
	/// Bill type code.
	/// </summary>
    public enum BillTypeCode
    {
        /// <summary>
        /// Edi Value : F
        /// Edi Description : Final
        /// Business Description : Last bill when an account is closed
        /// </summary>
        F = 1,

        /// <summary>
        /// Edi Value : N
        /// Edi Description : Normal
        /// Business Description : Normal Invoice
        /// </summary>
        N,

        /// <summary>
        /// Edi Value : W
        /// Edi Description : Withdrawn
        /// Business Description : Reverses a previous bill
        /// </summary>
        W
    }
	/// <summary>
	/// Reading data type.
	/// </summary>
    public enum ReadingDataType
    {
		/// <summary>
		/// The normal reading.
		/// </summary>
        NormalReading = 00,
        /// <summary>
        /// The estimated computer reading.
        /// </summary>
		Estimated_Computer_Reading = 02,
        /// <summary>
        /// The customers own reading.
        /// </summary>
		CustomersOwnReading = 04,
        /// <summary>
        /// The exchange meter reading.
        /// </summary>
		ExchangeMeterReading = 06,
        /// <summary>
        /// The third party normal reading.
        /// </summary>
		ThirdPartyNormalReading = 09,
        /// <summary>
        /// The third party estimated computer reading.
        /// </summary>
		ThirdPartyEstimated_Computer_Reading = 11,
        /// <summary>
        /// The reading for information only.
        /// </summary>
		ReadingForInformationOnly = 12,
    }
	/// <summary>
	/// Distribution business codes.
	/// </summary>
    public enum DistributionBusinessCodes
    {
		/// <summary>
		/// The eastern electricity.
		/// </summary>
        Eastern_Electricity = 10,
        /// <summary>
        /// The east midlands electricity.
        /// </summary>
		East_Midlands_Electricity = 11,
        /// <summary>
        /// The london electricity.
        /// </summary>
		London_Electricity = 12,
        /// <summary>
        /// The manweb.
        /// </summary>
		Manweb = 13,
        /// <summary>
        /// The midlands electricity.
        /// </summary>
		Midlands_Electricity = 14,
        /// <summary>
        /// The northern electricity.
        /// </summary>
		Northern_Electricity = 15,
        /// <summary>
        /// The norweb.
        /// </summary>
		NORWEB = 16,
        /// <summary>
        /// The scottish hydro electric.
        /// </summary>
		Scottish_Hydro_Electric = 17,
        /// <summary>
        /// The scottish power.
        /// </summary>
		Scottish_Power = 18,
        /// <summary>
        /// The seeboard.
        /// </summary>
		SEEBOARD = 19,
        /// <summary>
        /// The southern electric.
        /// </summary>
		Southern_Electric = 20,
        /// <summary>
        /// The swalec.
        /// </summary>
		SWALEC = 21,
        /// <summary>
        /// The south western electricity.
        /// </summary>
		South_Western_Electricity = 22,
        /// <summary>
        /// The yorkshire electricity.
        /// </summary>
		Yorkshire_Electricity = 23,
        //TRANSCO = TR
    }

    #endregion
}
