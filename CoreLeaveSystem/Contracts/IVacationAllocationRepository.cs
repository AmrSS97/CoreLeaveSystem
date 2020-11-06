using CoreLeaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Contracts
{
    public interface IVacationAllocationRepository : IRepositoryBase<VacationAllocation>
    {
        bool CheckAllocation(int vacationtypeid, string employeeid);
        ICollection<VacationAllocation> GetVacationAllocationsByEmployee(string id);
    }
}
