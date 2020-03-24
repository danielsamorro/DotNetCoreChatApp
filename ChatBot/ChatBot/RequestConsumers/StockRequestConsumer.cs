using ChatApp.Requests;
using ChatApp.Response;
using ChatBot.Services.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace ChatBot.RequestConsumers
{

    public class StockRequestConsumer : IConsumer<StockRequest>
    {
        private readonly IStockService _stockService;

        public StockRequestConsumer(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task Consume(ConsumeContext<StockRequest> context)
        {
            var stockQuote = _stockService.GetStockQuote(context.Message.StockValue);

            await context.RespondAsync<StockResponse>(new
            {
                Message = $"{stockQuote.Symbol} quote is ${stockQuote.Close} per share"
            });
        }
    }
}
