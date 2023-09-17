using AutoMapper;
using AutoMapper.EquivalencyExpression;

using NineteenSevenFour.Gatehub.Domain.Entities;
using NineteenSevenFour.Gatehub.Domain.Models;

namespace NineteenSevenFour.Gatehub.Data.Sqlite.Context.MappingProfile
{
  public partial class GateApplicationMetadataMappingProfile : Profile
  {
    public GateApplicationMetadataMappingProfile()
    {
      CreateMap<GateApplicationMetadataEntity, GateApplicationMetadataModel>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
        .EqualityComparison((src, dest) => src.Id == dest.Id)
        .ReverseMap();
    }
  }
}
