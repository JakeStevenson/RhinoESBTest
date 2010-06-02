using System;
using Castle.Core.Logging;
using Rhino.ServiceBus;
using RhinoESBTest.Core.Messages;

namespace RhinoESBTest.Core
{
    public class Worker : ConsumerOf<VideoToEncode>
    {
        public bool Working;

        public void Consume(VideoToEncode message)
        {
            Working = true;
            System.Threading.Thread.Sleep(1000);
            Working = false;
            Console.WriteLine("Completed {0}", message.FileName);
        }
    }
}