using Pheonix.OMS.Domain.SubDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pheonix.OMS.Domain
{
    public class CustomerAddress : BaseEntity
    {
        public int Id { get; set; }
        [Required] public int CustomerId { get; set; }
        [Required] public Customer Customer { get;}
        [Required, MaxLength(10)] public string AddressCode { get; set; } = string.Empty;
        [Required] public int BillToAddressId { get; set; }
        [MaxLength(30)] public string Name { get; set; } = string.Empty;
        [MaxLength(30)] public string Street1 { get; set; } = string.Empty;
        [MaxLength(30)] public string Street2 { get; set; } = string.Empty;
        [MaxLength(30)] public string City { get; set; } = string.Empty;
        [MaxLength(30)] public string County { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string Postcode { get; set; } = string.Empty;
        public int CountryId { get; set; }
        [Required, MaxLength(3)] public string CountryCode { get; set; } = string.Empty;

        [Required] public int WarehouseId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
       

    }
}
