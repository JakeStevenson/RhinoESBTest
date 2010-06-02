using Castle.Core.Logging;
using Rhino.ServiceBus;
using RhinoESBTest.Core.Messages;

namespace RhinoESBTest.Core
{
    public class Worker : ConsumerOf<VideoToEncode>
    {

        private ILogger _logger = NullLogger.Instance;
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }
        public bool Working;

        public void Consume(VideoToEncode message)
        {
            Working = true;
            System.Threading.Thread.Sleep(1000);
            Working = false;
            Logger.Info("Completed {0}", message.FileName);
        }
    }
}