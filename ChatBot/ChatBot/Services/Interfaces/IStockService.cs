using ChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Services.Interfaces
{
    public interface IStockService
    {
        StockQuote GetStockQuote(string arg);
    }
}
