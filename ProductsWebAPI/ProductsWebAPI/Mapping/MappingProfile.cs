using AutoMapper;
using ProductsDTOs.Classes;
using ProductsRepository.DataModels;

namespace ProductsWebAPI.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""));

            CreateMap<CreateProductDTO, Product>();

            CreateMap<Category, CategoryDTO>();
        }
    }
}
