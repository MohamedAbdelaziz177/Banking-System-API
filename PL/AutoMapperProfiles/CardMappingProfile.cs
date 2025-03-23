using AutoMapper;
using Banking_system.DAL.Enums.Card;
using Banking_system.DAL.Model;
using Banking_system.DTO_s.CardDto_s;

namespace Banking_system.PL.AutoMapperProfiles
{
    public class CardMappingProfile : Profile
    {
        public CardMappingProfile()
        {
            CreateMap<CardCreateDto, Card>()
                .ForMember(dest => dest.cardType,
                opt => opt.MapFrom(src => Enum.Parse<CardType>(src.cardType, true)))
                .ReverseMap();

            CreateMap<CardReadDto, Card>()
                .ForMember(dest => dest.cardType,
                opt => opt.MapFrom(src => Enum.Parse<CardType>(src.cardType, true)))
                .ForMember(dest => dest.cardStatus,
                opt => opt.MapFrom(src => Enum.Parse<CardStatus>(src.cardStatus, true)))
                .ReverseMap();

            CreateMap<CardUpdateDto, Card>()
                .ForMember(dest => dest.cardType,
                opt => opt.MapFrom(src => Enum.Parse<CardType>(src.cardStatus, true)))
                .ReverseMap();
        }

        // public void FuckGit() { }

    }
}
