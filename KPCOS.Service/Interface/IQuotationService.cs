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
    public interface IQuotationService
    {
        Task<IBusinessResult> GetAll(QuotationFilterParams request);
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(Quotation quotation);
        //Task<IBusinessResult> Update(Quotation quotation);
        Task<IBusinessResult> Save(Quotation quotation);
        Task<IBusinessResult> DeleteById(string Id);
        Task<IBusinessResult> GetByDesignId(string designId);
    }
}
