using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
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
            return _db.VacationAllocations.ToList();
        }

        public VacationAllocation FindById(int id)
        {
            return _db.VacationAllocations.Find(id);
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
