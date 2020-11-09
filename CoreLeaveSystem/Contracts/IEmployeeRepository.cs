using CoreLeaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        public Task<Employee> FindByIdStringAsync(string id);
    }
}
