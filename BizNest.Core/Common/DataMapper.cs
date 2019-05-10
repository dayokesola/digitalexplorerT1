using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Common
{
    public class DataMapper
    {
        public static T1 Map<T1, T2>(T2 obj)
        {
            return Mapper.Map<T1>(obj);
        }
    }

}
