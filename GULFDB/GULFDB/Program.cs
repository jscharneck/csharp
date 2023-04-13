using GULFDB.EFCore;
using GULFDB.Entities;
using GULFDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Nodes;
using System.Web.Mvc;


namespace GULFDB
{
    public class Program : ControllerBase
    {
        private const int StatusQualified = 100;
        private const int StatusInvited = 200;
        private const int StatusAwarded = 300;

        private const int SubStatusNotInvited = 110;
        private const int SubStatusPendingInvitation = 120;
        private const int SubStatusRevokedOptedOut = 130;

        private const int SubStatusDeclined = 210;
        private const int SubStatusAccepted = 220;
        private const int SubStatusUndecided = 230;
        private const int SubStatusProposed = 240;
        private const int SubStatusRevokedInvitation = 250;
        private const int SubStatusProposedLate = 260;
        private const int SubStatusAcceptedFailedToBid = 270;
        private const int SubStatusInNegotiation = 280;
        private const int SubStatusAwarded = 290;

        private const int SubStatusAwardedDeclined = 310;
        private const int SubStatusAwardedAccepted = 320;
        private const int SubStatusAwardedUndecided = 330;
        private const int SubStatusAwardedProposed = 340;
        private const int SubStatusAwardedNoResponse = 350;

        private const int StatusValue = 1;

        private static readonly int projectId = 1;
        private static readonly int companyId = 6;
        private static readonly bool selectedInd = false;
        private static readonly bool nonSelectedInd = false;

        //static AppDBContext _db; 
        
        public Program()
        { 
            //_db = new AppDBContext();
        }

        public static IQueryable<ZCOMPANY_Workflow_PROJECT> GetZCompanyWorkflowProjectsAsQueryable(AppDBContext db)
        {
            return db.ZCOMPANY_Workflow_PROJECTs.AsQueryable();
        }

