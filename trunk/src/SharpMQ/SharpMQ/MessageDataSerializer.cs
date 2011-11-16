using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpTest.Net.Serialization;
using System.Xml.Serialization;
using System.IO;

namespace SharpMQ
{
    class MessageDataSerializer : ISerializer<MessageData>
    {
        public MessageData ReadFrom(System.IO.Stream stream)
        {
            return new MessageData(
                PrimitiveSerializer.UInt64.ReadFrom(stream), 
                PrimitiveSerializer.Bytes.ReadFrom(stream));
        }

        public void WriteTo(MessageData value, System.IO.Stream stream)
        {
            PrimitiveSerializer.UInt64.WriteTo(value.Id, stream);
            PrimitiveSerializer.Bytes.WriteTo(value.Data, stream);
        }
    }
}
