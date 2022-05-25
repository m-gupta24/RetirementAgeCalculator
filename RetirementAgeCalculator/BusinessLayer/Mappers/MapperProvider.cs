using AutoMapper;

namespace BusinessLayer.Mappers
{
    public static class MapperProvider
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }

     

    }
}
