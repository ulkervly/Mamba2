using AutoMapper;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Repositories.İnterfaces;
using Mamba.Data.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Implementations
{
    public class PositionService:IPositionService
    {
        private readonly PositionRepository _PositionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionrepository PositionRepository, IMapper mapper)
        {
            _PositionRepository = PositionRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync([FromForm] PositionCreateDto PositionCreateDto)
        {
            Position Position = _mapper.Map<Position>(PositionCreateDto);

            Position.CreatedDate = DateTime.UtcNow.AddHours(4);
            Position.UpdatedDate = DateTime.UtcNow.AddHours(4);
            Position.DeletedDate = DateTime.UtcNow.AddHours(4);
            Position.IsDeleted = false;

            await _PositionRepository.CreateAsync(Position);
            await _PositionRepository.CommitChanges();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PositionGetDto>> GetAllAsync()
        {
            List<Position> Positions = await _PositionRepository.GetAllAsync(Position => Position.IsDeleted == false);

            IEnumerable<PositionGetDto> PositionGetDtos = Positions.Select(Position => new PositionGetDto { Id = Position.Id, Name = Position.Name });

            return PositionGetDtos;
        }

        public async Task<PositionGetDto> GetByIdAsync(int id)
        {

            Position Position = await _PositionRepository.GetByIdAsync(Position => Position.Id == id && Position.IsDeleted == false);

            if (Position == null) throw new notFound("Position couldn't be null!");

            PositionGetDto PositionGetDto = _mapper.Map<PositionGetDto>(Position);

            return PositionGetDto;
        }

        public IQueryable<Position> GetPositionTable()
        {
            var query = _PositionRepository.Table.AsQueryable();

            return query;
        }

        public async Task ToggleDelete(int id)
        {

            Position Position = await _PositionRepository.GetByIdAsync(Position => Position.Id == id);

            if (Position == null) throw new notFound("Position couldn't be null!");

            Position.IsDeleted = !Position.IsDeleted;
            Position.DeletedDate = DateTime.UtcNow.AddHours(4);

            await _PositionRepository.CommitChanges();
        }

        public async Task UpdateAsync([FromForm] PositionUpdateDto PositionUpdateDto)
        {

            Position Position = await _PositionRepository.GetByIdAsync(Position => Position.Id == PositionUpdateDto.Id);

            if (Position == null) throw new notFound("Position couldn't be null!");

            Position = _mapper.Map(PositionUpdateDto, Position);
            Position.UpdatedDate = DateTime.UtcNow.AddHours(4);

            await _PositionRepository.CommitChanges();
        }
    }
}
