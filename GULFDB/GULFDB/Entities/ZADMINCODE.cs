using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GULFDB.Entities;

[Table("zADMINCODE")]

public class ZADMINCODE
{
    public int Id { get; set; }

    public int? TenantId { get; set; }

    [Required]
    public virtual string CODE { get; set; }

    [Required]
    public virtual string CODE_NAME { get; set; }

    [Required]
    public virtual string CODE_DESCRIPTION { get; set; }

    public virtual int STATUS { get; set; }

    public virtual DateTime LASTUPDATE { get; set; }

    public virtual int? ZCodeCategoryId { get; set; }

    [ForeignKey("ZCodeCategoryId")]
    public ZCodeCategories ZCodeCategoryFk { get; set; }
}
