using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GULFDB.Entities
{
    [Table("zADMINCODE_AssignTo_zCOMPANY")]

    public class ZADMINCODE_AssignTo_zCOMPANY
    {
        public int Id { get; set; }
        public int? Company_Id { get; set; }
        public int? Code_Id { get; set; }
        public int? Composite_Id { get; set; }
        public string Display_Text { get; set; }
        public int TenantId { get; set; }
    }
}
