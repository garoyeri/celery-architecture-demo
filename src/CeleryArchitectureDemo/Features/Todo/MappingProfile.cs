using System;
using AutoMapper;

namespace CeleryArchitectureDemo.Features.Todo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, Domain.TodoItem>().ReverseMap();
            CreateMap<AddItem.Command, Domain.TodoItem>()
                .ForMember(d => d.WhenCompleted, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsCompleted, opt => opt.Ignore());
        }
    }
}