using AutoMapper;
using Mamba2.DAL;
using Mamba2.DTOs;
using Mamba2.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBiz.Business.Extensions.Helper;

namespace Mamba2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public EmployeesController(AppDbContext context, IWebHostEnvironment environment, IMapper mapper)
        {
            _context = context;
            _environment = environment;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeGetDto), 200)]
        public IActionResult Get(int id)
        {
            var emp=_context.Employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            EmployeeGetDto dto=_mapper.Map<EmployeeGetDto>(emp);
            return Ok();

        }

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeCreateDto), 201)] 
        [ProducesResponseType(typeof(EmployeeCreateDto), 400)]
        public IActionResult Create([FromForm] EmployeeCreateDto dto)
        {
            var emp=_mapper.Map<Employee>(dto);
            bool check = true;
            if (dto.PositionIds!=null)
            {
                foreach (var positionId in dto.PositionIds)
                {
                    check = true;
                    break;

                }

            }
            if (dto.ImageFile!=null)
            {
                if (dto.ImageFile.ContentType!="image/png" && dto.ImageFile.ContentType!="image/jpeg")
                {
                    return BadRequest();
                }
                if (dto.ImageFile.Length > 1048576)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
            string folder = "uploads/Employee";
          

            emp.EmployeeImageUrl = Helper.SaveFile(_environment.WebRootPath, "uploads/Employee", dto.ImageFile);
            emp.AddedDate = DateTime.UtcNow.AddHours(4);
            emp.UpdatedDate = DateTime.UtcNow.AddHours(4);


            emp.IsDeleted = false;
            _context.Employees.Add(emp);
            _context.SaveChanges();

            return StatusCode(201, new { message = "Object created" });
            
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmployeeUpdateDto), 204)]
        [ProducesResponseType(typeof(EmployeeUpdateDto), 400)]
        [ProducesResponseType(typeof(EmployeeUpdateDto), 404)]
        public IActionResult Update([FromForm]int id,EmployeeUpdateDto dto)
        {
            var emp=_context.Employees.FirstOrDefault(x=>x.Id == id);
            if (emp == null)
            {
                return NotFound();

            }
            bool check = true;
            if (dto.PositionIds != null)
            {
                foreach (var positionId in dto.PositionIds)
                {
                    check = true;
                    break;

                }

            }
            if (dto.ImageFile != null)
            {
                if (dto.ImageFile.ContentType != "image/png" && dto.ImageFile.ContentType != "image/jpeg")
                {
                    return BadRequest();
                }
                if (dto.ImageFile.Length > 1048576)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
            string folder = "uploads/Employee";


            emp.EmployeeImageUrl = Helper.SaveFile(_environment.WebRootPath, "uploads/Employee", dto.ImageFile);
            emp = _mapper.Map(dto, emp);
            emp.UpdatedDate= DateTime.UtcNow.AddHours(4);   
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
      
        public IActionResult Delete(int id)
        {
            var emp = _context.Employees.Find(id);

            if (emp is null)
            {
                return NotFound();
            }

            emp.IsDeleted = true;
            emp.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return StatusCode(201, new { message = "Object deleted" });
        }
    }
}
