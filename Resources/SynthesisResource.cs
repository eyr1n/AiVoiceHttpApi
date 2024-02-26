using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using System;
using System.IO;

namespace AiVoiceHttpApi.Resources
{
    [RestResource(BasePath = "/synthesis")]
    internal class SynthesisResource
    {
        [RestRoute(HttpMethod = HttpMethod.POST)]
        public IHttpContext Synthesize(IHttpContext context)
        {
            try
            {
                Synthesis play = context.GetJsonPayload<Synthesis>();
                string path = Path.GetTempPath() + "aivoice_http_api.wav";
                AiVoice.Instance.TtsControl.Text = play.Text;
                AiVoice.Instance.TtsControl.SaveAudioToFile(path);

                FileStream file = File.OpenRead(path);
                context.Response.ContentType = ContentType.WAV;
                context.Response.SendResponse(file);
            }
            catch (Exception ex)
            {
                context.SendErrorResponse(HttpStatusCode.InternalServerError, "音声の合成に失敗しました", ex);
            }
            return context;
        }
    }

    internal class Synthesis
    {
        public string Text;
    }
}
