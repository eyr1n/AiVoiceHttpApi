using AI.Talk.Editor.Api;

namespace AiVoiceHttpApi
{
    internal class AiVoice
    {
        public readonly TtsControl TtsControl;
        public readonly string[] Hosts;

        AiVoice()
        {
            TtsControl = new TtsControl();
            Hosts = TtsControl.GetAvailableHostNames();
        }

        // singleton
        static AiVoice _instance = null;
        public static AiVoice Instance => _instance ?? (_instance = new AiVoice());
    }
}
