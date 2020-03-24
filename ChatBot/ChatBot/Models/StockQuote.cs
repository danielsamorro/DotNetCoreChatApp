using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    public class StockQuote
    {
        public string Symbol { get; set; }
        public string Date { get; set; }     
        public string Time { get; set; }    
        public decimal Open { get; set; }        
        public decimal High { get; set; }    
        public decimal Low { get; set; }     
        public decimal Close { get; set; }    
        public string Volume { get; set; }
        

    }
}
