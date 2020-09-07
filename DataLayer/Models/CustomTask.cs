using System;
using System.ComponentModel.DataAnnotations.Schema;
using EFTasks.DAL.Abstractions;

namespace EFTasks.DAL.Models
{
    [Table("tasks")]
    public class CustomTask : TEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public int TaskStatusId { get; set; }
        public TaskStatus TaskStatus { get; set; }
    }
}


