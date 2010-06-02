using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using RhinoESBTest.Core;

namespace Encoder
{
    class WorkerProgram
    {
        static void Main()
        {

            log4net.Config.XmlConfigurator.Configure();
            var container = new WindsorContainer(new XmlInterpreter());// read config from app.config
            container.Kernel.AddFacility("worker.esb", new RhinoServiceBusFacility());// wire up facility for rhino service bus
            container.Register(Component.For<IMessageConsumer>().Named("Worker").ImplementedBy(typeof(Worker)));

            var bus = container.Resolve<IStartableServiceBus>();
            bus.Start();
            Console.WriteLine("Worker waiting");
            Console.ReadLine();
        }
    }
}