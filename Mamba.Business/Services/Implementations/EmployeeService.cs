using AutoMapper;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Repositories.İnterfaces;
using Mamba.Data.Repositories.Implementations;
using Mamba2.DAL;
using Mamba2.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyBiz.Business.Extensions.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Implementations
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeePositionRepository _EmployeeRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmployeePositionRepository _EmployeePositionRepository;
        private readonly AppDbContext _context;

        public EmployeeService(IEmployeePositionRepository EmployeeRepository,
                                IMapper mapper,
                                IWebHostEnvironment webHostEnvironment,
                                IEmployeePositionRepository EmployeePositionRepository,
                                AppDbContext context)
        {
            _EmployeeRepository = EmployeeRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _EmployeePositionRepository = EmployeePositionRepository;
            _context = context;
        }

        public async Task CreateAsync([FromForm] EmployeeAddedto EmployeeAddedto)
        {
            Employee Employee = _mapper.Map<Employee>(EmployeeAddedto);
            bool check = false;

            if (EmployeeAddedto.PositionIds != null)
            {
                foreach (int PositionId in EmployeeAddedto.PositionIds)
                {
                    if (!_EmployeePositionRepository.Table.Any(Position => Position.Id == PositionId))
                    {
                        check = true;
                        break;
                    }
                }
            }

            if (!check)
            {
                if (EmployeeAddedto.PositionIds != null)
                {
                    foreach (int PositionId in EmployeeAddedto.PositionIds)
                    {
                        EmployeePosition EmployeePosition = new EmployeePosition
                        {
                            Employee = Employee,
                            PositionId = PositionId,
                        };

                        await _EmployeePositionRepository.CreateAsync(EmployeePosition);
                    }
                }
            }
            else
            {
                throw new notFound("PositionId is not found");

            }

            if (EmployeeAddedto.ImageFile != null)
            {
                if (EmployeeAddedto.ImgFile.ContentType != "image/png" && EmployeeAddedto.ImgFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentTypeOrSize("enter the correct image contenttype!");
                }

                if (EmployeeAddedto.ImgFile.Length > 1048576)
                {
                    throw new InvalidImageContentTypeOrSize("image size must be less than 1mb!");
                }
            }
            else
            {
                throw new InvalidImage("Image is required!");
            }

            string folder = "Uploads/Employees-images";
            string newEmployeeImageUrl = await Helper.GetFileName(_webHostEnvironment.WebRootPath, folder, EmployeeAddedto.ImgFile);


            Employee.AddedDate = DateTime.UtcNow.AddHours(4);
            Employee.UpdatedDate = DateTime.UtcNow.AddHours(4);
           

            Employee.EmployeeImageUrl = newEmployeeImageUrl;
            Employee.IsDeleted = false;

            await _EmployeeRepository.CreateAsync(Employee);
            await _EmployeeRepository.CommitChanges();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeGetDto>> GetAllAsync(string? input, int? PositionId, int? orderId)
        {
            IQueryable<Employee> Employees = _EmployeeRepository.Table.Include(Employee => Employee.EmployeePositions).Where(Employee => Employee.IsDeleted == false).AsQueryable();
            if (Employees is not null)
            {
                if (input is not null)
                {
                    Employees = Employees.Where(Employee => Employee.FullName.ToLower().Contains(input.ToLower()) || Employee.Description.ToLower().Contains(input.ToLower()));
                }

                if (PositionId is not null)
                {
                    Employees = Employees.Where(Employee => Employee.EmployeePositions.Any(Employee => Employee.PositionId == PositionId));
                }

                if (orderId is not null)
                {
                    switch (orderId)
                    {
                        case 1:
                            Employees = Employees.OrderByDescending(Employee => Employee.AddedDate);
                            break;
                        case 2:
                            Employees = Employees.OrderBy(Employee => Employee.Salary);
                            break;
                        case 3:
                            Employees = Employees.OrderBy(Employee => Employee.FullName);
                            break;
                        default:
                            throw new notFound("enter the correct order value!");
                    }
                }
            }

            IEnumerable<EmployeeGetDto> EmployeeGetDtos = Employees.Select(Employee => new EmployeeGetDto { Id = Employee.Id, FullName = Employee.FullName, Description = Employee.Description, MediaUrl = Employee.MediaUrl, Salary = Employee.Salary });

            return EmployeeGetDtos;
        }

        public async Task<EmployeeGetDto> GetByIdAsync(int id)
        {

            Employee Employee = await _EmployeeRepository.GetByIdAsync(Employee => Employee.Id == id && Employee.IsDeleted == false);

            if (Employee == null) throw new notFound("Employee couldn't be null!");

            EmployeeGetDto EmployeeGetDto = _mapper.Map<EmployeeGetDto>(Employee);

            return EmployeeGetDto;
        }

        public IQueryable<Employee> GetEmployeeTable()
        {
            var query = _EmployeeRepository.Table.AsQueryable();

            return query;
        }

        public async Task ToggleDelete(int id)
        {
            string folder = "Uploads/Employees-images";

            Employee Employee = await _EmployeeRepository.GetByIdAsync(Employee => Employee.Id == id);

            if (Employee == null) throw new notFound("Employee couldn't be null!");

            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, folder, Employee.EmployeeImageUrl);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            Employee.IsDeleted = !Employee.IsDeleted;
            

            await _EmployeeRepository.CommitChanges();
        }

        public async Task UpdateAsync([FromForm] EmployeeUpdateDto EmployeeUpdateDto)
        {
            Employee Employee = await _EmployeeRepository.GetByIdAsync(Employee => Employee.Id == EmployeeUpdateDto.Id && Employee.IsDeleted == false, "EmployeePositions.Position");

            if (Employee == null) throw new notFound("Employee couldn't be null!");

            Employee.EmployeePositions.RemoveAll(wp => !EmployeeUpdateDto.PositionIds.Contains(wp.PositionId));

            foreach (var PositionId in EmployeeUpdateDto.PositionIds.Where(pId => !Employee.EmployeePositions.Any(wp => wp.PositionId == pId)))
            {
                EmployeePosition EmployeePosition = new EmployeePosition
                {
                    Employee = Employee,
                    PositionId = PositionId,
                };

                await _EmployeePositionRepository.CreateAsync(EmployeePosition);
            }

            if (EmployeeUpdateDto.ImgFile != null)
            {
                if (EmployeeUpdateDto.ImgFile.ContentType != "image/png" && EmployeeUpdateDto.ImgFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentTypeOrSize("enter the correct image contenttype!");
                }

                if (EmployeeUpdateDto.ImgFile.Length > 1048576)
                {
                    throw new InvalidImageContentTypeOrSize("image size must be less than 1mb!");
                }

                string folder = "Uploads/Employees-images";
                string newEmployeeImageUrl = await Helper.GetFileName(_webHostEnvironment.WebRootPath, folder, EmployeeUpdateDto.ImgFile);

                string oldImgPath = Path.Combine(_webHostEnvironment.WebRootPath, folder, Employee.EmployeeImageUrl);

                if (System.IO.File.Exists(oldImgPath))
                {
                    System.IO.File.Delete(oldImgPath);
                }

                Employee.EmployeeImageUrl = newEmployeeImageUrl;

            }

            Employee = _mapper.Map(EmployeeUpdateDto, Employee);
            Employee.AddedDate = DateTime.UtcNow.AddHours(4);

            await _EmployeeRepository.CommitChanges();
        }
    }
}
