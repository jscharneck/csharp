using GULFDB.EFCore;

namespace GULFDB
{
    public class DBQuerying
    {
        private  readonly AppDBContext _db;

       
        public DBQuerying(AppDBContext db) 
        {
            _db = db;
        }

        public  void QueryTheDB()
        {
            var query = _db.ZCOMPANY_Workflow_PROJECTs.Select(c => c).ToList();


            foreach (var row in query)
            {
                Console.WriteLine(row);


            }

        }
    }
}
