using Grapevine.Server;
using System;
using System.Threading;

namespace AiVoiceHttpApi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ManualResetEventSlim ev = new ManualResetEventSlim();

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                ev.Set();
            };

            using (RestServer server = new RestServer())
            {
                server.Host = "+";
                server.Port = args.Length > 0 ? args[0] : "8080";
                server.Router.BeforeRouting += (context) => {
                    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                };
                server.OnAfterStop += () =>
                {
                    ev.Set();
                };
                server.LogToConsole().Start();
                ev.Wait();
            }

            try
            {
                AiVoice.Instance.TtsControl.Disconnect();
                Console.WriteLine("ホストから切断しました");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("ホストからの切断に失敗しました");
                Console.Error.WriteLine(ex);
            }
        }
    }
}
