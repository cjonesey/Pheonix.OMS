using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Phoenix.Domain
{
    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(40)] public string Name { get; set; } = string.Empty;
        [MaxLength(30)] public string Street1 { get; set; } = string.Empty;
        [MaxLength(30)] public string Street2 { get; set; } = string.Empty;
        [MaxLength(30)] public string City { get; set; } = string.Empty;
        [MaxLength(30)] public string County { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string Postcode { get; set; } = string.Empty;
        public int CountryId { get; set; }
        [Required, MaxLength(3)] public string CountryCode { get; set; } = string.Empty;
        [Required, MaxLength(30)] public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(255)] public string Website { get; set; } = string.Empty;
        [Required, MaxLength(100)] public string ContactPerson { get; set; } = string.Empty; //To be updated

        //Invoicing
        [Required, MaxLength(30)] public string VATRegistrationNumber { get; set; } = string.Empty;
        [Required, MaxLength(40)] public string GLN { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string CompanyRegistrationNumber { get; set; } = string.Empty;
        [Required, MaxLength(30)] public string EORIRegistrationNumber { get; set; } = string.Empty;
        public int VATPostingGroupID { get; set; }
        //To do add in VAT Posting Group
        public int CustomerPostingGroupID { get; set; }
        //To do add in Customer Posting Group
        public int CustomerPriceGroupID { get; set; }
        //To do add in Customer Price Group
        public bool ConsolidateInvoices { get; set; } = false;

        [Precision(18, 2)] public decimal PrepaymentPercentage { get; set; } = 0;
        [Precision(18, 2)] public decimal CreditLimit { get; set; } = 0;
        public short CashApplicationMethod { get; set; } = 0;
        public int PaymentTermsID { get; set; }
        //To do add in Payment Terms
        public int PaymentMethodID { get; set; }
        //To do add in Payment Method

        //Shipping
        public short ReserveStock { get; set; } = 0;
        public short ShipComplete { get; set; } = 0;
        public short ShowPricesOnSalesDocuments { get; set; } = 0;

        public ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    }
}
