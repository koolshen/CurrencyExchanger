using CurrencyExchanger.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchanger.Data.Models
{
    public class Account
    {
        public Account(CurrencyCode Code, int number, decimal amount = 0)
        {
            this.Number = number;
            this.Code = Code;
            this.Amount = amount;
        }

        public int Number { get; set; }
        public CurrencyCode Code { get; set; }
        public decimal Amount { get; set; }


    }
}
