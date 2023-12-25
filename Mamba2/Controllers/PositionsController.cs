using AutoMapper;
using Mamba2.DAL;
using Mamba2.DTOs;
using Mamba2.Entities;
using Microsoft.AspNetCore.Mvc;
using MyBiz.Business.Extensions.Helper;

namespace Mamba2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController:ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public PositionsController(AppDbContext context, IWebHostEnvironment environment, IMapper mapper)
        {
            _context = context;
            _environment = environment;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var p = _context.Positions.FirstOrDefault(x => x.Id == id);
            if (p == null)
            {
                return NotFound();
            }
            PositionGetDto dto = _mapper.Map<PositionGetDto>(p);
            return Ok();

        }
        [HttpPost]
        public IActionResult Create( PositionCreateDto dto)
        {
            var p = _mapper.Map<Position>(dto);
            
            p.AddedDate = DateTime.UtcNow.AddHours(4);
            p.UpdatedDate = DateTime.UtcNow.AddHours(4);
                    

            p.IsDeleted = false;
            _context.Positions.Add(p);
            _context.SaveChanges();

            return StatusCode(201, new { message = "Object created" });

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, PositionUpdateDto dto)
        {
            var p = _context.Positions.FirstOrDefault(x => x.Id == id);
            if (p == null)
            {
                return NotFound();

            }
            p = _mapper.Map(dto, p);
            p.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _context.Positions.Add(p);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var p = _context.Positions.Find(id);

            if (p is null)
            {
                return NotFound();
            }

            p.IsDeleted = true;
            p.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return StatusCode(201, new { message = "Object deleted" });
        }
    }
}
