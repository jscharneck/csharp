using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GULFDB.Entities
{
    [Table("zUSER_AssignTo_zCOMPANY")]
    public class ZUSER_AssignTo_zCOMPANY
    {

        public int Id { get; set; }

        public int? TenantId { get; set; }

        public virtual int? Composite_Id { get; set; }

        public virtual long? User_Id { get; set; }

        //[ForeignKey("User_Id")]
        //public User User_Fk { get; set; }

        public virtual int? Company_Id { get; set; }

        [ForeignKey("Company_Id")]
        public ZCOMPANY Company_Fk { get; set; }

    }
}
