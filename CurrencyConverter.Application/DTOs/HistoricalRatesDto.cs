using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Application.DTOs
{
    public class HistoricalRatesDto
    {
        public Dictionary<string, object> Rates { get; set; }
    }
}
