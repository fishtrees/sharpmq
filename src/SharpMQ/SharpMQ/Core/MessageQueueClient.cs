using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FyndSharp.Communication.Server;
using FyndSharp.Utilities.Common;
using FyndSharp.Communication.Common;
using System.IO;
using SharpMQ.Protocols;

namespace SharpMQ.Core
{
    internal class MessageQueueClient
    {
        private readonly IClientDummy _TcpClientDummy;
        private readonly CommandFactory _CommandFactory = new CommandFactory();

        public long Id { get; set; }
        public List<string> WatchingTubeList { get; private set; }

        public MessageQueueClient(IClientDummy theTcpClientDummy)
        {
            Checker.NotNull<IClientDummy>(theTcpClientDummy);
            _TcpClientDummy = theTcpClientDummy;
            _TcpClientDummy.MessageReceived += new EventHandler<MessageEventArgs>(TcpClientDummy_MessageReceived);

            WatchingTubeList = new List<string>();
        }

        private void TcpClientDummy_MessageReceived(object sender, MessageEventArgs e)
        {
            RawDataMessage theRawDataMsg = e.Message as RawDataMessage;
            if(null == theRawDataMsg)
            {
                return;
            }
            
        }
    }
}
