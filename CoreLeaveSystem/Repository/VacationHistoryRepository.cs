using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Repository
{
    public class VacationHistoryRepository : IVacationHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public VacationHistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(VacationHistory entity)
        {
            _db.VacationHistories.Add(entity);
            return Save();
        }

        public bool Delete(VacationHistory entity)
        {
            _db.VacationHistories.Remove(entity);
            return Save();
        }

        public ICollection<VacationHistory> FindAll()
        {
            return _db.VacationHistories.ToList();
        }

        public VacationHistory FindById(int id)
        {
            return _db.VacationHistories.Find(id);
        }

        public bool isExists(int id)
        {
            var exists = _db.VacationHistories.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(VacationHistory entity)
        {
            _db.VacationHistories.Update(entity);
            return Save();
        }
    }
}
