using AutoMapper;
using Computer_Store_DataAccess;
using Computer_Store_DataAccess.ViewModel;
using Computer_Store_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Business.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductPrice, ProductPriceDTO>().ReverseMap();
            CreateMap<OrderHeaderDTO, OrderHeader>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
        }
    }
}
