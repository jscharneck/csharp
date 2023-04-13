using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GULFDB.Models
{
    public class CompanyWorkflowProjectAllDataSet
    {
        public int Id { get; set; }
        public int? ChildRowId { get; set; }
        public int? CompanyId { get; set; }
        public int? CodeId { get; set; }
        public string CompositeKey { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContactInformationEmail { get; set; }
        public string CompanyContactInformationFirstName { get; set; }
        public string CompanyContactInformationLastName { get; set; }
        public int WorkflowStatusId { get; set; }
        public int WorkflowSubStatusId { get; set; }
        public bool SelectedInd { get; set; }

        public string Notes { get; set; }

        public int? ProjectPackageId { get; set; }

    }
}
