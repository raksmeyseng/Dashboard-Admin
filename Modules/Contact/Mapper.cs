using AutoMapper;

namespace ArchtistStudio.Modules.Contact;

public class ContactMapper : Profile
{
    public ContactMapper()
    {
        CreateMap<Contact, ListContactResponse>();
        CreateMap<Contact, DetailContactResponse>();
        CreateMap<InsertContactRequest, Contact>();
       CreateMap<Contact, UpdateContactRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
}
