using AutoMapper;

namespace ArchtistStudio.Modules.PhoneNumber;

public class PhoneNumberMapper : Profile
{
    public PhoneNumberMapper()
    {
        CreateMap<PhoneNumber, ListPhoneNumberResponse>();
        CreateMap<PhoneNumber, DetailPhoneNumberResponse>();
        CreateMap<InsertPhoneNumberRequest, PhoneNumber>();
       CreateMap<PhoneNumber, UpdatePhoneNumberRequest>();
    }
}
