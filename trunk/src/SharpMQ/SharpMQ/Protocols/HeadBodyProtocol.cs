using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FyndSharp.Communication.Protocols;
using FyndSharp.Communication.Common;
using System.IO;
using FyndSharp.Utilities.IO;

namespace SharpMQ.Protocols
{
    internal class HeadBodyProtocol : IProtocol
    {
        private MemoryStream _InternalStream = new MemoryStream();
        private char _LastChar;
        private StringBuilder _CmdBuilder = new StringBuilder();
        private CommandHead _CurrentHead;

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
                    return false;
                }
                if (null != _CurrentHead && _CurrentHead.HasBody)
                {
                    // TODO: The head already readed, then read the body bytes
                    byte[] body = ReadBody(_CurrentHead.BodyLength);
                    if (null == body)
                    {
                        throw new Exception("BAD_COMMAND_PARAM");
                    }
                }
                _LastChar = (char)current;
                _CmdBuilder.Append(_LastChar);
                if (_LastChar.Equals(ProtocolConstants.TERMINATOR_CHAR_1))
                {
                    current = _InternalStream.ReadByte();
                    if (current < 0)
                    {
                        return false;
                    }
                    _LastChar = (char)current;
                    _CmdBuilder.Append(_LastChar);
                    if (_LastChar.Equals(ProtocolConstants.TERMINATOR_CHAR_2))
                    {
                        _CurrentHead = CreateCommandHead(_CmdBuilder.ToString().Trim());
                        if (!_CurrentHead.HasBody)
                        {
                            switch (_CurrentHead.CommandName)
                            {
                                case "use":
                                    theCollection.Add(new UseCommand()
                                    {
                                        TubeName = _CurrentHead.Parameters[0]
                                    });
                                    break;
                                case "reserve":
                                    theCollection.Add(new ReserveCommand());
                                    break;
                                case "reserve-with-timeout":
                                    theCollection.Add(new ReserveWithTimeoutCommand()
                                    {
                                        Timeout = Convert.ToInt32(_CurrentHead.Parameters[0])
                                    });
                                    break;
                                case "delete":
                                    theCollection.Add(new DeleteCommand()
                                    {
                                        JobId = Convert.ToInt64(_CurrentHead.Parameters[0])
                                    });
                                    break;
                                default:
                                    break;
                            }
                            byte[] restBytes = StreamHelper.ReadBytes(_InternalStream, (int)(_InternalStream.Length - _InternalStream.Position - 1));
                            _InternalStream = new MemoryStream(restBytes);


                        }
                    }
                    continue;
                }

            }
            throw new NotImplementedException();

        }
        private CommandHead CreateCommandHead(string theHead)
        {
            string[] headParts = theHead.Split(new char[] { ProtocolConstants.SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            if (headParts.Length <= 0)
            {
                throw new Exception("UNKNOWN_COMMAND");
            }
            CommandHead headObj = new CommandHead();
            headObj.CommandName = headParts[0];
            headObj.Parameters.AddRange(headParts.Skip(1));
            if (headObj.CommandName == "put")
            {
                headObj.HasBody = true;
                if (headParts.Length < 5)
                {
                    throw new Exception("BAD_COMMAND_PARAM");
                }
                int length = 0;
                if (!Int32.TryParse(headParts[4], out length))
                {
                    throw new Exception("BAD_COMMAND_PARAM");
                }
                headObj.BodyLength = length;
            }
            return headObj;
        }

        private byte[] ReadBody(int length)
        {
            if (null == _CurrentHead)
            {
                //head not found, no need to read body.
                return null;
            }
            if (_InternalStream.Length - _InternalStream.Position - 1 <= 0)
            {
                return null;
            }
            byte[] buffer = new byte[length];

            _InternalStream.Read(buffer, 0, length);
            return buffer;
        }

        private class CommandHead
        {
            public string CommandName { get; set; }
            public int BodyLength { get; set; }
            public bool HasBody { get; set; }
            public List<string> Parameters { get; private set; }

            public CommandHead()
            {
                Parameters = new List<string>();
                BodyLength = -1;
            }
        }
    }


}
