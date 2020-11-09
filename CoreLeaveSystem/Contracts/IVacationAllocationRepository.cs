using CoreLeaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Contracts
{
    public interface IVacationAllocationRepository : IRepositoryBase<VacationAllocation>
    {
        Task<bool> CheckAllocation(int vacationtypeid, string employeeid);
        Task<ICollection<VacationAllocation>> GetVacationAllocationsByEmployee(string employeeid);
        Task<VacationAllocation> GetVacationAllocationsByEmployeeAndType(string employeeid, int vacationtypeid);
    }
}
