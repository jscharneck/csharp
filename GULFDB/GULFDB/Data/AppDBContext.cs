﻿using GULFDB.Models;
using Microsoft.EntityFrameworkCore;

namespace GULFDB.Data
{
    public  class AppDBContext : DbContext
    {
        public DbSet<ZCOMPANY_Workflow_PROJECT> ZCOMPANY_Workflow_PROJECTs { get; set; }       
        public DbSet<ZCOMPANY> ZCOMPANYs { get; set; }       
        public DbSet<ZCOUNTRY> ZCOUNTRYs { get; set; }       
        public DbSet<ZADMINCODE> ZADMINCODEs { get; set; }       
        public DbSet<ZCodeCategories> ZCodeCategories { get; set; }       
        public DbSet<ZADMINCODE_AssignTo_zPROJECT> ZADMINCODE_AssignTo_zPROJECTs { get; set; }       
        public DbSet<ZPROJECTPACKAGE_AssignTo_zPROJECT> ZPROJECTPACKAGE_AssignTo_zPROJECTs { get; set; }       
        public DbSet<ZPROJECTPACKAGE> ZPROJECTPACKAGEs { get; set; }       
        public DbSet<ZADMINCODE_AssignTo_zCOMPANY> ZADMINCODE_AssignTo_zCOMPANYs { get; set; }       

        //public AppDBContext(){  }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER2019; Database=ASPNETCORE_MASTER_DB;  User=MCSSys; Password=Sh1ftM0n; Trusted_Connection=True; TrustServerCertificate=True");
        }
    }
}
