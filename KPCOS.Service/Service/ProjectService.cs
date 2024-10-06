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
    public class ProjectService : IProjectService
    {
        private readonly UnitOfWork _unitOfWork;
        public ProjectService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var projects = await _unitOfWork.Project.GetAllAsync();
            if (projects == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Project>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, projects);
            }
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(id);
            if (project == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Project());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, project);
            }
        }
        public async Task<IBusinessResult> Save(Project project)
        {
            try
            {
                int result = -1;
                var projectTmp = await _unitOfWork.Project.GetByIdAsync(project.Id);
                if (projectTmp != null)
                {
                    projectTmp.ActualCost = project.ActualCost;
                    projectTmp.ConstructionStaffId = project.ConstructionStaffId;
                    projectTmp.CustomerId = project.CustomerId;
                    projectTmp.DesignerId = project.DesignerId;
                    projectTmp.StartDate = project.StartDate;
                    projectTmp.EndDate = project.EndDate;
                    projectTmp.EstimatedCost = project.EstimatedCost;
                    projectTmp.Status = project.Status;

                    result = await _unitOfWork.Project.UpdateAsync(projectTmp);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, project);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.Project.CreateAsync(project);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, project);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, project);
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
                var project = await _unitOfWork.Project.GetByIdAsync(id);
                if (project == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Project());
                }
                else
                {
                    var result = await _unitOfWork.Project.RemoveAsync(project);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, project);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, project);
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
