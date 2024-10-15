using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Service
{
    public class QuotationService : IQuotationService
    {
        private readonly UnitOfWork _unitOfWork;
        public QuotationService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var quotations = await _unitOfWork.Quotation.GetAllAsync();
            if (quotations == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Quotation>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, quotations);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var quotation = await _unitOfWork.Quotation.GetByIdAsync(id);
            if (quotation == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Quotation());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, quotation);
            }
        }

        public async Task<IBusinessResult> GetByDesignId(string designId)
        {
            var quotation = await _unitOfWork.Quotation.GetAllAsync2(x => x.DesignId == designId);
            if (quotation == null)
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Quotation());

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, quotation);
        }

        public async Task<IBusinessResult> Save(Quotation quotation)
        {
            try
            {
                int result = -1;
                var quotationTmp = await _unitOfWork.Quotation.GetQuery()
                    .FirstOrDefaultAsync(q => q.Id == quotation.Id);
                var design = await _unitOfWork.Design.GetAllAsync();

                if (quotationTmp != null)
                {
                    quotationTmp.ComplexityLevel = quotation.ComplexityLevel;
                    quotationTmp.ConsultationAmount = quotation.ConsultationAmount;
                    quotationTmp.Note = quotation.Note;
                    quotationTmp.QuotationAmount = quotation.QuotationAmount;
                    quotationTmp.QuotationDate = quotation.QuotationDate;
                    quotationTmp.Scale = quotation.Scale;
                    quotationTmp.Status = quotation.Status;
                    quotationTmp.Style = quotation.Style;
                    var existingDesign = await _unitOfWork.Design.GetByIdAsync(quotation.DesignId);
                    quotationTmp.Design = existingDesign;

                    result = await _unitOfWork.Quotation.UpdateAsync(quotationTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, quotation);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    var existingDesign = await _unitOfWork.Design.GetQuery()
                        .Include(d => d.Quotations)
                        .FirstOrDefaultAsync(d => d.Id == quotation.DesignId);
                    quotation.Id = Guid.NewGuid().ToString();
                    if (existingDesign != null)
                    {
                        existingDesign.Quotations.Add(quotation);
                        result = await _unitOfWork.Design.UpdateAsync(existingDesign);
                    }
                    else
                    {
                        quotation.Design = null;
                        quotation.DesignId = null;
                        result = await _unitOfWork.Quotation.CreateAsync(quotation);
                    }
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, quotation);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, quotation);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
        public async Task<IBusinessResult> DeleteById(string id)
        {
            try
            {
                var quotation = await _unitOfWork.Quotation.GetByIdAsync(id);
                if (quotation == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Quotation());
                }
                else
                {
                    var result = await _unitOfWork.Quotation.RemoveAsync(quotation);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, quotation);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, quotation);
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
