using Refactoring.FraudDetection.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Abstractions.Fraud
{
    public interface IFraudRule
    {
        public bool IsFraudulent(Order thisOne, Order other);
    }
}
