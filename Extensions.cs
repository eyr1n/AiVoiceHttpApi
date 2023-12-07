using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Shared;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Web;

namespace AiVoiceHttpApi
{
    internal static class Extensions
    {
        public static T GetPathParameter<T>(this IHttpContext context, string key)
        {
            string param = HttpUtility.UrlDecode(context.Request.PathParameters[key], Encoding.UTF8);
            return (T)Convert.ChangeType(param, typeof(T));
        }

        public static T GetJsonPayload<T>(this IHttpContext context)
        {
            return JsonConvert.DeserializeObject<T>(context.Request.Payload);
        }

        public static void SendJsonResponse(this IHttpContext context, object content)
        {
            context.Response.ContentType = ContentType.JSON;
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.SendResponse(JsonConvert.SerializeObject(content));
        }

        public static void SendErrorResponse(this IHttpContext context, HttpStatusCode statusCode, string message, Exception details)
        {
            context.Response.StatusCode = statusCode;
            context.SendJsonResponse(new { Message = message, Details = details });
        }
    }
}
