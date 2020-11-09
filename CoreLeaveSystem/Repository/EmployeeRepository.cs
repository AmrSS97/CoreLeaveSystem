using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Employee entity)
        {
            await _db.Employees.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Employee entity)
        {
            _db.Employees.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Employee>> FindAll()
        {
            return await _db.Employees.ToListAsync();
        }


        public Employee FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> FindByIdStringAsync(string id)
        {
            return await _db.Employees.FindAsync(id);
        }

        public bool isExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Save()
        {
            var changes =await  _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Employee entity)
        {
            _db.Employees.Update(entity);
            return await Save();
        }

        Task<Employee> IRepositoryBase<Employee>.FindById(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepositoryBase<Employee>.isExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
