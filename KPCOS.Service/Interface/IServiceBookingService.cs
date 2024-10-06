using KPCOS.Data.Models;
using KPCOS.Service.Base;
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
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(ServiceBooking serviceBooking);
        //Task<IBusinessResult> Update(ServiceBooking serviceBooking);
        Task<IBusinessResult> Save(ServiceBooking serviceBooking);
        Task<IBusinessResult> DeleteById(string Id);
    }
}
