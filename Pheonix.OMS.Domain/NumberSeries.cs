using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pheonix.OMS.Domain
{
    public class NumberSeries : BaseEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(60)] public string Name { get; set; } = string.Empty;
        [Required, MaxLength(3)] public string Prefix { get; set; } = string.Empty;
        public int LastNumber { get; set; }
        public int Increment { get; set; }
        public int Length { get; set; }
        public int MaxNumber { get; set; }

    }
}
