using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Application.Interfaces;


namespace CurrencyConverter.Application.Factories
{
    public class CurrencyProviderFactory
    {
        private readonly ICurrencyProvider _provider;

        public CurrencyProviderFactory(ICurrencyProvider provider)
        {
            _provider = provider;
        }

        public ICurrencyProvider GetProvider()
        {
            return _provider;
        }
    }
}
