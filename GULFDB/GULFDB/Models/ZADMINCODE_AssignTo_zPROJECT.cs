using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GULFDB.Models
{
    [Table("zADMINCODE_AssignTo_zPROJECT")]

    public class ZADMINCODE_AssignTo_zPROJECT
    {
        public int Id { get; set; }
        
        public int Project_Id { get; set; }
        
        public int Code_Id { get; set; }
        
        public int Composite_Id { get; set; }
        
        public string Display_Text { get; set; }
        
        public int TenantId { get; set; }
        
        public DateTime Due_Date { get; set; }

    }
}