        public async static Task<object> AllItemParentGrid(AppDBContext _db)
        {
            //int projectId = 1;
            //bool selectedInd = false;
            //bool nonSelectedInd = false;

            ////////////////////Building up queryable for improved performance 
            IQueryable<ZCOMPANY_Workflow_PROJECT> e = GetZCompanyWorkflowProjectsAsQueryable(_db);
            var zcompanyWorkflowProjectsQuery = e.Where(c => c.Project_Id == projectId);

            var query = await (from work in zcompanyWorkflowProjectsQuery //_db.ZCOMPANY_Workflow_PROJECTs

                         join assigntoproj in _db.ZADMINCODE_AssignTo_zPROJECTs on work.Code_Id equals assigntoproj.Code_Id into groupedCodesInProj
                         from gcip in groupedCodesInProj.DefaultIfEmpty()

                         join adcode in _db.ZADMINCODEs on work.Code_Id equals adcode.Id into codesgroup
                         from cg in codesgroup.DefaultIfEmpty()

                         join ppproj in _db.ZPROJECTPACKAGE_AssignTo_zPROJECTs on work.ProjectPackage_Id equals ppproj.ProjectPackage_Id into groupedPackageIds
                         from gpi in groupedPackageIds.DefaultIfEmpty()

                         join projpackage in _db.ZPROJECTPACKAGEs on work.ProjectPackage_Id equals projpackage.Id into groupedProjectPackages
                         from gpp in groupedProjectPackages.DefaultIfEmpty()

                         where    work.Project_Id == projectId &&
                                  work.TenantId == AbpSession.TenantId &&
                                  work.Workflow_StatusId == StatusQualified &&
                                  (selectedInd && !nonSelectedInd ? (work.SelectedInd == selectedInd) :
                                  !selectedInd && nonSelectedInd ? (work.SelectedInd == selectedInd) : true)

                         select new
                         {
                             ProjectPackageOrTrade = work.ProjectPackage_Id != null ?
                                                     "P" :
                                                     cg.CODE,
                             Code = _db.ZADMINCODEs.First(c => c.Id == work.Code_Id).CODE,
                             CodeName = work.ProjectPackage_Id != null ?
                                         gpp.PACKAGE_NAME :
                                          cg.CODE_NAME,
                             CodeId = work.Code_Id,
                             ProjectId = work.Project_Id,
                             work.ProjectPackage_Id,
                             work.Workflow_StatusId,
                             gpp.PACKAGE_NAME,

                             CodesOrPackagesCountQualified = (from work in _db.ZCOMPANY_Workflow_PROJECTs
                                                              join codetocomp in _db.ZADMINCODE_AssignTo_zCOMPANYs on work.Code_Id equals codetocomp.Code_Id into groupedCTC
                                                              from ctc in groupedCTC.DefaultIfEmpty()

                                                              where ctc.Code_Id == gcip.Code_Id &&
                                                                   work.Project_Id == projectId &&
                                                                   work.Workflow_StatusId == StatusQualified
                                                              select new { work.Company_Id, work.Code_Id }).Distinct().ToList().Count,

                             CodesOrPackagesCountInvited = (from work in _db.ZCOMPANY_Workflow_PROJECTs
                                                            join codetocomp in _db.ZADMINCODE_AssignTo_zCOMPANYs on work.Code_Id equals codetocomp.Code_Id into groupedCTC
                                                            from ctc in groupedCTC.DefaultIfEmpty()

                                                            where ctc.Code_Id == gcip.Code_Id &&
                                                                 work.Project_Id == projectId &&
                                                                 work.Workflow_StatusId == StatusInvited
                                                            select new { work.Company_Id, work.Code_Id }).Distinct().ToList().Count,

                             CodesOrPackagesCountAwarded = (from work in _db.ZCOMPANY_Workflow_PROJECTs
                                                            join codetocomp in _db.ZADMINCODE_AssignTo_zCOMPANYs on work.Code_Id equals codetocomp.Code_Id into groupedCTC
                                                            from ctc in groupedCTC.DefaultIfEmpty()

                                                            where ctc.Code_Id == gcip.Code_Id &&
                                                                 work.Project_Id == projectId &&
                                                                 work.Workflow_StatusId == StatusAwarded
                                                            select new { work.Company_Id, work.Code_Id }).Distinct().ToList().Count,
                         }).Distinct().ToListAsync();


            List<CodeProjectsData> codeProjectsDatas = new();

            //foreach (var row in queryTest)
            foreach (var row in query)
            {
                CodeProjectsData projectsData = new();

                projectsData.Code = row.Code;
                projectsData.CodeName = row.CodeName;
                projectsData.ProjectId = row.ProjectId;
                projectsData.CodeId = row.CodeId;
                projectsData.ProjectPackageId = row.ProjectPackage_Id;
                projectsData.CompositeKey = row.ProjectId.ToString() + row.CodeId.ToString();
                projectsData.Id = Int32.Parse(projectsData.CompositeKey);
                projectsData.ProjectPackageOrTrade = row.ProjectPackageOrTrade;

                projectsData.CompaniesAssigned = $"[Qualified-{row.CodesOrPackagesCountQualified}] : [Invited-{row.CodesOrPackagesCountInvited}] : [Awarded-{row.CodesOrPackagesCountAwarded}] ";

                codeProjectsDatas.Add(projectsData);
            }

            Console.WriteLine(codeProjectsDatas);

            return query;
        }

