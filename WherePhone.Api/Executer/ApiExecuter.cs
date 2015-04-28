using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.Deserializers;
using WherePhone.Api.Models;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Executer
{
    public class ApiExecuter : IApiExecuter
    {
        private RestClient _restClient;

        public ApiExecuter(IApiSettings apiSettings)
        {
            _restClient = new RestSharp.Portable.RestClient(apiSettings.BaseUrl);
            _restClient.AddHandler("application/json", new JsonDeserializer());
       
        }
        public async Task<T> Execute<T>(IRequest request)
        {
            var restRequest = CreateRequest(request);
            var uri=_restClient.BuildUri(restRequest);
            Debug.WriteLine(uri);
            try
            {
                    var response = await _restClient.Execute<ApiResponse<T>>(restRequest);
                    var data = response.Data;
                    if (data.ErrorCode == 0)
                    {
                        return data.Result;
                    }
                    else
                    {
                        throw new ApiException(data.ErrorCode, data.ErrorMessage);
                    }
            }
            catch (Exception e)
            {
                throw new ApiException(10000, e.Message);
            }
        
         
        }

        private RestRequest CreateRequest(IRequest request)
        {
            var restRequest = CreateAndPrepareByUrl(request);
            restRequest.Parameters.Clear();
            if (request.Token != null) { restRequest.AddHeader("Authorization", String.Format("{0} {1}", "Bearer", request.Token.Value)); }//добавляем токен
            if (request.Type == HttpMethod.Post)
            {
                restRequest.AddHeader("Accept", "application/json");
                if (request.Params != null && request.Params.Any())
                {
                    restRequest.AddBody(request.Params);
                }
            }

            if (request.Type == HttpMethod.Get)
            {
                if (request.Params != null && request.Params.Any())
                {
                    foreach (var param in request.Params)
                    {
                        restRequest.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                    }
                  
                }
            }


            return restRequest;
        }

        private  RestRequest CreateAndPrepareByUrl(IRequest request)
        {
            var url = String.IsNullOrEmpty(request.Controller)
                ? request.MethodName
                : String.Format("{0}/{1}", request.Controller, request.MethodName);

            var restRequest = new RestRequest(url, request.Type);
            return restRequest;
        }
      
    }
}
