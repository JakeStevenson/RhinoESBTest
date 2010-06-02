using System;
using System.Configuration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.Msmq;

namespace RhinoESBTest.Core
{
    public class Requester
    {
        private readonly WindsorContainer _container;
        private readonly Uri _endpoint;
        public Requester()
        {
            _container = new WindsorContainer(new XmlInterpreter());
            _container.Kernel.AddFacility("requester.esb", new RhinoServiceBusFacility());
            _endpoint = new Uri(ConfigurationManager.AppSettings["encoderQueue"]);
            PrepareQueues.Prepare(ConfigurationManager.AppSettings["encoderQueue"], QueueType.Standard);
        }
        public void Request(Messages.VideoToEncode encode)
        {
            Console.WriteLine("Requesting encoding of {0}", encode.FileName);
            
            var oneWay = new OnewayBus(new[]
                                           {
                                               new MessageOwner
                                                   {
                                                       Endpoint = _endpoint,
                                                       Name = "RhinoESBTest.Core.Messages.VideoToEncode",
                                                   },
                                           }, new MessageBuilder(_container.Resolve<IMessageSerializer>(), null));
            oneWay.Send(encode);
        }

    }
}