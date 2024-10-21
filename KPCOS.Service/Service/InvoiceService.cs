using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Data.Repository;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ProjectRepository _projectRepository;  
        private readonly UnitOfWork _unitOfWork;
        public InvoiceService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var invoices = await _unitOfWork.Invoice.GetAllAsync();
            if (invoices == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Invoice>());
            }
            else
            {
                var sortedInvoices = invoices.OrderByDescending(invoice => invoice.PaymentDate).ToList();
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, sortedInvoices);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(id);
            if (invoice == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Invoice());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, invoice);
            }
        }
        public async Task<IBusinessResult> Save(Invoice invoice)
        {
            try
            {
                if (invoice.DiscountApplied < 0 || invoice.DiscountApplied > invoice.TotalAmount)
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, "Invalid discount", "Discount cannot be negative or exceed total amount");
                }

                if (invoice.TaxAmount < 0 || invoice.TaxAmount > invoice.TotalAmount)
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, "Invalid tax amount", "Tax amount cannot be negative or exceed total amount");
                }

                if (invoice.TotalAmount < 0)
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, "Invalid total amount", "Total amount cannot be negative");
                }

                int result = -1;
                var invoiceTmp = await _unitOfWork.Invoice.GetByIdAsync(invoice.Id);
                if (invoiceTmp != null)
                {
                    invoiceTmp.DiscountApplied = invoice.DiscountApplied;
                    invoiceTmp.PaymentDate = invoice.PaymentDate;
                    invoiceTmp.PaymentMethod = invoice.PaymentMethod;
                    invoiceTmp.ProjectId = invoice.ProjectId;
                    invoiceTmp.Status = invoice.Status;
                    invoiceTmp.TaxAmount = invoice.TaxAmount;
                    invoiceTmp.TotalAmount = invoice.TotalAmount;

                    result = await _unitOfWork.Invoice.UpdateAsync(invoice);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, invoice);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.Invoice.CreateAsync(invoice);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, invoice);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, invoice);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        //public async Task<IBusinessResult> Create(InvoiceDTO invoiceDTO)
        //{
        //    if (invoiceDTO is null)
        //    {
        //        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
        //    }
        //    var projectExist = await _projectRepository.GetByIdAsync(invoiceDTO.ProjectId);
        //    if (projectExist is null)
        //    {
        //        return new BusinessResult(Const.WARNING_NO_DATA_CODE, "Project does not exist");
        //    }
        //    try
        //    {
        //        var invoice = new Invoice
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            DiscountApplied = invoiceDTO.DiscountApplied,
        //            PaymentDate = invoiceDTO.PaymentDate,
        //            PaymentMethod = invoiceDTO.PaymentMethod,
        //            ProjectId = invoiceDTO.ProjectId,
        //            Status = invoiceDTO.Status,
        //            TaxAmount = invoiceDTO.TaxAmount,
        //            TotalAmount = invoiceDTO.TotalAmount
        //        };
        //        var result = await _unitOfWork.Invoice.CreateAsync(invoice);
        //        if (result > 0)
        //        {
        //            return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, invoice);
        //        }
        //        else
        //        {
        //            return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, invoice);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        //    }
        //}

        public async Task<IBusinessResult> DeleteById(string id)
        {
            try
            {
                var invoice = await _unitOfWork.Invoice.GetByIdAsync(id);
                if (invoice == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Invoice());
                }
                else
                {
                    var result = await _unitOfWork.Invoice.RemoveAsync(invoice);
                    if (result) 
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, invoice);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, invoice);
                    }

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
