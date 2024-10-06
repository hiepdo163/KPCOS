using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IDesignService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(Design design);
        //Task<IBusinessResult> Update(Design design);
        Task<IBusinessResult> Save(Design design);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
