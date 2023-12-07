using AI.Talk.Editor.Api;
using Grapevine.Interfaces.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using System;

namespace AiVoiceHttpApi.Resources
{
    [RestResource(BasePath = "/host")]
    internal class HostResource
    {
        [RestRoute(HttpMethod = HttpMethod.GET)]
        public IHttpContext GetHosts(IHttpContext context)
        {
            context.SendJsonResponse(AiVoice.Instance.Hosts);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/[index]")]
        public IHttpContext SetHost(IHttpContext context)
        {
            context.Server.Stop();
            try
            {
                int index = context.GetPathParameter<int>("index");
                AiVoice.Instance.TtsControl.Initialize(AiVoice.Instance.Hosts[index]);
                if (AiVoice.Instance.TtsControl.Status == HostStatus.NotRunning)
                {
                    AiVoice.Instance.TtsControl.StartHost();
                }
                AiVoice.Instance.TtsControl.Connect();
                context.SendJsonResponse(new { });
            }
            catch (Exception ex)
            {
                context.SendErrorResponse(HttpStatusCode.InternalServerError, "ホストへの接続に失敗しました", ex);
            }
            return context;
        }
    }
}
