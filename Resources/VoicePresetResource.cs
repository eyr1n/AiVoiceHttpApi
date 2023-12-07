using Grapevine.Interfaces.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using System;

namespace AiVoiceHttpApi.Resources
{
    [RestResource(BasePath = "/voice-preset")]
    internal class VoicePresetResource
    {
        [RestRoute(HttpMethod = HttpMethod.GET)]
        public IHttpContext GetVoicePresets(IHttpContext context)
        {
            try
            {
                context.SendJsonResponse(AiVoice.Instance.TtsControl.VoicePresetNames);
            }
            catch (Exception ex)
            {
                context.SendErrorResponse(HttpStatusCode.InternalServerError, "ボイスプリセット一覧の取得に失敗しました", ex);
            }
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/[index]")]
        public IHttpContext SetVoicePreset(IHttpContext context)
        {
            try
            {
                int index = context.GetPathParameter<int>("index");
                string[] voicePresets = AiVoice.Instance.TtsControl.VoicePresetNames;
                AiVoice.Instance.TtsControl.CurrentVoicePresetName = voicePresets[index];
                context.SendJsonResponse(new { });
            }
            catch (Exception ex)
            {
                context.SendErrorResponse(HttpStatusCode.InternalServerError, "ボイスプリセットの設定に失敗しました", ex);
            }
            return context;
        }
    }
}
