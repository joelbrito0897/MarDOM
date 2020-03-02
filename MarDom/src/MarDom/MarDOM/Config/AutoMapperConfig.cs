using AutoMapper;
using Model;
using Model.DTO;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarDOM.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<WarehouseDTO, Warehouse>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<ArticleDTO, Article>().ReverseMap();

            CreateMap<MovementArticleViewModel, MovementArticle>()
                .ForMember(des => des.WarehouseId, s => s.MapFrom(src => src.WarehouseId))
                .ForMember(des => des.Type, s => s.MapFrom(src => src.MovementType))
                .ForPath(des => des.MovementArticleDetails.ArticleId, s => s.MapFrom(src => src.ArticleId))
                .ForPath(des => des.MovementArticleDetails.Quantity, s => s.MapFrom(src => src.Quantity))
            .ReverseMap();
        }    
    }
}
