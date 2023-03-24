using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GULFDB.Models
{
    [Table("zCOUNTRY")]
    public class ZCOUNTRY
    {
        public int Id { get; set; }

        public int? TenantId { get; set; }

        [Required]
        public virtual string COUNTRY { get; set; }
    }
}
