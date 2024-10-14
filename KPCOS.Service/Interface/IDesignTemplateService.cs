using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IDesignTemplateService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(DesignTemplate designTemplate);
        //Task<IBusinessResult> Update(DesignTemplate designTemplate);
        Task<IBusinessResult> Save(DesignTemplate designTemplate);
        Task<IBusinessResult> DeleteById(string Id);
        Task<IBusinessResult> GetWithCondition(string name);
    }
}
