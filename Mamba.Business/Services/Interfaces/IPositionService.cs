using Mamba2.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Interfaces
{
    public interface IPositionService
    {
        Task CreateAsync([FromForm] PositionCreateDto PositionCreateDto);
        Task Delete(int id);
        Task ToggleDelete(int id);
        IQueryable<Position> GetPositionTable();
        Task<PositionGetDto> GetByIdAsync(int id);
        Task<IEnumerable<PositionGetDto>> GetAllAsync();
        Task UpdateAsync([FromForm] PositionUpdateDto PositionUpdateDto);
    }
}
