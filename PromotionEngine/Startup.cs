using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PromotionEngine.Models;

namespace PromotionEngine
{
    public class Startup : IHostedService
    {
        private readonly ILogger<Startup> logger;
        private readonly IPromotionEngineCheckoutProcess promotionEngineProcess;

        public Startup(ILogger<Startup> logger, IPromotionEngineCheckoutProcess promotionEngineProcess)
        {
            this.promotionEngineProcess = promotionEngineProcess;
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Application Started");

            Console.WriteLine("Hello World!");

            promotionEngineProcess.CalculateTotalOrderValue(null); //TODO: Check how to pass the correct value.

            Console.ReadLine();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //TODO: Handle this later on
            throw new System.NotImplementedException();
        }
    }
}