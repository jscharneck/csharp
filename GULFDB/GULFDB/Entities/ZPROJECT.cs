using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GULFDB.Entities
{
    [Table("zPROJECT")]

    public class ZPROJECT
    {
        public int Id { get; set; }
        public int? TenantId { get; set; }

        public virtual int? PROJECT_ALTERNATE_Id { get; set; }

        public virtual string PROJECT_TITLE { get; set; }

        public virtual decimal PROJECT_SQUARE_METERS { get; set; }

        public virtual decimal PROJECT_VALUE { get; set; }

        public virtual bool PROJECT_VALUE_ALLOW_RANGE_IND { get; set; }

        public virtual decimal PROJECT_VALUE_MIN { get; set; }

        public virtual decimal PROJECT_VALUE_MAX { get; set; }

        public virtual string PROJECT_ADDRESS_1 { get; set; }

        public virtual string PROJECT_ADDRESS_2 { get; set; }

        public virtual string PROJECT_ZIP_CODE { get; set; }

        public virtual string PROJECT_PLUS_FOUR { get; set; }

        public virtual bool PROJECT_CHECK_0001 { get; set; }

        public virtual bool PROJECT_CHECK_0002 { get; set; }

        public virtual bool PROJECT_CHECK_0003 { get; set; }

        public virtual bool PROJECT_CHECK_0004 { get; set; }

        public virtual bool PROJECT_CHECK_0005 { get; set; }

        public virtual bool PROJECT_CHECK_0006 { get; set; }

        public virtual bool PROJECT_CHECK_0007 { get; set; }

        public virtual bool PROJECT_CHECK_0008 { get; set; }

        public virtual bool PROJECT_CHECK_0009 { get; set; }

        public virtual bool PROJECT_CHECK_0010 { get; set; }

        public virtual DateTime PROJECT_DUE_DATE { get; set; }

        public virtual DateTime PROJECT_DUE_DATE_TIME { get; set; }

        public virtual DateTime PROJECT_PREBID_MEETING_DATE { get; set; }

        public virtual DateTime PROJECT_PREBID_TIME { get; set; }

        public virtual bool PROJECT_MANDATORY_IND { get; set; }

        public virtual string PROJECT_TEAM_PHONE { get; set; }

        public virtual string PROJECT_TEAM_FAX { get; set; }

        public virtual string PROJECT_TEAM_OWNER { get; set; }

        public virtual string PROJECT_TEAM_ARCHITECT { get; set; }

        public virtual string PROJECT_DESCRIPTION { get; set; }

        public virtual DateTime PROJECT_CLIENT_CLOSING_DATE { get; set; }

        public virtual string PROJECT_TENDER_VALIDITY { get; set; }

        public virtual string PROJECT_MID_TENDER_MEETING { get; set; }

        public virtual string PROJECT_SITE_VISIT { get; set; }

        public virtual int STATUS { get; set; }

        public virtual DateTime LASTUPDATE { get; set; }

        public virtual int? PROJECT_DUE_DATE_OPTION_Id { get; set; }

        //[ForeignKey("PROJECT_DUE_DATE_OPTION_Id")]
        //public ZPROJECT_DUE_DATE_OPTION PROJECT_DUE_DATE_OPTION_Fk { get; set; }

        //public virtual int? PROJECT_TEAM_INTERNAL_OFFICE_Id { get; set; }

        //[ForeignKey("PROJECT_TEAM_INTERNAL_OFFICE_Id")]
        //public zAbpOrganizationUnits_AssignTo_zPROJECT PROJECT_TEAM_INTERNAL_OFFICE_Fk { get; set; }

        //public virtual int? PROJECT_TEAM_MANAGER_Id { get; set; }

        //[ForeignKey("PROJECT_TEAM_MANAGER_Id")]
        //public ZADDRESSBOOK PROJECT_TEAM_MANAGER_Fk { get; set; }

        //public virtual int? PROJECT_TEAM_SHARED_MANAGERS_Id { get; set; }

        //[ForeignKey("PROJECT_TEAM_SHARED_MANAGERS_Id")]
        //public ZADDRESSBOOK PROJECT_TEAM_SHARED_MANAGERS_Fk { get; set; }

        //public virtual int? COUNTRY_Id { get; set; }

        //[ForeignKey("COUNTRY_Id")]
        //public ZCOUNTRY COUNTRY_Fk { get; set; }

        //public virtual int? COUNTRYSTATE_Id { get; set; }

        //[ForeignKey("COUNTRYSTATE_Id")]
        //public ZCOUNTRYSTATE COUNTRYSTATE_Fk { get; set; }

        //public virtual int? PROJECT_MANAGER_TYPE_Id { get; set; }

        //[ForeignKey("PROJECT_MANAGER_TYPE_Id")]
        //public ZADDRESSBOOK PROJECT_MANAGER_TYPE_Fk { get; set; }

        //public virtual int? PROJECT_TEAM_SHARED_OFFICES_Id { get; set; }

        //[ForeignKey("PROJECT_TEAM_SHARED_OFFICES_Id")]
        //public zAbpOrganizationUnits_AssignTo_zPROJECT PROJECT_TEAM_SHARED_OFFICES_Fk { get; set; }

        //public virtual int? PROJECT_TYPE_Id { get; set; }

        //[ForeignKey("PROJECT_TYPE_Id")]
        //public ZPROJECT_TYPE PROJECT_TYPE_Fk { get; set; }

        //public virtual int? PROJECT_STATUS_Id { get; set; }

        //[ForeignKey("PROJECT_STATUS_Id")]
        //public ZPROJECT_STATUS PROJECT_STATUS_Fk { get; set; }

        //public virtual int? PROJECT_CURRENCY_Id { get; set; }

        //[ForeignKey("PROJECT_CURRENCY_Id")]
        //public ZCURRENCY PROJECT_CURRENCY_Fk { get; set; }

        //public virtual int? COUNTRYCITY_Id { get; set; }

        //[ForeignKey("COUNTRYCITY_Id")]
        //public ZCOUNTRYCITY COUNTRYCITY_Fk { get; set; }

        //public virtual int? PROJECT_DUE_DATE_TIMEZONE_Id { get; set; }

        //[ForeignKey("PROJECT_DUE_DATE_TIMEZONE_Id")]
        //public ZTIMEZONE PROJECT_DUE_DATE_TIMEZONE_Fk { get; set; }

        //public virtual int? PROJECT_PREBID_TIMEZONE_Id { get; set; }

        //[ForeignKey("PROJECT_PREBID_TIMEZONE_Id")]
        //public ZTIMEZONE PROJECT_PREBID_TIMEZONE_Fk { get; set; }
    }
}
