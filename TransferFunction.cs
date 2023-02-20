using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OrderTransfer
{
    public class TransferFunction
    {
        private readonly IDownloadingService _downloadingService;
        private readonly IUpdatingService _updatingService;

        public TransferFunction(IUpdatingService updatingService, IDownloadingService downloadingService)
        {
            _downloadingService = downloadingService;
            _updatingService = updatingService;
        }
        [FunctionName("TransferFunction")]
        public async Task Run([TimerTrigger("0 */10 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var order = await _downloadingService.GetFaireOrder();
            await _updatingService.PostBaselinkerOrder(order);
        }
        
    }
}
