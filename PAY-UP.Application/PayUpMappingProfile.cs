using AutoMapper;

namespace PAY_UP.Application
{
    public class PayUpMappingProfile : Profile
    {
        public PayUpMappingProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;
        }
    }
}
