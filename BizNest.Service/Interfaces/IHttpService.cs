using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizNest.Service.Interfaces
{
    public interface IHttpService
    {
        (bool success, string message) SetClientHeader(string key, string value);
        Task<TOut> GetAsync<TOut>(string url) where TOut : class;

        Task<TOut> PostAsync<TOut, TIn>(TIn requestObject, string url) where TOut : class;
    }
}
