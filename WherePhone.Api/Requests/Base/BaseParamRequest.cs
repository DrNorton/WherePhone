﻿using System.Collections.Generic;
using System.Net.Http;
using WherePhone.Api.Models;

namespace WherePhone.Api.Requests.Base
{
    public abstract class BaseParamRequest:IRequest
    {
        private Dictionary<string, string> _params;

        public BaseParamRequest()
        {
            _params = new Dictionary<string, string>();
            Type = HttpMethod.Get; // По умолчанию
        }

        public void AddParam(string key, string value)
        {
            _params.Add(key,value);
        }

       public abstract string Controller { get; }
       public abstract  string MethodName { get; }

       public Dictionary<string, string> Params
       {
            get { return _params; }
       }


       public Token Token { get; set; }



       public HttpMethod Type { get; set; }

       public string BaseUrl { get; set; }
    }
}
