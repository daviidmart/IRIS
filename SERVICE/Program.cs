using Topshelf;
using API.Workers;
using System.Threading;
using System;

namespace API
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x => 
            {
                x.Service<Webserver>(s => 
                {
                    s.ConstructUsing(name => new Webserver());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.SetDescription("IRIS sms service");
                x.SetDisplayName("IRIS API");
                x.SetServiceName("IRIS");
            });
        }
    }
}
