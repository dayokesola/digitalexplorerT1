﻿using AutoMapper;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Form.App;
using BizNest.Core.Domain.Model.App;
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
                 
            });
        }
    }
}
