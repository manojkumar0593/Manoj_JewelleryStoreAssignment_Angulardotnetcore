using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using JewelleryStoreAPI;
using System;
using Xunit;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using JewelleryStore.DataAccess.Domain.Context;
using JewelleryStore.DataAccess.Domain.DataSeed;
using Microsoft.Extensions.Configuration;

namespace JewelleryStore.IntegrationTest
{
    public class TestClientProvider : IDisposable
    {
        private bool disposedValue;
        private TestServer server;
        public HttpClient Client { get; private set; }
        public TestClientProvider()
        {
           
            server = new TestServer(new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder()

                .AddJsonFile("appsettings.json")
                .Build()
            )
                .UseStartup<Startup>());
            using (var scope = server.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<JewelleryStoreDBContext>();

                DataSeed.Initialize(services);
            }
            Client = server.CreateClient();



        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    server?.Dispose();
                    Client?.Dispose();
                    // TODO: dispose managed state (managed objects)
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TestClientProvider()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