        public async static Task<object> AllItemsChildGrid(int projectId, bool selectedInd, bool nonSelectedInd, AppDBContext _db)
        {
            var query = await (from work in _db.ZCOMPANY_Workflow_PROJECTs

                         join adcode in _db.ZADMINCODE_AssignTo_zCOMPANYs on work.Code_Id equals adcode.Code_Id into groupedADCODE
                         from gac in groupedADCODE.DefaultIfEmpty()

                         join comp in _db.ZCOMPANYs on work.Company_Id equals comp.Id into groupedCOMP
                         from gcomp in groupedCOMP.DefaultIfEmpty()

                         join projpackproj in _db.ZPROJECTPACKAGE_AssignTo_zPROJECTs on work.ProjectPackage_Id equals projpackproj.ProjectPackage_Id into groupedPROJPACKPROJ
                         from gppp in groupedPROJPACKPROJ.DefaultIfEmpty()

                         join projpackage in _db.ZPROJECTPACKAGEs on gppp.ProjectPackage_Id equals projpackage.Id into groupedProjectPackages
                         from gpp in groupedProjectPackages.DefaultIfEmpty()

                             // outer join 


                             //where work.Project_Id == projectId
                             //&& work.Workflow_StatusId == StatusQualified
                             //&& work.TenantId == AbpSession.TenantId

                             //where gac.Code_Id == work.Code_Id &&
                             //    work.Project_Id == projectId &&
                             //    work.TenantId == AbpSession.TenantId &&
                             //    work.Workflow_StatusId == StatusQualified && (selectedInd && !nonSelectedInd ? (work.SelectedInd == selectedInd) :
                             //                                                 !selectedInd && nonSelectedInd ? (work.SelectedInd == selectedInd) : true)


                         select new
                         {
                             CompanyId = gcomp.Id,
                             gcomp.COMPANY_NAME,
                             gcomp.COMPANY_CONTACT_INFORMATION_EMAIL,
                             gcomp.COMPANY_CONTACT_INFORMATION_FIRST_NAME,
                             gcomp.COMPANY_CONTACT_INFORMATION_LAST_NAME,
                             work.Workflow_StatusId,
                             work.Workflow_SubStatusId,
                             work.SelectedInd,
                             gac.Code_Id,
                             work.Project_Id,
                             work.Notes,
                             ChildRowId = work.Id,
                             WorkflowCodeId = work.Code_Id,

                             gppp.ProjectPackage_Id,
                             gppp.Display_Text,

                             gpp.PACKAGE_NAME

                         }).Distinct().ToListAsync();


            List<CompanyWorkflowProjectAllDataSet> companyWorkflowProjectAllDatas = new();


            foreach (var row in query)
            {

                Console.WriteLine(row.ChildRowId);
                    //CodeId = row.Code_Id,
                Console.WriteLine(row.WorkflowCodeId);
                Console.WriteLine(row.Code_Id);
                //Console.WriteLine(row.Id);
                Console.WriteLine(row.CompanyId);
                //Console.WriteLine(row.COMPANY_NAME);
                //Console.WriteLine(row.COMPANY_CONTACT_INFORMATION_EMAIL);
                //Console.WriteLine( row.COMPANY_CONTACT_INFORMATION_FIRST_NAME);
                //Console.WriteLine( row.COMPANY_CONTACT_INFORMATION_LAST_NAME);
                //Console.WriteLine("CompositeKey: "+row.Id.ToString() + row.Code_Id.ToString() + AbpSession.TenantId.ToString());
                //Console.WriteLine(Int32.Parse(row.Project_Id.ToString() + row.Code_Id.ToString()));
                Console.WriteLine(row.Workflow_StatusId);
                Console.WriteLine(row.Workflow_SubStatusId);
                Console.WriteLine( row.SelectedInd);
                Console.WriteLine( row.Notes);
                //Console.WriteLine( row.ProjectPackage_Id);
                
            }

            foreach (var row in query)
            {
                CompanyWorkflowProjectAllDataSet projectAllData = new()
                {
                    ChildRowId = row.ChildRowId,
                    //CodeId = row.Code_Id,
                    CodeId = row.WorkflowCodeId,
                    CompanyId = row.CompanyId,
                    CompanyName = row.COMPANY_NAME,
                    CompanyContactInformationEmail = row.COMPANY_CONTACT_INFORMATION_EMAIL,
                    CompanyContactInformationFirstName = row.COMPANY_CONTACT_INFORMATION_FIRST_NAME,
                    CompanyContactInformationLastName = row.COMPANY_CONTACT_INFORMATION_LAST_NAME,
                    CompositeKey = row.CompanyId.ToString() + row.WorkflowCodeId.ToString() + AbpSession.TenantId.ToString(),
                    Id = Int32.Parse(row.Project_Id.ToString() + row.WorkflowCodeId.ToString()),
                    WorkflowStatusId = row.Workflow_StatusId,
                    WorkflowSubStatusId = row.Workflow_SubStatusId,
                    SelectedInd = row.SelectedInd,
                    Notes = row.Notes,
                    ProjectPackageId  = row.ProjectPackage_Id
                };

                companyWorkflowProjectAllDatas.Add(projectAllData);
            }

            return query;        
        }


        public async static Task<List<ZADMINCODE>> GetZADMINCODES(AppDBContext _db)
        {
            var query = await _db.ZADMINCODEs.Select(c => c).ToListAsync();

            foreach (var item in query)
            {
                Console.WriteLine("Item: " + item.CODE_NAME);
            }

            return query;
        }
        
