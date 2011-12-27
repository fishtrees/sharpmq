using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SharpMQ.Protocols
{
    public class CommandFactory
    {
        private MemoryStream _InternalStream = new MemoryStream();

        public IEnumerable<Command> CreateCommands(byte[] theBytes)
        {
            _InternalStream.Write(theBytes, 0, theBytes.Length);
            List<Command> commands = new List<Command>();
            while (CreateSingleCommand(commands)) { }
            return commands;
        }

        private bool CreateSingleCommand(ICollection<Command> theCmdCollection)
        {
            _InternalStream.Position = 0;

            StringBuilder cmd = new StringBuilder();
            int current = -1;
            char currentChar;
            bool isCR = false;
            while (true)
            {
                current = _InternalStream.ReadByte();
                if (current < 0)
                {
                    break;
                }
                currentChar = (char)current;
                cmd.Append(currentChar);
                if (currentChar.Equals(ProtocolConstants.TERMINATOR_CHAR_1))
                {
                    isCR = true;
                    continue;
                }
                else if(currentChar.Equals(ProtocolConstants.TERMINATOR_CHAR_2) && isCR == true)
                {

                }
            }
            throw new NotImplementedException();
            
        }

    }
}
