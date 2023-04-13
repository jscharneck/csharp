namespace GULFDB.Models
{
    public class CodeProjectsData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public int ProjectId { get; set; }
        public int? CodeId { get; set; }
        public string CompositeKey { get; set; }
        public int CompanyCount { get; set; }

        public string CompaniesAssigned { get; set; }


        public int? ProjectPackageId { get; set; }
        public string ProjectPackageOrTrade { get; set; }


    }
}