        public async static Task<List<ZUSER_AssignTo_zCOMPANY>> GetZUSER_AssignTo_zCOMPANYs(AppDBContext _db)//ZUSER_AssignTo_zCOMPANY
        {
            var query = await _db.ZUSER_AssignTo_zCOMPANYs.Select(c => c).ToListAsync();

            foreach (var item in query)
            {
                Console.WriteLine("Item.User_Id: " + item.User_Id);
            }

            return query;
        }


        public static async Task<object>  ViewProjectCodesGrid(int projectId, int companyId, AppDBContext _db)
        {
            //select* from ZCOMPANY_Workflow_PROJECT A
            //left outer join zADMINCODE_AssignTo_zPROJECT B on A.Code_Id = B.Code_Id
            //left outer join zPROJECTPACKAGE_AssignTo_zPROJECT C on A.ProjectPackage_Id = C.ProjectPackage_Id
            //left outer join zUSER_AssignTo_zCOMPANY D on A.Company_Id = D.Company_Id
            //left outer join zADMINCODE E on B.Code_Id = E.Id

            var queryB = await (from work in  _db.ZCOMPANY_Workflow_PROJECTs
                         
                         join zacproj in _db.ZADMINCODE_AssignTo_zPROJECTs on work.Code_Id equals zacproj.Code_Id into groupedZACPROJ
                         from gzp in groupedZACPROJ.DefaultIfEmpty()

                         join zac in _db.ZADMINCODEs on gzp.Code_Id equals zac.Id into groupedZAC
                         from gz in groupedZAC.DefaultIfEmpty()

                         join projpackproj in _db.ZPROJECTPACKAGE_AssignTo_zPROJECTs on work.ProjectPackage_Id equals projpackproj.ProjectPackage_Id into groupedPROJPACKPROJ
                         from gppp in groupedPROJPACKPROJ.DefaultIfEmpty()

                         join projpackage in _db.ZPROJECTPACKAGEs on work.ProjectPackage_Id equals projpackage.Id into groupedProjectPackages
                         from gpp in groupedProjectPackages.DefaultIfEmpty()

                         join usr in _db.ZUSER_AssignTo_zCOMPANYs on work.Company_Id equals usr.Company_Id into groupedUSR
                         from gu in groupedUSR.DefaultIfEmpty()

                         

                        where work.Project_Id == projectId
                            && work.TenantId == AbpSession.TenantId
                            && gu.Company_Id == companyId

                          select new
                         {

                              ProjectPackageOrTrade = work.ProjectPackage_Id != null ? "P" : gz.CODE,
                              //gz.Id,
                              gz.ZCodeCategoryId,
                              gz.CODE,
                              //gz.CODE_NAME,
                              CodeName = work.ProjectPackage_Id != null ?
                                         gpp.PACKAGE_NAME :
                                          gz.CODE_NAME,

                              gz.CODE_DESCRIPTION,

                              WorkflowId = work.Id,
                              UserCompanyId = gu.Company_Id,
                              work.Company_Id,
                              work.Workflow_StatusId,
                              work.Workflow_SubStatusId,
                              work.STATUS,
                              CompanyName = _db.ZCOMPANYs.First(n => n.Id == work.Company_Id).COMPANY_NAME,

                              gpp.PACKAGE_NAME,

                          }).Distinct().ToListAsync();





            Console.WriteLine(queryB);


            var query = await (from proj in _db.ZPROJECTs
                         join zacproj in _db.ZADMINCODE_AssignTo_zPROJECTs on proj.Id equals zacproj.Project_Id
                         join cwp in _db.ZCOMPANY_Workflow_PROJECTs on zacproj.Code_Id equals cwp.Code_Id
                         join usr in _db.ZUSER_AssignTo_zCOMPANYs on cwp.Company_Id equals usr.Company_Id
                         join zac in _db.ZADMINCODEs on zacproj.Code_Id equals zac.Id


                         //from proj in _db.ZPROJECTs
                         //join zacproj in _db.ZADMINCODE_AssignTo_zPROJECTs on proj.Id equals zacproj.Project_Id
                         //join zac in _db.ZADMINCODEs on zacproj.Code_Id equals zac.Id
                         //join cwp in _db.ZCOMPANY_Workflow_PROJECTs on proj.Id equals cwp.Project_Id 
                         //join usr in _db.ZUSER_AssignTo_zCOMPANYs on cwp.Company_Id equals usr.Company_Id


                         where proj.Id == projectId
                             && proj.TenantId == AbpSession.TenantId
                             && usr.Company_Id == companyId
                             //&& cwp.Workflow_SubStatusId != 110
                         select new
                         {
                             zac.Id,
                             zac.ZCodeCategoryId,
                             zac.CODE,
                             zac.CODE_NAME,
                             zac.CODE_DESCRIPTION,
                             WorkflowId = cwp.Id,
                             UserCompanyId = usr.Company_Id,
                             cwp.Company_Id,
                             cwp.Workflow_StatusId,
                             cwp.Workflow_SubStatusId,
                             cwp.STATUS,
                             CompanyName = _db.ZCOMPANYs.First(n => n.Id == cwp.Company_Id).COMPANY_NAME
                         }).Distinct().ToListAsync();

            var distinctResult = query.DistinctBy(x => x.Id).ToList();

            return query;
        }

