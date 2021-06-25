using System;
using System.Threading.Tasks;
using GrpcEmployee;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // This is likely the direction we'll go in the future, but as of now WinHttpHandler is only available on newer windows 10 builds
            // see: https://docs.microsoft.com/en-us/aspnet/core/grpc/netstandard?view=aspnetcore-5.0
            //using var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions() { HttpHandler = new WinHttpHandler() });

            // workaround for calling a gRPC service using a .NET framework client using an insecure connection
            // see: https://stackoverflow.com/questions/60766836/how-do-you-create-a-grpc-client-in-net-framework
            var channel = new Grpc.Core.Channel("localhost", 5001, Grpc.Core.ChannelCredentials.Insecure);

            var client = new Employee.EmployeeClient(channel);
            var reply = await client.GetEmployeeInfoAsync(new EmployeeInfoRequest() { ID = 100 });

            Console.WriteLine(reply);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
