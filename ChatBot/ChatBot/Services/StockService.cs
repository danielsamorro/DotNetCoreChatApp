using ChatBot.Models;
using ChatBot.Services.Interfaces;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatBot.Services
{
    public class StockService : IStockService
    {
        

        public StockService()
        {

        }

        public StockQuote GetStockQuote(string arg)
        {
            using (var client = new WebClient())
            {
                var url = "https://stooq.com/q/l/?";
                var linkParams = $"s={arg}&f=sd2t2ohlcv&h&e=csv";

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + linkParams);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                StreamReader sr = new StreamReader(resp.GetResponseStream());
                

                TextReader reader = sr;
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                var records = csvReader.GetRecords<StockQuote>();
                return records.FirstOrDefault();
            }
        }
    }
}
