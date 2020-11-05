using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Repository
{
    public class VacationTypeRepository : IVacationTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public VacationTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(VacationType entity)
        {
            _db.VacationTypes.Add(entity);
            return Save();
        }

        public bool Delete(VacationType entity)
        {
            _db.VacationTypes.Remove(entity);
            return Save();
        }

        public ICollection<VacationType> FindAll()
        {
            //Returning All VacationTypes Table Records
            return _db.VacationTypes.ToList();
        }

        public VacationType FindById(int id)
        {
            //Return a specific record by ID
            return _db.VacationTypes.Find(id);
        }

        public ICollection<VacationType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            //Saving changes and returning their number if present
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(VacationType entity)
        {
            _db.VacationTypes.Update(entity);
            return Save();
        }
    }
}
