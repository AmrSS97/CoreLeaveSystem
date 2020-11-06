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
        public bool Create(Employee entity)
        {
            _db.Employees.Add(entity);
            return Save();
        }

        public bool Delete(Employee entity)
        {
            _db.Employees.Remove(entity);
            return Save();
        }

        public ICollection<Employee> FindAll()
        {
            return _db.Employees.ToList();
        }


        public Employee FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Employee FindByIdString(string id)
        {
            return _db.Employees.Find(id);
        }

        public bool isExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Employee entity)
        {
            _db.Employees.Update(entity);
            return Save();
        }
    }
}
