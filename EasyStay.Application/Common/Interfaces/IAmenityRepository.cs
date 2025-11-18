using EasyStay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyStay.Application.Common.Interfaces
{
    public interface IAmenityRepository: IRepository<Amenity>
    {
        public void Update(Amenity amenity);
    }
}
