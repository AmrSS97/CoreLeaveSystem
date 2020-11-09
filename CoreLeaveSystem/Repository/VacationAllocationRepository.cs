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

        public async Task<bool> CheckAllocation(int vacationtypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return  allocations.Where(q => q.EmployeeId == employeeid && q.VacationTypeId == vacationtypeid && q.Period == period).Any();
        }

        public async Task<bool> Create(VacationAllocation entity)
        {
            await _db.VacationAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(VacationAllocation entity)
        {
            _db.VacationAllocations.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<VacationAllocation>> FindAll()
        {
            var vacationallocations = await _db.VacationAllocations.Include(q => q.VacationType).ToListAsync();
            return vacationallocations;
        }

        public async Task<VacationAllocation> FindById(int id)
        {
            var vacationallocation =await  _db.VacationAllocations.Include(q => q.VacationType).Include(q => q.Employee).FirstOrDefaultAsync(q => q.Id == id);
            return vacationallocation;
        }

        public async Task<ICollection<VacationAllocation>> GetVacationAllocationsByEmployee(string employeeid)
        {
            var period = DateTime.Now.Year;
            var vacationAllocations = await FindAll();
            return vacationAllocations.Where(q => q.EmployeeId == employeeid && q.Period == period).ToList();
        }

        public async Task<VacationAllocation> GetVacationAllocationsByEmployeeAndType(string employeeid, int vacationtypeid)
        {
            var period = DateTime.Now.Year;
            var vacationAllocations = await FindAll();
            return  vacationAllocations.FirstOrDefault(q => q.EmployeeId == employeeid && q.Period == period && q.VacationTypeId == vacationtypeid);
        }

        public async Task<bool> isExists(int id)
        {
            var exists = await _db.VacationAllocations.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            //Saving Changes and returning their number
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(VacationAllocation entity)
        {
            _db.VacationAllocations.Update(entity);
            return await Save();
        }
    }
}
