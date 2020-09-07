using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

using EFTasks.BLL.Abstractions;
using EFTasks.DAL;

namespace WebAPI
{
    public class TaskHostedService : IHostedService
    {
        private Timer _timer;
        private ITaskService _taskService;
        public TaskHostedService (Context context, ITaskService taskService)
        {
            this._taskService = taskService;
        }
        public Task StartAsync(CancellationToken token)
        {
            _timer = new Timer(ActualizeTasks, null, TimeSpan.Zero, TimeSpan.FromMinutes(3));

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
                && task.TaskStatusId!=6);

            foreach (var task in tasks)
            {
                task.TaskStatusId = 6;
                _taskService.UpdateTask(task);
            }
                
            _taskService.SaveChanges();
        }
    }
}
