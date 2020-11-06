using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoreLeaveSystem.Models;

namespace CoreLeaveSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<VacationHistory> VacationHistories { get; set; }
        public DbSet<VacationAllocation> VacationAllocations { get; set; }
        public DbSet<CoreLeaveSystem.Models.VacationTypeVM> VacationTypeVM { get; set; }
        public DbSet<CoreLeaveSystem.Models.EmployeeVM> EmployeeVM { get; set; }
        public DbSet<CoreLeaveSystem.Models.VacationAllocationVM> VacationAllocationVM { get; set; }
        public DbSet<CoreLeaveSystem.Models.EditVacationAllocationVM> EditVacationAllocationVM { get; set; }

    }
}
