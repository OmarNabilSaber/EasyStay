using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyStay.Application.Common.Interfaces;
using EasyStay.Infrastructure.Data;
namespace EasyStay.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IVillaRepository Villa { get; private set; }
        public IVillaNumberRepository VillaNumber { get; private set; }
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            Villa = new VillaRepository(db);
            VillaNumber = new VillaNumberRepository(db);
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
