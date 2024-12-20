﻿using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Interface
{
    public interface IInvoiceService
    {
        Task<IBusinessResult> GetAll(
            string searchId = null,
            string paymentMethod = null,
            string status = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int pageNumber = 1,
            int pageSize = 3);
        Task<IBusinessResult> GetById(string code);
        //Task<IBusinessResult> Create(InvoiceDTO invoice);
        //Task<IBusinessResult> Create(Invoice invoice);
        //Task<IBusinessResult> Update(Invoice invoice);
        Task<IBusinessResult> Save(Invoice invoice);
        Task<IBusinessResult> DeleteById(string Id);
        Task<IBusinessResult> SearchById(string searchId);
    }
}
