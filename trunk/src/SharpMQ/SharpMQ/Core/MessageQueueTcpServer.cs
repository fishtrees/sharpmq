using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FyndSharp.Communication.Server;
using System.Net;
using FyndSharp.Communication.Common;

namespace SharpMQ.Core
{
    internal class MessageQueueTcpServer
    {
        private readonly IServer _TcpServer;

        public event EventHandler ClientConnected;

        public MessageQueueTcpServer()
        {
            _TcpServer = ServerFactory.CreateServer(new IPEndPoint(IPAddress.Any, 12345));
            _TcpServer.ClientConnected += new EventHandler<ClientDummyEventArgs>(TcpServer_ClientConnected);
            _TcpServer.ClientDisconnected += new EventHandler<ClientDummyEventArgs>(TcpServer_ClientDisconnected);
        }
        public void Start()
        {
            _TcpServer.Start();
        }

        private void TcpServer_ClientDisconnected(object sender, ClientDummyEventArgs e)
        {
            
        }

        

        private void TcpServer_ClientConnected(object sender, ClientDummyEventArgs e)
        {
            
        }
        public void Stop()
        {

        }
    }
}
