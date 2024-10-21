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
    public class ProjectService : IProjectService
    {
        private readonly UnitOfWork _unitOfWork;
        public ProjectService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            var projects = await _unitOfWork.Project.GetProjectsAsync();
            return projects == null
                ? new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Project>())
                : new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, projects);
        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var project = await _unitOfWork.Project.GetProjectByIdAsync(id);
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
                var projectTmp = await _unitOfWork.Project.GetByIdAsync(project.Id.ToString());
                var constStaff = await _unitOfWork.Employee.GetByIdAsync(project.ConstructionStaffId);
                if (projectTmp != null)
                {
                    projectTmp.ActualCost = project.ActualCost;
                    projectTmp.ConstructionStaff = project.ConstructionStaff;
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
                    var newProject = new Project
                    {
                        ActualCost = project.ActualCost,
                        ConstructionStaffId = project.ConstructionStaffId,
                        CustomerId = project.CustomerId,
                        DesignerId = project.DesignerId,
                        StartDate = project.StartDate,
                        EndDate = project.EndDate,
                        EstimatedCost = project.EstimatedCost,
                        Status = project.Status
                    };
                    result = await _unitOfWork.Project.CreateAsync(newProject);
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

        public async Task<IBusinessResult> GetProjectsAsync(string customerName, string designerId, DateTime? startDate, string status)
        {
            var projects = await _unitOfWork.Project.GetProjectsByFilterAsync(customerName, designerId, startDate, status);
            return projects == null && !projects.Any()
                ? new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Project>())
                : new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, projects);
        }
    }
}
