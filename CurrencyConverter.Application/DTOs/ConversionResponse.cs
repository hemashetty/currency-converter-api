using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Application.DTOs
{
    public class ConversionResponse
    {
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
