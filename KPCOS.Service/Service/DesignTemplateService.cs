using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Service
{
    public class DesignTemplateService : IDesignTemplateService
    {
        private readonly UnitOfWork _unitOfWork;
        public DesignTemplateService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll(DesignTemplateFilterDTO filter)
        {
            #region Business rule
            #endregion

            var designTemplates = await _unitOfWork.DesignTemplate.GetAllAsync();

            if (!string.IsNullOrEmpty(filter.DefaultLocation))
            {
                designTemplates = designTemplates.Where(d => d.DefaultLocation.Contains(filter.DefaultLocation)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.DefaultShape))
            {
                designTemplates = designTemplates.Where(d => d.DefaultShape.Contains(filter.DefaultShape)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                designTemplates = designTemplates.Where(d => d.Name.Contains(filter.Name)).ToList();
            }

            if (filter.TotalPrice.HasValue && filter.TotalPrice != 0)
            {
                designTemplates = designTemplates.Where(d => d.TotalPrice >= filter.TotalPrice).ToList();
            }

            if (designTemplates == null || !designTemplates.Any())
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<DesignTemplate>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, designTemplates);
            }
        }

        public async Task<IBusinessResult> GetWithCondition(string name)
        {
            #region Business rule
            #endregion

            var designTemplates = await _unitOfWork.DesignTemplate.GetAllAsync2(x => x.Name.Contains(name));
            if (designTemplates == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<DesignTemplate>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, designTemplates);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var designTemplate = await _unitOfWork.DesignTemplate.GetByIdAsync(id);
            if (designTemplate == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new DesignTemplate());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, designTemplate);
            }
        }
        public async Task<IBusinessResult> Save(DesignTemplate template)
        {
            try
            {
                int result = -1;
                var templateTmp = await _unitOfWork.DesignTemplate.GetQuery()
                    .FirstOrDefaultAsync(d => d.Id == template.Id);
                if (templateTmp != null)
                {
                    templateTmp.DefaultLocation = template.DefaultLocation;
                    templateTmp.DefaultShape = template.DefaultShape;
                    templateTmp.DefaultSize = template.DefaultSize;
                    templateTmp.Description = template.Description;
                    templateTmp.Name = template.Name;
                    templateTmp.Image = template.Image;
                    templateTmp.TotalPrice = template.TotalPrice;

                    result = await _unitOfWork.DesignTemplate.UpdateAsync(template);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, template);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    string Id = Guid.NewGuid().ToString();
                    template.Id = Id;
                    result = await _unitOfWork.DesignTemplate.CreateAsync(template);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, template);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, template);
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
                var template = await _unitOfWork.DesignTemplate.GetByIdAsync(id);
                if (template == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new DesignTemplate());
                }
                else
                {
                    var result = await _unitOfWork.DesignTemplate.RemoveAsync(template);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, template);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, template);
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
