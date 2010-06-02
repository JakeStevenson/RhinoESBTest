using System;
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
        public Requester()
        {
            _container = new WindsorContainer(new XmlInterpreter());
            _container.Kernel.AddFacility("requester.esb", new RhinoServiceBusFacility());
        }
        public void Request(Messages.VideoToEncode encode)
        {
            Console.WriteLine("Requesting encoding of {0}", encode.FileName);
            using (var bus = _container.Resolve<IStartableServiceBus>())
            {
                bus.Start();
                var oneWay = new OnewayBus(new[]
                                               {
                                                   new MessageOwner
                                                       {
                                                           Endpoint = bus.Endpoint.Uri,
                                                           Name = "RhinoESBTest.Core.Messages.VideoToEncode",
                                                       },
                                               }, new MessageBuilder(_container.Resolve<IMessageSerializer>(), null));
                oneWay.Send(encode);
            }
        }

    }
}