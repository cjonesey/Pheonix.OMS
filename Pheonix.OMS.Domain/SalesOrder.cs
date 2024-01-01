using Microsoft.EntityFrameworkCore;
using Pheonix.OMS.Domain.SubDomains;
using System.ComponentModel.DataAnnotations;

namespace Pheonix.OMS.Domain
{
    public class SalesOrder : BaseEntity
    {
        public int Id { get; set; }
        [Required] string SalesOrderNumber { get; set; } = string.Empty;
        [Required] public int CustomerId { get; set; }
        [Required] public string CustomerName { get; set; } = string.Empty;
        //Billing Address
        [Required, MaxLength(30)] public string BillToName { get; set; } = string.Empty;
        [Required, MaxLength(30)] public string BillToStreet1 { get; set; } = string.Empty;
        [MaxLength(30)] public string BillToStreet2 { get; set; } = string.Empty;
        [MaxLength(30)] public string BillToCity { get; set; } = string.Empty;
        [MaxLength(30)] public string BillToCounty { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string BillToPostcode { get; set; } = string.Empty;
        [Required] public int BillToCountryId { get; set; }
        [Required, MaxLength(3)] public string BillToCountryCode { get; set; } = string.Empty;

        //Shipping Address
        [MaxLength(30)] public string ShipToName { get; set; } = string.Empty;
        [MaxLength(30)] public string ShipToStreet1 { get; set; } = string.Empty;
        [MaxLength(30)] public string ShipToStreet2 { get; set; } = string.Empty;
        [MaxLength(30)] public string ShipToCity { get; set; } = string.Empty;
        [MaxLength(30)] public string ShipToCounty { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string ShipToPostcode { get; set; } = string.Empty;
        public int ShipToCountryId { get; set; }
        [Required, MaxLength(3)] public string ShipToCountryCode { get; set; } = string.Empty;
        public byte OrderStatus { get; set; }
        public byte OrderType { get; set; }
        public DateOnly OrderDate { get; set; }
        [Precision(18, 2)] public decimal OrderTotal { get; set; }
        [Precision(18, 2)] public decimal OrderTax { get; set; }
        [Precision(18, 2)] public decimal OrderShipping { get; set; }
        [Precision(18, 2)] public decimal OrderDiscount { get; set; }
        [Precision(18, 2)] public decimal OrderSubTotal { get; set; }
        [Precision(18, 2)] public decimal OrderGrandTotal { get; set; }
        public byte OrderPaymentMethod { get; set; }
        public byte OrderPaymentStatus { get; set; }
        public string OrderPaymentReference { get; set; } = string.Empty;

    }
}
