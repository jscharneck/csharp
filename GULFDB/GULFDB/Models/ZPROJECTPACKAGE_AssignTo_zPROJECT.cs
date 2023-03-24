using System.ComponentModel.DataAnnotations.Schema;

namespace GULFDB.Models
{
    [Table("zPROJECTPACKAGE_AssignTo_zPROJECT")]

    public class ZPROJECTPACKAGE_AssignTo_zPROJECT
    {
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public virtual int Project_Id { get; set; }

        public virtual int? ProjectPackage_Id { get; set; }

        public virtual int Composite_Id { get; set; }

        public virtual string Display_Text { get; set; }

        public virtual DateTime Due_Date { get; set; }
    }
}
