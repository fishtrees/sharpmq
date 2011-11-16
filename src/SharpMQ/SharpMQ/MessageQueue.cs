using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpTest.Net.Serialization;
using CSharpTest.Net.Collections;
using System.IO;
using CSharpTest.Net.Synchronization;

namespace SharpMQ
{
    public class MessageQueue : IDisposable
    {
        private BPlusTree<MessageKey, MessageData> _Tree;

        public MessageQueue(string theFilePath)
        {
            BPlusTree<MessageKey, MessageData>.Options options
                = new BPlusTree<MessageKey, MessageData>.Options(
                    new MessageKeySerializer(),
                    new MessageDataSerializer(),
                    new MessageKeyComparer())
            {
                CreateFile = CreatePolicy.IfNeeded,
                FileName = theFilePath,
                FileBlockSize = 8192,
                FileOpenOptions = FileOptions.RandomAccess,
                StorageType = StorageType.Disk,
                CacheKeepAliveTimeout = 10000,
                CacheKeepAliveMinimumHistory = 0,
                CacheKeepAliveMaximumHistory = 3000
            };

            options.CallLevelLock = new ReaderWriterLocking();
            options.LockingFactory = new LockFactory<SimpleReadWriteLocking>();
            options.LockTimeout = 10000;
            options.ConcurrentWriters = 8;

            this._Tree = new BPlusTree<MessageKey, MessageData>(options);
        }

        public bool Enqueue(byte[] data, uint priority, uint timeout)
        {
            MessageKey msgKey = new MessageKey((ulong)MessageIdManager.Default.GetNextId(), priority, timeout, MessageStatus.Ready);
            MessageData msgData = new MessageData(msgKey.Id, data);
            return this._Tree.Add(msgKey, msgData);
        }

        public bool Delete(ulong id)
        {
            return false;
        }

        public byte[] Dequeue()
        {
            MessageKey theKey = this._Tree.Keys.First<MessageKey>(new Func<MessageKey, bool>(FindNextMessage));
            MessageData theData = null;
            if (this._Tree.TryGetValue(theKey, out theData))
            {
                theKey.Status = MessageStatus.Reserved;
                return theData.Data;
            }
            return new byte[] { };    
        }

        private bool FindNextMessage(MessageKey theKey)
        {
            if (theKey.Status == MessageStatus.Ready)
            {
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
