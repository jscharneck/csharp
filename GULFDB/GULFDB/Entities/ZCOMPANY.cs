using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GULFDB.Entities
{
    [Table("zCOMPANY")]
    public class ZCOMPANY
    {
        public int Id { get; set; }

        public int? TenantId { get; set; }

        [Required]
        
        public virtual string COMPANY_NAME { get; set; }

        public virtual string COMPANY_WEBSITE { get; set; }

        [Required]
        public virtual string COMPANY_LOCATION_NAME { get; set; }

        public virtual string COMPANY_LOCATION_PHONE { get; set; }

        public virtual string COMPANY_LOCATION_FAX { get; set; }

        public virtual string COMPANY_CITY { get; set; }

        public virtual string COMPANY_ADDRESS1 { get; set; }

        public virtual string COMPANY_ADDRESS2 { get; set; }

        public virtual string COMPANY_ZIP_POSTAL { get; set; }

        public virtual string COMPANY_CONTACT_INFORMATION_FIRST_NAME { get; set; }

        public virtual string COMPANY_CONTACT_INFORMATION_LAST_NAME { get; set; }

        public virtual string COMPANY_CONTACT_INFORMATION_TITLE { get; set; }

        public virtual string COMPANY_CONTACT_INFORMATION_EMAIL { get; set; }

        public virtual string COMPANY_CONTACT_INFORMATION_PHONE { get; set; }

        public virtual string COMPANY_CONTACT_INFORMATION_MOBILE { get; set; }

        public virtual int STATUSIND { get; set; }

        public virtual string LASTUPDATE { get; set; }

        public virtual int? COUNTRY_Id { get; set; }

        [ForeignKey("COUNTRY_Id")]
        public ZCOUNTRY COUNTRY_Fk { get; set; }

        //public virtual int? COUNTRY_STATE_Id { get; set; }

        //[ForeignKey("COUNTRY_STATE_Id")]
        //public ZCOUNTRYSTATE COUNTRY_STATE_Fk { get; set; }

        //public virtual int? ZADMINCOMPANY_GROUPId { get; set; }

        //[ForeignKey("ZADMINCOMPANY_GROUPId")]
        //public ZADMINCOMPANY_GROUP ZADMINCOMPANY_GROUPFk { get; set; }

        public virtual int? ZADMINCODEId { get; set; }

        [ForeignKey("ZADMINCODEId")]
        public ZADMINCODE ZADMINCODEFk { get; set; }

        public virtual int? ZADMINPACKAGEId { get; set; }

        //[ForeignKey("ZADMINPACKAGEId")]
        //public ZADMINPACKAGE ZADMINPACKAGEFk { get; set; }

        public byte[] COMPANY_LOGO { get; set; }
    }
}
