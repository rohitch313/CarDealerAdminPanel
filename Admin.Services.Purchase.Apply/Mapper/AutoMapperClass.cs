
using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Admin.Services.Purchase.Apply.BusinessLayer.Services;
using Admin.Services.Purchase.Apply.Models;
using AutoMapper;

namespace Admin.Services.Purchase.Apply.Mapper
{
    public class AutoMapperClass: Profile
    {
        public AutoMapperClass() 
        {
            CreateMap<PvAggregatorstbl, PvAggregatorsDto>().ReverseMap();
            CreateMap<PvNewCarDealerstbl, PvNewCarDealersDto>().ReverseMap();
            CreateMap<PvOpenMarketstbl, PvOpenMarketsDto>().ReverseMap();
            CreateMap<PvaMaketbl, PvaMakeDto>().ReverseMap();
            CreateMap<PvaModeltbl, PvaModelDto>().ReverseMap();
            CreateMap<PvaVarianttbl, PvaVariantDto>().ReverseMap();
            CreateMap<PvaYearOfRegtbl, PvaYearOfRegDto>().ReverseMap();

        }
    }
}
