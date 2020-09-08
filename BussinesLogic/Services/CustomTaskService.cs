using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using AutoMapper;


using EFTasks.BLL.Abstractions;
using EFTasks.BLL.Models;
using EFTasks.DAL.Abstractions;
using EFTasks.DAL.Models;
using EFTasks.BLL.DTO;


namespace EFTasks.BLL.Services
{
    public class CustomTaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CustomTask> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomTaskService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetRepository<CustomTask>();
        }
        public void AddTask(CustomTaskDTO task)
        {
            if (task == null)
                throw new Exception("Entity cannot be null");

            _repository.Create(_mapper.Map<CustomTask>(task));   
        }
        public void DeleteTask(int Id)
        {
            if (_repository.Get(Id) == null)
                throw new Exception("Task not found");

            _repository.Delete(Id);
        }
        public IEnumerable<CustomTaskDTO> GetTasks(Expression<Func<CustomTask, bool>> filter = null, bool isTracking = false)
        {
            var tasks = _mapper.Map<IEnumerable<CustomTaskDTO>>(_repository.Get(filter));
            foreach (var task in tasks)
            {
                CheckActuality(task);
            }
            return tasks;
        }
        public CustomTaskDTO GetTask(int Id, bool isTracking = false)
        {
            var task = _mapper.Map<CustomTaskDTO>(_repository.Get(Id));
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            CheckActuality(task);
            return task;
        }
        public void UpdateTask(CustomTaskDTO task)
        {
            if (task == null)
                throw new Exception("Entity cannot be null");

            if (_repository.Get(task.Id) == null)
                throw new Exception("Task not found");

            _repository.Update(_mapper.Map<CustomTask>(task));
            SaveChanges();
        }
        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public void CheckActuality(CustomTaskDTO task)
        {
            if (task!= null 
                && DateTime.Compare(task.ExpireDate, DateTime.Now) <= 0
                && task.TaskStatusId != (int)TaskStatusEnum.EXPIRED)
            {
                task.TaskStatusId = (int)TaskStatusEnum.EXPIRED;
            }
        }
    }
}
