using CoreLeaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Contracts
{
    public interface IVacationRequestRepository : IRepositoryBase<VacationRequest>
    {
        ICollection<VacationRequest> GetVacationRequestByEmployee(string employeeid);
    }
}
