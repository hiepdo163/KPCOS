using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IMaterialServiec
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(Material material);
        //Task<IBusinessResult> Update(Material material);
        Task<IBusinessResult> Save(Material material);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
