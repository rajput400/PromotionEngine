using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PromotionEngine.Models.BusinessLogic;

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
            logger.LogInformation("Application Started, Press Enter to continue");

            Console.ReadLine();

            Console.WriteLine("Enter the list of Items in the Cart in the form of A,B,C,D only example: A,B");

            var cartValue = Console.ReadLine().Replace(',',' ').Replace(" ","").ToCharArray().ToList();

            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cartValue);

            Console.WriteLine($"Total Order Value is: {orderValue}");

            Console.ReadLine();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Task Completed");

            return Task.CompletedTask;
        }
    }
}