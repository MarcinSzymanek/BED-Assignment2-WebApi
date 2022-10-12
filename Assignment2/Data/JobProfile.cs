using Assignment2.Models;
using AutoMapper;

namespace Assignment2.Data;

public class JobProfile : Profile
{
    public JobProfile()
    {
        CreateMap<Job, JobDtoSingleModel>();
        CreateMap<Job, JobDtoUpdate>();
        CreateMap<Job, JobWModelNames>();
        CreateMap<Job, JobDtoSimple>();
        CreateMap<Job, JobDtoWExpenses>();
        CreateMap<Job, JobDtoNoId>().ReverseMap();
    }
}