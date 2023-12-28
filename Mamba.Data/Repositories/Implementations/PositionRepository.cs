using Mamba.Core.Repositories.İnterfaces;
using Mamba2.DAL;
using Mamba2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Data.Repositories.Implementations
{
    public class PositionRepository : GenericRepository<Position> , IPositionrepository
    {

        public PositionRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
