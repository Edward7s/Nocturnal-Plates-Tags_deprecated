using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Nocturnal.Utils
{
    public class WebScoketHandler
    {
        public static WebScoketHandler Instance { get; private set; }
        private WebSocket _webSocket { get; set; }
        public WebScoketHandler()
        {
            Instance = this;
            using (_webSocket = new WebSocket("wss://wsserver.nocturnal-client.xyz"))
            {
                _webSocket.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                _webSocket.Connect();
                _webSocket.OnClose += (sender, e) =>
                {
                    try
                    {
                     _webSocket.Connect();
                    }
                    catch (Exception ex)
                    {
                        MelonLoader.MelonLogger.Error(ex);
                    }
                };
                _webSocket.OnOpen += MessageHandler.SocketConnected;
                _webSocket.OnMessage += MessageHandler.OnMessageRecived;
                _webSocket.Log.Output = (_, __) => { };
            }
        }
        public void SendMessage(object message)
        {
            if (_webSocket.IsAlive)
                _webSocket.Send(message.ToString());
        }
    }
}
