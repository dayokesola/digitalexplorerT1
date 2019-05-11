using AutoMapper;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Form.App;
using BizNest.Core.Domain.Model.App;
using BizNest.Core.Domain.Search.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Common
{
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Registers the mappings.
        /// </summary>
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BusinessType, BusinessTypeModel>().ReverseMap();
                cfg.CreateMap<BusinessType, BusinessTypeForm>().ReverseMap();
                cfg.CreateMap<BusinessTypeModel, BusinessTypeForm>().ReverseMap();
                cfg.CreateMap<ProhibitedName,ProhibitedWordModel>().ReverseMap();



                cfg.CreateMap<Business, BusinessModel>().ReverseMap();
                cfg.CreateMap<Business, BusinessForm>().ReverseMap();
                cfg.CreateMap<BusinessModel, BusinessForm>().ReverseMap();

                cfg.CreateMap<BusinessModel, BusinessIndexModel>().ReverseMap();
                cfg.CreateMap<Business, BusinessIndexModel>().ReverseMap();
                cfg.CreateMap<BusinessModel, BusinessCreateModel>();
            });
        }
    }
}
