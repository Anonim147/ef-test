using EFTasks.DAL.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTasks.DAL.Models
{
    [Table("TaskStatus")]
    public class TaskStatus : TEntity
    {
        public string State { get; set; }
    }
}
