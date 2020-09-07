using System;

namespace EFTasks.BLL.DTO
{
    public class CustomTaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public int TaskStatusId { get; set; }
    }
}
