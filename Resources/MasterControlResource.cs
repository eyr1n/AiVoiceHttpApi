using Grapevine.Interfaces.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using Newtonsoft.Json;
using System;

namespace AiVoiceHttpApi.Resources
{
    [RestResource(BasePath = "/master-control")]
    internal class MasterControlResource
    {
        [RestRoute(HttpMethod = HttpMethod.GET)]
        public IHttpContext GetMasterControl(IHttpContext context)
        {
            try
            {
                MasterControl masterControl = JsonConvert.DeserializeObject<MasterControl>(AiVoice.Instance.TtsControl.MasterControl);
                context.SendJsonResponse(masterControl);
            }
            catch (Exception ex)
            {
                context.SendErrorResponse(HttpStatusCode.InternalServerError, "マスターコントロールの取得に失敗しました", ex);
            }
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.POST)]
        public IHttpContext SetMasterControl(IHttpContext context)
        {
            try
            {
                MasterControl masterControl = context.GetJsonPayload<MasterControl>();
                AiVoice.Instance.TtsControl.MasterControl = JsonConvert.SerializeObject(masterControl ?? new MasterControl());
                context.SendJsonResponse(new { });
            }
            catch (Exception ex)
            {
                context.SendErrorResponse(HttpStatusCode.InternalServerError, "マスターコントロールの設定に失敗しました", ex);
            }
            return context;
        }
    }

    internal class MasterControl
    {
        public double Volume = 1.0;
        public double Speed = 1.0;
        public double Pitch = 1.0;
        public double PitchRange = 1.0;
        public int MiddlePause = 150;
        public int LongPause = 370;
        public int SentencePause = 800;
    }
}
