using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreLeaveSystem.Repository
{
    public class VacationTypeRepository : IVacationTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public VacationTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(VacationType entity)
        {
            await _db.VacationTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(VacationType entity)
        {
             _db.VacationTypes.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<VacationType>> FindAll()
        {
            //Returning All VacationTypes Table Records
            return await _db.VacationTypes.ToListAsync();
        }

        public async Task<VacationType> FindById(int id)
        {
            //Return a specific record by ID
            return await _db.VacationTypes.FindAsync(id);
        }

     public  ICollection<VacationType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

      public async Task<bool> isExists(int id)
        {
            var exists = await _db.VacationTypes.AnyAsync(q => q.Id == id);
            return exists;
        }

       public async Task<bool> Save()
        {
            //Saving changes and returning their number if present
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

       public async Task<bool> Update(VacationType entity)
        {
            _db.VacationTypes.Update(entity);
            return await Save();
        }
    }
}
