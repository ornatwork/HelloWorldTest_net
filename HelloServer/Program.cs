using System;
using System.Threading.Tasks;
using System.Threading;
using Grpc.Core;
using HelloShared;

namespace HelloServer
{
    class GreeterImpl : PingServer.PingServerBase
    {
      private Random _random = new Random();

      /// <summary>
      /// Server side handler of the HelloRandom RPC
      /// </summary>
      public override Task<HelloReply> HelloRandom(HelloRequest req, ServerCallContext context)
      {
        // Set the return, mark it with deley number 
        HelloReply reply = new HelloReply();
        int rndom = _random.Next(1, 6);
        reply.Message = req.Message + ", random delay=" + rndom;
        // Random delay
        Thread.Sleep(rndom * 1000);
        // Done
        return Task.FromResult(reply);
      }

      /// <summary>
      /// Server side handler of the Hello RPC
      /// </summary>
      public override Task<HelloReply> Hello(HelloRequest req, ServerCallContext context)
      {
        HelloReply reply = new HelloReply();
        reply.Message = Reverse(req.Message);
        return Task.FromResult(reply);
      }

      /// <summary>
      /// Reverses the string passed
      /// </summary>
      private static string Reverse(string s)
      {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
      }

    }

    class Program
    {
        const int Port = 50051;

        public static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { PingServer.BindService(new GreeterImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
