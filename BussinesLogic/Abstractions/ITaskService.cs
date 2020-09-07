using System;
using System.Collections.Generic;

using EFTasks.BLL.DTO;
using EFTasks.DAL.Models;

namespace EFTasks.BLL.Abstractions
{
    public interface ITaskService
    {
        void AddTask(CustomTaskDTO task);
        void UpdateTask(CustomTaskDTO task);
        void DeleteTask(int id);
        CustomTaskDTO GetTask(int id, bool isTracking = false);
        IEnumerable<CustomTaskDTO> GetTasks(Func<CustomTask, bool> filter = null, bool isTracking = false);
        void SaveChanges();
    }
}
