using System.ComponentModel.DataAnnotations.Schema;

namespace GULFDB.Models
{
    [Table("ZCOMPANY_Workflow_PROJECT")]
    public class ZCOMPANY_Workflow_PROJECT 
    {
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public virtual int Company_Id { get; set; }

        public virtual int Project_Id { get; set; }

        public virtual int Workflow_StatusId { get; set; }

        public virtual DateTime LastUpdate { get; set; }

        public virtual int LastUpdateUserId { get; set; }

        public virtual int STATUS { get; set; }

        public virtual int Workflow_SubStatusId { get; set; }

        public virtual bool SelectedInd { get; set; }

        public virtual string Notes { get; set; }

        public virtual int? Code_Id { get; set; }

        public virtual int? ProjectPackage_Id { get; set; }
    }
}
