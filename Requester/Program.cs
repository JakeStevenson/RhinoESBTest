using System;
using System.Configuration;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using RhinoESBTest.Core;
using RhinoESBTest.Core.Messages;

namespace Requester
{
    class RequesterProgram
    {
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            var requester = new RhinoESBTest.Core.Requester();

            Console.Clear();
            Console.WriteLine("Requester waiting");
            Console.WriteLine("Enter the number of requests to generate:");
            var input = Console.ReadLine();
            while (!string.IsNullOrEmpty(input))
            {
                var count = int.Parse(input);
                for (int x = 1; x <= count; x++)
                {
                    requester.Request(GetRequest(x.ToString()));
                }
                Console.WriteLine("Enter the number of requests to generate:");
                input = Console.ReadLine();
            }
        }

        private static VideoToEncode GetRequest(string id)
        {
            return new VideoToEncode()
                       {
                           FileName=id,
                           ID = id,
                           RequestDate=DateTime.Now
                       };
        }
    }
}