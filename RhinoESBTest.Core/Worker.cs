using Castle.Core.Logging;
using Rhino.ServiceBus;

namespace RhinoESBTest.Core
{
    public class Worker : ConsumerOf<Messages.VideoToEncode>
    {

        private ILogger _logger = NullLogger.Instance;
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }
        public bool Working;

        public void Consume(Messages.VideoToEncode message)
        {
            Working = true;
            System.Threading.Thread.Sleep(1000);
            Working = false;
            Logger.Info("Completed {0}", message.FileName);
        }
    }
}