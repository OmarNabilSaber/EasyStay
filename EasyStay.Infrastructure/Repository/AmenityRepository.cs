using EasyStay.Application.Common.Interfaces;
using EasyStay.Domain.Entities;
using EasyStay.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyStay.Infrastructure.Repository
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        public AmenityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Amenity amenity)
        {
            _db.Amenities.Update(amenity);
        }
    }
}
