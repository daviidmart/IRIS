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
            GSM gsm = new GSM();
            new Timer(e => gsm.ServerStatus(), null, TimeSpan.Zero, TimeSpan.FromSeconds(30.0));

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
                x.SetDescription("GO4IT sms service");
                x.SetDisplayName("IRIS API");
                x.SetServiceName("IRIS API");
            });
        }
    }
}
