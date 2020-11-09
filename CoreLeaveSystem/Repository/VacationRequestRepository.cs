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

        public async Task<bool> Create(VacationRequest entity)
        {
            await _db.VacationRequests.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(VacationRequest entity)
        {
            _db.VacationRequests.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<VacationRequest>> FindAll()
        {
            return await _db.VacationRequests.Include(q => q.RequestingEmployee).Include(q => q.ApprovedBy).Include(q => q.VacationType).ToListAsync();
        }
    
public async Task<VacationRequest> FindById(int id)
        {
            return await _db.VacationRequests.Include(q => q.RequestingEmployee).Include(q => q.ApprovedBy).Include(q => q.VacationType).FirstOrDefaultAsync(q => q.Id == id);
        }

public async Task<ICollection<VacationRequest>> GetVacationRequestByEmployee(string employeeid)
        {
            var vacationRequests = await FindAll();
            return vacationRequests.Where(q => q.RequestingEmployeeId == employeeid).ToList(); ;
        }

public async Task<bool> isExists(int id)
        {
            var exists = await _db.VacationRequests.AnyAsync(q => q.Id == id);
            return  exists;
        }

public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

public async Task<bool> Update(VacationRequest entity)
        {
            _db.VacationRequests.Update(entity);
            return await Save();
        }
    }
}
