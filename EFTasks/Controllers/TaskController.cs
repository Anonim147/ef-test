using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using EFTasks.BLL.Abstractions;
using EFTasks.BLL.DTO;

namespace EFTasks.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            this._taskService = taskService;
        }
        /// <summary>
        /// Getting all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CustomTaskDTO>> GetTasks()
        {
            try
            {
                return new JsonResult(_taskService.GetTasks());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Getting task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            try
            {
                return new JsonResult(_taskService.GetTask(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Creating new task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTask([FromBody]CustomTaskDTO task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.AddTask(task);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Updating task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateTask([FromBody]CustomTaskDTO task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.UpdateTask(task);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Deleting task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                _taskService.DeleteTask(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
