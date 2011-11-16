using System;

namespace SharpMQ
{
    enum MessageStatus
    {
        Ready = 0,
        Reserved = 1,
        Deleted = 2,
        Delayed = 3,
        Buried = 4
    }
}
