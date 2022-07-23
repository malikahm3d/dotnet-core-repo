using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DAL.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void update(CoverType Obj);
    }
}
