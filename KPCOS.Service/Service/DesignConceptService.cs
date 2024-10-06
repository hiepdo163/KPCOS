using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Service
{
    public class DesignConceptService : IDesignConceptService
    {
        private readonly UnitOfWork _unitOfWork;
        public DesignConceptService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var designConcepts = await _unitOfWork.DesignConcept.GetAllAsync();
            if (designConcepts == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, designConcepts);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var designConcept = await _unitOfWork.DesignConcept.GetByIdAsync(id);
            if (designConcept == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, designConcept);
            }
        }
        public async Task<IBusinessResult> Save(DesignConcept concept)
        {
            try
            {
                int result = -1;
                var conceptTmp = await _unitOfWork.DesignConcept.GetByIdAsync(concept.Id);
                if (conceptTmp != null)
                {
                    conceptTmp.Description = concept.Description;
                    conceptTmp.Name = concept.Name;
                    conceptTmp.Image = concept.Image;
                    conceptTmp.ProjectId = concept.ProjectId;

                    result = await _unitOfWork.DesignConcept.UpdateAsync(conceptTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, concept);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.DesignConcept.CreateAsync(concept);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, concept);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, concept);
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
                var concept = await _unitOfWork.DesignConcept.GetByIdAsync(id);
                if (concept == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new DesignConcept());
                }
                else
                {
                    var result = await _unitOfWork.DesignConcept.RemoveAsync(concept);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, concept);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, concept);
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
