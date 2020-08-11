using Refactoring.FraudDetection.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Abstractions.OrderSource
{
    public interface IOrderSource
    {
         IList<Order> Load();
    }
}
