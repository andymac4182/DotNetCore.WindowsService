﻿using PeterKottas.DotNetCore.WindowsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeterKottas.DotNetCore.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceRunner<ExampleService>.Run(config =>
            {
                var name = config.GetDefaultName();
                config.Service(serviceConfig =>
                {
                    serviceConfig.ServiceFactory(() =>
                    {
                        return new ExampleService();
                    });
                    serviceConfig.OnStart(service =>
                    {
                        Console.WriteLine("Service {0} started", name);
                        service.Start();
                    });

                    serviceConfig.OnStop(service =>
                    {
                        Console.WriteLine("Service {0} stopped", name);
                        service.Stop();
                    });

                    serviceConfig.OnError(e =>
                    {
                        Console.WriteLine("Service {0} errored with exception : {1}", name, e.Message);
                    });
                });
            });
            Console.ReadKey();
        }
    }
}
