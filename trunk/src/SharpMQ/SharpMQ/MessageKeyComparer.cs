using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMQ
{
    class MessageKeyComparer : IComparer<MessageKey>
    {
        public int Compare(MessageKey x, MessageKey y)
        {
            int theIdResult = x.Id.CompareTo(y.Id);
            if (theIdResult.Equals(0))
            {
                return 0;
            }
            int thePriorityResult = x.Priority.CompareTo(y.Priority);
            if (thePriorityResult.Equals(0))
            {
                return theIdResult;
            }
            return thePriorityResult;
        }
    }
}
