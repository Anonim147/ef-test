using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

using EFTasks.BLL.Abstractions;
using EFTasks.BLL.Models;

namespace WebAPI
{
    public class TaskHostedService : IHostedService
    {
        private Timer _timer;
        private int _actualizePeriod;
        private ITaskService _taskService;
        public TaskHostedService (ITaskService taskService, int actualizePeriod)
        {
            _taskService = taskService;
            _actualizePeriod = actualizePeriod;
        }
        public Task StartAsync(CancellationToken token)
        {
            _timer = new Timer(ActualizeTasks, null, TimeSpan.Zero, TimeSpan.FromMinutes(_actualizePeriod));

            return Task.CompletedTask;

        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
        void ActualizeTasks(object state)
        {
            
            var tasks = _taskService.GetTasks(task=>
                DateTime.Compare(task.ExpireDate, DateTime.Now)<=0 
                && task.TaskStatusId != (int)TaskStatusEnum.EXPIRED);

            foreach (var task in tasks)
            {
                task.TaskStatusId = (int)TaskStatusEnum.EXPIRED;
                _taskService.UpdateTask(task);
            }
                
            _taskService.SaveChanges();
        }
    }
}
