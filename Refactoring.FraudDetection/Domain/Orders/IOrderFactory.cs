using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Domain.Orders
{
    public interface IOrderFactory
    {
        public Order Create(string[] orderFields);
    }
}
