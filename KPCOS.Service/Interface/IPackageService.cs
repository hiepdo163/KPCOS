using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IPackageService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(Package package);
        //Task<IBusinessResult> Update(Package package);
        Task<IBusinessResult> Save(Package package);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
