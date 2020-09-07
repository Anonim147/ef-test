using AutoMapper;

using EFTasks.BLL.DTO;
using EFTasks.DAL.Models;


namespace EFTasks.BLL
{
    public class ConfigurationMapper:Profile
    {
        public ConfigurationMapper() 
        {
            CreateMap<CustomTask, CustomTaskDTO>();
            CreateMap<CustomTaskDTO, CustomTask>();
        }
    }
}
