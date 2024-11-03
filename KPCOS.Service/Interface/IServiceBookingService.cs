using KPCOS.Common;
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
    public interface IServiceBookingService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetByPage(QueryPagedServiceBooking query);
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Create(ServiceBookingDTO request);
        Task<IBusinessResult> Update(string id, ServiceBookingDTO request);
        //Task<IBusinessResult> Save(ServiceBooking serviceBooking);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
