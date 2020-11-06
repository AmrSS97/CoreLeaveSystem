using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Repository
{
    public class VacationAllocationRepository : IVacationAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public VacationAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CheckAllocation(int vacationtypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            return FindAll().Where(q => q.EmployeeId == employeeid && q.VacationTypeId == vacationtypeid && q.Period == period).Any();
        }

        public bool Create(VacationAllocation entity)
        {
            _db.VacationAllocations.Add(entity);
            return Save();
        }

        public bool Delete(VacationAllocation entity)
        {
            _db.VacationAllocations.Remove(entity);
            return Save();
        }

        public ICollection<VacationAllocation> FindAll()
        {
            var vacationallocations =  _db.VacationAllocations.Include(q => q.VacationType).ToList();
            return vacationallocations;
        }

        public VacationAllocation FindById(int id)
        {
            var vacationallocation = _db.VacationAllocations.Include(q => q.VacationType).Include(q => q.Employee).FirstOrDefault(q => q.Id == id);
            return vacationallocation;
        }

        public ICollection<VacationAllocation> GetVacationAllocationsByEmployee(string id)
        {
            var period = DateTime.Now.Year;
            return FindAll().Where(q => q.EmployeeId == id && q.Period == period).ToList();
        }

        public bool isExists(int id)
        {
            var exists = _db.VacationAllocations.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            //Saving Changes and returning their number
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(VacationAllocation entity)
        {
            _db.VacationAllocations.Update(entity);
            return Save();
        }
    }
}
