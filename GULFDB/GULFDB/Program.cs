using GULFDB.Data;
using GULFDB.Models;
using System.Text.Json.Nodes;
using System.Web.Mvc;

namespace GULFDB
{
    public class Program
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

        static AppDBContext _db; 
        
        public Program()
        {
            _db = new AppDBContext();
        }

        public static IQueryable<ZCOMPANY_Workflow_PROJECT> GetZCompanyWorkflowProjectsAsQueryable(AppDBContext db)
        {
            return db.ZCOMPANY_Workflow_PROJECTs.AsQueryable();
        }

        public static object RunQuery(AppDBContext _db)
        {
            int projectId = 1;
            bool selectedInd = false;
            bool nonSelectedInd = false;

            var queryTest2 = (from work in _db.ZCOMPANY_Workflow_PROJECTs

                              join assigntoproj in _db.ZADMINCODE_AssignTo_zPROJECTs on work.Code_Id equals assigntoproj.Code_Id into groupedCodesInProj
                              from gcip in groupedCodesInProj.DefaultIfEmpty()

                              join adcode in _db.ZADMINCODEs on work.Code_Id equals adcode.Id into codesgroup
                              from cg in codesgroup.DefaultIfEmpty()

                              join ppproj in _db.ZPROJECTPACKAGE_AssignTo_zPROJECTs on work.ProjectPackage_Id equals ppproj.ProjectPackage_Id into groupedPackageIds
                              from gpi in groupedPackageIds.DefaultIfEmpty()

                              join projpackage in _db.ZPROJECTPACKAGEs on work.ProjectPackage_Id equals projpackage.Id into groupedProjectPackages
                              from gpp in groupedProjectPackages.DefaultIfEmpty()

                              where work.Project_Id == projectId &&
                                       work.TenantId == AbpSession.TenantId &&
                                       work.Workflow_StatusId == StatusQualified &&
                                       (selectedInd && !nonSelectedInd ? (work.SelectedInd == selectedInd) :
                                       !selectedInd && nonSelectedInd ? (work.SelectedInd == selectedInd) : true)

                              select new
                              {

                                  ProjectPackageOrTrade = work.ProjectPackage_Id != null ?
                                                          gpp.PACKAGE_NAME :
                                                          cg.CODE,
                                  Code = _db.ZADMINCODEs.First(c => c.Id == work.Code_Id).CODE,
                                  CodeName = _db.ZADMINCODEs.First(c => c.Id == work.Code_Id).CODE_NAME,
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


                              })
                              .Distinct()
                              .ToList();


            foreach (var row in queryTest2)
            {
                Console.Write("CodeName: " + row.CodeName);
                //Console.WriteLine(row.PACKAGE_NAME);
                Console.WriteLine("ProjectPackageOrTrade: " + row.ProjectPackageOrTrade);
                Console.WriteLine("ProjectId: " + row.ProjectId);
                Console.WriteLine("CodeId:" + row.CodeId);
                Console.WriteLine("CodesOrPackagesCountQualified: " + row.CodesOrPackagesCountQualified);
                Console.WriteLine("CodesOrPackagesCountQualified: " + row.CodesOrPackagesCountInvited);
                Console.WriteLine("CodesOrPackagesCountAwarded: " + row.CodesOrPackagesCountAwarded);
                Console.WriteLine("ProjectPackage_Id: " + row.ProjectPackage_Id);
                Console.WriteLine("-----------------------------------------------------------");

            }

            return queryTest2;

            
        }

        public static object AllItemsChildGrid(int projectId, bool selectedInd, bool nonSelectedInd, AppDBContext _db)
        {
            var query = (from work in _db.ZCOMPANY_Workflow_PROJECTs

                         join adcode in _db.ZADMINCODE_AssignTo_zCOMPANYs on work.Code_Id equals adcode.Code_Id into groupedADCODE
                         from gac in groupedADCODE.DefaultIfEmpty()

                         join comp in _db.ZCOMPANYs on work.Company_Id equals comp.Id into groupedCOMP
                         from gcomp in groupedCOMP.DefaultIfEmpty()

                         join projpackproj in _db.ZPROJECTPACKAGE_AssignTo_zPROJECTs on work.ProjectPackage_Id equals projpackproj.ProjectPackage_Id into groupedPROJPACKPROJ
                         from gppp in groupedPROJPACKPROJ.DefaultIfEmpty()

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

                             gppp.ProjectPackage_Id

                         })
                            .Distinct()
                            .ToList();


            foreach (var row in query)
            {

                Console.WriteLine(row.ChildRowId);
                    //CodeId = row.Code_Id,
                Console.WriteLine(row.WorkflowCodeId);
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
                Console.WriteLine( row.ProjectPackage_Id);
                
            }

