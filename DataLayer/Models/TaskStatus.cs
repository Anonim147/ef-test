using System.ComponentModel.DataAnnotations.Schema;

using EFTasks.DAL.Abstractions;

namespace EFTasks.DAL.Models
{
    [Table("TaskStatus")]
    public class TaskStatus : TEntity
    {
        public string State { get; set; }
    }
}