        public async static Task Main(string[] args)
        {
            AppDBContext _db = new ();

            //Id        COMPANY_NAME
            //6         MCSZA
            //7         MCSAU
            //3002      Microsoft
            //3003      Netflix
            //3005      Apple
            //3015      JSWebDev

            //await AllItemParentGrid(_db);
            //await AllItemsChildGrid(projectId, selectedInd, nonSelectedInd, _db);
            //await GetZADMINCODES(_db);
            //await GetZUSER_AssignTo_zCOMPANYs(_db);
            //await ViewProjectCodesGrid(projectId, 3003, _db);
            await UnionQuery(projectId, 6, _db);
            //TODO: do stuff


        }

        public void Create()
        {
            //var dbName = "gulfdb.db";

            //if (File.Exists(dbName))
            //{
            //    //File.Delete(dbName);    

            //}

            //await using (var dbContext = new AppDBContext())
            //{
            //    await dbContext.Database.EnsureCreatedAsync();
            //    await dbContext.ZADMINCODEs.AddRangeAsync(new ZADMINCODE[]
            //    {
            //         new ZADMINCODE (){CODE = "00 00 01", CODE_NAME = "Test01", CODE_DESCRIPTION = "TEstDesc"},
            //         new ZADMINCODE (){CODE = "00 00 02", CODE_NAME = "Test02", CODE_DESCRIPTION = "TEstDesc"}
            //    });
            //    await dbContext.SaveChangesAsync();
            //}
        }

        protected override void ExecuteCore()
        {
            throw new NotImplementedException();
        }

        protected static async Task<object> UnionQuery(int projectId, int companyId, AppDBContext _db)
        {

            var PackageQuery = (from A in _db.ZPROJECTs
                                join B in _db.ZPROJECTPACKAGE_AssignTo_zPROJECTs on A.Id equals B.Project_Id
                                join C in _db.ZPROJECTPACKAGEs on B.ProjectPackage_Id equals C.Id

                                where A.Id == projectId && A.TenantId == AbpSession.TenantId

                                select new
                                {
                                    Id = "P" + C.Id,
                                    Code = "P",
                                    Name = C.PACKAGE_NAME,

                                }).ToList();
            
            var TradeQuery = (from A in _db.ZPROJECTs
                                join B in _db.ZADMINCODE_AssignTo_zPROJECTs on A.Id equals B.Project_Id
                                join C in _db.ZADMINCODEs on B.Code_Id equals C.Id

                                where A.Id == projectId && A.TenantId == AbpSession.TenantId

                                select new
                                {
                                    Id = "T" + C.Id,
                                    Code = C.CODE,
                                    Name = C.CODE_NAME,

                                }).ToList();           

            var unionResult = PackageQuery.Union(TradeQuery).OrderByDescending(c => c.Id).ToList();

            foreach(var item in unionResult)
            {
                await Console.Out.WriteAsync("Id: " + item.Id + "   ");
                await Console.Out.WriteAsync("\tCode: " + item.Code + "   ");
                await Console.Out.WriteLineAsync("\tName: " + item.Name);
            }

            return unionResult;
        }
    }


}



