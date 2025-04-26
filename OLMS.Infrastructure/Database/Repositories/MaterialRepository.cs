//using Microsoft.EntityFrameworkCore;
//using OLMS.Domain.Entities;
//using OLMS.Domain.Repositories;
//using System;

//namespace OLMS.Infrastructure.Database.Repositories
//{
//    public class MaterialRepository : Repository<Material>, IMaterialRepository
//    {
//        public MaterialRepository(ApplicationDbContext context) : base(context)
//        {
//        }

//        public async override Task<Material?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
//        {
//            return await _context.Materials.SingleOrDefaultAsync(c => c.Id == id);
//        }
//    }
//}