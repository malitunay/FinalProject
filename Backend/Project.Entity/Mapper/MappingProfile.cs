using AutoMapper;
using Project.Entity.Dto;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, DtoUser>().ReverseMap();
            CreateMap<Apartment, DtoApartment>().ReverseMap();
            CreateMap<ApartmentStatus, DtoApartmentStatus>().ReverseMap();
            CreateMap<Invoice, DtoInvoice>().ReverseMap();
            CreateMap<InvoiceStatus, DtoInvoiceStatus>().ReverseMap();
            CreateMap<InvoiceType, DtoInvoiceType>().ReverseMap();
            CreateMap<MessageStatus, DtoMessageStatus>().ReverseMap();
            CreateMap<UserType, DtoUserType>().ReverseMap();
            CreateMap<Message, DtoMessage>().ReverseMap();
            CreateMap<Role, DtoRole>().ReverseMap();
            CreateMap<User, DtoLoginUser>();
            CreateMap<DtoLogin, User>();
            //CreateMap<DtoUnreadMessageCount, Message>().ReverseMap();
        }
    }
}
