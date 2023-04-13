using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GULFDB.Entities
{
    [Table("ZCodeCategories")]
    public class ZCodeCategories
    {
        public int Id { get; set; }
        public int? TenantId { get; set; }

        [Required]
        public virtual string CODE_CATEGORY { get; set; }

        [Required]
        public virtual string CODE_CATEGORY_DESC { get; set; }

        public virtual int? STATUS { get; set; }

        public virtual DateTime? LASTUPDATE { get; set; }
    }
}
