using AutoMapper;
using MicroService.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.ServerHost.Order
{
    public class MappingProfile : Profile, IProfile
    {
        public MappingProfile()
        {
            //忽略rows的映射
            AddGlobalIgnore("Payload"); //its not work?
          
        }
    }
}
