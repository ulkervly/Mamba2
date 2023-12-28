using AutoMapper;
using Mamba.Core.Repositories.İnterfaces;
using Mamba2.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyBiz.Business.Extensions.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Interfaces
{
    public interface IEmployeeService
    {

        Task CreateAsync([FromForm] EmployeeCreateDto EmployeeCreateDto);
        Task Delete(int id);
        Task ToggleDelete(int id);
        IQueryable<Employee> GetEmployeeTable();
        Task<EmployeeGetDto> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeGetDto>> GetAllAsync(string? input, int? professionId, int? orderId);
        Task UpdateAsync([FromForm] EmployeeUpdateDto EmployeeUpdateDto);

    }
}
