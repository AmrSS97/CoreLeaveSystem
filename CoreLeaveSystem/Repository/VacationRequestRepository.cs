using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Repository
{
    public class VacationRequestRepository : IVacationRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public VacationRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(VacationRequest entity)
        {
            _db.VacationRequests.Add(entity);
            return Save();
        }

        public bool Delete(VacationRequest entity)
        {
            _db.VacationRequests.Remove(entity);
            return Save();
        }

        public ICollection<VacationRequest> FindAll()
        {
            return _db.VacationRequests.Include(q => q.RequestingEmployee).Include(q => q.ApprovedBy).Include(q => q.VacationType).ToList();
        }

        public VacationRequest FindById(int id)
        {
            return _db.VacationRequests.Include(q => q.RequestingEmployee).Include(q => q.ApprovedBy).Include(q => q.VacationType).FirstOrDefault(q => q.Id == id);
        }

        public ICollection<VacationRequest> GetVacationRequestByEmployee(string employeeid)
        {
            var vacationRequests = FindAll().Where(q => q.RequestingEmployeeId == employeeid).ToList();
            return vacationRequests;
        }

        public bool isExists(int id)
        {
            var exists = _db.VacationRequests.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(VacationRequest entity)
        {
            _db.VacationRequests.Update(entity);
            return Save();
        }
    }
}
