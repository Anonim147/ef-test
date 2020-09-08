using EFTasks.BLL.Abstractions;
using EFTasks.BLL.Models;
using EFTasks.DAL.Abstractions;
using EFTasks.DAL.Models;
using System.Linq;

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using EFTasks.BLL.DTO;
using System.Linq.Expressions;

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
            _repository.Create(_mapper.Map<CustomTask>(task));   
        }
        public void DeleteTask(int Id)
        {
            if (_repository.Get(Id) == null)
                throw new Exception("Cannot find");
            _repository.Delete(Id);
        }
        public IEnumerable<CustomTaskDTO> GetTasks(Expression<Func<CustomTask, bool>> filter = null, bool isTracking = false)
        {
            return _mapper.Map<IEnumerable<CustomTaskDTO>>(_repository.Get(filter));
        }
        public CustomTaskDTO GetTask(int Id, bool isTracking = false)
        {
            return _mapper.Map<CustomTaskDTO>(_repository.Get(Id));
        }
        public void UpdateTask(CustomTaskDTO task)
        {
            _repository.Get(task.Id);
            _repository.Update(_mapper.Map<CustomTask>(task));
            _unitOfWork.SaveChanges();
        }
        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

    }
}
