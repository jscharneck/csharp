using System.ComponentModel.DataAnnotations.Schema;

namespace GULFDB.Models
{
    [Table("zPROJECTPACKAGE")]
    public class ZPROJECTPACKAGE
    {
        public int Id { get; set; }

        public int? TenantId { get; set; }
                
        public virtual string PACKAGE_NAME { get; set; }

        public virtual int STATUS { get; set; }

        public virtual DateTime LASTUPDATE { get; set; }
    }
}
