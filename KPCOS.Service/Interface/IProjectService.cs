using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IProjectService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> GetProjectsAsync(string customerName, string? designerId, DateTime? startDate, string? status);
        //Task<IBusinessResult> Create(Project project);
        //Task<IBusinessResult> Update(Project project);
        Task<IBusinessResult> Save(Project project);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
