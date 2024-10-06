using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IInvoiceService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(Invoice invoice);
        //Task<IBusinessResult> Update(Invoice invoice);
        Task<IBusinessResult> Save(Invoice invoice);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
