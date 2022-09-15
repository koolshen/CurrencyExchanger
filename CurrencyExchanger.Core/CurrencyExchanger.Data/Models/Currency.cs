using CurrencyExchanger.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchanger.Data.Models
{
    public class Currency
    {
        public Currency(CurrencyCode Code, decimal amount = 0)
        {
            this.Code = Code;
            Amount = amount;
        }

        public CurrencyCode Code { get; set; }
        public decimal Amount { get; set; }

    }
}
