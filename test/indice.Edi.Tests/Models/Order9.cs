using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace indice.Edi.Tests.Models
{
	/// <summary>
	/// Order9.
	/// </summary>
    public class Order9
    {
		/// <summary>
		/// Gets or sets the type of the order.
		/// </summary>
		/// <value>The type of the order.</value>
        public Order9Type OrderType { get; set; }
        
		/// <summary>
		/// Gets or sets the supplier.
		/// </summary>
		/// <value>The supplier.</value>
        public Order9Supplier Supplier { get; set; }
        
		/// <summary>
		/// Gets or sets the customer.
		/// </summary>
		/// <value>The customer.</value>
        public Order9Customer Customer { get; set; }

		/// <summary>
		/// Gets or sets the customer depot gln.
		/// </summary>
		/// <value>The customer depot gln.</value>
        public string CustomerDepotGLN { get; set; }

		/// <summary>
		/// Gets or sets the customer depot code.
		/// </summary>
		/// <value>The customer depot code.</value>
        public string CustomerDepotCode { get; set; }
		/// <summary>
		/// Gets or sets the customer depot.
		/// </summary>
		/// <value>The customer depot.</value>
        public string CustomerDepot { get; set; }
        /// <summary>
        /// Gets or sets the customer depot address.
        /// </summary>
        /// <value>The customer depot address.</value>
		public string CustomerDepotAddress { get; set; }

		/// <summary>
		/// Gets or sets the customer order no.
		/// </summary>
		/// <value>The customer order no.</value>
        public string CustomerOrderNo { get; set; }

		/// <summary>
		/// Gets or sets the supplier order no.
		/// </summary>
		/// <value>The supplier order no.</value>
        public string SupplierOrderNo { get; set; }

		/// <summary>
		/// Gets or sets the order date.
		/// </summary>
		/// <value>The order date.</value>
        public DateTime OrderDate { get; set; }

		/// <summary>
		/// Gets or sets the depot date time.
		/// </summary>
		/// <value>The depot date time.</value>
        public DateTime DepotDateTime { get; set; }
        
		/// <summary>
		/// Gets or sets the order9 lines.
		/// </summary>
		/// <value>The order9 lines.</value>
        public List<Order9Line> Order9Lines { get; set; }
        
    }

	/// <summary>
	/// Order9 line.
	/// </summary>
    public class Order9Line 
    {
		/// <summary>
		/// Gets or sets the line no.
		/// </summary>
		/// <value>The line no.</value>
        public string LineNo { get; set; }

		/// <summary>
		/// Gets or sets the product supplier gtin.
		/// </summary>
		/// <value>The product supplier gtin.</value>
        public string ProductSupplierGTIN { get; set; }

		/// <summary>
		/// Gets or sets the product supplier code.
		/// </summary>
		/// <value>The product supplier code.</value>
        public string ProductSupplierCode { get; set; }

		/// <summary>
		/// Gets or sets the product gtin.
		/// </summary>
		/// <value>The product gtin.</value>
        public string ProductGTIN { get; set; }

		/// <summary>
		/// Gets or sets the product customer gtin.
		/// </summary>
		/// <value>The product customer gtin.</value>
        public string ProductCustomerGTIN { get; set; }

		/// <summary>
		/// Gets or sets the product customer code.
		/// </summary>
		/// <value>The product customer code.</value>
        public string ProductCustomerCode { get; set; }

		/// <summary>
		/// Gets or sets the order unit.
		/// </summary>
		/// <value>The order unit.</value>
        public int OrderUnit { get; set; }

		/// <summary>
		/// Gets or sets the order qty.
		/// </summary>
		/// <value>The order qty.</value>
        public int OrderQty { get; set; }

		/// <summary>
		/// Gets or sets the order unit price.
		/// </summary>
		/// <value>The order unit price.</value>
        public decimal OrderUnitPrice { get; set; }

		/// <summary>
		/// Gets or sets the product description.
		/// </summary>
		/// <value>The product description.</value>
        public string ProductDescription { get; set; }
    }

	/// <summary>
	/// Order9 customer.
	/// </summary>
    public class Order9Customer
    {
		/// <summary>
		/// Gets or sets the customer gln.
		/// </summary>
		/// <value>The customer gln.</value>
        public string CustomerGLN { get; set; }

		/// <summary>
		/// Gets or sets the name of the customer.
		/// </summary>
		/// <value>The name of the customer.</value>
        public string CustomerName { get; set; }
    }

	/// <summary>
	/// Order9 supplier.
	/// </summary>
    public class Order9Supplier
    {
		/// <summary>
		/// Gets or sets the supplier gln.
		/// </summary>
		/// <value>The supplier gln.</value>
        public string SupplierGLN { get; set; }

		/// <summary>
		/// Gets or sets the name of the supplier.
		/// </summary>
		/// <value>The name of the supplier.</value>
        public string SupplierName { get; set; }

    }

	/// <summary>
	/// Order9 type.
	/// </summary>
    public class Order9Type
    {
		/// <summary>
		/// Gets or sets the order type code.
		/// </summary>
		/// <value>The order type code.</value>
        public string OrderTypeCode { get; set; }

		/// <summary>
		/// Gets or sets the type of the order.
		/// </summary>
		/// <value>The type of the order.</value>
        public string OrderType { get; set; }

		/// <summary>
		/// Gets or sets the file generation no.
		/// </summary>
		/// <value>The file generation no.</value>
        public string FileGenerationNo { get; set; }

		/// <summary>
		/// Gets or sets the file version no.
		/// </summary>
		/// <value>The file version no.</value>
        public string FileVersionNo { get; set; }

		/// <summary>
		/// Gets or sets the file creation date.
		/// </summary>
		/// <value>The file creation date.</value>
        public DateTime FileCreationDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
        public string FileName { get; set; }
    }

}
