using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FyndSharp.Communication.Protocols;
using FyndSharp.Communication.Common;
using System.IO;

namespace SharpMQ.Protocols
{
    internal class HeadBodyProtocol : IProtocol
    {
        private MemoryStream _InternalStream = new MemoryStream();
        private char _LastChar;
        private StringBuilder _Head = new StringBuilder();

        public IEnumerable<IMessage> BuildMessages(byte[] theBytes)
        {
            _InternalStream.Write(theBytes, 0, theBytes.Length);
            List<IMessage> commands = new List<IMessage>();
            while (CreateSingleCommand(commands)) { }
            return commands;
        }

        public byte[] GetBytes(IMessage theMsg)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        private bool CreateSingleCommand(ICollection<IMessage> theCollection)
        {
            _InternalStream.Position = 0;

            int current = -1;
            while (true)
            {
                current = _InternalStream.ReadByte();
                if (current < 0)
                {
                    break;
                }
                _LastChar = (char)current;
                _Head.Append(_LastChar);
                if (_LastChar.Equals(ProtocolConstants.TERMINATOR_CHAR_1))
                {
                    current = _InternalStream.ReadByte();
                    if (current < 0)
                    {
                        break;
                    }
                    _LastChar = (char)current;
                    _Head.Append(_LastChar);
                    if (_LastChar.Equals(ProtocolConstants.TERMINATOR_CHAR_2))
                    {

                    }
                    continue;
                }
                
            }
            throw new NotImplementedException();

        }
    }
}
