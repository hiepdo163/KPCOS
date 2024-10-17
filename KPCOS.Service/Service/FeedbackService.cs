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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ProjectRepository _projectRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly UnitOfWork _unitOfWork;
        public FeedbackService()
        {
            _unitOfWork ??= new UnitOfWork();
            _customerRepository ??= new CustomerRepository();
            _projectRepository ??= new ProjectRepository();
        }
       
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule
            #endregion

            var feedbacks = await _unitOfWork.Feedback.GetFeedbackInclude(f => f.IsDeleted == false || f.IsDeleted == null);
            if (feedbacks == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<FeedbackDTO>());
            }
            var feedbackDtos = feedbacks.Select(f => new FeedbackDTO
            {
                ID = f.Id,
                CustomerId = f.CustomerId,
                ProjectId = f.ProjectId,
                Content = f.Content,
                Rating = f.Rating
            }).ToList();
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, feedbackDtos);

        }
        public async Task<IBusinessResult> GetById(string id)
        {
            var feedback = await _unitOfWork.Feedback.GetByIdAsync(id);
            if (feedback == null || feedback.IsDeleted == true)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new FeedbackDTO());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, feedback);
            }
        }
        public async Task<IBusinessResult> GetByProjectId(string id)
        {
            var feedback =  await _unitOfWork.Feedback.GetAllAsync2(x => x.ProjectId == id);
            if (feedback == null)
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Feedback());

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, feedback);
        }
        public async Task<IBusinessResult> DeleteById(string id)
        {
            try
            {
                var feedback = await _unitOfWork.Feedback.GetByIdAsync(id);
                if (feedback == null || feedback.IsDeleted == true)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Feedback());
                }
                else
                {
                    feedback.IsDeleted = true;
                    feedback.UpdateDate = DateTime.UtcNow;

                    var result = await _unitOfWork.Feedback.UpdateAsync(feedback);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, feedback);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, feedback);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Create(FeedbackDTO feedbackDto)
        {
            if (feedbackDto == null)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            var customerExist = await _customerRepository.GetByIdAsync(feedbackDto.CustomerId);
            if (customerExist == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, "Customer does not exist");
            }
            var projectExist = await _projectRepository.GetByIdAsync(feedbackDto.ProjectId);    
            if (projectExist == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, "Project does not exist");
            }

            try
            {
                var feedback = new Feedback
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = feedbackDto.Content,
                    ProjectId = feedbackDto.ProjectId,
                    CustomerId = feedbackDto.CustomerId,
                    Rating = feedbackDto.Rating,
                    CreateDate = DateTime.UtcNow
                };
                var result = await _unitOfWork.Feedback.CreateAsync(feedback);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, feedback);
                }

                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }



        public async Task<IBusinessResult> Update(FeedbackDTO feedbackDto)
        {
            if (feedbackDto == null)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }

            try
            {
                var existingFeedback = await _unitOfWork.Feedback.GetByIdAsync(feedbackDto.ID);
                if (existingFeedback == null || existingFeedback.IsDeleted == true)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                existingFeedback.Content = feedbackDto.Content;
                existingFeedback.CustomerId = feedbackDto.CustomerId;
                existingFeedback.Rating = feedbackDto.Rating;
                existingFeedback.UpdateDate = DateTime.UtcNow; 

                var result = await _unitOfWork.Feedback.UpdateAsync(existingFeedback);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, existingFeedback);
                }

                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
