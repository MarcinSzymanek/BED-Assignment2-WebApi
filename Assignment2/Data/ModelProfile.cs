using Assignment2.Models;
using AutoMapper;

namespace Assignment2.Data;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        CreateMap<Model, ModelDto>().ReverseMap();
        CreateMap<Model, ModelDtoFull>();
    }
}