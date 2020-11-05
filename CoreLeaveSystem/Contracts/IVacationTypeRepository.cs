using CoreLeaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Contracts
{
    public interface IVacationTypeRepository : IRepositoryBase<VacationType>
    {
        ICollection<VacationType> GetEmployeesByLeaveType(int id);
    }
}
