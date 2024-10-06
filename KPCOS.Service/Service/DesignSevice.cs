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
    public class DesignSevice : IDesignService
    {
        private readonly UnitOfWork _unitOfWork;
        public DesignSevice()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var designs = await _unitOfWork.Design.GetAllAsync();
            if (designs == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Design>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, designs);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var design = await _unitOfWork.Design.GetByIdAsync(id);
            if (design == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Design());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, design);
            }
        }
        public async Task<IBusinessResult> Save(Design design)
        {
            try
            {
                int result = -1;
                var designTmp = await _unitOfWork.Design.GetByIdAsync(design.Id);
                if (designTmp != null)
                {
                    designTmp.Budget = design.Budget;
                    designTmp.ConsultantBy = design.ConsultantBy;
                    designTmp.CustomerId = design.CustomerId;
                    designTmp.Depth = design.Depth;
                    designTmp.DesignType = design.DesignType;
                    designTmp.FiltrationSystem = design.FiltrationSystem;
                    designTmp.KoiCountRange = design.KoiCountRange;
                    designTmp.KoiType = design.KoiType;
                    designTmp.Location = design.Location;
                    designTmp.MinLength = design.MinLength;
                    designTmp.MinWidth = design.MinWidth;
                    designTmp.Note = design.Note;
                    designTmp.Shape = design.Shape;
                    designTmp.Status = design.Status;
                    designTmp.TemplateId = design.TemplateId;
                    designTmp.UpdateDate = design.UpdateDate;
                    designTmp.WaterQuality = design.WaterQuality;
                    designTmp.WaterLevel = design.WaterLevel;

                    result = await _unitOfWork.Design.UpdateAsync(designTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, design);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.Design.CreateAsync(design);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, design);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, design);
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
                var design = await _unitOfWork.Design.GetByIdAsync(id);
                if (design == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Design());
                }
                else
                {
                    var result = await _unitOfWork.Design.RemoveAsync(design);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, design);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, design);
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