            return query;
            
        }

        public static void Main(string[] args)
        {
            AppDBContext _db = new ();
            int projectId = 1;
            bool selectedInd = false;
            bool nonSelectedInd = false;

            

            AllItemsChildGrid(projectId, selectedInd, nonSelectedInd, _db);
            RunQuery(_db);

            //var workflowQueryable = GetZCompanyWorkflowProjectsAsQueryable(_db);

            //var query = _db.ZCOMPANY_Workflow_PROJECTs.Where(c => c.Project_Id == 1).ToList();

            //var admincodesQuery = (from zac in _db.ZADMINCODEs
            //                       select zac).ToList();

            //var zcodeCatQuery = (from zcc in _db.ZCodeCategories
            //                     select zcc ).ToList();

            //var admincodesProjectQuery = (from acp in _db.ZADMINCODE_AssignTo_zPROJECTs
            //                              select acp ).ToList();

            //var projectPackagesQuery = (from ppp in _db.ZPROJECTPACKAGE_AssignTo_zPROJECTs
            //                            select ppp ).ToList();

            //var packagesQuery = (from pp in _db.ZPROJECTPACKAGEs
            //                     select pp ).ToList();

            //var admincodesCompanyQuery = (from aca in _db.ZADMINCODE_AssignTo_zCOMPANYs
            //                     select aca ).ToList();

            //var outerJoin = 

            //int projectId = 1;            
            //bool selectedInd = false;
            //bool nonSelectedInd = false;

            //var queryTest2 = (from work in _db.ZCOMPANY_Workflow_PROJECTs

            //                  join assigntoproj in _db.ZADMINCODE_AssignTo_zPROJECTs on work.Code_Id equals assigntoproj.Code_Id into groupedCodesInProj
            //                  from gcip in groupedCodesInProj.DefaultIfEmpty()

            //                  join adcode in _db.ZADMINCODEs on work.Code_Id equals adcode.Id into codesgroup
            //                  from cg in codesgroup.DefaultIfEmpty()

            //                  join ppproj in _db.ZPROJECTPACKAGE_AssignTo_zPROJECTs on work.ProjectPackage_Id equals ppproj.ProjectPackage_Id into groupedPackageIds
            //                  from gpi in groupedPackageIds.DefaultIfEmpty()

            //                  join projpackage in _db.ZPROJECTPACKAGEs on work.ProjectPackage_Id equals projpackage.Id into groupedProjectPackages
            //                  from gpp in groupedProjectPackages.DefaultIfEmpty()

            //                  where work.Project_Id == projectId &&
            //                           work.TenantId == AbpSession.TenantId &&
            //                           work.Workflow_StatusId == StatusQualified &&
            //                           (selectedInd && !nonSelectedInd ? (work.SelectedInd == selectedInd) :
            //                           !selectedInd && nonSelectedInd ? (work.SelectedInd == selectedInd) : true)

            //                  select new
            //                  {

            //                      ProjectPackageOrTrade = work.ProjectPackage_Id != null ?
            //                                              gpp.PACKAGE_NAME :
            //                                              cg.CODE,
            //                      Code = _db.ZADMINCODEs.First(c => c.Id == work.Code_Id).CODE,
            //                      CodeName = _db.ZADMINCODEs.First(c => c.Id == work.Code_Id).CODE_NAME,
            //                      CodeId = work.Code_Id,
            //                      ProjectId = work.Project_Id,
            //                      work.ProjectPackage_Id,
            //                      work.Workflow_StatusId,
            //                      gpp.PACKAGE_NAME,

            //                      CodesOrPackagesCountQualified = (from work in _db.ZCOMPANY_Workflow_PROJECTs
            //                                                       join codetocomp in _db.ZADMINCODE_AssignTo_zCOMPANYs on work.Code_Id equals codetocomp.Code_Id into groupedCTC
            //                                                       from ctc in groupedCTC.DefaultIfEmpty()

            //                                                       where ctc.Code_Id == gcip.Code_Id &&
            //                                                            work.Project_Id == projectId &&
            //                                                            work.Workflow_StatusId == StatusQualified
            //                                                       select new { work.Company_Id, work.Code_Id }).Distinct().ToList().Count,

            //                      CodesOrPackagesCountInvited = (from work in _db.ZCOMPANY_Workflow_PROJECTs
            //                                                     join codetocomp in _db.ZADMINCODE_AssignTo_zCOMPANYs on work.Code_Id equals codetocomp.Code_Id into groupedCTC
            //                                                     from ctc in groupedCTC.DefaultIfEmpty()

            //                                                     where ctc.Code_Id == gcip.Code_Id &&
            //                                                          work.Project_Id == projectId &&
            //                                                          work.Workflow_StatusId == StatusInvited
            //                                                     select new { work.Company_Id, work.Code_Id }).Distinct().ToList().Count,

            //                      CodesOrPackagesCountAwarded = (from work in _db.ZCOMPANY_Workflow_PROJECTs
            //                                                     join codetocomp in _db.ZADMINCODE_AssignTo_zCOMPANYs on work.Code_Id equals codetocomp.Code_Id into groupedCTC
            //                                                     from ctc in groupedCTC.DefaultIfEmpty()

            //                                                     where ctc.Code_Id == gcip.Code_Id &&
            //                                                          work.Project_Id == projectId &&
            //                                                          work.Workflow_StatusId == StatusAwarded
            //                                                     select new { work.Company_Id, work.Code_Id }).Distinct().ToList().Count,


            //                  }).Distinct().ToList();


            //foreach (var row in queryTest2)
            //{
            //    Console.Write("CodeName: " + row.CodeName);
            //    //Console.WriteLine(row.PACKAGE_NAME);
            //    Console.WriteLine("ProjectPackageOrTrade: " + row.ProjectPackageOrTrade);
            //    Console.WriteLine("ProjectId: " + row.ProjectId);
            //    Console.WriteLine("CodeId:" + row.CodeId);
            //    Console.WriteLine("CodesOrPackagesCountQualified: " + row.CodesOrPackagesCountQualified);
            //    Console.WriteLine("CodesOrPackagesCountQualified: " + row.CodesOrPackagesCountInvited);
            //    Console.WriteLine("CodesOrPackagesCountAwarded: " + row.CodesOrPackagesCountAwarded);
            //    Console.WriteLine("ProjectPackage_Id: " + row.ProjectPackage_Id);
            //    Console.WriteLine("-----------------------------------------------------------");

            //}            

            //var cwp = db.CompanyWorkflows.Where(c => c.Project_Id == 1);

            //DBQuerying dBQuerying = new(_db);


        }
    }


}



