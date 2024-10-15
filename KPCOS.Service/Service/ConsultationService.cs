using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
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
    public class ConsultationService : IConsultationService
    {
        private readonly UnitOfWork _unitOfWork;
        public ConsultationService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var consultations = await _unitOfWork.Consultation.GetAllAsync();
            if (consultations == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Consultation>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, consultations);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var consultation = await _unitOfWork.Consultation.GetByIdAsync(id);
            if (consultation == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Consultation());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, consultation);
            }
        }

        public async Task<Design?> IsDesignExist(string DesignId)
        {
            var existingDesign = await _unitOfWork.Design.GetByIdAsync(DesignId);
            if (existingDesign == null)
                return null;
            return existingDesign;
        }

        public async Task<IBusinessResult> Save(Consultation consultation)
        {
            try
            {
                int result = -1;
                var consultationTmp = await _unitOfWork.Consultation.GetByIdAsync(consultation.Id);
                if (consultationTmp != null)
                {                    
                    consultationTmp.DesignId = consultation.DesignId;
                    consultationTmp.AdjustedDesign = consultation.AdjustedDesign;
                    consultationTmp.AdjustedSpecification = consultation.AdjustedSpecification;
                    consultationTmp.Note = consultation.Note;

                    result = await _unitOfWork.Consultation.UpdateAsync(consultationTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, consultation);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                } else
                {
                    var existingDesign = await IsDesignExist(consultation.DesignId);
                    consultation.Id = Guid.NewGuid().ToString();
                    if (existingDesign != null)
                    {
                        consultation.DesignId = existingDesign.Id;
                        consultation.Design = existingDesign;
                    }
                    else
                    {
                        consultation.Design = null;
                        consultation.DesignId = null;
                    }

                    result = await _unitOfWork.Consultation.CreateAsync(consultation);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, consultation);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, consultation);
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
                var consultation = await _unitOfWork.Consultation.GetByIdAsync(id);
                if (consultation == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Consultation());
                }
                else
                {
                    var result = await _unitOfWork.Consultation.RemoveAsync(consultation);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, consultation);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, consultation);
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
