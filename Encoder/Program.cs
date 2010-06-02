using System;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using RhinoESBTest.Core;
using RhinoESBTest.Core.Messages;

namespace Encoder
{
    class WorkerProgram
    {
        static void Main()
        {

            log4net.Config.XmlConfigurator.Configure();
            var container = new WindsorContainer(new XmlInterpreter());// read config from app.config
            container.Kernel.AddFacility("worker.esb", new RhinoServiceBusFacility());// wire up facility for rhino service bus
            //container.Register(Component.For<IMessageConsumer>().Named("Worker").ImplementedBy(typeof(Worker)));
            container.Kernel.Register(Component.For<Worker>());
            var handlers = container.Kernel.GetAssignableHandlers(typeof(ConsumerOf<VideoToEncode>)); 
            var bus = container.Resolve<IStartableServiceBus>();
            bus.Start();
            Console.Clear();
            Console.WriteLine("Worker waiting");
            Console.ReadLine();
        }
    }
}