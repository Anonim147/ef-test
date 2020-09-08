using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using EFTasks.DAL;
using EFTasks.DAL.Models;
using EFTasks.BLL.DTO;
using EFTasks.BLL.Abstractions;
using EFTasks.BLL.Models;
using System.Linq.Expressions;

namespace EFTasks.BLL.Services
{
    public class TaskService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public TaskService(Context context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public void AddTask(CustomTaskDTO task)
        {
            _context.tasks.Add(_mapper.Map<CustomTask>(task));
            _context.SaveChanges();
        }
        public void UpdateTask(CustomTaskDTO task)
        {

            var targetTask = GetTask(task.Id);
            if (targetTask == null)
                throw new Exception("Task doesn`t exists");
            var mappedEntity = _mapper.Map<CustomTask>(task);
            _context.tasks.Update(mappedEntity);
            _context.SaveChanges();
        }
        public void DeleteTask(int id) 
        {
            var entity = _context.tasks.FirstOrDefault(t => t.Id == id);
            if (entity == null)
                throw new Exception("Task doesn`t exists");
            _context.tasks.Remove(entity);
            _context.SaveChanges();
        }
        public CustomTaskDTO GetTask(int id, bool isTracking = false)
        {
            if (isTracking)
            {
                return _mapper.Map<CustomTaskDTO>(_context.tasks.FirstOrDefault(t => t.Id == id));
            }
            return _mapper.Map<CustomTaskDTO>(_context.tasks.AsNoTracking().FirstOrDefault(t => t.Id == id));
        }
        public IEnumerable<CustomTaskDTO> GetTasks(Func<CustomTask, bool> filter = null, bool isTracking = false)
        {
            IEnumerable<CustomTask> tasks;
            if (!isTracking)
            {
                tasks = _context.tasks.AsNoTracking();
            }
            else 
            {
                tasks = _context.tasks;
            }

            if (filter != null)
                return _mapper.Map<IEnumerable<CustomTaskDTO>>(tasks.Where(filter));

            return _mapper.Map<IEnumerable<CustomTaskDTO>>(tasks);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /*private CustomTaskDTO CheckForExpiring(CustomTaskDTO task)
        {
            if (DateTime.Compare(task.ExpireDate, DateTime.Now) <= 0
                && task.TaskStatusId != (int)TaskStatusEnum.EXPIRED)
            {
                task.TaskStatusId = (int)TaskStatusEnum.EXPIRED;
            }
            return task;
        }*/
    }
}
