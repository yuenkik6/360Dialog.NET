﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WABA360Dialog.Common.Helpers;
using WABA360Dialog.Common.Interfaces;

namespace WABA360Dialog.ApiClient.Payloads.Base
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class ClientApiRequestBase<TResponse> : IRequest<TResponse> where TResponse : ClientApiResponseBase
    {

        protected ClientApiRequestBase(HttpMethod method)
        {
            Method = method;
        }

        protected ClientApiRequestBase(string methodName, HttpMethod method)
        {
            MethodName = methodName;
            Method = method;
        }

        protected ClientApiRequestBase(string methodName, HttpMethod method, Dictionary<string, string> queryParams)
        {
            MethodName = methodName;
            Method = method;
            QueryParams = queryParams;
        }

        [JsonIgnore]
        public Dictionary<string, string> QueryParams { get; protected set; }

        [JsonIgnore]
        public HttpMethod Method { get; protected set; }

        [JsonIgnore]
        public string MethodName { get; protected set; }

        public virtual HttpContent ToHttpContent()
        {
            return ToHttpJsonContent(this);            
        }

        public virtual HttpContent ToHttpJsonContent(object model)
        {
            var payload = JsonHelper.SerializeObjectToJson(model);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }
    }

}